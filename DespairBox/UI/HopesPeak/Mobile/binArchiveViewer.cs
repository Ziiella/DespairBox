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

namespace DespairBox.UI.HopesPeak.Mobile
{
    public partial class binArchiveViewer : Form
    {
        public binArchiveViewer()
        {
            InitializeComponent();
        }




        BinaryReader binArchiveBR;
        long[] FileLocation;
        uint[] FileSize;
        string FNAME = Path.GetFileNameWithoutExtension(Program.FileName);
        int FCount;

        private void binArchiveViewer_Load(object sender, EventArgs e)
        {
            FileStream binFile = new FileStream(Program.FilePath, FileMode.Open);
            binArchiveBR = new BinaryReader(binFile);
            FileLocation = new long[6666];
            FileSize = new uint[6666];

            int i = 0;
            
            while (binArchiveBR.BaseStream.Position != binArchiveBR.BaseStream.Length)
            {

                FileLocation[i] = binArchiveBR.BaseStream.Position;
                binArchiveBR.BaseStream.Position += 27;


                var data = binArchiveBR.ReadBytes(4);
                Array.Reverse(data);
                FileSize[i] = BitConverter.ToUInt32(data, 0);

                binArchiveBR.BaseStream.Position = FileLocation[i] + FileSize[i];


                while (binArchiveBR.BaseStream.Position % 4 != 0)
                {
                    binArchiveBR.BaseStream.Position += 1;
                }


                if (binArchiveBR.BaseStream.Position != binArchiveBR.BaseStream.Length)
                {
                    int dummy = binArchiveBR.ReadInt32();

                    if (dummy != 0)
                    {
                        binArchiveBR.BaseStream.Position -= 4;
                    }
                }

                




                treeView1.Nodes.Add(i.ToString());
                //binArchiveBR.ReadInt32();

                i++;
            }

            FCount = i;


        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdialog = new SaveFileDialog();
            sfdialog.Filter = ("Unity3D assets file(*.assets)|*.assets|All files (*.*)|*.*");
            sfdialog.ShowDialog();


            if (sfdialog.FileName == "")
            {
                return;
            }
            else
            {
                var FileCreation = File.Create(sfdialog.FileName);
                //FileCreation.Close();

                BinaryWriter NewFile = new BinaryWriter(FileCreation);

                binArchiveBR.BaseStream.Position = FileLocation[treeView1.SelectedNode.Index];
                NewFile.Write(binArchiveBR.ReadBytes(Convert.ToInt32(FileSize[treeView1.SelectedNode.Index])));

                NewFile.Close();
                FileCreation.Close();

                MessageBox.Show("File Extracted!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();

            if (FBD.SelectedPath == "")
            {
                return;
            }
            else
            {

                Directory.CreateDirectory(FBD.SelectedPath + "/" + FNAME + "_Extracted");

                for (int i = 0; i < FCount; i++)
                {
                    var FileCreation = File.Create(FBD.SelectedPath + "/" + FNAME + "_Extracted/" + i + ".assets");
                    //FileCreation.Close();

                    BinaryWriter NewFile = new BinaryWriter(FileCreation);

                    binArchiveBR.BaseStream.Position = FileLocation[i];
                    NewFile.Write(binArchiveBR.ReadBytes(Convert.ToInt32(FileSize[i])));

                    NewFile.Close();
                    FileCreation.Close();
                }
                MessageBox.Show("Files Extracted!");

            }


        }
    }
}
