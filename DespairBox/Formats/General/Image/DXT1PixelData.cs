using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace DespairBox.Formats.General.Image
{
    class DXT1PixelData
    {

        public static Bitmap ConvertedImage;

        public static void read(int width, int height, Stream DXT1Data)
        {
            
            Stream processedData = DXT1Data;
            ConvertedImage = new Bitmap(width, height);
            for (int supposedIndex = 0; supposedIndex < ((height * width) / 16); supposedIndex++)
            {
                Color[] texelPalette = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Black };
                int[] colorBytes = new int[] { ((processedData.ReadByte()) | (processedData.ReadByte() << 8)), ((processedData.ReadByte()) | (processedData.ReadByte() << 8)) };
                int indices = processedData.ReadByte() | (processedData.ReadByte() << 8) | (processedData.ReadByte() << 16) | (processedData.ReadByte() << 24);
                

                int i = 0;
                while (i < 2)
                {
                    int rgb565 = colorBytes[i];

                    int r = (rgb565 & 0xF800) >> 8;
                    int g = (rgb565 & 0x7E0) >> 3;
                    int b = (rgb565 & 0x1F) << 3;

                    texelPalette[i] = Color.FromArgb(r | (r >> 5), g | (g >> 6), b | (b >> 5));
                    i++;
                }

                    if (colorBytes[0] > colorBytes[1])
                    {

                        texelPalette[2] = Color.FromArgb(
                                (2 * texelPalette[0].R + 1 * texelPalette[1].R) / 3,
                                (2 * texelPalette[0].G + 1 * texelPalette[1].G) / 3,
                                (2 * texelPalette[0].B + 1 * texelPalette[1].B) / 3);

                        texelPalette[3] = Color.FromArgb(
                        (1 * texelPalette[0].R + 2 * texelPalette[1].R) / 3,
                        (1 * texelPalette[0].G + 2 * texelPalette[1].G) / 3,
                        (1 * texelPalette[0].B + 2 * texelPalette[1].B) / 3);
                    }
                else
                {
                        texelPalette[2] = Color.FromArgb((texelPalette[0].R + texelPalette[1].R) / 2, (texelPalette[0].G + texelPalette[1].G) / 2, (texelPalette[0].B + texelPalette[1].B) / 2);
                        texelPalette[3] = Color.FromArgb(0, 0, 0, 0);
                }
                for (int index = 0; index < 16; index++)
                    ConvertedImage.SetPixel((supposedIndex % (width / 4)) * 4 + (index % 4), (supposedIndex / (width / 4)) * 4 + (index / 4), texelPalette[3 & (indices >> (2 * index))]);
            }

            Console.WriteLine(processedData.Position);

        }
    }
}