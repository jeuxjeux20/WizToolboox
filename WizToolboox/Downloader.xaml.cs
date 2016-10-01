﻿using System;
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
    /// Logique d'interaction pour Downloader.xaml
    /// </summary>
    public partial class Downloader : Window
    {
        private string filetype;
        private HttpResponseMessage response;
        System.Net.Http.HttpClient client = new HttpClient();
        WebClient wc = new WebClient();
        private string urlF;
        public Downloader(string url)
        {
            InitializeComponent();
            progress = new Progress<int>(value => omgProg.Value = value);
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
            urlF = AppDomain.CurrentDomain.BaseDirectory + DateTime.UtcNow.Ticks + $".{thisThingy}";
            
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