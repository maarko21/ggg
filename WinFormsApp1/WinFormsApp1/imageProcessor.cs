using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WinFormsApp1
{
    public static class ImageProcessor
    {
        public static unsafe Bitmap ApplySharpen(Bitmap original)
        {
            int width = original.Width;
            int height = original.Height;
            Bitmap sharpened = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            int[,] kernel = {
                {  0, -1,  0 },
                { -1,  5, -1 },
                {  0, -1,  0 }
            };

            int kernelSize = 3;

            BitmapData originalData = original.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData resultData = sharpened.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int stride = originalData.Stride;
            byte* origPtr = (byte*)originalData.Scan0;
            byte* resPtr = (byte*)resultData.Scan0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int[] rgb = new int[3];

                    for (int ky = 0; ky < kernelSize; ky++)
                    {
                        int ny = y + ky - 1;
                        ny = Math.Clamp(ny, 0, height - 1);

                        for (int kx = 0; kx < kernelSize; kx++)
                        {
                            int nx = x + kx - 1;
                            nx = Math.Clamp(nx, 0, width - 1);

                            byte* p = origPtr + ny * stride + nx * 3;
                            int kernelVal = kernel[ky, kx];

                            rgb[0] += p[0] * kernelVal; 
                            rgb[1] += p[1] * kernelVal; 
                            rgb[2] += p[2] * kernelVal; 
                        }
                    }

                    byte* destPixel = resPtr + y * stride + x * 3;

                    destPixel[0] = (byte)Math.Clamp(rgb[0], 0, 255);
                    destPixel[1] = (byte)Math.Clamp(rgb[1], 0, 255);
                    destPixel[2] = (byte)Math.Clamp(rgb[2], 0, 255);
                }
            }

            original.UnlockBits(originalData);
            sharpened.UnlockBits(resultData);

            return sharpened;
        }

        public static unsafe bool AdjustColorTemperature(Bitmap b, int temperature)
        {
            if (temperature < 1000 || temperature > 40000) return false;

            double t = temperature / 100.0;

            double r, g, bComp;

            if (t <= 66) r = 255;
            else r = 329.698727446 * Math.Pow(t - 60, -0.1332047592);
            r = Math.Clamp(r, 0, 255);

            if (t <= 66) g = 99.4708025861 * Math.Log(t) - 161.1195681661;
            else g = 288.1221695283 * Math.Pow(t - 60, -0.0755148492);
            g = Math.Clamp(g, 0, 255);

            if (t >= 66) bComp = 255;
            else if (t <= 19) bComp = 0;
            else bComp = Math.Clamp(138.5177312231 * Math.Log(t - 10) - 305.0447927307, 0, 255);

            double redFactor = r / 255.0;
            double greenFactor = g / 255.0;
            double blueFactor = bComp / 255.0;

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            byte* scan0 = (byte*)bmData.Scan0.ToPointer();

            for (int y = 0; y < b.Height; ++y)
            {
                byte* row = scan0 + y * stride;
                for (int x = 0; x < b.Width; ++x)
                {
                    int index = x * 3;
                    row[index + 2] = (byte)Math.Clamp(row[index + 2] * redFactor, 0, 255);
                    row[index + 1] = (byte)Math.Clamp(row[index + 1] * greenFactor, 0, 255);
                    row[index + 0] = (byte)Math.Clamp(row[index + 0] * blueFactor, 0, 255);
                }
            }

            b.UnlockBits(bmData);
            return true;
        }

        public static unsafe bool ApplyGamma(Bitmap b, double red, double green, double blue)
        {
            if (red < 0.2 || red > 5) return false;
            if (green < 0.2 || green > 5) return false;
            if (blue < 0.2 || blue > 5) return false;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)(255.0 * Math.Pow(i / 255.0, 1.0 / red) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)(255.0 * Math.Pow(i / 255.0, 1.0 / green) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)(255.0 * Math.Pow(i / 255.0, 1.0 / blue) + 0.5));
            }

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            byte* scan0 = (byte*)bmData.Scan0.ToPointer();

            for (int y = 0; y < b.Height; ++y)
            {
                byte* row = scan0 + y * stride;
                for (int x = 0; x < b.Width; ++x)
                {
                    row[x * 3 + 2] = redGamma[row[x * 3 + 2]];
                    row[x * 3 + 1] = greenGamma[row[x * 3 + 1]];
                    row[x * 3 + 0] = blueGamma[row[x * 3 + 0]];
                }
            }

            b.UnlockBits(bmData);
            return true;
        }

        public static void SaveAsMarkoFormat(Bitmap bitmap, string path)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[,] Y = new byte[height, width];
            int halfWidth = (width + 1) / 2;
            int halfHeight = (height + 1) / 2;
            byte[,] U = new byte[halfHeight, halfWidth];
            byte[,] V = new byte[halfHeight, halfWidth];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    byte r = c.R, g = c.G, b = c.B;
                    Y[y, x] = (byte)Math.Clamp(0.299 * r + 0.587 * g + 0.114 * b, 0, 255);
                }
            }

            for (int y = 0; y < halfHeight; y++)
            {
                for (int x = 0; x < halfWidth; x++)
                {
                    int srcX = x * 2;
                    int srcY = y * 2;

                    int sumU = 0, sumV = 0, count = 0;

                    for (int dy = 0; dy < 2; dy++)
                    {
                        for (int dx = 0; dx < 2; dx++)
                        {
                            int px = srcX + dx;
                            int py = srcY + dy;
                            if (px < width && py < height)
                            {
                                Color c = bitmap.GetPixel(px, py);
                                byte r = c.R, g = c.G, b = c.B;

                                int Uval = (int)Math.Clamp(-0.169 * r - 0.331 * g + 0.5 * b + 128, 0, 255);
                                int Vval = (int)Math.Clamp(0.5 * r - 0.419 * g - 0.081 * b + 128, 0, 255);

                                sumU += Uval;
                                sumV += Vval;
                                count++;
                            }
                        }
                    }

                    U[y, x] = (byte)(sumU / count);
                    V[y, x] = (byte)(sumV / count);
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(width);
                writer.Write(height);

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        writer.Write(Y[y, x]);

                for (int y = 0; y < halfHeight; y++)
                    for (int x = 0; x < halfWidth; x++)
                        writer.Write(U[y, x]);

                for (int y = 0; y < halfHeight; y++)
                    for (int x = 0; x < halfWidth; x++)
                        writer.Write(V[y, x]);
            }
        }

        public static Bitmap LoadFromMarkoFormat(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();

                byte[,] Y = new byte[height, width];
                int halfWidth = (width + 1) / 2;
                int halfHeight = (height + 1) / 2;
                byte[,] U = new byte[halfHeight, halfWidth];
                byte[,] V = new byte[halfHeight, halfWidth];

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        Y[y, x] = reader.ReadByte();

                for (int y = 0; y < halfHeight; y++)
                    for (int x = 0; x < halfWidth; x++)
                        U[y, x] = reader.ReadByte();

                for (int y = 0; y < halfHeight; y++)
                    for (int x = 0; x < halfWidth; x++)
                        V[y, x] = reader.ReadByte();

                byte[,] fullU = new byte[height, width];
                byte[,] fullV = new byte[height, width];

                for (int y = 0; y < height; y++)
                {
                    int uy = y / 2;
                    for (int x = 0; x < width; x++)
                    {
                        int ux = x / 2;
                        fullU[y, x] = U[uy, ux];
                        fullV[y, x] = V[uy, ux];
                    }
                }

                Bitmap bmp = new Bitmap(width, height);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int yy = Y[y, x];
                        int uu = fullU[y, x] - 128;
                        int vv = fullV[y, x] - 128;

                        int r = (int)Math.Clamp(yy + 1.402 * vv, 0, 255);
                        int g = (int)Math.Clamp(yy - 0.344136 * uu - 0.714136 * vv, 0, 255);
                        int b = (int)Math.Clamp(yy + 1.772 * uu, 0, 255);

                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                return bmp;
            }
        }
    }
}
