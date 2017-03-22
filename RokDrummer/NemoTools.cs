using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NAudio.Midi;
using Un4seen.Bass;

namespace RokDrummer
{
    public class NemoTools
    {
        public float[,] GetChannelMatrix(SongData song, int chans, string stems)
        {
            //initialize matrix
            //matrix must be float[output_channels, intpu_channels]
            var matrix = new float[2, chans];
            var ArrangedChannels = ArrangeStreamChannels(chans, true);
            if (song.ChannelsDrums > 0 && (stems.Contains("drums") || stems.Contains("allstems")))
            {
                //for drums it's a bit tricky because of the possible combinations
                switch (song.ChannelsDrums)
                {
                    case 2:
                        //stereo kit
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 0);
                        break;
                    case 3:
                        //mono kick
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 1, 0);
                        //stereo kit
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 1);
                        break;
                    case 4:
                        //mono kick
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 1, 0);
                        //mono snare
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 1, 1);
                        //stereo kit
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 2);
                        break;
                    case 5:
                        //mono kick
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 1, 0);
                        //stereo snare
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 1);
                        //stereo kit
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 3);
                        break;
                    case 6:
                        //stereo kick
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 0);
                        //stereo snare
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 2);
                        //stereo kit
                        matrix = DoMatrixPanning(song, matrix, ArrangedChannels, 2, 4);
                        break;
                }
            }
            var channel = song.ChannelsDrums;
            if (song.ChannelsBass > 0 && (stems.Contains("bass") || stems.Contains("allstems")))
            {
                matrix = DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsBass, channel);
            }
            channel = channel + song.ChannelsBass;
            if (song.ChannelsGuitar > 0 && (stems.Contains("guitar") || stems.Contains("allstems")))
            {
                matrix = DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsGuitar, channel);
            }
            channel = channel + song.ChannelsGuitar;
            if (song.ChannelsVocals > 0 && (stems.Contains("vocals") || stems.Contains("allstems")))
            {
                matrix = DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsVocals, channel);
            }
            channel = channel + song.ChannelsVocals;
            if (song.ChannelsKeys > 0 && (stems.Contains("keys") || stems.Contains("allstems")))
            {
                matrix = DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsKeys, channel);
            }
            channel = channel + song.ChannelsKeys;
            if (song.ChannelsBacking() > 0 && (stems.Contains("backing") || stems.Contains("allstems")))
            {
                matrix = DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsBacking(), channel);
            }
            channel = channel + song.ChannelsBacking();
            if (song.ChannelsCrowd == 0 || (!stems.Contains("crowd") && !stems.Contains("allstems"))) return matrix;
            return DoMatrixPanning(song, matrix, ArrangedChannels, song.ChannelsCrowd, channel);
        }

        public int[] ArrangeStreamChannels(int totalChannels, bool isOgg)
        {
            var channels = new int[totalChannels];
            if (isOgg)
            {
                switch (totalChannels)
                {
                    case 3:
                        channels[0] = 0;
                        channels[1] = 2;
                        channels[2] = 1;
                        break;
                    case 5:
                        channels[0] = 0;
                        channels[1] = 2;
                        channels[2] = 1;
                        channels[3] = 3;
                        channels[4] = 4;
                        break;
                    case 6:
                        channels[0] = 0;
                        channels[1] = 2;
                        channels[2] = 1;
                        channels[3] = 4;
                        channels[4] = 5;
                        channels[5] = 3;
                        break;
                    case 7:
                        channels[0] = 0;
                        channels[1] = 2;
                        channels[2] = 1;
                        channels[3] = 4;
                        channels[4] = 5;
                        channels[5] = 6;
                        channels[6] = 3;
                        break;
                    case 8:
                        channels[0] = 0;
                        channels[1] = 2;
                        channels[2] = 1;
                        channels[3] = 4;
                        channels[4] = 5;
                        channels[5] = 6;
                        channels[6] = 7;
                        channels[7] = 3;
                        break;
                    default:
                        goto DoAllChannels;
                }
                return channels;
            }
        DoAllChannels:
            for (var i = 0; i < totalChannels; i++)
            {
                channels[i] = i;
            }
            return channels;
        }

        private static float[,] DoMatrixPanning(SongData song, float[,] in_matrix, IList<int> ArrangedChannels, int inst_channels, int curr_channel)
        {
            //by default matrix values will be 0 = 0 volume
            //if nothing is assigned here, it stays at 0 so that channel won't be played
            //otherwise we assign a volume level based on the dta volumes

            //initialize output matrix based on input matrix, just in case something fails there's something going out
            var matrix = in_matrix;

            //split attenuation and panning info from DTA file for index access
            var volumes = song.AttenuationValues.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            var pans = song.PanningValues.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            //BASS.NET lets us specify maximum volume when converting dB to Level
            //in case we want to change this letter, it's only one value to change
            const double max_dB = 1.0;

            //technically we could do each channel, but Magma only allows us to specify volume per track, 
            //so both channels should have same volume, let's same a tiny bit of processing power
            float vol;
            try
            {
                vol = (float)Utils.DBToLevel(Convert.ToDouble(volumes[ArrangedChannels[curr_channel]]), max_dB);
            }
            catch (Exception)
            {
                vol = (float)1.0;
            }

            //assign volume level to channels in the matrix
            if (inst_channels == 2) //is it a stereo track
            {
                try
                {
                    //assign current channel (left) to left channel
                    matrix[0, ArrangedChannels[curr_channel]] = vol;
                }
                catch (Exception)
                { }
                try
                {
                    //assign next channel (right) to the right channel
                    matrix[1, ArrangedChannels[curr_channel + 1]] = vol;
                }
                catch (Exception)
                { }
            }
            else
            {
                //it's a mono track, let's assign based on the panning vaue
                double pan;
                try
                {
                    pan = Convert.ToDouble(pans[ArrangedChannels[curr_channel]]);
                }
                catch (Exception)
                {
                    pan = 0.0; // in case there's an error above, it gets centered
                }

                if (pan <= 0) //centered or left, assign it to the left channel
                {
                    matrix[0, ArrangedChannels[curr_channel]] = vol;
                }
                if (pan >= 0) //centered or right, assignt to the right channel
                {
                    matrix[1, ArrangedChannels[curr_channel]] = vol;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Will safely try to move, and if fails, copy/delete a file
        /// </summary>
        /// <param name="input">Full starting path of the file</param>
        /// <param name="output">Full destination path of the file</param>
        public bool MoveFile(string input, string output)
        {
            try
            {
                DeleteFile(output);
                File.Move(input, output);
            }
            catch (Exception)
            {
                try
                {
                    File.Copy(input, output);
                    DeleteFile(input);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return File.Exists(output);
        }

        /// <summary>
        /// Simple function to safely delete folders
        /// </summary>
        /// <param name="folder">Full path of folder to be deleted</param>
        /// <param name="delete_contents">Whether to delete folders that are not empty</param>
        public void DeleteFolder(string folder, bool delete_contents)
        {
            if (!Directory.Exists(folder)) return;

            try
            {
                if (delete_contents)
                {
                    Directory.Delete(folder, true);
                    return;
                }
                if (!Directory.GetFiles(folder).Any())
                {
                    Directory.Delete(folder);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Simple function to safely delete files
        /// </summary>
        /// <param name="file">Full path of file to be deleted</param>
        public void DeleteFile(string file)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file)) return;
            try
            {
                File.Delete(file);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Simple function to safely delete folders
        /// </summary>
        /// <param name="folder">Full path of folder to be deleted</param>
        public void DeleteFolder(string folder)
        {
            if (!Directory.Exists(folder)) return;
            DeleteFolder(folder, false);
        }

        /// <summary>
        /// Returns string with correctly formatted characters
        /// </summary>
        /// <param name="raw_line">Raw line from songs.dta file</param>
        /// <returns></returns>
        public string FixBadChars(string raw_line)
        {
            var line = raw_line.Replace("Ã¡", "á");
            line = line.Replace("Ã©", "é");
            line = line.Replace("Ã¨", "è");
            line = line.Replace("ÃŠ", "Ê");
            line = line.Replace("Ã¬", "ì");
            line = line.Replace("Ã­­­", "í");
            line = line.Replace("Ã¯", "ï");
            line = line.Replace("Ã–", "Ö");
            line = line.Replace("Ã¶", "ö");
            line = line.Replace("Ã³", "ó");
            line = line.Replace("Ã²", "ò");
            line = line.Replace("Ãœ", "Ü");
            line = line.Replace("Ã¼", "ü");
            line = line.Replace("Ã¹", "ù");
            line = line.Replace("Ãº", "ú");
            line = line.Replace("Ã¿", "ÿ");
            line = line.Replace("Ã±", "ñ");
            line = line.Replace("ï¿½", "");
            line = line.Replace("�", "");
            line = line.Replace("E½", "");
            return line;
        }

        /// <summary>
        /// Returns byte array in hex value
        /// </summary>
        /// <param name="xIn">String value to be converted to hex</param>
        /// <returns></returns>
        public byte[] ToHex(string xIn)
        {
            for (var i = 0; i < (xIn.Length % 2); i++)
                xIn = "0" + xIn;
            var xReturn = new List<byte>();
            for (var i = 0; i < (xIn.Length / 2); i++)
                xReturn.Add(Convert.ToByte(xIn.Substring(i * 2, 2), 16));
            return xReturn.ToArray();
        }

        /// <summary>
        /// Returns clean Track Name from midi event string
        /// </summary>
        /// <param name="raw_event">The raw ToString value of the midi event</param>
        /// <returns></returns>
        public string GetMidiTrackName(string raw_event)
        {
            var name = raw_event;
            name = name.Substring(2, name.Length - 2); //remove track number
            name = name.Replace("SequenceTrackName", "");
            return name.Trim();
        }

        /// <summary>
        /// Returns cleaned string for file names, etc
        /// </summary>
        /// <param name="raw_string">Raw string from the songs.dta file</param>
        /// <param name="removeDash">Whether to remove dashes from the string</param>
        /// <param name="DashForSlash">Whether to replace slashes with dashes</param>
        /// <returns></returns>
        public string CleanString(string raw_string, bool removeDash, bool DashForSlash = false)
        {
            var mystring = raw_string;

            //remove forbidden characters if present
            mystring = mystring.Replace("\"", "");
            mystring = mystring.Replace(">", " ");
            mystring = mystring.Replace("<", " ");
            mystring = mystring.Replace(":", " ");
            mystring = mystring.Replace("|", " ");
            mystring = mystring.Replace("?", " ");
            mystring = mystring.Replace("*", " ");
            mystring = mystring.Replace("'", "");
            mystring = mystring.Replace("&#8217;", "'"); //Don't Speak
            mystring = mystring.Replace("   ", "");
            mystring = mystring.Replace("  ", "");
            mystring = mystring.Replace(" ", "");
            mystring = FixBadChars(mystring).Trim();

            if (removeDash)
            {
                if (mystring.Substring(0, 1) == "-") //if starts with -
                {
                    mystring = mystring.Substring(1, mystring.Length - 1);
                }
                if (mystring.Substring(mystring.Length - 1, 1) == "-") //if ends with -
                {
                    mystring = mystring.Substring(0, mystring.Length - 1);
                }

                mystring = mystring.Trim();
            }

            if (mystring.EndsWith(".", StringComparison.Ordinal)) //can't have files like Y.M.C.A.
            {
                mystring = mystring.Substring(0, mystring.Length - 1);
            }

            mystring = mystring.Replace("\\", DashForSlash && mystring != "AC/DC" ? "-" : (mystring != "AC/DC" ? " " : ""));
            mystring = mystring.Replace("/", DashForSlash && mystring != "AC/DC" ? "-" : (mystring != "AC/DC" ? " " : ""));

            return mystring;
        }

        public Image NemoLoadImage(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }
            Image img;
            using (var bmpTemp = new Bitmap(file))
            {
                img = new Bitmap(bmpTemp);
            }
            return img;
        }

        public MidiFile NemoLoadMIDI(string midi_in)
        {
            //NAudio is limited in its ability to read some non-standard MIDIs
            //before this step was added, 3 different errors would prevent this program from reading
            //MIDIs with those situations
            //thanks raynebc we can fix them first and load the fixed MIDIs
            var midishrink = Application.StartupPath + "\\bin\\midishrink.exe";
            if (!File.Exists(midishrink)) return null;
            var midi_out = Application.StartupPath + "\\bin\\fix.mid";
            DeleteFile(midi_out);
            MidiFile MIDI;
            try
            {
                MIDI = new MidiFile(midi_in, false);
            }
            catch (Exception)
            {
                var folder = Path.GetDirectoryName(midi_in) ?? Environment.CurrentDirectory;
                var startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    FileName = Application.StartupPath + "\\bin\\midishrink.exe",
                    Arguments = "\"" + midi_in + "\" \"" + midi_out + "\"",
                    WorkingDirectory = folder
                };
                var start = (DateTime.Now.Minute * 60) + DateTime.Now.Second;
                var process = Process.Start(startInfo);
                do
                {
                    //this code checks for possible memory leak in midishrink
                    //typical usage outputs a fixed file in 1 second or less, at 15 seconds there's a problem
                    if ((DateTime.Now.Minute * 60) + DateTime.Now.Second - start < 15) continue;
                    break;

                } while (!process.HasExited);
                if (!process.HasExited)
                {
                    process.Kill();
                    process.Dispose();
                }
                if (File.Exists(midi_out))
                {
                    try
                    {
                        MIDI = new MidiFile(midi_out, false);
                    }
                    catch (Exception)
                    {
                        MIDI = null;
                    }
                }
                else
                {
                    MIDI = null;
                }
            }
            DeleteFile(midi_out);  //the file created in the loop is useless, delete it
            return MIDI;
        }

        /// <summary>
        /// Use to quickly grab value on right side of = in C3 options/fix files
        /// </summary>
        /// <param name="raw_line">Raw line from the c3 file</param>
        /// <returns></returns>
        public string GetConfigString(string raw_line)
        {
            var line = raw_line;
            var index = line.IndexOf("=", StringComparison.Ordinal) + 1;
            try
            {
                line = line.Substring(index, line.Length - index);
            }
            catch (Exception)
            {
                line = "";
            }
            return line.Trim();
        }

        /// <summary>
        /// Returns line with featured artist normalized as 'ft.'
        /// </summary>
        /// <param name="line">Line to normalize</param>
        /// <returns></returns>
        public string FixFeaturedArtist(string line)
        {
            if (string.IsNullOrEmpty(line)) return "";

            var adjusted = line;

            adjusted = adjusted.Replace("Featuring", "ft.");
            adjusted = adjusted.Replace("featuring", "ft.");
            adjusted = adjusted.Replace("feat.", "ft.");
            adjusted = adjusted.Replace("Feat.", "ft.");
            adjusted = adjusted.Replace(" ft ", " ft. ");
            adjusted = adjusted.Replace(" FT ", " ft. ");
            adjusted = adjusted.Replace("Ft. ", "ft. ");
            adjusted = adjusted.Replace("FEAT. ", "ft. ");
            adjusted = adjusted.Replace(" FEAT ", " ft. ");

            if (adjusted.StartsWith("ft ", StringComparison.Ordinal))
            {
                adjusted = "ft. " + adjusted.Substring(3, adjusted.Length - 3);
            }

            return FixBadChars(adjusted);
        }

        /// <summary>
        /// Loads and formats help file for display on the HelpForm
        /// </summary>
        /// <param name="file">Name of the file, path assumed to be \bin\help/</param>
        /// <returns></returns>
        public string ReadHelpFile(string file)
        {
            var message = "";
            var helpfile = Application.StartupPath + "\\bin\\help\\" + file;
            if (File.Exists(helpfile))
            {
                var sr = new StreamReader(helpfile);
                while (sr.Peek() > 0)
                {
                    var line = sr.ReadLine();
                    message = message == "" ? line : message + "\r\n" + line;
                }
                sr.Dispose();
            }
            else
            {
                message = "Could not find help file, please redownload this program and DO NOT delete any files";
            }

            return message;
        }

        public bool HasMasterPassword()
        {
            //REDACTED BY TROJANNEMO
            return false;
        }

        public bool HasCorrectPassword(string authFile, string authPassword)
        {
            //REDACTED BY TROJANNEMO
            return false;
        }

        public string EncryptC3Password(string password)
        {
            //REDACTED BY TROJANNEMO
            return password;
        }

        public bool ConfirmPasswordMatches(string pass)
        {
            //REDACTED BY TROJANNEMO
            return false;
        }

        #region Mogg Stuff
        public void ReleaseStreamHandle(bool isNext)
        {
            try
            {
                if (isNext)
                {
                    NextOggStreamHandle.Free();
                }
                else
                {
                    PlayingOggStreamHandle.Free();
                }
            }
            catch (Exception)
            { }
        }

        public IntPtr GetOggStreamIntPtr(bool isNext)
        {
            ReleaseStreamHandle(isNext);
            if (isNext)
            {
                NextOggStreamHandle = GCHandle.Alloc(NextSongOggData, GCHandleType.Pinned);
                return NextOggStreamHandle.AddrOfPinnedObject();
            }
            PlayingOggStreamHandle = GCHandle.Alloc(PlayingSongOggData, GCHandleType.Pinned);
            return PlayingOggStreamHandle.AddrOfPinnedObject();
        }

        public bool MoggIsEncrypted(byte[] mData)
        {
            //REDACTED BY TROJANNEMO
            return false;
        }

        public bool DecM(byte[] mData, bool bypass, bool keep_header, bool isNext, DecryptMode mode, string mOut = "")
        {
            //REDACTED BY TROJANNEMO
            return false;
        }

        public void RemoveMHeader(byte[] mData, DecryptMode mode, string mOut)
        {
            byte[] buffer;
            using (var br = new BinaryReader(new MemoryStream(mData)))
            {
                br.ReadInt32();
                var num = br.ReadInt32();
                br.BaseStream.Seek(num, SeekOrigin.Begin);
                buffer = new byte[br.BaseStream.Length - num];
                br.Read(buffer, 0, buffer.Length);
            }
            if (mode == DecryptMode.ToMemory)
            {
                PlayingSongOggData = buffer;
            }
            else
            {
                WriteOutData(buffer, mOut);
            }
        }

        public void WriteOutData(byte[] mData, string mOut)
        {
            DeleteFile(mOut);
            using (var fs = File.Create(mOut))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(mData);
                }
            }
        }

        public bool IsC3Mogg(byte[] mData)
        {
            //REDACTED BY TROJANNEMO
            return false;
        }
        
        public byte[] DeObfM(byte[] mData)
        {
            //REDACTED BY TROJANNEMO
            return mData;
        }

        public byte[] ObfM(byte[] mData)
        {
            //REDACTED BY TROJANNEMO
            return mData;
        }

        private GCHandle PlayingOggStreamHandle;
        private GCHandle NextOggStreamHandle;
        public byte[] PlayingSongOggData;
        public byte[] NextSongOggData;
        #endregion
    }

    public enum DecryptMode
    {
        ToFile,
        ToMemory
    }

    public enum CryptVersion
    {
        x0A = 0x0A, //No encryption
        x0B = 0x0B, //RB1, RB1 DLC
        x0C = 0x0C, //RB2, AC/DC Live, some RB2 DLC
        x0E = 0x0E, //Lego, Green Day, most RB2 DLC
        x0F = 0x0F, //RBN
        x10 = 0x10, //RB3, RB3 DLC
    }
}
