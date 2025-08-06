using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Bitmap? currentImage;
        private Bitmap? loadedImage;

        private Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private Stack<Bitmap> redoStack = new Stack<Bitmap>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadedImage = new Bitmap(openFileDialog.FileName);
                    currentImage = new Bitmap(loadedImage);
                    undoStack.Clear();
                    redoStack.Clear();
                    pictureBox1.Image = currentImage;
                    Console.WriteLine($"Bitmap size in bytes: {GetBitmapSizeInBytes(currentImage)}");
                }
            }
        }

        private long GetBitmapSizeInBytes(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                return memoryStream.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            if (double.TryParse(textBox1.Text, out double redGamma) &&
                double.TryParse(textBox2.Text, out double greenGamma) &&
                double.TryParse(textBox3.Text, out double blueGamma))
            {
                if (redGamma < 0.2 || redGamma > 5.0 ||
                    greenGamma < 0.2 || greenGamma > 5.0 ||
                    blueGamma < 0.2 || blueGamma > 5.0)
                {
                    MessageBox.Show("Gamma values must be between 0.2 and 5.0.");
                    return;
                }

                PushToUndoStack();
                bool success = ImageProcessor.ApplyGamma(currentImage, redGamma, greenGamma, blueGamma);
                if (success)
                {
                    pictureBox1.Image = currentImage;
                    redoStack.Clear();
                    MessageBox.Show("Gamma correction applied successfully.");
                }
                else
                {
                    MessageBox.Show("Gamma application failed.");
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numeric gamma values.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            PushToUndoStack();
            Bitmap sharpenedImage = ImageProcessor.ApplySharpen(currentImage);
            currentImage = sharpenedImage;
            pictureBox1.Image = currentImage;
            redoStack.Clear();
            MessageBox.Show("Sharpen filter applied successfully.");
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(CloneBitmap(currentImage));
                currentImage = undoStack.Pop();
                pictureBox1.Image = currentImage;
            }
            else
            {
                MessageBox.Show("No more undo steps.");
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(CloneBitmap(currentImage));
                currentImage = redoStack.Pop();
                pictureBox1.Image = currentImage;
            }
            else
            {
                MessageBox.Show("No more redo steps.");
            }
        }

        private void PushToUndoStack()
        {
            if (currentImage != null)
            {
                undoStack.Push(CloneBitmap(currentImage));
            }
        }

        private Bitmap CloneBitmap(Bitmap source)
        {
            return new Bitmap(source); // shallow clone enough for pixel-level operations
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                currentImage = new Bitmap(loadedImage);
                pictureBox1.Image = currentImage;
            }
            else
            {
                MessageBox.Show("No original image to restore.");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Optional: save as bmp, jpeg, etc.
        }

        private void buttonACT_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            if (!int.TryParse(textBoxACT.Text, out int temperature))
            {
                MessageBox.Show("Please enter a valid number for temperature (1000 - 40000).");
                return;
            }

            if (temperature < 1000 || temperature > 40000)
            {
                MessageBox.Show("Temperature must be between 1000K and 40000K.");
                return;
            }

            PushToUndoStack();

            Bitmap adjustedImage = new Bitmap(currentImage);

            bool success = ImageProcessor.AdjustColorTemperature(adjustedImage, temperature);

            if (!success)
            {
                MessageBox.Show("Failed to apply color temperature adjustment.");
                return;
            }

            currentImage = adjustedImage;
            pictureBox1.Image = currentImage;
            redoStack.Clear();

            MessageBox.Show("Color temperature adjusted successfully.");
        }

        private void loadmarkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Marko Image Format (*.marko)|*.marko";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap loadedMarko = ImageProcessor.LoadFromMarkoFormat(openFileDialog.FileName);
                        loadedImage = new Bitmap(loadedMarko);
                        currentImage = new Bitmap(loadedMarko);
                        undoStack.Clear();
                        redoStack.Clear();
                        pictureBox1.Image = currentImage;
                        MessageBox.Show("Image loaded successfully from .marko format.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load .marko image: " + ex.Message);
                    }
                }
            }
        }

        private void savemarkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentImage == null)
            {
                MessageBox.Show("No image loaded to save.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Marko Image Format (*.marko)|*.marko";
                saveFileDialog.DefaultExt = "marko";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImageProcessor.SaveAsMarkoFormat(currentImage, saveFileDialog.FileName);
                        MessageBox.Show("Image saved successfully in .marko format.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to save image: " + ex.Message);
                    }
                }
            }
        }
    }
}
