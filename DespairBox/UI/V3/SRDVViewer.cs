using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DespairBox.UI.V3
{
    public partial class SRDVViewer : Form
    {
        public SRDVViewer()
        {
            InitializeComponent();
        }

        BinaryReader SRDVFileBR;
        BinaryReader SRDFileBR;
        Bitmap FinalImage;
        Bitmap PreviewImage;
        public static Stream processing;

        int unknown1;
        int swizzle;
        int displayWidth;
        int displayHeight;
        int scanline;
        int format;
        int unknown2;
        int palette;
        int paletteid;
        int transformedX;
        int transformedY;
        int bytespp;
        byte[] processingData;
        byte[] unswizzled;


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SRDVViewer_Load(object sender, EventArgs e)
        {
            //FileStream SRDVFile = new FileStream(Program.FilePath, FileMode.Open);

            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.ShowDialog();
            if (ofDialog.FileName == "")
            {
                MessageBox.Show("Error: No SRD file selected.");
                this.Close();
            }
            else
            {

                FileStream SRDFile = new FileStream(ofDialog.FileName, FileMode.Open);
                //SRDVFileBR = new BinaryReader(SRDVFile);
                SRDFileBR = new BinaryReader(SRDFile);

                int dummy = SRDFileBR.ReadInt32();
                while (dummy != 1381520420)
                {
                    dummy = SRDFileBR.ReadInt32();
                }

                //Read TXR Data
                SRDFileBR.BaseStream.Position += 12;
                unknown1 = SRDFileBR.ReadInt32();
                swizzle = SRDFileBR.ReadInt16();
                displayWidth = SRDFileBR.ReadInt16();
                displayHeight = SRDFileBR.ReadInt16();
                scanline = SRDFileBR.ReadInt16();
                format = SRDFileBR.ReadByte();
                unknown2 = SRDFileBR.ReadByte();
                palette = SRDFileBR.ReadByte();
                paletteid = SRDFileBR.ReadByte();

                HeightLabel.Text = "Height: " + displayHeight;
                WidthLabel.Text = "Width: " + displayWidth;


                Console.WriteLine(unknown1);
                Console.WriteLine(swizzle);
                Console.WriteLine(displayWidth);
                Console.WriteLine(displayHeight);
                Console.WriteLine(format);
                Console.WriteLine(unknown2);
                Console.WriteLine(paletteid);


                TXREntryReadTexture();

                FinalImage = Formats.General.Image.DXT1PixelData.ConvertedImage;

                PreviewImage = FinalImage;
                pictureBox1.Image = PreviewImage;

            }


        }

        private static int DecodeMorton2X(int code)
        {
            return Compact1By1(code >> 0);
        }

        private static int DecodeMorton2Y(int code)
        {
            return Compact1By1(code >> 1);
        }

        private static int Compact1By1(int x)
        {
            x &= 0x55555555;                    // x = -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
            x = (x ^ (x >> 1)) & 0x33333333;    // x = --fe --dc --ba --98 --76 --54 --32 --10
            x = (x ^ (x >> 2)) & 0x0f0f0f0f;    // x = ---- fedc ---- ba98 ---- 7654 ---- 3210
            x = (x ^ (x >> 4)) & 0x00ff00ff;    // x = ---- ---- fedc ba98 ---- ---- 7654 3210
            x = (x ^ (x >> 8)) & 0x0000ffff;    // x = ---- ---- ---- ---- fedc ba98 7654 3210
            return x;
        }



        private void GetPixelCoordinatesSwizzledVita(int origX, int origY, int width, int height)
        {
            // TODO: verify this is even sensible
            if (width == 0) width = 16;
            if (height == 0) height = 16;

            int i = (origY * width) + origX;

            int min = width < height ? width : height;
            int k = (int)Math.Log(min, 2);

            if (height < width)
            {
                // XXXyxyxyx → XXXxxxyyy
                int j = i >> (2 * k) << (2 * k)
                    | (DecodeMorton2Y(i) & (min - 1)) << k
                    | (DecodeMorton2X(i) & (min - 1)) << 0;
                transformedX = j / height;
                transformedY = j % height;
            }
            else
            {
                // YYYyxyxyx → YYYyyyxxx
                int j = i >> (2 * k) << (2 * k)
                    | (DecodeMorton2X(i) & (min - 1)) << k
                    | (DecodeMorton2Y(i) & (min - 1)) << 0;
                transformedX = j % width;
                transformedY = j / width;
            }

        }



        private void TXREntryReadTexture()
        {
            //FileStream texture = new FileStream(Program.FilePath, FileMode.Open);
            //BinaryReader textureBR = new BinaryReader(texture);

            int width = displayWidth;
            int height = displayHeight;

            //val swizzled = !(swizzle hasBitSet 1)
            if (format == 1 || format == 2 || format == 5 || format == 26)
            {

                switch (format)
                {
                    case 1:
                        bytespp = 4;
                        break;

                    case 2:
                        bytespp = 2;
                        break;

                    case 5:
                        bytespp = 2;
                        break;

                    case 26:
                        bytespp = 4;
                        break;

                    default:
                        bytespp = 2;
                        break;
                }

                if (swizzle == 1)
                {
                    processingData = File.ReadAllBytes(Program.FilePath);
                    Deswizzle(width / 4, height / 4, bytespp);
                    Stream processing = new MemoryStream(unswizzled);
                }
                else
                {
                    processingData = File.ReadAllBytes(Program.FilePath);
                    processing = new MemoryStream(File.ReadAllBytes(Program.FilePath));
                }



                //Next Part
                switch (format)
                {
                    case 0x01:

                        Bitmap resultingImage = new Bitmap(displayWidth, displayHeight);
                        for (int y = 0; y < width; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                int B = processing.ReadByte();
                                int G = processing.ReadByte();
                                int R = processing.ReadByte();
                                int A = processing.ReadByte();
                                resultingImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                            }
                        }
                        pictureBox1.Width = displayWidth;
                        pictureBox1.Height = displayHeight;
                        break;

                    default:
                        MessageBox.Show("Unimplemented format: " + format);
                        break;
                }

            }
            else if (format == 15 || format == 17 || format == 20 || format == 22 || format == 28)
            {

                switch (format)
                {
                    case 15:
                        bytespp = 8;
                        break;

                    case 28:
                        bytespp = 16;
                        break;

                    default:
                        bytespp = 8;
                        break;
                }

                if (width % 4 != 0)
                {
                    width += 4 - (width % 4);


                    if (height % 4 != 0)
                    {
                        height += 4 - (height % 4);

                    }

                }
                if ((swizzle == 1) && width >= 4 && height >= 4)
                {
                    processingData = File.ReadAllBytes(Program.FilePath);
                    Deswizzle(width / 4, height / 4, bytespp);
                    processing = new MemoryStream(processingData);
                }
                else
                {
                    processing = new MemoryStream(File.ReadAllBytes(Program.FilePath));
                }


                switch (format)
                {
                    case 15:
                        Formats.General.Image.DXT1PixelData.read(width, height, processing);
                        pictureBox1.Image = Formats.General.Image.DXT1PixelData.ConvertedImage;
                        return;

                    case 22:
                        MessageBox.Show("Unimplemented format: " + format);
                        return;

                    case 28:
                        MessageBox.Show("Unimplemented format: " + format);
                        return;

                    default:
                        MessageBox.Show("Unimplemented format: " + format);
                        return;
                }
            }
            else
            {
                Console.WriteLine("Other format: " + format);

                return;
            }
        }

        private void Deswizzle(int width, int height, int bytespp)
        {
            unswizzled = new Byte[processingData.Length];
            int min = Math.Min(width, height);
            int k = (int)Math.Log(min, 2);

            for (int i = 0; i < (width * height); i++)
            {
                int x;
                int y;

                if (height < width)
                {
                    int j = i >> (2 * k) << (2 * k) | (DecodeMorton2Y(i) & (min - 1)) << k | (DecodeMorton2X(i) & (min - 1)) << 0;
                    x = j / height;
                    y = j % height;
                }
                else
                {
                    int j = i >> (2 * k) << (2 * k) | (DecodeMorton2X(i) & (min - 1)) << k | (DecodeMorton2Y(i) & (min - 1)) << 0;
                    x = j % width;
                    y = j / width;
                }

                int p = ((y * width) + x) * bytespp;

                for (int l = 0; l < bytespp; l++)
                {
                    unswizzled[(p + l)] = processingData[(i * bytespp + l)];

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = "Images (*.png,*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            sfDialog.ShowDialog();
            if (sfDialog.FileName == "")
            {
                return;
            }
            else
            if (format == 15)
            {
                Formats.General.Image.DXT1PixelData.ConvertedImage.Save(sfDialog.FileName);
            }
            
        }
    }

}