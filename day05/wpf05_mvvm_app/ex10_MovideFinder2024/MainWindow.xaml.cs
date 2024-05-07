using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;

namespace ex10_MovideFinder2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //await this.ShowMessageAsync("검색", "검색을 시작합니다!");
            if (string.IsNullOrEmpty(TxtMovieName.Text))
            {
                await this.ShowMessageAsync("검색", "검색할 영화명을 입력하세요!!");
                return;
            }

            SearchMovie(TxtMovieName.Text);
        }
        private async void SearchMovie(string movieName)
        {
            string tmdb_apiKey = "df6d9244cc4d096998b241389e04b93e"; // TMDB에서 받아온 API키값
            string encoding_movieName = HttpUtility.UrlEncode(movieName, Encoding.UTF8);
            string openApiUri = $"https://api.themoviedb.org/3/search/movie?api_key={tmdb_apiKey}" +
                                $"&language=ko-KR&page=1&include_adult=false&query={encoding_movieName}";
            //Debug.WriteLine(openApiUri);

            string result = string.Empty; //결과값

            // openapi 실행 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                // tmdb api 요청
                req = WebRequest.Create(openApiUri); // URL을 넣어서 객체를 생성
                res = await req.GetResponseAsync(); // 요청한 URL의 결과를 비동기응답
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd(); // json결과를 문자열로 저장

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TODO : 메세지박스로 출력
            }
            finally
            {

            }
        }

        private async void TxtMovieName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private async void GrdResult_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            await this.ShowMessageAsync("포스터", "포스터처리합니다.");
        }

        private async void BtnAddFavorite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 추가합니다.");
        }

        private async void BtnWatchFavorite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 확인합니다.");
        }

        private async void BtnDelFavorite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "즐겨찾기 삭제합니다");

        }

        private async void BtnViewFavorite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("즐겨찾기", "예고편 확인합니다");

        }
    }
}