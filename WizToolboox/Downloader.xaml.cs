using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// A useless class xd <![CDATA[idk]]>
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// This function uses the current date ; and make it acceptable for a file name
        /// </summary>
        /// <returns>A formatted file name with unvalid symbols replaced with an underscore</returns>
        public static string TimeToFile()
        {
            //var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            //return timeSpan.TotalSeconds.ToString();
            string stringy = DateTime.Today.ToShortTimeString() + DateTime.Today.ToShortDateString();
            string formatted = string.Empty;
            char preced = 'k';
            foreach (var item in stringy.ToCharArray().ToList())
            {
                if (char.IsLetterOrDigit(item))
                {
                    formatted += item;
                }
                else if (preced != '_')
                {
                    formatted += '_';
                }
                preced = item;
            }
            return formatted;
        }
    }

    public partial class Downloader : Window
    {
        private string filetype;
        private HttpResponseMessage response;
        HttpClient client = new HttpClient();
        WebClient wc = new WebClient();
        private string urlF;
        public Downloader(string url, string filePath)
        {

            InitializeComponent();
            fromText.Text = "Downloading from " + url;
            progress = new Progress<int>(value => { omgProg.Value = value; omgProg.IsIndeterminate = false; });
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
            init2(url);
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
            ContentRendered += Downloader_ContentRendered;
        }

        private void Downloader_ContentRendered(object sender, EventArgs e)
        {
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
        }

        private async Task init2(string url)
        {
            await Task.Run(() => init(url));
        }
        private void init(string url)
        {


            var thisThingy = test(url).Result;
            urlF = Down.filePath + @"/" + Utilities.TimeToFile() + $".{thisThingy}";

            wc.DownloadFileAsync(new Uri(url),
            urlF);


        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show($"complete : {urlF}");
            wc = new WebClient();
        }

        IProgress<int> progress = null;

        private async Task<string> test(string url)
        {
            response = await client.GetAsync(url);
            filetype = response.Content.Headers.ContentType.MediaType;
            return filetype.Substring(filetype.IndexOf('/') + 1);
        }
        
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Report(e.ProgressPercentage);
            Application.Current.Dispatcher.Invoke(() => okek.Content = $"{e.BytesReceived}/{e.TotalBytesToReceive} Bytes");
        }


    }
}
