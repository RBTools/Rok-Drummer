using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Midi;

namespace RokDrummer
{
    class MIDIStuff
    {
        private int TicksPerQuarter;
        private List<TempoEvent> TempoEvents;
        private MidiFile MIDIFile;
        public MIDIChart MIDI_Chart;
        private long LengthLong;
        public List<PracticeSection> PracticeSessions;
        private const int DRUM_SOLO = 103;
        private const int DRUM_FILL = 120;
        private const int DRUM_OD = 116;

        public void Initialize()
        {
            MIDI_Chart = new MIDIChart();
            MIDI_Chart.Initialize();
            PracticeSessions = new List<PracticeSection>();
        }

        public bool ReadMIDIFile(string midi)
        {
            if (!File.Exists(midi)) return false;
            var Tools = new NemoTools();
            LengthLong = 0;
            MIDIFile = null;
            MIDIFile = Tools.NemoLoadMIDI(midi);
            if (MIDIFile == null) return false;
            try
            {
                TicksPerQuarter = MIDIFile.DeltaTicksPerQuarterNote;
                BuildTempoList();
                for (var i = 0; i < MIDIFile.Events.Tracks; i++)
                {
                    var trackname = MIDIFile.Events[i][0].ToString();
                    if (trackname.Contains("DRUMS") && !trackname.Contains("BAND"))
                    {
                        GetDiscoFlips(MIDIFile.Events[i]);
                        List<MIDINote> toadd;

                        //expert
                        GetToms(MIDIFile.Events[i]);
                        MIDI_Chart.DrumsX.Overdrive = GetSpecialMarker(MIDIFile.Events[i], DRUM_OD);
                        CheckMIDITrack(MIDIFile.Events[i], MIDI_Chart.DrumsX.ValidNotes, out toadd, true);
                        MIDI_Chart.DrumsX.ChartedNotes.AddRange(toadd);
                        MIDI_Chart.DrumsX.Solos = GetSpecialMarker(MIDIFile.Events[i], DRUM_SOLO);
                        MIDI_Chart.DrumsX.Fills = GetSpecialMarker(MIDIFile.Events[i], DRUM_FILL);

                        //hard
                        CheckMIDITrack(MIDIFile.Events[i], MIDI_Chart.DrumsH.ValidNotes, out toadd, true);
                        MIDI_Chart.DrumsH.ChartedNotes.AddRange(toadd);
                        MIDI_Chart.DrumsH.Toms = MIDI_Chart.DrumsX.Toms;
                        MIDI_Chart.DrumsH.Overdrive = MIDI_Chart.DrumsX.Overdrive;
                        MIDI_Chart.DrumsH.Solos = MIDI_Chart.DrumsX.Solos;
                        MIDI_Chart.DrumsH.Fills = MIDI_Chart.DrumsX.Fills;

                        //medium
                        CheckMIDITrack(MIDIFile.Events[i], MIDI_Chart.DrumsM.ValidNotes, out toadd, true);
                        MIDI_Chart.DrumsM.ChartedNotes.AddRange(toadd);
                        MIDI_Chart.DrumsM.Toms = MIDI_Chart.DrumsX.Toms;
                        MIDI_Chart.DrumsM.Overdrive = MIDI_Chart.DrumsX.Overdrive;
                        MIDI_Chart.DrumsM.Solos = MIDI_Chart.DrumsX.Solos;
                        MIDI_Chart.DrumsM.Fills = MIDI_Chart.DrumsX.Fills;

                        //easy
                        CheckMIDITrack(MIDIFile.Events[i], MIDI_Chart.DrumsE.ValidNotes, out toadd, true);
                        MIDI_Chart.DrumsE.ChartedNotes.AddRange(toadd);
                        MIDI_Chart.DrumsE.Toms = MIDI_Chart.DrumsX.Toms;
                        MIDI_Chart.DrumsE.Overdrive = MIDI_Chart.DrumsX.Overdrive;
                        MIDI_Chart.DrumsE.Solos = MIDI_Chart.DrumsX.Solos;
                        MIDI_Chart.DrumsE.Fills = MIDI_Chart.DrumsX.Fills;
                    }
                    else if (trackname.Contains("EVENTS"))
                    {
                        foreach (var note in MIDIFile.Events[i])
                        {
                            switch (note.CommandCode)
                            {
                                case MidiCommandCode.MetaEvent:
                                    var section_event = (MetaEvent) note;
                                    if (section_event.MetaEventType != MetaEventType.Lyric &&
                                        section_event.MetaEventType != MetaEventType.TextEvent)
                                    {
                                        continue;
                                    }
                                    if (section_event.ToString().Contains("[section "))
                                    {
                                        var index = section_event.ToString().IndexOf("[", StringComparison.Ordinal);
                                        var new_section = section_event.ToString().Substring(index, section_event.ToString().Length - index);
                                        new_section = new_section.Replace("section ", "prc_");
                                        new_section = new_section.Replace("guitar", "gtr");
                                        new_section = new_section.Replace("practice_outro", "outro");
                                        new_section = new_section.Replace("big_rock_ending", "bre");
                                        new_section = new_section.Replace(" ", "_").Replace("-", "").Replace("!", "").Replace("?", "");
                                        GetPracticeSession(new_section, section_event.AbsoluteTime);
                                    }
                                    else if (section_event.ToString().Contains("[prc_"))
                                    {
                                        GetPracticeSession(section_event.ToString(), section_event.AbsoluteTime);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            MIDI_Chart.AverageBPM = AverageBPM();
            PracticeSessions.Sort((a, b) => a.SectionStart.CompareTo(b.SectionStart));
            MIDI_Chart.DrumsX.Sort();
            return true;
        }

        private void GetDiscoFlips(IEnumerable<MidiEvent> track)
        {
            foreach (var midiEvent in track.Where(midiEvent => midiEvent.CommandCode == MidiCommandCode.MetaEvent).Where(midiEvent => midiEvent.ToString().Contains("mix 3 drums")))
            {
                if (midiEvent.ToString().Contains("d]"))
                {
                    MIDI_Chart.DiscoFlips.Add(new SpecialMarker {MarkerBegin = GetRealtime(midiEvent.AbsoluteTime), MarkerEnd = -1.0});
                }
                else
                {
                    if (MIDI_Chart.DiscoFlips.Any() && MIDI_Chart.DiscoFlips[MIDI_Chart.DiscoFlips.Count - 1].MarkerEnd == -1.0)
                    {
                        MIDI_Chart.DiscoFlips[MIDI_Chart.DiscoFlips.Count - 1].MarkerEnd = GetRealtime(midiEvent.AbsoluteTime);
                    }
                }
            }
        }

        private void GetToms(IList<MidiEvent> track)
        {
            var ValidToms = new List<int> { 110, 111, 112 };
            for (var z = 0; z < track.Count(); z++)
            {
                try
                {
                    var notes = track[z];
                    if (notes.AbsoluteTime > LengthLong)
                    {
                        LengthLong = notes.AbsoluteTime;
                    }
                    if (notes.CommandCode != MidiCommandCode.NoteOn) continue;
                    var note = (NoteOnEvent)notes;
                    if (note.Velocity <= 0 || !ValidToms.Contains(note.NoteNumber)) continue;
                    var time = GetRealtime(note.AbsoluteTime);
                    var length = GetRealtime(note.NoteLength);
                    var end = Math.Round(time + length, 5);
                    var n = new MIDINote
                    {
                        NoteStart = time,
                        NoteLength = length,
                        NoteEnd = end,
                        NoteNumber = note.NoteNumber,
                        isTom = true
                    };
                    MIDI_Chart.DrumsX.Toms.Add(n);
                }
                catch (Exception)
                { }
            }
        }
        
        private List<SpecialMarker> GetSpecialMarker(IEnumerable<MidiEvent> track, int marker_note)
        {
            return (from notes in track where notes.CommandCode == MidiCommandCode.NoteOn select (NoteOnEvent) notes into note where note.Velocity > 0 && note.NoteNumber == marker_note let time = GetRealtime(note.AbsoluteTime) let end = GetRealtime(note.AbsoluteTime + note.NoteLength) select new SpecialMarker {MarkerBegin = time, MarkerEnd = end}).ToList();
        }
        
        private void GetPracticeSession(string session, long start_time)
        {
            var index = session.IndexOf("[", StringComparison.Ordinal);
            session = session.Substring(index, session.Length - index).Replace("[","").Replace("]","").Replace("{","").Replace("}","").Trim();
            if (File.Exists(Application.StartupPath + "\\bin\\sections"))
            {
                var sr = new StreamReader(Application.StartupPath + "\\bin\\sections");
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    line = line.Replace("(", "").Replace(")", "");
                    var i = line.IndexOf("\"", StringComparison.Ordinal);
                    var prc = line.Substring(0, i).Trim();
                    if (prc != session) continue;
                    session = line.Substring(i, line.Length - i).Replace("\"", "").Trim();
                    session = session.Replace("Gtr", "Guitar");
                    var myTI = new CultureInfo("en-US", false).TextInfo;
                    session = myTI.ToTitleCase(session);
                    break;
                }
                sr.Dispose();
            }
            var practice = new PracticeSection
            {
                SectionStart = GetRealtime(start_time),
                SectionName = "[" + session.Replace("prc", "").Replace("_", " ").Trim() + "]"
            };
            PracticeSessions.Add(practice);
        }
        
        private void CheckMIDITrack(IList<MidiEvent> track, ICollection<int> valid_notes, out List<MIDINote> output, bool isDrums = false)
        {
            output = new List<MIDINote>();
            for (var z = 0; z < track.Count(); z++)
            {
                try
                {
                    var notes = track[z];
                    if (notes.AbsoluteTime > LengthLong)
                    {
                        LengthLong = notes.AbsoluteTime;
                    }
                    if (notes.CommandCode != MidiCommandCode.NoteOn) continue;
                    var note = (NoteOnEvent)notes;
                    if (note.Velocity <= 0) continue;
                    if (!valid_notes.Contains(note.NoteNumber)) continue;
                    var time = GetRealtime(note.AbsoluteTime);
                    var length = GetRealtime(note.NoteLength);
                    var end = Math.Round(time + length, 5);
                    if (isDrums && MIDI_Chart.DiscoFlips.Any() && (note.NoteNumber == 97 || note.NoteNumber == 98))
                    {
                        if (MIDI_Chart.DiscoFlips.Where(flip => flip.MarkerBegin <= time).Any(flip => flip.MarkerEnd > time || flip.MarkerEnd == -1.0))
                        {
                            note.NoteNumber = note.NoteNumber == 97 ? 98 : 97;
                        }
                    }
                    var isTom = MIDI_Chart.DrumsX.Toms.Where(tom => tom.NoteStart <= time).Where(tom => tom.NoteEnd >= time).Any(tom => (tom.NoteNumber - note.NoteNumber)%12==0);
                    var hasOD = MIDI_Chart.DrumsX.Overdrive.Where(OD => OD.MarkerBegin <= time).Any(OD => OD.MarkerEnd >= end);
                    var n = new MIDINote
                    {
                        NoteStart = time,
                        NoteLength = length,
                        NoteEnd = end,
                        NoteNumber = note.NoteNumber,
                        isTom = isTom,
                        hasOD = hasOD
                    };
                    output.Add(n);
                }
                catch (Exception)
                {}
            }
        }
        
        private double GetRealtime(long absdelta)
        {
            //code by raynebc
            var BPM = 120.0;   //As per the MIDI specification, until a tempo change is reached, 120BPM is assumed
            var reldelta = absdelta;   //The number of delta ticks between the delta time being converted and the tempo change immediately at or before it
            var time = 0.0;   //The real time position of the tempo change immediately at or before the delta time being converted
            foreach (var tempo in TempoEvents.Where(tempo => tempo.AbsoluteTime <= absdelta))
            {
                BPM = tempo.BPM;
                time = tempo.RealTime;
                reldelta = absdelta - tempo.AbsoluteTime;
            }
            time += (double)reldelta / TicksPerQuarter * (60000.0 / BPM);
            return Math.Round(time / 1000, 5);
        }
        
        private void BuildTempoList()
        {
            //code provided by raynebc
            //Build tempo list
            var currentbpm = 120.00;
            var realtime = 0.0;
            var reldelta = 0;   //The number of delta ticks since the last tempo change
            TempoEvents = new List<TempoEvent>();
            foreach (var ev in MIDIFile.Events[0])
            {
                reldelta += ev.DeltaTime;
                if (ev.CommandCode != MidiCommandCode.MetaEvent) continue;
                var tempo = (MetaEvent)ev;
                if (tempo.MetaEventType != MetaEventType.SetTempo) continue;
                var relativetime = (double)reldelta / TicksPerQuarter * (60000.0 / currentbpm);
                var index1 = tempo.ToString().IndexOf("SetTempo", StringComparison.Ordinal) + 9;
                var index2 = tempo.ToString().IndexOf("bpm", StringComparison.Ordinal);
                var bpm = tempo.ToString().Substring(index1, index2 - index1);
                currentbpm = Convert.ToDouble(bpm);   //As per the MIDI specification, until a tempo change is reached, 120BPM is assumed
                realtime += relativetime;   //Add that to the ongoing current real time of the MIDI
                reldelta = 0;
                var tempo_event = new TempoEvent
                {
                    AbsoluteTime = tempo.AbsoluteTime,
                    RealTime = realtime,
                    BPM = currentbpm
                };
                TempoEvents.Add(tempo_event);
            }
        }
              
        private double AverageBPM()
        {
            var total_bpm = 0.0;
            var last = 0.0;
            var bpm = 120.0;
            double difference;
            var LengthSeconds = GetRealtime(LengthLong);
            if (LengthSeconds <= 0.0)
            {
                var count = TempoEvents.Sum(tempo => tempo.BPM);
                return Math.Round(count / TempoEvents.Count, 2);
            }
            foreach (var tempo in TempoEvents)
            {
                var current = GetRealtime(tempo.AbsoluteTime);
                difference = current - last;
                last = GetRealtime(tempo.AbsoluteTime);
                if (difference <= 0.0)
                {
                    bpm = tempo.BPM;
                    continue;
                }
                total_bpm += bpm * (difference / LengthSeconds);
                bpm = tempo.BPM;
            }
            difference = LengthSeconds - last;
            total_bpm += bpm * (difference / LengthSeconds);
            if (total_bpm == 0)
            {
                total_bpm = bpm;
            }
            return Math.Round(total_bpm, 2);
        }
    }

    public class MIDITrack
    {
        public string Name { get; set; }
        public List<int> ValidNotes { get; set; }
        public List<MIDINote> ChartedNotes { get; set; }
        public List<MIDINote> Toms { get; set; }
        public List<SpecialMarker> Solos { get; set; } 
        public List<SpecialMarker> Overdrive { get; set; } 
        public List<SpecialMarker> Fills { get; set; } 
        public void Sort()
        {
            ChartedNotes.Sort((a,b) => a.NoteStart.CompareTo(b.NoteStart));
            Solos.Sort((a, b) => a.MarkerBegin.CompareTo(b.MarkerBegin));
            Overdrive.Sort((a, b) => a.MarkerBegin.CompareTo(b.MarkerBegin));
            Fills.Sort((a, b) => a.MarkerBegin.CompareTo(b.MarkerBegin));
            Toms.Sort((a, b) => a.NoteStart.CompareTo(b.NoteStart));
        }
        public void Initialize()
        {
            ChartedNotes = new List<MIDINote>();
            Toms = new List<MIDINote>();
            Solos = new List<SpecialMarker>();
            Overdrive = new List<SpecialMarker>();
            Fills = new List<SpecialMarker>();
        }
    }

    public class MIDIChart
    {
        public MIDITrack DrumsX { get; set; }
        public MIDITrack DrumsH { get; set; }
        public MIDITrack DrumsM { get; set; }
        public MIDITrack DrumsE { get; set; }
        public double AverageBPM { get; set; }
        public List<SpecialMarker> DiscoFlips; 
        public void Initialize()
        {
            DrumsX = new MIDITrack { Name = "Expert", ValidNotes = new List<int> { 100, 99, 98, 97, 96 } };
            DrumsH = new MIDITrack { Name = "Hard", ValidNotes = new List<int> { 88, 87, 86, 85, 84 } };
            DrumsM = new MIDITrack { Name = "Medium", ValidNotes = new List<int> { 76, 75, 74, 73, 72 } };
            DrumsE = new MIDITrack { Name = "Easy", ValidNotes = new List<int> { 64, 63, 62, 61, 60 } };
            DrumsX.Initialize();
            DrumsH.Initialize();
            DrumsM.Initialize();
            DrumsE.Initialize();
            DiscoFlips = new List<SpecialMarker>();
            AverageBPM = 0.0;
        }
    }
    
    public class MIDINote
    {
        public int NoteNumber { get; set; }
        public double NoteStart { get; set; }
        public double NoteEnd { get; set; }
        public double NoteLength { get; set; }
        public Color NoteColor { get; set; }
        public bool isTom { get; set; }
        public bool hasOD { get; set; }
        public bool Played { get; set; }
        public bool Stopped { get; set; }
    }

    public class TempoEvent
    {
        public long AbsoluteTime { get; set; }
        public double RealTime { get; set; }
        public double BPM { get; set; }
    }
    
    public class PracticeSection
    {
        public double SectionStart { get; set; }
        public string SectionName { get; set; }
    }

    public class SpecialMarker
    {
        public double MarkerBegin { get; set; }
        public double MarkerEnd { get; set; }
    }
}