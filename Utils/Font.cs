using System.Collections.Generic;

namespace SartoxOS.Utils
{
    public class Font
    {
        private readonly RawStream RawFile;

        // header:
        // magic number: CFF - Custom Font File
        // Font name : string
        // font style : byte
        // font size : short
        // charcounts: int

        // body
        // charindex []
        // charcode asci
        // char Width
        // char Height
        // short data lenth
        // char data.

        public string Name { get; set; }

        public byte Style { get; set; }

        public short Size { get; set; }

        public int CharsCount { get; set; }

        public List<byte[]> Data { get; set; } = new List<byte[]>();

        public List<char> Char { get; set; } = new List<char>();

        public List<byte> Width { get; set; } = new List<byte>();

        public List<byte> Height { get; set; } = new List<byte>();

        public Font() { }

        public Font(byte[] File)
        {
            RawFile = new RawStream(File);
            DeserializeFile();
        }

        private void DeserializeFile()
        {
            RawFile._index = -1;
            RawFile.ReadString();
            Name = RawFile.ReadString();
            Style = RawFile.ReadByte();
            Size = (short)RawFile.ReadInt32();
            CharsCount = RawFile.ReadInt32();

            for (int i = 0; i < CharsCount; i++)
            {
                Char.Add((char)RawFile.ReadByte());
                Width.Add(RawFile.ReadByte());
                Height.Add(RawFile.ReadByte());
                int l = RawFile.ReadInt32();
                List<byte> data = new List<byte>();
                for (int dat = 0; dat < l; dat++) data.Add(RawFile.ReadByte());
                Data.Add(data.ToArray());
            }
        }
    }
}
