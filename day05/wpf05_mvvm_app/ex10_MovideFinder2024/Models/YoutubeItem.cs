using System.Windows.Media.Imaging;

namespace ex10_MovideFinder2024.Models
{
    public class YoutubeItem
    {
        public string Title {  get; set; }
        public string Autor { get; set; }
        public string ChannelTitle {  get; set; }
        public string URL {  get; set; }
        public BitmapImage Thumbnail { get; set; }

    }
}
