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
    public partial class WADArchiveViewer : Form
    {
        public WADArchiveViewer()
        {
            InitializeComponent();
            
        }

        int[] FileNameLength;
        List<string> FNameList = new List<string>();
        string[] WadFileFileName;
        long[] FileSize;
        long[] FileLength;
        long FileStart;
        BinaryReader wadfileBR;


        private void WADArchiveViewer_Load(object sender, EventArgs e)
        {
            this.Text = "WAD Editor - " + Program.FileName;
            //Program.FileName = "";
                FileStream wadFile = new FileStream(Program.FilePath, FileMode.Open);
                wadfileBR = new BinaryReader(wadFile);
                wadfileBR.BaseStream.Position = 4;
                int MajorVersion = wadfileBR.ReadInt32();
                int MinorVersion = wadfileBR.ReadInt32();
                int HeaderSize = wadfileBR.ReadInt32();
                wadfileBR.BaseStream.Position += HeaderSize;
                int FileCount = wadfileBR.ReadInt32();
                FileNameLength = new int[FileCount];
                WadFileFileName = new string[FileCount];
                FileSize = new long[FileCount];
                FileLength = new long[FileCount];
            
            try
            {
                for (int i = 0; i < FileCount; i++)
                {
                    FileNameLength[i] = wadfileBR.ReadInt32();
                    //Console.WriteLine(FileNameLength[i]);
                    StringBuilder FileNameSB = new StringBuilder();

                    for (int d = 0; d < FileNameLength[i]; d++)
                    {
                        char c = wadfileBR.ReadChar();
                        FileNameSB.Append(c);
                    }

                    FNameList.Add(FileNameSB.ToString());
                    //Console.WriteLine(WadFileFileName[i]);


                    FileSize[i] = wadfileBR.ReadInt64();
                    FileLength[i] = wadfileBR.ReadInt64();

                    treeView1.Nodes.Add(FNameList[i]);



                }
                FileStart = wadfileBR.BaseStream.Position;
            }
            catch (NullReferenceException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(FileNameLength[treeView1.SelectedNode.Index]);
        }

        private void FileExtractButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.ShowDialog();
            if (sfDialog.FileName == "")
            {
                return;
            }
            else
            {
                using (FileStream fs = File.Create(sfDialog.FileName))
                {
                    int i = 0;
                    BinaryWriter FileExtraction = new BinaryWriter(fs);
                    wadfileBR.BaseStream.Position = FileStart + FileLength[treeView1.SelectedNode.Index];
                    FileExtraction.Write(wadfileBR.ReadBytes(i = (int)FileSize[treeView1.SelectedNode.Index]));
                    fs.Close();
                }
                wadfileBR.BaseStream.Position = FileStart;
            }
        }
    }
}
