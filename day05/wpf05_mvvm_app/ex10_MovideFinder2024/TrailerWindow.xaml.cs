using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ex10_MovideFinder2024.Models;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using MahApps.Metro.Controls;
using System.Diagnostics;

namespace ex10_MovieFinder2024
{
    /// <summary>
    /// TrailerWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TrailerWindow : MetroWindow
    {
        List<YoutubeItem> youtubeItems = null; // 유튜브API 검색결과 담을 객체리스트
        public TrailerWindow()
        {
            InitializeComponent();
        }

        // MainWindow 그리드에서 선택된 영화제목 넘기면서 생성
        // 재정의생성자
        public TrailerWindow(string movieName) : this()
        {
            // this() => TrailerWindow() 생성자를 먼저 실행한 뒤
            LblMovieName.Content = $"{movieName} 예고편";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            youtubeItems = new List<YoutubeItem>();
            SerachYoutubeApi(); // 핵시메서드 실행
        }
        private async void SerachYoutubeApi()
        {
            await LoadDataCollection();
            LsvResult.ItemsSource = youtubeItems;
        }

        private async Task LoadDataCollection()
        {
            // YoutubeService용 패키지
            var service = new YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyATGLZEJzuaZmuzOCPcyaoeD--rJRZrct4",
                    ApplicationName = this.GetType().ToString()
                }) ;
            var req = service.Search.List("snippet");
            req.Q = LblMovieName.Content.ToString(); // 어벤져스 인피니티워 예고편
            req.MaxResults = 10;

            var res = await req.ExecuteAsync();  // Youtube 서버에서 요청된값 실행하고 결과 리턴(비동기)

            //await this.ShowMessageAsync("검색결과", res.Items.Count.ToString());
            foreach(var item in res.Items)
            {
                if (item.Id.Kind.Equals("youtube#video")) // 동영상플레이 가능한 아이템만
                {
                    var youtube = new YoutubeItem()
                    {
                        Title = item.Snippet.Title,
                        ChannelTitle = item.Snippet.ChannelTitle,
                        URL = $"https://youtube.com/watch?v={item.Id.VideoId}", // 유트브 플레이링크
                        Autor = item.Snippet.ChannelId,
                    };

                    youtube.Thumbnail = new BitmapImage(new Uri(item.Snippet.Thumbnails.Default__.Url, UriKind.RelativeOrAbsolute));

                    youtubeItems.Add(youtube);
                }
            }
        }



        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 한번씩 CefSharp 브라우저에서 메모리 릭발생
            BrsYoutube.Address=string.Empty;
            BrsYoutube.Dispose();
        }

        private void LsvResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LsvResult.SelectedItem is YoutubeItem) // is => true/false
            {
                var video = LsvResult.SelectedItem as YoutubeItem; // as => casting 실패하면 null
                Debug.WriteLine(video.URL);
                BrsYoutube.Address = video.URL;
            }
        }
    }
}