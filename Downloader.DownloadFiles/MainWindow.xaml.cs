using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.IO;

namespace Downloader.DownloadFiles
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

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            var val = saveFile.ShowDialog().Value;
            using (var client = new WebClient())
            {
                DownloadButton.IsEnabled = false;

                try
                {
                    var address = new Uri(UrlTextBox.Text);
                    Task<String> downloadTask = client.DownloadStringTaskAsync(address);
                    string result = await downloadTask;
                    if (val)
                    {
                        File.WriteAllText(saveFile.FileName, result);
                    }
                }
                catch (UriFormatException)
                {
                    MessageBox.Show("Invalid Uri");
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    DownloadButton.IsEnabled = true;
                }
            }
        }
    }
}
