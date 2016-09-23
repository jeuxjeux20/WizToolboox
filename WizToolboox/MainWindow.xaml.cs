using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WizToolboox
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Batch files (.bat)|*.bat|Everything (*.*)|*.*";

            try
            {
                fileDialog.InitialDirectory = (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads");
            }
            catch (Exception)
            {
                fileDialog.InitialDirectory = (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }
            if (fileDialog.ShowDialog() == true)
            {
                using (var fileRead = new StreamReader(fileDialog.OpenFile()))
                {

                    if (fileRead.ReadToEnd().ToLower().Contains("rd"))
                    {
                        MessageBox.Show("The file is suspicious... it contains rd", "Suspicious",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("This file seems to be fine", "Fine", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    fileRead.Close();
                }

            }
        }
    }
}
