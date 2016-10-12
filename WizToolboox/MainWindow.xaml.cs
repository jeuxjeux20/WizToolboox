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
            Loaded += MainWindow_Loaded;
            
        }
        private AboutWindow aboutOpened;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Closing += MainWindow_Closing;
            if (DateTime.Today.Day == 12 && DateTime.Today.Month == 10)
            {
                new XezBirthday().Show();
            }
        }

        private List<Down> WindowsDown = new List<Down>();
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var item in WindowsDown)
            {
                item.Close();
            }

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
                try
                {
                    fileDialog.InitialDirectory = (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                }
                catch (Exception)
                {
                    fileDialog.InitialDirectory = (Environment.GetFolderPath(Environment.SpecialFolder.MyComputer));
                }
            }

            if (fileDialog.ShowDialog() == true)
            {
                using (var fileRead = new StreamReader(fileDialog.OpenFile()))
                {

                    if (fileRead.ReadToEnd().ToLower().Contains("rd") || fileRead.ReadToEnd().ToLower().Contains("rmdir"))
                    {
                        MessageBox.Show("The file is suspicious... it contains rd", "Suspicious", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        if (fileRead.ReadToEnd().ToLower().Contains("C:"))
                            if (!fileRead.ReadToEnd().ToLower().Contains("C:/ /s /q"))
                                MessageBox.Show("It contains C:/, but bit /s or /q.", "A little bit worse than expected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            else
                                MessageBox.Show("The files also contains C:/, it MAY destruct your pc", "Much worse than expected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                    if (System.Text.RegularExpressions.Regex.IsMatch(fileRead.ReadToEnd().ToString(), "((?!::).*^(echo ([>]*.(con|aux|nul))))", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        MessageBox.Show("It also seems to contain some con,aux,nul writing.", "con/aux/nul overwrite", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("This file seems to be fine", "Fine", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    fileRead.Close();
                }

            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var downloadContext = new Down();
            WindowsDown.Add(downloadContext);
            downloadContext.Show();
        }

        private void aboutMenuClick(object sender, RoutedEventArgs e)
        {
            aboutOpened = new AboutWindow();
            aboutOpened.Show();
        }
    }
}
