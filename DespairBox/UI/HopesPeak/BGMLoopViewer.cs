using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DespairBox.UI
{
    public partial class BGMLoopViewer : Form
    {
        public BGMLoopViewer()
        {
            InitializeComponent();
        }

        int LoopingPoint;
        int LoopStartPos;
        string filelocation = Program.FilePath;

        private void BGMLoopViewer_Load_1(object sender, EventArgs e)
        {

            if (Program.FileName != "")
            {
                //Load the Window and File
                this.Text = ".loop Editor - " + Program.FileName;
                FileStream LoopFile = new FileStream(Program.FilePath, FileMode.Open);
                BinaryReader LoopFileBR = new BinaryReader(LoopFile);

                //
                LoopFileBR.BaseStream.Position = 4;
                LoopStartPos = LoopFileBR.ReadInt32();
                LoopingPoint = LoopFileBR.ReadInt32();
                LoopFileBR.Close();

                try
                {
                    LoopPointValue.Value = LoopingPoint;
                    LoopStartPositionValue.Value = LoopStartPos;
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {

                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }
            }
            else
            {
                MessageBox.Show("Loop creation not implemented.");
                this.Close();
            }
        }

        private void SaveLOOPButton_Click(object sender, EventArgs e)
        {
            BinaryWriter LoopFileWriter = new BinaryWriter(File.Open(filelocation, FileMode.Open));
            LoopFileWriter.BaseStream.Position = 4;
            LoopFileWriter.Write(LoopStartPos);
            LoopFileWriter.Write(LoopingPoint);
            LoopFileWriter.Close();
        }

        private void LoopPointValue_ValueChanged(object sender, EventArgs e)
        {
            LoopingPoint = Convert.ToInt32(LoopPointValue.Value);
        }

        private void LoopStartPositionValue_ValueChanged(object sender, EventArgs e)
        {
            LoopStartPos = Convert.ToInt32(LoopStartPositionValue.Value);
        }
    }
}
