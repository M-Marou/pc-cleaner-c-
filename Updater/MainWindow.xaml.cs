using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Net;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            WebClient webClient = new WebClient();
            var client = new WebClient();

            try
            {
                System.Threading.Thread.Sleep(5000);
                File.Delete(@".\MCleaner.exe");
                client.DownloadFile("http://download1652.mediafire.com/d7ku4kiiijwg/12fyfdbde6fqvwj/MCleaner.zip", @"C:\Users\Youcode\Desktop\PC Cleaner/MCleaner.zip");
                string zipPath = @".\MCleaner.zip";
                string extractPath = @".\";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                File.Delete(@".\MCleaner.zip");
                Process.Start(@".\MCleaner.exe");
                this.Close();
            }
            catch
            {
                Process.Start("MCleaner.exe");
                this.Close();
            }
        }
    }
}
