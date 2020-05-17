using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using SnakeGame.Views;

namespace SnakeGame.Models
{
    public class BackgroundImage
    {
        private PixabayApi pixabay = new PixabayApi();

        public BackgroundImage() {}

        public void setNewBackgroundImage(Window window)
        {
            PixabayRequest data = pixabay.GetDataFromApi("snake");

            Random random = new Random();
            var imageNumber = random.Next(0, (int)data.TotalHits);

            PixabayRequest request = pixabay.GetDataFromApi("snake", ((imageNumber / 20) + 1), 20);

            var jpgFileName = $"{request.Hits[(int) imageNumber % 20].Id}.jpg";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(request.Hits[(int)imageNumber%20].LargeImageUrl, jpgFileName);
            }

            window.Dispatcher.Invoke(() =>
            {
                window.Background = new ImageBrush(
                    new BitmapImage(new Uri(jpgFileName, UriKind.Relative)));
            });
        }
    }
}
