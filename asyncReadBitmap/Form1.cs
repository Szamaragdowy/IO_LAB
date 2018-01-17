using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asyncReadBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = load_bmp();
            if (path != null)
            {
                pictureBox1.Image = new Bitmap(path);
            }
        }

        public Task<Bitmap> LoadBitmapAsync(string path)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException("Path == null");
                }
                System.Threading.Thread.Sleep(2000);

                return new Bitmap(path);
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string path = load_bmp();
            try
            {
                pictureBox1.Image = await LoadBitmapAsync(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public string load_bmp()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BMP (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
