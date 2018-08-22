using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DespairBox.Formats.Misc
{
    class BGMLoop
    {

        public static int LoopStartPos;
        public static int LoopingPoint;
        private static BinaryReader LoopFileBR;

        public static void LoadLoopFile(string LoopFilePath = "")
        {
            FileStream LoopFile = new FileStream(LoopFilePath, FileMode.Open);
            LoopFileBR = new BinaryReader(LoopFile);
        }

        public static void ReadValues()
        {
            LoopFileBR.BaseStream.Position = 4;
            LoopingPoint = LoopFileBR.ReadInt32();
            LoopStartPos = LoopFileBR.ReadInt32();
            Console.WriteLine(LoopingPoint);
            Console.WriteLine(LoopStartPos);
        }

    }
}
