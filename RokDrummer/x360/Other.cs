﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RokDrummer.x360
{
    [DebuggerStepThrough]
    internal static class ByteArrayExtentsion
    {
        public static string HexString(this byte[] x)
        {
            return BitConverter.ToString(x).Replace("-", "");
        }
    }
    
    /// <summary>
    /// Various Function Exceptions
    /// </summary>
    [DebuggerStepThrough]
    public static class VariousExcepts
    {
        [CompilerGenerated]
        static readonly Exception xByteInput = new Exception("Invalid input");
        /// <summary>
        /// Invalid byte input exception
        /// </summary>
        public static Exception ByteInput { get { return xByteInput; } }
    }

    /// <summary>
    /// Xbox File Types
    /// </summary>
    public enum XboxFileType
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Secure Transacted File System
        /// </summary>
        STFS,
        /// <summary>
        /// SVOD (Unknown Name)
        /// </summary>
        SVOD,
        /// <summary>
        /// Game Progress Data
        /// </summary>
        GPD,
        /// <summary>
        /// Game Disc File system
        /// </summary>
        GDF,
        /// <summary>
        /// Music file
        /// </summary>
        Music,
        /// <summary>
        /// File Allocation Tables for the Xbox
        /// </summary>
        FATX,
    }

    [DebuggerStepThrough]
    internal static class Constants
    {
        [CompilerGenerated]
        public const ushort FATX16End = 0xFFFF;
        [CompilerGenerated]
        public const uint STFSEnd = 0xFFFFFF;
        [CompilerGenerated]
        public const uint FATX32End = 0xFFFFFFFF;
        [CompilerGenerated]
        public static readonly uint[] BlockLevel = { 0xAA, 0x70E4 };
        [CompilerGenerated]
        public static readonly uint[] SVODBL = { 0xCC, 0xA1C4 };

        // For future use?
        [CompilerGenerated]
        internal static readonly byte[] _1BLKey =
        { 0xDD, 0x88, 0xAD, 0x0C,0x9E, 0xD6, 0x69, 0xE7, 0xB5, 0x67, 0x94, 0xFB, 0x68, 0x56, 0x3E, 0xFA };
        [CompilerGenerated]
        internal static readonly byte[] XEX1Key =
        { 0xA2, 0x6C, 0x10, 0xF7,0x1F, 0xD9, 0x35, 0xE9, 0x8B, 0x99, 0x92, 0x2C, 0xE9, 0x32, 0x15, 0x72 };
        [CompilerGenerated]
        internal static readonly byte[] XEX2Key =
        { 0x20, 0xB1, 0x85, 0xA5,0x9D, 0x28, 0xFD, 0xC3, 0x40, 0x58, 0x3F, 0xBB, 0x08, 0x96, 0xBF, 0x91 };
    }
    
    /// <summary>
    /// Various programming functions
    /// </summary>
    [DebuggerStepThrough]
    public static class VariousFunctions
    {
        /// <summary>
        /// Gets a File Filter from just a file name
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        public static string GetFilter(string File)
        {
            var xReturn = "";
            for (var i = File.Length - 1; i >= 0; i--)
            {
                if (File.Substring(i, 1) == ".")
                    xReturn = File.Substring(i, File.Length - i);
            }
            if (xReturn.Where((t, i) => xReturn.Substring(i, 1) == "/" || xReturn.Substring(i, 0) == "\\").Any())
            {
                return "";
            }
            if (xReturn.Length == 0)
                return xReturn;
            return xReturn.Substring(1).ToUpper() + "|*" + xReturn;
        }

        /// <summary>
        /// Deletes all unused X360 temporary files
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTempFiles()
        {
            var xpath = Path.GetTempPath() + "X360/";
            if (!Directory.Exists(xpath))
                return true;
            var xFolders = Directory.GetDirectories(xpath);
            foreach (var x in xFolders)
            {
                try { Directory.Delete(x, true); }
                catch (Exception)
                {}
            }
            var xFiles = Directory.GetFiles(xpath);
            foreach (var x in xFiles)
                DeleteFile(x);
            return true;

        }

        /// <summary>
        /// Generates a reserved temporary file location
        /// </summary>
        /// <returns></returns>
        public static string GetTempFileLocale()
        {
            var xpath = Path.GetTempPath() + "X360/";
            if (!xCheckDirectory(xpath))
                return null;
            return xpath + GrabRandomString();
        }

        static readonly Random rand = new Random();

        static string GrabRandomString()
        {
            var buff = new byte[5];
            rand.NextBytes(buff);
            return buff.HexString() + ".tmp";
        }

        /// <summary>
        /// Attempts to delete a file
        /// </summary>
        /// <param name="xLongFileName"></param>
        /// <returns></returns>
        public static bool DeleteFile(string xLongFileName)
        {
            try { File.Delete(xLongFileName); return true; }
            catch { return false; }
        }

        /// <summary>
        /// Converts a hexidecimal string to bytes, returns nothing if error
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string xInput)
        {
            xInput = xInput.Replace(" ", "");
            xInput = xInput.Replace("-", "");
            xInput = xInput.Replace("0x", "");
            xInput = xInput.Replace("0X", "");
            if ((xInput.Length % 2) != 0)
                xInput = "0" + xInput;
            var xOutput = new byte[(xInput.Length / 2)];
            try
            {
                for (var i = 0; i < xOutput.Length; i++)
                    xOutput[i] = Convert.ToByte(xInput.Substring((i * 2), 2), 16);
            }
            catch { throw VariousExcepts.ByteInput; }
            return xOutput;
        }

        /// <summary>
        /// Converts
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static long BinaryToLong(string xInput)
        {
            return Convert.ToInt64(xInput, 2);
        }

        /// <summary>
        /// Converts a byte array to a hexidecimal string
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static string BytesToHex(byte[] xInput) { return BitConverter.ToString(xInput).Replace("-", ""); }

        /// <summary>
        /// Gets a piece of an array from a specified position and size
        /// </summary>
        /// <param name="xInput"></param>
        /// <param name="xOffset"></param>
        /// <param name="xSize"></param>
        /// <returns></returns>
        public static byte[] BytePiece(this byte[] xInput, int xOffset, int xSize)
        {
            var buff = new byte[xSize];
            Array.Copy(xInput, xOffset, buff, 0, xSize);
            return buff;
        }

        /// <summary>
        /// Converts the "endianess" of a byte array
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static byte[] EndianConvert(this byte[] xInput)
        {
            Array.Reverse(xInput);
            return xInput;
        }

        /// <summary>
        /// Adds a byte to the specified array
        /// </summary>
        /// <param name="xArray"></param>
        /// <param name="xAdd"></param>
        /// <returns></returns>
        public static bool ByteArrayCombine(ref byte[] xArray, byte[] xAdd)
        {
            try
            {
                var xReturn = xArray.ToList();
                xReturn.AddRange(xAdd);
                xArray = xReturn.ToArray();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Moves a file
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Dest"></param>
        /// <returns></returns>
        public static bool MoveFile(string Source, string Dest)
        {
            try
            {
                DeleteFile(Dest);
                File.Move(Source, Dest);
                return true;
            }
            catch { return false; }
        }

        internal static void xSetByteArray(ref byte[] xOutput, byte[] xInput)
        {
            Array.Copy(xInput, 0, xOutput, 0, xInput.Length >= xOutput.Length ? xOutput.Length : xInput.Length);
        }

        internal static string xGetUnusedFile(string xIn)
        {
            long y = 0;
            var indx = xIn.Length - 1;
            var xadd = "";
            for (var i = 0; i < xIn.Length; i++)
            {
                if (xIn[i] != '.') continue;
                xadd = xIn.Substring(i, xIn.Length - i);
                xIn = xIn.Substring(0, i);
                indx = i;
                break;
            }
            while (File.Exists(xIn + xadd))
                xIn = xIn.Substring(0, indx) + "(" + (y++) + ")";
            return xIn + xadd;
        }

        internal static bool xCheckDirectory(string xOutLocale)
        {
            // Checks for directory
            if (Directory.Exists(xOutLocale)) return true;
            // Tries and creates it, string could be 'bitchfit' for all we know
            try { Directory.CreateDirectory(xOutLocale); }
            catch (Exception)
            {}
            return Directory.Exists(xOutLocale);
        }

        internal static string xExtractLegitPath(this string xin)
        {
            if (string.IsNullOrEmpty(xin))
                return "";
            xin = xin.Replace('\\', '/');
            if (xin[0] == '/')
                xin = xin.Substring(1, xin.Length - 1);
            if (xin[xin.Length - 1] == '/')
                xin = xin.Substring(0, xin.Length - 1);
            return xin;
        }

        internal static string xExtractName(this string xin)
        {
            var idx = xin.LastIndexOf('/');
            return idx == -1 ? xin : xin.Substring(idx + 1, xin.Length - idx - 1);
        }

        internal static int xPathCount(this string xin) { return xin.Split(new[] { '/' }).Length; }

        /// <summary>
        /// Checks if the name is a valid Xbox Name
        /// </summary>
        /// <param name="x">String value</param>
        /// <returns>True if valid</returns>
        public static bool IsValidXboxName(this string x)
        {
            if (string.IsNullOrEmpty(x)) throw STFSExcepts.InvalChars;
            var no = new List<char>();
            for (byte i = 0; i < 0x20; i++)
                no.Add((char)i);
            // char 0x20 - 0x2D usable symbols except 0x22 and 0x2A
            no.Add((char)0x22); // '"'
            no.Add((char)0x2A); // '*'
            no.Add((char)0x2F); // '/'
            // char 0x30 - 0x39 are '0' - '9'
            no.Add((char)0x3A); // ':'
            // char 0x3B and 0x3D are usable
            no.Add((char)0x3C); // '<'
            for (byte i = 0x3E; i < 0x40; i++)
                no.Add((char)i); // unusuable
            // 0x41 - 0x5A are A - Z, usable symbols up thru 0x60 except 0x5C
            no.Add((char)0x5C); // '\'
            // 0x61 - 0x7A are a - z, 0x7B, 0x7D, and 0x7E are usable
            no.Add((char)0x7C); // '|'
            for (byte i = 0x7F; i < 0xFF; i++)
                no.Add((char)i);
            no.Add((char)0xFF);
            if (x.IndexOfAny(no.ToArray()) == -1)
                return true;
            throw STFSExcepts.InvalChars;
        }

        /// <summary>
        /// Grabs a location from the end user
        /// </summary>
        /// <param name="Title">Dialog Title</param>
        /// <param name="Filter">Dialog Filter</param>
        /// <param name="IsOpen">True for Open File Dialog, false for Save File Dialog</param>
        /// <returns></returns>
        public static string GetUserFileLocale(string Title, string Filter, bool IsOpen)
        {
            return GetUserFileLocale(Title, Filter, "", IsOpen);
        }

        /// <summary>
        /// Grabs a user selected file
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Filter"></param>
        /// <param name="StartingFileName"></param>
        /// <param name="IsOpen"></param>
        /// <returns></returns>
        public static string GetUserFileLocale(string Title, string Filter, string StartingFileName, bool IsOpen)
        {
            if (IsOpen)
            {
                var ofd = new OpenFileDialog { Title = Title, Filter = Filter };
                return ofd.ShowDialog() != DialogResult.OK ? null : ofd.FileName;
            }
            var sfd = new SaveFileDialog { Title = Title, Filter = Filter };
            return sfd.ShowDialog() != DialogResult.OK ? null : sfd.FileName;
        }

        /// <summary>
        /// Grabs a user selected folder path
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static string GetUserFolderLocale(string Description)
        {
            return GetUserFolderLocale(Description, "");
        }

        /// <summary>
        /// Grabs a user selected folder path with a starting folder point
        /// </summary>
        /// <param name="Description"></param>
        /// <param name="StartPath"></param>
        /// <returns></returns>
        public static string GetUserFolderLocale(string Description, string StartPath)
        {
            var fbd = new FolderBrowserDialog { Description = Description };
            if (!string.IsNullOrEmpty(StartPath))
                fbd.SelectedPath = StartPath;
            return fbd.ShowDialog() != DialogResult.OK ? null : fbd.SelectedPath;
        }

        /// <summary>
        /// Attempts to find the type of the file
        /// </summary>
        /// <param name="FileLocale"></param>
        /// <returns></returns>
        public static XboxFileType ReadFileType(string FileLocale)
        {
            /* ADD FATX SUPPORT */
            var xIO = new DJsIO(FileLocale, DJFileMode.Open, true);
            try
            {
                var xReturn = XboxFileType.None;
                xIO.Position = 0;
                var sig = xIO.ReadUInt32();
                switch (sig)
                {
                    case (uint) AllMagic.CON:
                    case (uint) AllMagic.LIVE:
                    {
                        if (xIO.Length < 0x37C)
                            break;
                        xIO.Position = 0x379;
                        var desc = xIO.ReadBytes(3);
                        if (desc[0] == 0x24 && desc[1] == 0 &&
                            (desc[2] == 0 || desc[2] == 1 || desc[2] == 2))
                            xReturn = XboxFileType.STFS;
                        else if (desc[0] == 0x24 && desc[1] == 5 &&
                                 desc[2] == 5)
                            xReturn = XboxFileType.SVOD;
                    }
                        break;

                    case (uint) AllMagic.Music:
                    {
                        xReturn = XboxFileType.Music;
                    }
                        break;

                    case 0:
                    {
                        if (xIO.Length < 0x130EB0004)
                            break;
                        xIO.Position = 0x130EB0000;
                        xReturn = XboxFileType.FATX;
                    }
                        break;
                }
                xIO.Dispose();
                return xReturn;
            }
            catch (Exception)
            {
                xIO.Close();
                return XboxFileType.None;
                //throw;
            }
        }
    }

    /// <summary>
    /// General Time Stamps used
    /// </summary>
    [DebuggerStepThrough]
    public static class TimeStamps
    {
        /// <summary>
        /// Converts an Int64 FileTime to a DateTime
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static DateTime LongToDateTime(long xInput)
        {
            try { return DateTime.FromFileTime(xInput); }
            catch { return DateTime.Now; }
        }

        /// <summary>
        /// Converts a DateTime to an Int64 FileTime
        /// </summary>
        /// <param name="xInput"></param>
        /// <returns></returns>
        public static long DateTimeToLong(DateTime xInput)
        {
            try { return xInput.ToFileTime(); }
            catch { return DateTime.Now.ToFileTime(); }
        }

        /// <summary>
        /// Converts an Int32 FatTime to a DateTime
        /// </summary>
        /// <param name="xDateTime"></param>
        /// <returns></returns>
        public static DateTime FatTimeDT(int xDateTime)
        {
            var xDate = (short)(xDateTime >> 0x10);
            var xTime = (short)(xDateTime & 0xFFFF);
            if (xDate == 0 && xTime == 0)
                return DateTime.Now;
            return new DateTime(
                (((xDate & 0xFE00) >> 9) + 0x7BC),
                ((xDate & 0x1E0) >> 5),
                (xDate & 0x1F),
                ((xTime & 0xF800) >> 0xB),
                ((xTime & 0x7E0) >> 5),
                ((xTime & 0x1F) * 2));
        }

        /// <summary>
        /// Returns an all 0 DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime DateTimeZero { get { return new DateTime(0); } }

        /// <summary>
        /// Converts a DateTime to an Int32 FatTime
        /// </summary>
        /// <param name="xDateTime"></param>
        /// <returns></returns>
        public static int FatTimeInt(DateTime xDateTime)
        {
            if (xDateTime.Year < 1980)
                xDateTime = new DateTime(1980, xDateTime.Month, xDateTime.Day,
                    xDateTime.Hour, xDateTime.Minute, xDateTime.Second);
            var xTime = ((xDateTime.Hour << 11) | (xDateTime.Minute << 5)) | (xDateTime.Second >> 1);
            var xDate = (((xDateTime.Year - 1980) << 9) | (xDateTime.Month << 5)) | xDateTime.Day;
            return ((xDate << 0x10) | xTime);
        }
    }
    
    /// <summary>
    /// All headers for Xbox 360 Packages
    /// </summary>
    public enum AllMagic : uint
    {
        /// <summary>
        /// Music files
        /// </summary>
        Music = 0x464D494D,
        /// <summary>
        /// Console signed STFS
        /// </summary>
        CON = PackageMagic.CON,
        /// <summary>
        /// Xbox Live Server signed STFS
        /// </summary>
        LIVE = PackageMagic.LIVE
    }

    [DebuggerStepThrough]
    internal static class BitConv
    {
        // USED INTERNALLY, no size or position checks

        public static unsafe short ToInt16(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((short*)pbyte);
        }

        public static unsafe ushort ToUInt16(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((ushort*)pbyte);
        }

        public static unsafe int ToInt32(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((int*)pbyte);
        }

        public static unsafe uint ToUInt32(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((uint*)pbyte);
        }

        public static unsafe long ToInt64(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((long*)pbyte);
        }

        public static unsafe ulong ToUInt64(byte[] value, bool BigEndian)
        {
            if (BigEndian)
                value.EndianConvert();
            fixed (byte* pbyte = &value[0])
                return *((ulong*)pbyte);
        }

        public static unsafe float ToSingle(byte[] value, bool BigEndian)
        {
            var buff = ToInt32(value, BigEndian);
            return *(float*)&buff;
        }

        public static unsafe double ToDouble(byte[] value, bool BigEndian)
        {
            var buff = ToInt64(value, BigEndian);
            return *(double*)buff;
        }

        public static unsafe byte[] GetBytes(short value, bool BigEndian)
        {
            var buff = new byte[2];
            fixed (byte* pbyte = buff)
                *((short*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(ushort value, bool BigEndian)
        {
            var buff = new byte[2];
            fixed (byte* pbyte = buff)
                *((ushort*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(int value, bool BigEndian)
        {
            var buff = new byte[4];
            fixed (byte* pbyte = buff)
                *((int*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(uint value, bool BigEndian)
        {
            var buff = new byte[4];
            fixed (byte* pbyte = buff)
                *((uint*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(long value, bool BigEndian)
        {
            var buff = new byte[8];
            fixed (byte* pbyte = buff)
                *((long*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(ulong value, bool BigEndian)
        {
            var buff = new byte[8];
            fixed (byte* pbyte = buff)
                *((ulong*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(float value, bool BigEndian)
        {
            var buff = new byte[4];
            fixed (byte* pbyte = buff)
                *((float*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }

        public static unsafe byte[] GetBytes(double value, bool BigEndian)
        {
            var buff = new byte[8];
            fixed (byte* pbyte = buff)
                *((double*)pbyte) = value;
            if (BigEndian)
                buff.EndianConvert();
            return buff;
        }
    }
}