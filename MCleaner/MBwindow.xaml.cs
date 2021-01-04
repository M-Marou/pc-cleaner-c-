using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace MCleaner
{
    /// <summary>
    /// Interaction logic for MBwindow.xaml
    /// </summary>
    public partial class MBwindow : Window
    {


        public MBwindow()
        {
            InitializeComponent();
            String line;
            StreamReader sr = new StreamReader(@"C:\Users\Youcode\Desktop\Dates.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                lastDate.Text = line;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
