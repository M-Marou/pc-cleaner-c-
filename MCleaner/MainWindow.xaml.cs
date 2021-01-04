using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public void filesizes()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp");
            FileInfo[] files = di.GetFiles("*", SearchOption.AllDirectories);

            long size = 0;
            if (files.Length == 0)
            {
                CurrentFiles.Text = "Your PC is clean";
            }
            else
            {
                foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
                {
                    size += fi.Length;
                    CurrentFiles.Text = ($"{size / 1000000} Mb Has found to clear.");
                }
            }
        }

        public void readDate()
        {
            String line;
            StreamReader sr = new StreamReader(@"C:\Users\Youcode\Desktop\Dates.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                LastAnalyse.Text = line;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            filesizes();
            readDate();


            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://pastebin.com/raw/ni7HMv6F").Contains("version 1.4.4"))
                {
                    //MessageBox.Show("update now");
                    if (MessageBox.Show("Looks like there is an update! Do you want to download it?", "Demo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        using (var client = new WebClient())
                        {
                            Process.Start("Updater.exe");
                            this.Close();
                        }
                }
            }
            catch
            {

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Analyszer_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "Analyse en cours :";
            CurrentFiles.Visibility = Visibility.Visible;
            //LastAnalyse.Visibility = Visibility.Visible;
            ProgB.Visibility = Visibility.Visible;
            pers.Visibility = Visibility.Visible;

            //LastAnalyse.Text = DateTime.Now.ToLongDateString();

            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgB.Value = e.ProgressPercentage;
            CurrentFiles.Text = (string)e.UserState;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp");
            FileInfo[] fi = di.GetFiles("*", SearchOption.AllDirectories);

            int count = fi.Length;
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0, string.Format("Processing file 1..."));

            //double result = 100 / count;

            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
                worker.ReportProgress((int)(double)((i + 1) * 1.923076923076923), string.Format("Processing file {0}...", i + 2));
            }
            worker.ReportProgress(100, "Done Processing.");
        }

        public void resets()
        {
            filesizes();
            ProgB.Value = 0;
            //CurrentFiles.Text = "";
            ProgB.Visibility = Visibility.Hidden;
            pers.Visibility = Visibility.Hidden;
            title.Text = "Analyse du PC nécessaire";

            //LastAnalyse.Text = DateTime.Now.ToLongDateString();
            //LastAnalyseTime.Text = DateTime.Now.ToLongTimeString();
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp");
            long size = 0;
            foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
            {
                size += fi.Length;
            }
            double result = size / 1000000;
            File.AppendAllText(@"C:\Users\Youcode\Desktop\Dates.txt", DateTime.Now.ToString() + " " + result.ToString() + " MBs" + Environment.NewLine);
            readDate();
            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp");
            FileInfo[] fi = di.GetFiles("*", SearchOption.AllDirectories);
            if (fi.Length == 0)
            {
                MessageBox.Show("no files to clear", "Analyszing result");
                CurrentFiles.Text = "Your PC is clean";
                resets();
             
            } else { 
            if (MessageBox.Show("would you like to clean the files", "Analyszing result", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                        foreach (FileInfo file in di.GetFiles())
                        {
                        try
                        {
                            file.Delete();
                        }
                        catch (Exception)
                        {

                        }
                            
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch (Exception)
                        {

                        }
                            
                        }
                        resets();

            } else  {
                    resets();
                    }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Nettoyer_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Windows\Temp");
            FileInfo[] fi = di.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {

                }

            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch (Exception)
                {

                }

            }

            if (fi.Length == 0)
            {
                CurrentFiles.Text = "Your PC is clean";
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            MBwindow History = new MBwindow();
            History.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
