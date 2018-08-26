using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace DespairBox
{

    public partial class DespairBox : Form
    {
        public DespairBox()
        {
            InitializeComponent();
        }

        private void OpenFile()
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.ShowDialog();
            if (ofDialog.FileName == "")
            {
                MessageBox.Show("Error, no file selected.");
            }
            else
            {

                FileStream FileDetection = new FileStream(ofDialog.FileName, FileMode.Open);
                BinaryReader FileDetectionBR = new BinaryReader(FileDetection);

                int HeaderMagic = FileDetectionBR.ReadInt32();

                switch (HeaderMagic)
                {
                    case 1380009793:
                        Program.FilePath = ofDialog.FileName;
                        FileDetectionBR.Close();
                        FileDetection.Close();
                        UI.WADArchiveViewer wadviewer;
                        wadviewer = new UI.WADArchiveViewer();
                        wadviewer.MdiParent = this;
                        Program.FileName = Path.GetFileName(ofDialog.FileName);
                        wadviewer.Show();
                        break;

                    case 1953066581:
                        Program.FilePath = ofDialog.FileName;
                        Program.FileName = Path.GetFileName(ofDialog.FileName);

                        FileDetectionBR.Close();
                        FileDetection.Close();

                        UI.HopesPeak.Mobile.binArchiveViewer unityarchive = new UI.HopesPeak.Mobile.binArchiveViewer(); unityarchive.MdiParent = this; unityarchive.Show();
                        break;

                    case 1178750514:
                            Program.FilePath = ofDialog.FileName;
                            Program.FileName = Path.GetFileName(ofDialog.FileName);

                            FileDetectionBR.Close();
                            FileDetection.Close();

                            UI.ZankiZero.tpcArchive tpcarchive = new UI.ZankiZero.tpcArchive(); tpcarchive.MdiParent = this; tpcarchive.Show();
                            break;

                    case 9460301:
                        Program.FilePath = ofDialog.FileName;
                        FileDetectionBR.Close();
                        FileDetection.Close();
                        Program.FileName = Path.GetFileName(ofDialog.FileName);
                        UI.exePatcher exePatcher;
                        if (Path.GetFileName(ofDialog.FileName) != "DR2_us.exe")
                        {
                            MessageBox.Show("Error: Not a valid Danganronpa 1/2 executable.");
                        }
                        exePatcher = new UI.exePatcher();
                        exePatcher.MdiParent = this;
                        exePatcher.Show();

                        break;
                    default:
                        //Checks if Loop File
                        if (FileDetectionBR.BaseStream.Length == 12)
                        {
                            FileDetectionBR.Close();
                            FileDetection.Close();
                            Program.FileName = Path.GetFileName(ofDialog.FileName);
                            Program.FilePath = ofDialog.FileName;
                            Console.WriteLine(Program.FilePath);
                            UI.BGMLoopViewer bgmloopeditor;

                            bgmloopeditor = new UI.BGMLoopViewer();
                            bgmloopeditor.MdiParent = this;
                            Program.FileName = Path.GetFileName(ofDialog.FileName);
                            bgmloopeditor.Show();


                        }

                        else
                        {
                            if (Path.GetExtension(ofDialog.FileName) == ".pak")
                            {
                                FileDetectionBR.Close();
                                FileDetection.Close();
                                MessageBox.Show(".pak editing not implemented.");
                            }
                            if (Path.GetExtension(ofDialog.FileName) == ".srdv")
                            {
                                Program.FilePath = ofDialog.FileName;
                                Program.FileName = Path.GetFileName(ofDialog.FileName);

                                FileDetectionBR.Close();
                                FileDetection.Close();
                                
                                UI.V3.SRDVViewer SRDViewer = new UI.V3.SRDVViewer(); SRDViewer.MdiParent = this; SRDViewer.Show();
                            }
                            else
                            {
                                FileDetectionBR.Close();
                                FileDetection.Close();
                                MessageBox.Show("Unsupported file.");
                            }
                        }
                        break;

                }
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void loopToolStripMenuItem_Click(object sender, EventArgs e)
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
                    BinaryWriter loopCreator = new BinaryWriter(fs);
                    loopCreator.Write(00000001);
                    loopCreator.Write(00000000);
                    loopCreator.Write(00000000);
                    loopCreator.Close();
                    fs.Close();
                }

                Program.FileName = Path.GetFileName(sfDialog.FileName);
                Program.FilePath = sfDialog.FileName;
                UI.BGMLoopViewer bgmloopeditor;
                bgmloopeditor = new UI.BGMLoopViewer();
                bgmloopeditor.MdiParent = this;
                Program.FileName = Path.GetFileName(sfDialog.FileName);
                bgmloopeditor.Show();
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void DespairBox_Load(object sender, EventArgs e)
        {

        }
    }
}