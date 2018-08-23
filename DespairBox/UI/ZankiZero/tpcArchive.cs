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

namespace DespairBox.UI.ZankiZero
{
    public partial class tpcArchive : Form
    {
        public tpcArchive()
        {
            InitializeComponent();
        }

        BinaryReader tpcArchiveBR;
        int[] FileLocation;
        int[] FileSize;
        string[] FileName;
        long TablePos;

        private void tpcArchive_Load(object sender, EventArgs e)
        {
            FileStream tpcFile = new FileStream(Program.FilePath, FileMode.Open);
            tpcArchiveBR = new BinaryReader(tpcFile);
            tpcArchiveBR.BaseStream.Position = 4;
            int FileCount = tpcArchiveBR.ReadInt32();
            int DataStartPosition = tpcArchiveBR.ReadInt32();
            int FileDataTablePosition = tpcArchiveBR.ReadInt32();
            tpcArchiveBR.BaseStream.Position = FileDataTablePosition;

            TablePos = tpcArchiveBR.BaseStream.Position;

            FileLocation = new int[FileCount];
            FileSize = new int[FileCount];
            FileName = new string[FileCount];


            for (int i = 0; i < FileCount; i++)
            {
                StringBuilder FileNameSB = new StringBuilder();

                int dummy = tpcArchiveBR.ReadByte();

                while (dummy != 0)
                {
                    tpcArchiveBR.BaseStream.Position -= 1;
                    char c = tpcArchiveBR.ReadChar();
                    FileNameSB.Append(c);
                    dummy = tpcArchiveBR.ReadByte();
                }

                tpcArchiveBR.BaseStream.Position = TablePos + 136;
                FileSize[i] = tpcArchiveBR.ReadInt32();

                if (i != 0)
                {
                    FileLocation[i] = FileLocation[i - 1] + FileSize[i - 1];
                }
                else
                {
                    FileLocation[i] = DataStartPosition;
                }
                FileName[i] = FileNameSB.ToString();
                treeView1.Nodes.Add(FileName[i]);

            }





        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdialog = new SaveFileDialog();
            sfdialog.Filter = ("Extracted File (*" + Path.GetExtension(treeView1.SelectedNode.Text) + ")|*." + Path.GetExtension(treeView1.SelectedNode.Text) + "|All files (*.*)|*.*");
            sfdialog.ShowDialog();

            var FileCreation = File.Create(sfdialog.FileName);
            //FileCreation.Close();

            BinaryWriter NewFile = new BinaryWriter(FileCreation);

            tpcArchiveBR.BaseStream.Position = FileLocation[treeView1.SelectedNode.Index];
            NewFile.Write(tpcArchiveBR.ReadBytes(FileSize[treeView1.SelectedNode.Index]));

            NewFile.Close();
            FileCreation.Close();

            MessageBox.Show("File Extracted!");



        }
    }
}
