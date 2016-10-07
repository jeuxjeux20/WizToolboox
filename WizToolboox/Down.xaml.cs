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
using System.Windows.Shapes;

namespace WizToolboox
{
    /// <summary>
    /// Logique d'interaction pour Down.xaml
    /// </summary>
    public partial class Down : Window
    {
        public Down()
        {
            InitializeComponent();
            textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.Length > 0)
                button.IsEnabled = true;
            else
                button.IsEnabled = false;        
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string url = textBox.Text;
            if (!(url.Contains("http://") || url.Contains("https://")))
            {
                url = $"http://{url}";
            }
            new Downloader(url).Show();           
        }
    }
}
