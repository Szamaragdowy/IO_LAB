using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Asyn_Read_Bitmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BMP (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                Image_Bitmap.Source = new BitmapImage(new Uri(path));
            }
        }
        public Task<Bitmap> LoadBitmapAsync(string path)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentException("Path is empty!");
                }
                System.Threading.Thread.Sleep(5000);

                return new Bitmap(path);
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BMP (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Image_Bitmap.Image = await bmpController.LoadBitmapAsync(PathTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}
