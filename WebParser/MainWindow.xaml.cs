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
using WebParser.Core;
using WebParser.Core.Habra;

namespace WebParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebParserWorker<string[]> webParser;
        public MainWindow()
        {
            InitializeComponent();
            webParser = new WebParserWorker<string[]>(new HabraParser());

            webParser.OnCompleted += WebParser_OnCompleted;
            webParser.OnNewData += WebParser_OnNewData;
        }

        private void WebParser_OnNewData(object arg1, string[] arg2)
        {
            for (int i = 0; i < arg2.Length; i++)
            {
                lbTitles.Items.Add(arg2[i]);
            }
            
        }

        private void WebParser_OnCompleted(object obj)
        {
            MessageBox.Show("Well done!");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int startPage = Int32.Parse(tbStart.Text);
            int endPage = Int32.Parse(tbAbort.Text);

            webParser.ParserSettings = new HabraSettings(startPage, endPage);

            webParser.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webParser.Abort();
        }
    }
}
