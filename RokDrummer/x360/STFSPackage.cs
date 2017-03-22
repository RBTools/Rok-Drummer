﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace RokDrummer.x360
{
    /// <summary>
    /// Base class for STFS items
    /// </summary>
    public class ItemEntry
    {
        #region Variables
        [CompilerGenerated]
        internal STFSPackage xPackage;
        [CompilerGenerated]
        internal int xCreated;
        [CompilerGenerated]
        internal int xAccessed;
        [CompilerGenerated]
        internal int xSize;
        [CompilerGenerated]
        internal uint xBlockCount;
        [CompilerGenerated]
        internal uint xStartBlock;
        [CompilerGenerated]
        string xName;
        [CompilerGenerated]
        internal ushort xEntryID = 0;
        [CompilerGenerated]
        internal ushort xFolderPointer = 0xFFFF;
        [CompilerGenerated]
        byte xFlag;
        [CompilerGenerated]
        long xDirectoryOffset;
        #endregion

        /// <summary>
        /// Time Created
        /// </summary>
        public DateTime Created { get { return TimeStamps.FatTimeDT(xCreated); } }
        /// <summary>
        /// Entry size
        /// </summary>
        public int Size { get { return xSize; } }
        /// <summary>
        /// Time last accessed
        /// </summary>
        public DateTime Accessed { get { return TimeStamps.FatTimeDT(xAccessed); } }
        /// <summary>
        /// Unknown flag
        /// </summary>
        public bool UnknownFlag
        {
            get { return ((xFlag >> 6) & 1) == 1; }
            set
            {
                if (value)
                    xFlag = (byte)(((FolderFlag ? 1 : 0) << 7) | (1 << 6) | xNameLength);
                else xFlag = (byte)(((FolderFlag ? 1 : 0) << 7) | (0 << 6) | xNameLength);
            }
        }
        byte xNameLength
        {
            get { return (byte)(xFlag & 0x3F); }
            set
            {
                if (value > 0x28)
                    value = 0x28;
                xFlag = (byte)(((FolderFlag ? 1 : 0) << 7) | ((UnknownFlag ? 1 : 0) << 6) | value);
            }
        }
        /// <summary>
        /// Parent directory pointer
        /// </summary>
        public ushort FolderPointer { get { return xFolderPointer; } }
        /// <summary>
        /// Deleted flag
        /// </summary>
        public bool IsDeleted { get { return xNameLength == 0; } }
        /// <summary>
        /// Offset in the package
        /// </summary>
        public long DirectoryOffset { get { return xDirectoryOffset; } }
        /// <summary>
        /// Is a folder
        /// </summary>
        public bool FolderFlag { get { return ((xFlag >> 7) & 1) == 1; } }
        /// <summary>
        /// Entry name
        /// </summary>
        public string Name
        {
            get { return xName; }
            set
            {
                value.IsValidXboxName();
                if (value.Length >= 0x28)
                    value = value.Substring(0, 0x28);
                xName = value;
                xNameLength = (byte)value.Length;
            }
        }
        /// <summary>
        /// Entry ID
        /// </summary>
        public ushort EntryID { get { return xEntryID; } }
        /// <summary>
        /// Start Blocm
        /// </summary>
        public uint StartBlock { get { return xStartBlock; } }
        /// <summary>
        /// Block Count
        /// </summary>
        public uint BlockCount { get { return xBlockCount; } }

        internal ItemEntry(byte[] xDataIn, long DirectOffset, ushort xID, STFSPackage xPackageIn)
        {
            try
            {
                xPackage = xPackageIn;
                var xFileIO = new DJsIO(xDataIn, true) { Position = 0 };
                xEntryID = xID;
                xFileIO.Position = 0x28;
                xFlag = xFileIO.ReadByte();
                if (xNameLength > 0x28)
                    xNameLength = 0x28;
                xFileIO.Position = 0;
                if (xNameLength == 0)
                    return;
                xName = xFileIO.ReadString(StringForm.ASCII, xNameLength);
                xName.IsValidXboxName();
                xFileIO.Position = 0x2F;
                xStartBlock = xFileIO.ReadUInt24(false);
                xFolderPointer = xFileIO.ReadUInt16();
                xSize = xFileIO.ReadInt32();
                xBlockCount = (uint)(((xSize - 1) / 0x1000) + 1);
                xCreated = xFileIO.ReadInt32();
                xAccessed = xFileIO.ReadInt32();
                xDirectoryOffset = DirectOffset;
            }
            catch { xNameLength = 0; }
        }

        internal ItemEntry(string NameIn, int SizeIn, bool xIsFolder, ushort xID, ushort xFolder, STFSPackage xPackageIn)
        {
            xPackage = xPackageIn;
            xEntryID = xID;
            xFolderPointer = xFolder;
            xName = NameIn.Length >= 0x28 ? NameIn.Substring(0, 0x28) : NameIn;
            xFlag = (byte)(((xIsFolder ? 1 : 0) << 7) | xName.Length);
            var x = DateTime.Now;
            xCreated = TimeStamps.FatTimeInt(x);
            xAccessed = xCreated;
            if (xIsFolder)
            {
                xSize = 0;
                xStartBlock = 0;
                xBlockCount = 0;
            }
            else
            {
                xSize = SizeIn;
                if (xSize != 0)
                    xBlockCount = (uint)(((xSize - 1) / 0x1000) + 1);
            }
        }

        internal ItemEntry(ItemEntry x)
        {
            xName = x.xName;
            xAccessed = x.xAccessed;
            xCreated = x.xCreated;
            xBlockCount = x.xBlockCount;
            xDirectoryOffset = x.xDirectoryOffset;
            xFlag = x.xFlag;
            xEntryID = x.xEntryID;
            xFolderPointer = x.xFolderPointer;
            xSize = x.xSize;
            xStartBlock = x.xStartBlock;
            xFlag = (byte)((x.FolderFlag ? 1 : 0) << 7 | (x.UnknownFlag ? 1 : 0) << 6 | xName.Length);
            xPackage = x.xPackage;
        }

        internal void xFixOffset() { xDirectoryOffset = xPackage.STFSStruct.GenerateDataOffset(xPackage.xFileBlocks[xEntryID / 0x40].ThisBlock) + ((0x40 * xEntryID) % 0x40); }

        /// <summary>
        /// Grabs the binary data representation
        /// </summary>
        /// <returns></returns>
        public byte[] GetEntryData()
        {
            try
            {
                var xReturn = new List<byte>();
                xReturn.AddRange(Encoding.ASCII.GetBytes(xName.ToCharArray()));
                xReturn.AddRange(new byte[0x28 - xName.Length]);
                xReturn.Add(xFlag);
                var xbuff = new List<byte>();
                xbuff.AddRange(BitConv.GetBytes(xBlockCount, false));
                xbuff.RemoveAt(3);
                xReturn.AddRange(xbuff);
                xReturn.AddRange(xbuff);
                xbuff.Clear();
                xbuff.AddRange(BitConv.GetBytes(xStartBlock, false));
                xbuff.RemoveAt(3);
                xReturn.AddRange(xbuff);
                xbuff.Clear();
                xReturn.AddRange(BitConv.GetBytes(xFolderPointer, true));
                xReturn.AddRange(BitConv.GetBytes(xSize, true));
                xReturn.AddRange(BitConv.GetBytes(xCreated, false));
                xReturn.AddRange(BitConv.GetBytes(xAccessed, false));
                return xReturn.ToArray();
            }
            catch { return new byte[0]; }
        }

        /// <summary>
        /// Writes the binary data
        /// </summary>
        /// <returns></returns>
        public bool WriteEntry()
        {
            if (!xPackage.ActiveCheck())
                return false;
            try
            {
                xPackage.xIO.Position = xDirectoryOffset;
                xPackage.xIO.Write(GetEntryData());
                xPackage.xIO.Flush();
                return true;
            }
            catch { return (xPackage.xActive = false); }
        }

        /// <summary>
        /// Grabsthe path
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            var xReturn = Name;
            var currfold = xFolderPointer;
            while (currfold != 0xFFFF)
            {
                ItemEntry xAbove = xPackage.xGetFolder(currfold);
                if (xAbove == null)
                    return null;
                xReturn = xAbove.Name + "/" + xReturn;
                currfold = xAbove.xFolderPointer;
            }
            return xReturn;
        }
    }

    /// <summary>
    /// Object for STFS File Entry
    /// </summary>
    public sealed class FileEntry : ItemEntry
    {
        internal FileEntry(ItemEntry xEntry) : base(xEntry) { }

        internal FileEntry(string NameIn, int SizeIn, bool xIsFolder, ushort xID, ushort xFolder, STFSPackage xPackageIn)
            : base(NameIn, SizeIn, xIsFolder, xID, xFolder, xPackageIn) { }

        [CompilerGenerated]
        internal BlockRecord[] xBlocks;
        [CompilerGenerated]
        internal DJsIO RealStream = null;

        internal bool Opened { get { return xBlocks != null && xBlocks.Length > 0; } }

        internal bool ReadBlocks()
        {
            try
            {
                if (RealStream != null)
                    return false;
                xPackage.GetBlocks(xBlockCount, xStartBlock, out xBlocks);
                if (xBlocks.Length < xBlockCount)
                    ClearBlocks();
                return Opened;
            }
            catch { return false; }
        }

        internal bool ClearBlocks()
        {
            try
            {
                if (RealStream != null)
                    return false;
                xBlocks = null;
                return true;
            }
            catch { return false; }
        }

        internal bool xExtract(DJsIO xIOOut)
        {
            if (!Opened && !ReadBlocks())
                return false;
            try
            {
                // Gets data and writes it
                xIOOut.Position = 0;
                for (uint i = 0; i < xBlockCount; i++)
                {
                    xPackage.xIO.Position = xPackage.GenerateDataOffset(xBlocks[i].ThisBlock);
                    xIOOut.Write(i < (xBlockCount - 1)
                                     ? xPackage.xIO.ReadBytes(0x1000)
                                     : xPackage.xIO.ReadBytes((((Size - 1) % 0x1000) + 1)));
                }
                xIOOut.Flush();
                ClearBlocks();
                return true;
            }
            catch
            {
                ClearBlocks();
                return false;
            }
        }

        /// <summary>
        /// Extracts the entry via user selection
        /// </summary>
        /// <param name="DialogTitle"></param>
        /// <param name="DialogFilter"></param>
        /// <returns></returns>
        public bool Extract(string DialogTitle, string DialogFilter)
        {
            var FileOut = VariousFunctions.GetUserFileLocale(DialogTitle, DialogFilter, false);
            return FileOut != null && Extract(FileOut);
        }

        /// <summary>
        /// Extracts entry to a location
        /// </summary>
        /// <param name="FileOut"></param>
        /// <returns></returns>
        public bool Extract(string FileOut)
        {
            if (!xPackage.ActiveCheck())
                return false;
            bool xReturn;
            var xIO = new DJsIO(true);
            try
            {
                xReturn = xExtract(xIO);
                xIO.Close();
                if (xReturn)
                {
                    if (!VariousFunctions.MoveFile(xIO.FileNameLong, FileOut))
                        throw new Exception();
                }
            }
            catch
            {
                xReturn = false;
                xIO.Close();
            }
            VariousFunctions.DeleteFile(xIO.FileNameLong);
            xPackage.xActive = false;
            return xReturn;
        }

        /// <summary>
        /// Extracts the entire file to memory, in a byte array. Caution: may use too much RAM.
        /// </summary>
        /// <returns></returns>
        public byte[] Extract()
        {
            if (!Opened && !ReadBlocks())
                throw new Exception("File not opened or couldn't read blocks.");
            try
            {
                using (var ms = new MemoryStream())
                {
                    ms.Position = 0;
                    for (uint i = 0; i < xBlockCount; i++)
                    {
                        xPackage.xIO.Position = xPackage.GenerateDataOffset(xBlocks[i].ThisBlock);
                        byte[] buffer;
                        if (i < (xBlockCount - 1))
                        {
                            buffer = xPackage.xIO.ReadBytes(0x1000);
                            ms.Write(buffer, 0, 0x1000);
                        }
                        else
                        {
                            buffer = xPackage.xIO.ReadBytes((((Size - 1) % 0x1000) + 1));
                            ms.Write(buffer, 0, buffer.Length);
                        }
                    }
                    ClearBlocks();
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                ClearBlocks();
                return null;
            }
        }

        /// <summary>
        /// Extracts certain number of bytes of a file into a byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Extract(uint bytes)
        {
            if (!Opened && !ReadBlocks())
                throw new Exception("File not opened or couldn't read blocks.");
            try
            {
                using (var ms = new MemoryStream())
                {
                    ms.Capacity = (int)bytes;
                    ms.Position = 0;
                    for (uint i = 0; i < xBlockCount; i++)
                    {
                        xPackage.xIO.Position = xPackage.GenerateDataOffset(xBlocks[i].ThisBlock);
                        byte[] buffer;
                        if (i < (xBlockCount - 1))
                        {
                            buffer = xPackage.xIO.ReadBytes(0x1000);
                            ms.Write(buffer, 0, (int)(ms.Position - 0x1000 < bytes ? 0x1000 : 0x1000 - (bytes - ms.Position)));
                        }
                        else
                        {
                            buffer = xPackage.xIO.ReadBytes((((Size - 1) % 0x1000) + 1));
                            ms.Write(buffer, 0, (int)(ms.Position - buffer.Length < bytes ? buffer.Length : buffer.Length - (bytes - ms.Position)));
                        }
                        if (ms.Position == bytes)
                        {
                            break;
                        }
                    }
                    ClearBlocks();
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                ClearBlocks();
                return null;
            }
        }

        /// <summary>
        /// Returns a real time STFS file stream
        /// </summary>
        /// <param name="MakeCopy"></param>
        /// <param name="BigEndian"></param>
        /// <returns></returns>
        public DJsIO GrabSTFSStream(bool MakeCopy, bool BigEndian)
        {
            try
            {
                if (RealStream != null)
                    return RealStream;
                if (!xPackage.ActiveCheck())
                    return null;
                if (MakeCopy)
                {
                    var xtemp = new DJsIO(true);
                    if (!xExtract(xtemp))
                    {
                        xtemp.Close();
                        VariousFunctions.DeleteFile(xtemp.FileNameLong);
                        return null;
                    }
                    xtemp.Close();
                    VariousFunctions.DeleteFile(xtemp.FileNameLong);
                }
                if (!Opened && !ReadBlocks())
                    return null;
                return (RealStream = new STFSStreamIO(this, BigEndian));
            }
            catch { RealStream = null; xPackage.xActive = false; return null; }
        }

        internal DJsIO xGetTempIO(bool BigEndian)
        {
            if (!Opened && !ReadBlocks())
                return null;
            var xIO = new DJsIO(BigEndian);
            if (!xExtract(xIO))
            {
                xIO.Close();
                VariousFunctions.DeleteFile(xIO.FileNameLong);
                xIO = null;
            }
            ClearBlocks();
            return xIO;
        }

        /// <summary>
        /// Extracts the file to a temporary location
        /// </summary>
        /// <param name="BigEndian"></param>
        /// <returns></returns>
        public DJsIO GetTempIO(bool BigEndian)
        {
            if (!xPackage.ActiveCheck())
                return null;
            var xReturn = xGetTempIO(BigEndian);
            xPackage.xActive = false;
            return xReturn;
        }

        internal FileEntry Copy()
        {
            var x = new FileEntry(this) { xBlocks = xBlocks };
            return x;
        }
    }

    /// <summary>
    /// Class for STFS Folder items
    /// </summary>
    public sealed class FolderEntry : ItemEntry
    {
        internal FolderEntry(ItemEntry xEntry) : base(xEntry) { }

        internal FolderEntry(string NameIn, int SizeIn, ushort xID, ushort xFolder, STFSPackage xPackageIn)
            : base(NameIn, SizeIn, true, xID, xFolder, xPackageIn) { }

        internal bool folderextract(bool xInclude, string xOut)
        {
            try
            {
                if (!VariousFunctions.xCheckDirectory(xOut))
                    return false;
                foreach (var x in xPackage.xFileDirectory)
                {
                    if (x.FolderPointer != xEntryID)
                        continue;
                    var xIO = new DJsIO(VariousFunctions.xGetUnusedFile(xOut + "/" + x.Name), DJFileMode.Create, true);
                    if (!xIO.Accessed) continue;
                    x.xExtract(xIO);
                    xIO.Dispose();
                }
                foreach (var z in xPackage.xFolderDirectory.Where(z => z.FolderPointer == EntryID))
                {
                    z.folderextract(xInclude, xOut + "/" + z.Name);
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Extract the files
        /// </summary>
        /// <param name="xOutLocale"></param>
        /// <param name="xIncludeSubItems"></param>
        /// <returns></returns>
        public bool Extract(string xOutLocale, bool xIncludeSubItems)
        {
            if (!xPackage.ActiveCheck())
                return false;
            if (xOutLocale[xOutLocale.Length - 1] == '/' ||
                xOutLocale[xOutLocale.Length - 1] == '\\')
                xOutLocale = xOutLocale.Substring(0, xOutLocale.Length - 1);
            if (!VariousFunctions.xCheckDirectory(xOutLocale))
            {
                return false;
            }
            folderextract(xIncludeSubItems, xOutLocale);
            return true;
        }

        /// <summary>
        /// Extract the files
        /// </summary>
        /// <param name="xIncludeSubItems"></param>
        /// <param name="xDescription"></param>
        /// <returns></returns>
        public bool Extract(bool xIncludeSubItems, string xDescription)
        {
            if (!xPackage.ActiveCheck())
                return false;
            var y = VariousFunctions.GetUserFolderLocale("Select a save location");
            return y != null && folderextract(xIncludeSubItems, y);
        }

        /// <summary>
        /// Grabs the subfolders
        /// </summary>
        /// <returns></returns>
        public FolderEntry[] GetSubFolders()
        {
            if (!xPackage.ActiveCheck())
                return null;
            var xReturn = xGetFolders();
            xPackage.xActive = false;
            return xReturn;
        }

        internal FolderEntry[] xGetFolders()
        {
            return xPackage.xFolderDirectory.Where(x => x.FolderPointer == EntryID).ToArray();
        }

        /// <summary>
        /// Grabs the files
        /// </summary>
        /// <returns></returns>
        public FileEntry[] GetSubFiles()
        {
            if (!xPackage.ActiveCheck())
                return null;
            var xReturn = xGetFiles();
            xPackage.xActive = false;
            return xReturn;
        }

        internal FileEntry[] xGetFiles()
        {
            return xPackage.xFileDirectory.Where(x => x.FolderPointer == EntryID).ToArray();
        }
    }

    /// <summary>
    /// Class for STFS Licenses
    /// </summary>
    public sealed class STFSLicense
    {
        [CompilerGenerated]
        internal long xID;
        [CompilerGenerated]
        internal int xInt1;
        [CompilerGenerated]
        internal int xInt2;

        /// <summary>
        /// ID
        /// </summary>
        public long ID { get { return xID; } }
        /// <summary>
        /// Bits
        /// </summary>
        public int Var1 { get { return xInt1; } }
        /// <summary>
        /// Flags
        /// </summary>
        public int Flags { get { return xInt2; } }

        internal STFSLicense(long xid, int x1, int x2)
        {
            xInt1 = x1;
            xInt2 = x2;
            xID = xid;
        }
    }

    /// <summary>
    /// Class for Header info
    /// </summary>
    public sealed class HeaderData
    {
        #region Non-Property Variables
        [CompilerGenerated]
        internal PackageMagic xMagic = PackageMagic.Unknown;
        [CompilerGenerated]
        List<STFSLicense> xLisc = new List<STFSLicense>();
        [CompilerGenerated]
        internal PackageType xThisType = PackageType.SavedGame;
        [CompilerGenerated]
        byte[] xPackageImage;
        [CompilerGenerated]
        byte[] xContentImage;
        /// <summary>
        /// Meta Data Version
        /// </summary>
        [CompilerGenerated]
        public uint MetaDataVersion = 2;
        [CompilerGenerated]
        long xContentSize;
        /// <summary>
        /// Media ID
        /// </summary>
        [CompilerGenerated]
        public uint MediaID;
        /// <summary>
        /// Version
        /// </summary>
        [CompilerGenerated]
        public uint Version_;
        /// <summary>
        /// Base Version
        /// </summary>
        [CompilerGenerated]
        public uint Version_Base;
        /// <summary>
        /// Title ID
        /// </summary>
        [CompilerGenerated]
        public uint TitleID = 0xFFFE07D1;
        /// <summary>
        /// Platform
        /// </summary>
        [CompilerGenerated]
        public byte Platform;
        /// <summary>
        /// Executable Type
        /// </summary>
        [CompilerGenerated]
        public byte ExecutableType;
        /// <summary>
        /// Disc Number
        /// </summary>
        [CompilerGenerated]
        public byte DiscNumber;
        /// <summary>
        /// Disc In Set
        /// </summary>
        [CompilerGenerated]
        public byte DiscInSet;
        /// <summary>
        /// Save Game ID
        /// </summary>
        [CompilerGenerated]
        public uint SaveGameID;
        /// <summary>
        /// Data File Count
        /// </summary>
        [CompilerGenerated]
        public uint DataFileCount;
        /// <summary>
        /// Data File Size
        /// </summary>
        [CompilerGenerated]
        public long DataFileSize;
        /// <summary>
        /// Reserved
        /// </summary>
        [CompilerGenerated]
        public long Reserved;
        [CompilerGenerated]
        byte[] xSeriesID = new byte[0x10];
        [CompilerGenerated]
        byte[] xSeasonID = new byte[0x10];
        /// <summary>
        /// Season Number
        /// </summary>
        [CompilerGenerated]
        public ushort SeasonNumber;
        /// <summary>
        /// Episode Number
        /// </summary>
        [CompilerGenerated]
        public ushort EpidsodeNumber;
        [CompilerGenerated]
        long xSaveConsoleID;
        /// <summary>
        /// Profile ID
        /// </summary>
        [CompilerGenerated]
        public long ProfileID;
        [CompilerGenerated]
        byte[] xDeviceID = new byte[20];
        [CompilerGenerated]
        readonly string[] xTitles = new string[9];
        [CompilerGenerated]
        readonly string[] xDescriptions = new string[9];
        [CompilerGenerated]
        string xPublisher = "";
        [CompilerGenerated]
        string xTitle = "";
        [CompilerGenerated]
        byte IDTransferByte;
        [CompilerGenerated]
        bool xLoaded;

        #endregion

        #region Property Variables
        /// <summary>
        /// Signature type
        /// </summary>
        public PackageMagic Magic { get { return xMagic; } }
        /// <summary>
        /// Package type
        /// </summary>
        public PackageType ThisType { get { return xThisType; } set { xThisType = value; } }
        /// <summary>
        /// STFS Licenses
        /// </summary>
        public STFSLicense[] Liscenses { get { return xLisc.ToArray(); } }
        /// <summary>
        /// STFS Licenses
        /// </summary>
        public long RecordedContentSize { get { return xContentSize; } }
        /// <summary>
        /// Series ID
        /// </summary>
        public byte[] SeriesID { get { return xSeriesID; } set { VariousFunctions.xSetByteArray(ref xSeriesID, value); } }
        /// <summary>
        /// Season ID
        /// </summary>
        public byte[] SeasonID { get { return xSeasonID; } set { VariousFunctions.xSetByteArray(ref xSeasonID, value); } }
        /// <summary>
        /// Console ID (creator)
        /// </summary>
        public long SaveConsoleID
        {
            get { return xSaveConsoleID; }
            set
            {
                if (value > 0xFFFFFFFFFF)
                    value = 0xFFFFFFFFFF;
                xSaveConsoleID = value;
            }
        }
        /// <summary>
        /// Device ID
        /// </summary>
        public byte[] DeviceID { get { return xDeviceID; } set { VariousFunctions.xSetByteArray(ref xDeviceID, value); } }
        /// <summary>
        /// Package Title
        /// </summary>
        public string Title_Package
        {
            get { return xTitle; }
            set
            {
                if (value.Length > 0x40)
                    value = value.Substring(0, 0x40);
                xTitle = value;
            }
        }
        #endregion

        void read(DJsIO xIO, PackageMagic MagicType)
        {
            xMagic = MagicType;
            xIO.Position = 0x22C;
            xLisc = new List<STFSLicense>();
            for (var i = 0; i < 0x10; i++)
                xLisc.Add(new STFSLicense(xIO.ReadInt64(), xIO.ReadInt32(), xIO.ReadInt32()));
            xIO.Position = 0x344;
            xThisType = (PackageType)xIO.ReadUInt32();
            MetaDataVersion = xIO.ReadUInt32();
            xContentSize = xIO.ReadInt64();
            MediaID = xIO.ReadUInt32();
            Version_ = xIO.ReadUInt32();
            Version_Base = xIO.ReadUInt32();
            TitleID = xIO.ReadUInt32();
            Platform = xIO.ReadByte();
            ExecutableType = xIO.ReadByte();
            DiscNumber = xIO.ReadByte();
            DiscInSet = xIO.ReadByte();
            SaveGameID = xIO.ReadUInt32();
            SaveConsoleID = (long)xIO.ReadUInt40();
            ProfileID = xIO.ReadInt64();
            xIO.Position = 0x39D;
            DataFileCount = xIO.ReadUInt32();
            DataFileSize = xIO.ReadInt64();
            Reserved = xIO.ReadInt64();
            xSeriesID = xIO.ReadBytes(0x10);
            xSeasonID = xIO.ReadBytes(0x10);
            SeasonNumber = xIO.ReadUInt16();
            EpidsodeNumber = xIO.ReadUInt16();
            xIO.Position += 0x28;
            xDeviceID = xIO.ReadBytes(0x14);
            for (var i = 0; i < 9; i++)
                xTitles[i] = xIO.ReadString(StringForm.Unicode, 0x80).Replace("\0", "");
            for (var i = 0; i < 9; i++)
                xDescriptions[i] = xIO.ReadString(StringForm.Unicode, 0x80).Replace("\0", "");
            xPublisher = xIO.ReadString(StringForm.Unicode, 0x40).Replace("\0", "");
            xTitle = xIO.ReadString(StringForm.Unicode, 0x40).Replace("\0", "");
            IDTransferByte = xIO.ReadByte();
            // Package Image
            var xSize = xIO.ReadInt32();
            xIO.Position = 0x171A;
            xPackageImage = xIO.ReadBytes(xSize < 0x4000 ? xSize : 0x4000);
            // Content Image
            xIO.Position = 0x1716;
            xSize = xIO.ReadInt32();
            xIO.Position = 0x571A;
            xContentImage = xIO.ReadBytes(xSize < 0x4000 ? xSize : 0x4000);
            xLoaded = true;
        }

        internal HeaderData(STFSPackage xPackage, PackageMagic MagicType) { read(xPackage.xIO, MagicType); }

        internal HeaderData(DJsIO xIOIn, PackageMagic MagicType) { read(xIOIn, MagicType); }

        internal bool Write(ref DJsIO x)
        {
            if (!xLoaded)
                return false;
            try
            {
                if (x == null || !x.Accessed)
                    return false;
                x.Position = 0x22C;
                foreach (var b in xLisc)
                {
                    x.Write(b.ID);
                    x.Write(b.Var1);
                    x.Write(b.Flags);
                }
                x.Position = 0x344;
                x.Write((uint)xThisType);
                x.Write(MetaDataVersion);
                x.Write(xContentSize);
                x.Write(MediaID);
                x.Write(Version_);
                x.Write(Version_Base);
                x.Write(TitleID);
                x.Write(Platform);
                x.Write(ExecutableType);
                x.Write(DiscNumber);
                x.Write(DiscInSet);
                x.Write(SaveGameID);
                x.WriteUInt40((ulong)SaveConsoleID);
                x.Write(ProfileID);
                x.Position = 0x39D;
                x.Write(DataFileCount);
                x.Write(DataFileSize);
                x.Write(Reserved);
                x.Write(SeriesID);
                x.Write(SeasonID);
                x.Write(SeasonNumber);
                x.Write(EpidsodeNumber);
                x.Position += 0x28;
                x.Write(xDeviceID);
                for (var i = 0; i < 9; i++)
                    x.Write(xTitles[i], StringForm.Unicode, 0x80, PadLocale.Right, PadType.Null);
                for (var i = 0; i < 9; i++)
                    x.Write(xDescriptions[i], StringForm.Unicode, 0x80, PadLocale.Right, PadType.Null);
                x.Write(xPublisher, StringForm.Unicode, 0x40, PadLocale.Right, PadType.Null);
                x.Write(xTitle, StringForm.Unicode, 0x40, PadLocale.Right, PadType.Null);
                x.Write(IDTransferByte);
                x.Write(xPackageImage.Length);
                x.Write(xContentImage.Length);
                x.Write(xPackageImage);
                x.Write(new byte[0x4000 - xPackageImage.Length]);
                x.Write(xContentImage);
                x.Write(new byte[(0x4000 - xContentImage.Length)]);
                return true;
            }
            catch { return false; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class STFSPackage
    {
        #region Variables
        [CompilerGenerated]
        HeaderData xHeader;
        [CompilerGenerated]
        internal List<FileEntry> xFileDirectory = new List<FileEntry>();
        [CompilerGenerated]
        internal List<FolderEntry> xFolderDirectory = new List<FolderEntry>();
        [CompilerGenerated]
        internal DJsIO xIO;
        [CompilerGenerated]
        internal STFSDescriptor xSTFSStruct;
        [CompilerGenerated]
        internal BlockRecord[] xFileBlocks;
        [CompilerGenerated]
        internal bool xActive = false; // To protect against errors from multithreading
        [CompilerGenerated]
        FolderEntry xroot;

        /// <summary>
        /// Package read correctly
        /// </summary>
        public bool ParseSuccess { get { return xIO != null; } }
        /// <summary>
        /// The STFS struct of the package
        /// </summary>
        public STFSDescriptor STFSStruct { get { return xSTFSStruct; } }
        /// <summary>
        /// Header metadata
        /// </summary>
        public HeaderData Header { get { return xHeader; } }
        /// <summary>
        /// Root Directory of package
        /// </summary>
        public FolderEntry RootDirectory { get { return xroot; } }

        uint xNewEntBlckCnt(uint xCount)
        {
            var x = (uint)(xFileDirectory.Count + xFolderDirectory.Count + xCount);
            if (x != 0)
                return ((x - 1) / 0x40) + 1;
            return 0;
        }

        internal uint xCurEntBlckCnt
        {
            get
            {
                var x = (xFileDirectory.Count + xFolderDirectory.Count);
                if (x != 0)
                    return (uint)(((x - 1) / 0x40) + 1);
                return 0;
            }
        }
        #endregion

        #region Local Methods
        /// <summary>
        /// Checks to see if the package was parsed
        /// </summary>
        /// <returns></returns>
        protected internal bool ParseCheck()
        {
            if (xIO == null || !xIO.Accessed || !ParseSuccess)
                throw STFSExcepts.Unsuccessful;
            return true;
        }

        /// <summary>
        /// Checks if the package is fine
        /// </summary>
        /// <returns></returns>
        internal bool ActiveCheck()
        {
            if (!ParseCheck())
                return false;
            //if (xActive)
            //return false;
            return (xActive = true);
        }

        /// <summary>
        /// Returns the blocks of a file
        /// </summary>
        /// <param name="xCount"></param>
        /// <param name="xStartBlock"></param>
        /// <param name="xOutBlocks"></param>
        /// <returns></returns>
        internal bool GetBlocks(uint xCount, uint xStartBlock, out BlockRecord[] xOutBlocks)
        {
            // Follows the blocks for the specified max count
            var xBlockList = new List<BlockRecord>();
            var xBlock = GetRecord(xStartBlock, TreeLevel.L0);
            if (xBlock.ThisBlock >= xSTFSStruct.xBlockCount)
                throw STFSExcepts.InvalBlock;
            for (uint i = 0; i < xCount; i++)
            {
                if (!xBlockList.ContainsBlock(xBlock))
                    xBlockList.Add(xBlock);
                else break; // If it contains, it's just going to loop
                if (xBlock.NextBlock == Constants.STFSEnd)
                    break; // Stop means stop
                if (xBlock.NextBlock >= xSTFSStruct.xBlockCount)
                    throw STFSExcepts.InvalBlock;
                // Gets the next block record
                xBlock = GetRecord(xBlock.NextBlock, TreeLevel.L0);
            }
            xOutBlocks = xBlockList.ToArray();
            // Success if 1 - end block is reached and 2 - count is the count of the blocks found
            return (xBlockList.Count == xCount);
        }

        /// <summary>
        /// Writes a SHA1 hash from base IO
        /// </summary>
        /// <param name="xRead"></param>
        /// <param name="xWrite"></param>
        /// <param name="xSize"></param>
        /// <returns></returns>
        internal bool XTakeHash(long xRead, long xWrite, int xSize)
        {
            try { return XTakeHash(ref xIO, xRead, xWrite, xSize, ref xIO); }
            catch { return false; }
        }

        /// <summary>
        /// Writes a SHA1 hash reading from base IO to another IO
        /// </summary>
        /// <param name="xRead"></param>
        /// <param name="xWrite"></param>
        /// <param name="xSize"></param>
        /// <param name="io"></param>
        /// <returns></returns>
        static void XTakeHash(long xRead, long xWrite, int xSize, ref DJsIO io)
        {
            try
            {
                XTakeHash(ref io, xRead, xWrite, xSize, ref io);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Reads from one IO, hashes, stores it in another IO
        /// </summary>
        /// <param name="ioin"></param>
        /// <param name="xRead"></param>
        /// <param name="xWrite"></param>
        /// <param name="xSize"></param>
        /// <param name="ioout"></param>
        /// <returns></returns>
        static bool XTakeHash(ref DJsIO ioin, long xRead, long xWrite, int xSize, ref DJsIO ioout)
        {
            try
            {
                ioin.Position = xRead;
                var xData = ioin.ReadBytes(xSize);
                ioout.Position = xWrite;
                ioout.Write(SHA1Quick.ComputeHash(xData));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Returns a bool if the corresponding offset/hash is the same
        /// </summary>
        /// <param name="xRead"></param>
        /// <param name="xSize"></param>
        /// <param name="xHash"></param>
        /// <returns></returns>
        bool XVerifyHash(long xRead, int xSize, ref byte[] xHash)
        {
            try
            {
                // Compares strings of the hashes
                xIO.Position = xRead;
                var xData = xIO.ReadBytes(xSize);
                return (BitConverter.ToString(xHash) == BitConverter.ToString(SHA1Quick.ComputeHash(xData)));
            }
            catch { return false; }
        }

        /// <summary>
        /// Produces a file via entries
        /// </summary>
        /// <param name="xFile"></param>
        /// <returns></returns>
        bool xEntriesToFile(out DJsIO xFile)
        {
            xFile = null;
            try
            {
                // Not much explaination is needed, just writes entries into a file
                xFile = new DJsIO(true);
                ushort xCurEnt = 0;
                foreach (var v in xFolderDirectory.Where(v => !v.IsDeleted))
                {
                    xFile.Position = 0x40 * xCurEnt;
                    // Reorders the folders to current entry
                    // Note: Don't have to do this, but I think it's sexy to handle folders at top of directory
                    var v1 = v;
                    foreach (var y in xFolderDirectory.Where(y => y.xFolderPointer == v1.EntryID))
                    {
                        y.xFolderPointer = xCurEnt;
                    }
                    var v2 = v;
                    foreach (var y in xFileDirectory.Where(y => y.xFolderPointer == v2.EntryID))
                    {
                        y.xFolderPointer = xCurEnt;
                    }
                    // Sets current entry
                    v.xEntryID = xCurEnt;
                    // Writes
                    xFile.Write(v.GetEntryData());
                    xCurEnt++;
                }
                for (var i = 0; i < xFolderDirectory.Count; i++)
                {
                    // Write new folder pointer
                    xFile.Position = (0x40 * i) + 0x32;
                    xFile.Write(xFolderDirectory[i].xFolderPointer);
                }
                foreach (var y in xFileDirectory.Where(y => !y.IsDeleted))
                {
                    // Sets
                    y.xEntryID = xCurEnt;
                    xFile.Position = 0x40 * xCurEnt;
                    // Writes
                    xFile.Write(y.GetEntryData());
                    xCurEnt++;
                }
                xFile.Flush();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Generates the data location of the block
        /// </summary>
        /// <param name="xBlock"></param>
        /// <returns></returns>
        internal long GenerateDataOffset(uint xBlock)
        {
            return xSTFSStruct.GenerateDataOffset(xBlock);
        }

        /// <summary>
        /// Generates a hash offset via block
        /// </summary>
        /// <param name="xBlock"></param>
        /// <param name="xTree"></param>
        /// <returns></returns>
        internal long GenerateHashOffset(uint xBlock, TreeLevel xTree)
        {
            var xReturn = xSTFSStruct.GenerateHashOffset(xBlock, xTree);
            if (xSTFSStruct.ThisType == STFSType.Type1) // Grabs the one up level block record for shifting
                xReturn += (GetRecord(xBlock, (TreeLevel)((byte)xTree + 1)).Index << 0xC);
            return xReturn;
        }

        /// <summary>
        /// Generates the Hash Base
        /// </summary>
        /// <param name="xBlock"></param>
        /// <param name="xTree"></param>
        /// <returns></returns>
        internal long GenerateBaseOffset(uint xBlock, TreeLevel xTree)
        {
            var xReturn = xSTFSStruct.GenerateBaseOffset(xBlock, xTree);
            if (xSTFSStruct.ThisType == STFSType.Type1) // Grabs the one up level block record for shifting
                xReturn += (GetRecord(xBlock, (TreeLevel)((byte)xTree + 1)).Index << 0xC);
            return xReturn;
        }

        /// <summary>
        /// Verifies the Header signature
        /// </summary>
        /// <param name="xDev"></param>
        /// <returns></returns>
        Verified VerifySignature(bool xDev)
        {
            try
            {
                var xRSAKeyz = new RSAParameters();
                short xSigSpot = 0;
                switch (xHeader.Magic)
                {
                    case PackageMagic.CON: // signature is the same way for both Dev and Stock
                        {
                            xSigSpot = 0x1AC;
                            xIO.Position = 0x28;
                            xRSAKeyz.Exponent = xIO.ReadBytes(4);
                            xRSAKeyz.Modulus = ScrambleMethods.StockScramble(xIO.ReadBytes(0x80), false);
                        }
                        break;
                    case PackageMagic.LIVE:
                        {
                            xSigSpot = 4;
                            xRSAKeyz.Exponent = !xDev ? new byte[] { 0, 1, 0, 1 } : new byte[] { 0, 0, 0, 3 };
                        }
                        break;
                }
                xIO.Position = xSigSpot;
                var xSiggy = ScrambleMethods.StockScramble(xIO.ReadBytes(xRSAKeyz.Modulus.Length), true);
                xIO.Position = 0x22C;
                var xHeadr = xIO.ReadBytes(0x118);
                return new Verified(ItemType.Signature, RSAQuick.SignatureVerify(xRSAKeyz, SHA1Quick.ComputeHash(xHeadr), xSiggy), 0x22C, xSigSpot);
            }
            catch { throw CryptoExcepts.CryptoVeri; }
        }

        /// <summary>
        /// Sets a package comming in to this package
        /// </summary>
        /// <param name="xIn"></param>
        void SetSamePackage(ref STFSPackage xIn)
        {
            xIO = xIn.xIO;
            xSTFSStruct = xIn.STFSStruct;
            xFolderDirectory = xIn.xFolderDirectory;
            xFileDirectory = xIn.xFileDirectory;
            xHeader = xIn.xHeader;
            xFileBlocks = xIn.xFileBlocks;
            xActive = xIn.xActive;
            xroot = xIn.xroot;
            xIn = null;
            foreach (var x in xFileDirectory)
                x.xPackage = this;
            foreach (var x in xFolderDirectory)
                x.xPackage = this;
        }

        /// <summary>
        /// Gets a folder name via it's Entry ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        internal string GetFolderNameByID(ushort ID)
        {
            return (from x in xFolderDirectory where x.EntryID == ID select x.Name).FirstOrDefault();
        }

        /// <summary>
        /// Gets a folder entry by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        internal FolderEntry xGetFolder(ushort ID)
        {
            return xFolderDirectory.FirstOrDefault(x => x.EntryID == ID);
        }

        /// <summary>
        /// Gets the last folder before the target ItemEntry
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        internal FolderEntry xGetParentFolder(string Path)
        {
            Path = Path.xExtractLegitPath();
            var folds = Path.Split(new[] { '/' }).ToList();
            foreach (var x in folds)
                x.IsValidXboxName();
            folds.RemoveAt(folds.Count - 1); // Last entry
            var xcurrent = xroot;
            foreach (var x in folds)
            {
                var found = false;
                // Grab folders pointing to current instance
                var folderz = xcurrent.xGetFolders();
                var x1 = x;
                foreach (var y in folderz.Where(y => y.Name.ToLowerInvariant() == x1.ToLowerInvariant()))
                {
                    // Set new found variables
                    found = true;
                    xcurrent = y;
                    break;
                }
                if (!found)
                {
                    return null;
                }
            }
            return xcurrent; // Must've been found, return it
        }

        /// <summary>
        /// Searches the files for the name and folder pointer
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="FolderPointer"></param>
        /// <returns></returns>
        internal FileEntry xGetFile(string Name, ushort FolderPointer)
        {
            return xFileDirectory.FirstOrDefault(x => x.FolderPointer == FolderPointer && x.Name.ToLowerInvariant() == Name.ToLowerInvariant());
        }

        enum SwitchType { None, Allocate, Delete }

        void SwitchNWrite(BlockRecord RecIn, SwitchType Change)
        {
            var canswitch = (xSTFSStruct.ThisType == STFSType.Type1);
            var current = xSTFSStruct.TopRecord;
            var pos = new long[] { 0, 0, 0 };
            // Grab base starting points
            if (RecIn.ThisBlock >= Constants.BlockLevel[0] ||
                xSTFSStruct.xBlockCount > Constants.BlockLevel[0])
            {
                if (RecIn.ThisBlock >= Constants.BlockLevel[1] ||
                    xSTFSStruct.xBlockCount > Constants.BlockLevel[1])
                    pos[0] = xSTFSStruct.GenerateHashOffset(RecIn.ThisBlock, TreeLevel.L2) + 0x14;
                pos[1] = xSTFSStruct.GenerateHashOffset(RecIn.ThisBlock, TreeLevel.L1) + 0x14;
            }
            pos[2] = xSTFSStruct.GenerateHashOffset(RecIn.ThisBlock, TreeLevel.L0) + 0x14;
            var len = GenerateDataOffset(RecIn.ThisBlock) + 0x1000;
            if (xIO.Length < len)
                xIO.SetLength(len);
            switch (Change)
            {
                case SwitchType.Allocate:
                    xSTFSStruct.TopRecord.BlocksFree--;
                    break;
                case SwitchType.Delete:
                    xSTFSStruct.TopRecord.BlocksFree++;
                    break;
            }
            if (pos[0] != 0)
            {
                //KILL ON RECONSTRUCTION
                if (canswitch)
                    pos[0] += (current.Index << 0xC);
                // ---------------------

                xIO.Position = pos[0];
                current = new BlockRecord(xIO.ReadUInt32());
                if (Change != SwitchType.None)
                {
                    if (Change == SwitchType.Allocate)
                        current.BlocksFree--; // Takes away a free block
                    else current.BlocksFree++; // Adds a free block
                    xIO.Position = pos[0];
                    xIO.Write(current.Flags);
                    xIO.Flush();
                }
            }
            // Follows same pattern
            if (pos[1] != 0)
            {
                //KILL ON RECONSTRUCTION
                if (canswitch)
                    pos[1] += (current.Index << 0xC);
                // ---------------------

                xIO.Position = pos[1];
                current = new BlockRecord(xIO.ReadUInt32());
                if (Change != SwitchType.None)
                {
                    if (Change == SwitchType.Allocate)
                        current.BlocksFree--; // Takes away a free block
                    else current.BlocksFree++; // Adds a free block
                    xIO.Position = pos[1];
                    xIO.Write(current.Flags);
                    xIO.Flush();
                }
            }

            //KILL ON RECONSTRUCTION
            if (canswitch)
                pos[2] += (current.Index << 0xC);
            // ---------------------

            switch (Change)
            {
                case SwitchType.Allocate:
                    RecIn.Status = RecIn.Status == HashStatus.Old ? HashStatus.Reused : HashStatus.New;
                    break;
                case SwitchType.Delete:
                    RecIn.MarkOld();
                    break;
            }
            xIO.Position = pos[2];
            xIO.Write(RecIn.Flags);
            xIO.Flush();
            if (RecIn.ThisBlock >= xSTFSStruct.xBlockCount)
                xSTFSStruct.xBlockCount = RecIn.ThisBlock + 1;
        }

        BlockRecord GetRecord(uint xBlock, TreeLevel xLevel)
        {
            if (xLevel == TreeLevel.LT)
                return xSTFSStruct.TopRecord;
            var current = xSTFSStruct.TopRecord;
            var canswitch = (xSTFSStruct.ThisType == STFSType.Type1);
            if (xSTFSStruct.xBlockCount > Constants.BlockLevel[1])
            {
                // Grab base position
                xIO.Position = (xSTFSStruct.GenerateHashOffset(xBlock, TreeLevel.L2) + 0x14);
                if (canswitch)
                    xIO.Position += (current.Index << 0xC);
                current = new BlockRecord(xIO.ReadUInt32()); // Read new flag
                if (xLevel == TreeLevel.L2)
                {
                    // return if needed
                    current.ThisBlock = xBlock;
                    current.ThisLevel = TreeLevel.L2;
                    return current;
                }
            }
            else if (xLevel == TreeLevel.L2)
                return xSTFSStruct.TopRecord;
            // Follows same procedure
            if (xSTFSStruct.xBlockCount > Constants.BlockLevel[0])
            {
                xIO.Position = (xSTFSStruct.GenerateHashOffset(xBlock, TreeLevel.L1)) + 0x14;
                if (canswitch)
                    xIO.Position += (current.Index << 0xC);
                current = new BlockRecord(xIO.ReadUInt32());
                if (xLevel == TreeLevel.L1)
                {
                    current.ThisBlock = xBlock;
                    current.ThisLevel = TreeLevel.L1;
                    return current;
                }
            }
            else if (xLevel == TreeLevel.L1)
                return xSTFSStruct.TopRecord;
            xIO.Position = (xSTFSStruct.GenerateHashOffset(xBlock, TreeLevel.L0)) + 0x14;
            if (canswitch)
                xIO.Position += (current.Index << 0xC);
            current = new BlockRecord(xIO.ReadUInt32()) { ThisBlock = xBlock, ThisLevel = TreeLevel.L0 };
            return current;
        }

        internal bool xWriteChain(BlockRecord[] xRecs)
        {
            for (var i = 0; i < xRecs.Length; i++)
            {
                xRecs[i].NextBlock = (i + 1) < xRecs.Length ? xRecs[i + 1].ThisBlock : Constants.STFSEnd;
                SwitchNWrite(xRecs[i], SwitchType.Allocate);
            }
            return true;
        }

        internal bool xDeleteChain(BlockRecord[] xBlocks)
        {
            if (xBlocks == null)
                return true;
            foreach (var x in xBlocks)
            {
                SwitchNWrite(x, SwitchType.Delete);
            }
            return true;
        }

        internal BlockRecord[] xAllocateBlocks(uint count, uint xStart)
        {
            if ((xSTFSStruct.BlockCount + count) > xSTFSStruct.SpaceBetween[2])
                return new BlockRecord[0];
            var xReturn = new List<BlockRecord>();
            for (uint i = 0; i < count; i++)
            {
                BlockRecord x = null;
                while (x == null)
                {
                    if (xStart > xSTFSStruct.SpaceBetween[2])
                        break;
                    // Grab record or make new one
                    if (xStart < xSTFSStruct.xBlockCount)
                    {
                        var y = GetRecord(xStart, TreeLevel.L0);
                        if (y.Status == HashStatus.Old || y.Status == HashStatus.Unused)
                            x = y;
                    }
                    else
                    {
                        if (xStart == Constants.BlockLevel[0])
                        {
                            xIO.Position = GenerateHashOffset(0, TreeLevel.L1) + (xSTFSStruct.TopRecord.Index << 0xC) + 0x14;
                            xIO.Write(xSTFSStruct.TopRecord.Flags);
                            xIO.Flush();
                        }
                        else if (xStart == Constants.BlockLevel[1])
                        {
                            xIO.Position = GenerateHashOffset(0, TreeLevel.L2) + (xSTFSStruct.TopRecord.Index << 0xC) + 0x14;
                            xIO.Write(xSTFSStruct.TopRecord.Flags);
                            xIO.Flush();
                        }
                        x = new BlockRecord(HashStatus.New, Constants.STFSEnd)
                        {
                            ThisBlock = xStart,
                            ThisLevel = TreeLevel.L0
                        };
                    }
                    xStart++;
                }
                xReturn.Add(x);
            }
            return xReturn.ToArray();
        }
        #endregion

        #region Package initialization
        /// <summary>
        /// Lets user auto select package
        /// </summary>
        public STFSPackage()
            : this(new DJsIO(DJFileMode.Open, "Open an Xbox Package", "", true))
        {
            if (!ParseSuccess && xIO != null)
                xIO.Dispose();
        }

        /// <summary>
        /// Initializes a package parse from an already accessed file
        /// </summary>
        /// <param name="xIOIn"></param>
        public STFSPackage(DJsIO xIOIn)
        {
            if (!xIOIn.Accessed)
                return;
            xIO = xIOIn;
            xActive = true;
            try
            {
                xIO.Position = 0;
                xIO.IsBigEndian = true;
                var xBuff = xIOIn.ReadUInt32();
                PackageMagic xMagic;
                if (Enum.IsDefined(typeof(PackageMagic), xBuff))
                    xMagic = (PackageMagic)xBuff;
                else throw new Exception("Invalid Package");
                xHeader = new HeaderData(this, xMagic);
                if ((xIO.Length % 0x1000) != 0)
                {
                    xIO.Position = xIO.Length;
                    xIO.Write(new byte[(int)(0x1000 - (xIO.Length % 0x1000))]);
                    xIO.Flush();
                }
                if (xHeader.ThisType == PackageType.HDDInstalledGame ||
                    xHeader.ThisType == PackageType.OriginalXboxGame ||
                    xHeader.ThisType == PackageType.GamesOnDemand ||
                    xHeader.ThisType == PackageType.SocialTitle)
                    throw STFSExcepts.Game;
                xSTFSStruct = new STFSDescriptor(this);
                xFileBlocks = new BlockRecord[0];
                GetBlocks(xSTFSStruct.DirectoryBlockCount, xSTFSStruct.DirectoryBlock, out xFileBlocks);
                ushort xEntryID = 0;
                foreach (var xCurrentOffset in xFileBlocks.Select(x => GenerateDataOffset(x.ThisBlock)))
                {
                    for (var i = 0; i < 0x40; i++)
                    {
                        xIO.Position = (xCurrentOffset + (0x40 * i));
                        if (xIO.ReadByte() == 0)
                            continue;
                        xIO.Position--;
                        var xItem = new ItemEntry(xIO.ReadBytes(0x40), (xIO.Position - 0x40), xEntryID, this);
                        if (xItem.IsDeleted)
                            continue;
                        if (!xItem.FolderFlag)
                            xFileDirectory.Add(new FileEntry(xItem));
                        else xFolderDirectory.Add(new FolderEntry(xItem));
                        xEntryID++;
                    }
                }
                xroot = new FolderEntry("", 0, 0xFFFF, 0xFFFF, this);
                xActive = false;
            }
            catch (Exception) { xIO = null; throw; }

        }

        /// <summary>
        /// Attempts to parse a file from a specific location
        /// </summary>
        /// <param name="xLocation"></param>
        public STFSPackage(string xLocation)
            : this(new DJsIO(xLocation, DJFileMode.Open, true)) { }

        /// <summary>
        /// Function for partial classes, importing packages
        /// </summary>
        /// <param name="xIn"></param>
        protected STFSPackage(ref STFSPackage xIn) { xActive = true; SetSamePackage(ref xIn); xActive = false; }
        #endregion

        #region Public Methods
        /* Structure Verification */
        /// <summary>
        /// Returns a List of details containing the package
        /// </summary>
        /// <returns></returns>
        public Verified[] VerifyHashTables()
        {
            if (!ActiveCheck())
                return null;
            var xReturn = new List<Verified>();
            try
            {
                // Verifies each level needed
                for (uint i = 0; i < xSTFSStruct.BlockCount; i++)
                {
                    var lvl = GetRecord(i, TreeLevel.L1);
                    if (lvl.BlocksFree >= Constants.BlockLevel[0]) continue;
                    var xDataBlock = GenerateDataOffset(i);
                    if (xDataBlock >= xIO.Length) continue;
                    var xHashLocale = xSTFSStruct.GenerateHashOffset(i, 0) + (lvl.Index << 0xC);
                    xIO.Position = xHashLocale;
                    var xHash = xIO.ReadBytes(20);
                    xReturn.Add(new Verified(ItemType.Data, XVerifyHash(xDataBlock, 0x1000, ref xHash), xDataBlock, xHashLocale));
                }
                if (STFSStruct.BlockCount > Constants.BlockLevel[0])
                {
                    var ct = (((xSTFSStruct.xBlockCount - 1) / Constants.BlockLevel[0]) + 1);
                    for (uint i = 0; i < ct; i++)
                    {
                        var lvl = GetRecord(i * Constants.BlockLevel[0], TreeLevel.L2);
                        var current = GetRecord(i * Constants.BlockLevel[0], TreeLevel.L1);
                        if (lvl.BlocksFree >= Constants.BlockLevel[1] && current.BlocksFree >= Constants.BlockLevel[0])
                            continue;
                        var xInputLocale = xSTFSStruct.GenerateBaseOffset(i * Constants.BlockLevel[0], TreeLevel.L0) + (current.Index << 0xC);
                        if (xInputLocale >= xIO.Length) continue;
                        var xHashLocale = xSTFSStruct.GenerateHashOffset((i * Constants.BlockLevel[0]), TreeLevel.L1) + (lvl.Index << 0xC);
                        xIO.Position = xHashLocale;
                        var xHash = xIO.ReadBytes(20);
                        xReturn.Add(new Verified(ItemType.TableTree0, XVerifyHash(xInputLocale, 0x1000, ref xHash), xInputLocale, xHashLocale));
                    }
                    if (STFSStruct.BlockCount > Constants.BlockLevel[1])
                    {
                        ct = (((xSTFSStruct.xBlockCount - 1) / Constants.BlockLevel[1]) + 1);
                        for (uint i = 0; i < ct; i++)
                        {
                            var current = GetRecord(i * Constants.BlockLevel[1], TreeLevel.L2);
                            if (current.BlocksFree >= Constants.BlockLevel[1]) continue;
                            var xInputLocale = xSTFSStruct.GenerateBaseOffset((i * Constants.BlockLevel[1]), TreeLevel.L1) + (current.Index << 0xC);
                            var xHashLocale = GenerateHashOffset((i * Constants.BlockLevel[1]), TreeLevel.L2);
                            xIO.Position = xHashLocale;
                            var xHash = xIO.ReadBytes(20);
                            xReturn.Add(new Verified(ItemType.TableTree1, XVerifyHash(xInputLocale, 0x1000, ref xHash), xInputLocale, xHashLocale));
                        }
                    }
                }
                xActive = false;
                return xReturn.ToArray();
            }
            catch (Exception) { xActive = false; throw; }
        }

        /// <summary>
        /// Verify the header
        /// </summary>
        /// <returns></returns>
        public Verified[] VerifyHeader()
        {
            if (!ActiveCheck())
                return null;
            try
            {
                var xReturn = new List<Verified>();
                // Verifies master hash with currently written header
                xIO.Position = 0x395;
                xIO.IsBigEndian = true;
                var xBlockCount = xIO.ReadInt32();
                long xLocale;
                if (xBlockCount <= Constants.BlockLevel[0])
                    xLocale = GenerateBaseOffset(0, 0);
                else if (xBlockCount <= Constants.BlockLevel[1])
                    xLocale = GenerateBaseOffset(0, TreeLevel.L1);
                else xLocale = GenerateBaseOffset(0, TreeLevel.L2);
                xIO.Position = 0x381;
                var xHash = xIO.ReadBytes(20);
                xReturn.Add(new Verified(ItemType.Master, XVerifyHash(xLocale, 0x1000, ref xHash), xLocale, (0x381)));
                // Verifies currently written header
                var xSize = xSTFSStruct.BaseBlock == 0xA000 ? 0x9CBC : 0xACBC;
                xIO.Position = 0x32C;
                xHash = xIO.ReadBytes(20);
                xReturn.Add(new Verified(ItemType.Header, XVerifyHash(0x344, xSize, ref xHash), 0x344, 0x32C));
                switch (xHeader.Magic)
                {
                    case PackageMagic.CON:
                        {
                            // Verifies Certificate
                            var xRSAKeyz = new RSAParameters
                            {
                                Exponent = new byte[] { 0, 0, 0, 3 },
                            };
                            xIO.Position = 4;
                            var xCert = xIO.ReadBytes(0xA8);
                            var xSig = xIO.ReadBytes(0x100);
                            xReturn.Add(new Verified(ItemType.Certificate, RSAQuick.SignatureVerify(xRSAKeyz, SHA1Quick.ComputeHash(xCert), ScrambleMethods.StockScramble(xSig, true)), 4, 0xAC));
                            xReturn.Add(VerifySignature(false)); // Doesn't matter, same thing for CON
                            xActive = false;
                            return xReturn.ToArray();
                        }
                    default:
                        {
                            xReturn.Add(VerifySignature(false));
                            xReturn.Add(VerifySignature(true));
                            xActive = false;
                            return xReturn.ToArray();
                        }
                }
            }
            catch { xActive = false; throw STFSExcepts.General; }
        }

        /// <summary>
        /// Gets a file by path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public FileEntry GetFile(string Path)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                if (string.IsNullOrEmpty(Path))
                    throw new Exception();
                Path = Path.Replace("\\", "/");
                if (Path[0] == '/')
                    Path = Path.Substring(1, Path.Length - 1);
                if (Path[Path.Length - 1] == '/')
                    Path = Path.Substring(0, Path.Length - 1);
                var parent = xGetParentFolder(Path);
                if (parent == null)
                    throw new Exception();
                var file = Path.Split(new[] { '/' }).LastValue();
                if (string.IsNullOrEmpty(file))
                    throw new Exception();
                var z = xGetFile(file, parent.EntryID);
                xActive = false;
                return z;
            }
            catch { xActive = false; return null; }
        }

        /// <summary>
        /// Gets a file by the name and pointer
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="FolderPointer"></param>
        /// <returns></returns>
        public FileEntry GetFile(string Name, ushort FolderPointer)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                var xReturn = xGetFile(Name, FolderPointer);
                xActive = false;
                return xReturn;
            }
            catch { xActive = false; return null; }
        }

        /// <summary>
        /// Gets a folder by it's ID
        /// </summary>
        /// <param name="FolderID"></param>
        /// <returns></returns>
        public FolderEntry GetFolder(ushort FolderID)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                var xReturn = xGetFolder(FolderID);
                xActive = false;
                return xReturn;
            }
            catch { xActive = false; return null; }
        }

        /// <summary>
        /// Gets a folder by it's path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public FolderEntry GetFolder(string Path)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                Path = Path.Replace("\\", "/");
                if (Path[0] == '/')
                    Path = Path.Substring(1, Path.Length - 1);
                if (Path[Path.Length - 1] == '/')
                    Path = Path.Substring(0, Path.Length - 1);
                var parent = xGetParentFolder(Path);
                if (parent == null)
                    throw new Exception();
                var folder = Path.Split(new[] { '/' }).LastValue();
                if (string.IsNullOrEmpty(folder))
                    throw new Exception();
                var foldz = parent.xGetFolders();
                foreach (var x in foldz.Where(x => x.Name.ToLowerInvariant() == folder.ToLowerInvariant()))
                {
                    xActive = false;
                    return x;
                }
                throw new Exception();
            }
            catch { xActive = false; return null; }
        }

        /// <summary>
        /// Grabs the files via the pointer
        /// </summary>
        /// <param name="FolderPointer"></param>
        /// <returns></returns>
        public FileEntry[] GetFiles(ushort FolderPointer)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                xActive = false;
                return xFileDirectory.Where(x => x.FolderPointer == FolderPointer).ToArray();
            }
            catch { xActive = false; return null; }
        }

        /// <summary>
        /// Grabs the files via the path
        /// </summary>
        /// <param name="FolderPath"></param>
        /// <returns></returns>
        public FileEntry[] GetFiles(string FolderPath)
        {
            if (!ActiveCheck())
                return null;
            try
            {
                if (string.IsNullOrEmpty(FolderPath))
                    throw new Exception();
                FolderPath = FolderPath.Replace("\\", "/");
                if (FolderPath[0] == '/')
                    FolderPath = FolderPath.Substring(1, FolderPath.Length - 1);
                if (FolderPath[FolderPath.Length - 1] == '/')
                    FolderPath = FolderPath.Substring(0, FolderPath.Length - 1);
                FolderPath += "/a"; // Fake a random name so i can just use the parent folder function
                var parent = xGetParentFolder(FolderPath);
                if (parent == null)
                    throw new Exception();
                xActive = false;
                return xFileDirectory.Where(x => x.FolderPointer == parent.FolderPointer).ToArray();
            }
            catch { xActive = false; return null; }
        }
        #endregion

        #region Package IO stuff
        /// <summary>
        /// File location
        /// </summary>
        public string FileNameLong { get { return xIO.FileNameLong; } }
        /// <summary>
        /// File Name
        /// </summary>
        public string FileNameShort { get { return xIO.FileNameShort; } }
        /// <summary>
        /// File Path
        /// </summary>
        public string FilePath { get { return xIO.FilePath; } }
        /// <summary>
        /// File Extension
        /// </summary>
        public string FileExtension { get { return xIO.FileExtension; } }

        /// <summary>
        /// Close the IO
        /// </summary>
        /// <returns></returns>
        public bool CloseIO()
        {
            //if (xActive)
            //    return false;
            xActive = true;
            if (xIO != null)
            {
                xIO.Close();
            }
            return true;
        }
        #endregion
    }

    static class extenz
    {
        public static uint[] Reverse(this uint[] xIn)
        {
            var xreturn = new List<uint>(xIn);
            xreturn.Reverse();
            xIn = xreturn.ToArray();
            return xIn;
        }

        public static string LastValue(this string[] xIn)
        {
            return xIn.Length == 0 ? null : xIn[xIn.Length - 1];
        }

        public static bool ContainsBlock(this List<BlockRecord> x, BlockRecord y)
        {
            return x.Any(z => z.ThisBlock == y.ThisBlock);
        }
    }
}