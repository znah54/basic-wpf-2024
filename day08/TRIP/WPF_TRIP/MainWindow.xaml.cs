using WPF_TRIP.Models;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using System.Data;


namespace WPF_TRIP
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
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitComboDateFromDB();
        }

        private void InitComboDateFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.tourinfo.GETDATE_QUERY, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet);
                List<string> saveDates = new List<string>();

                foreach (DataRow row in dSet.Tables[0].Rows)
                {
                    saveDates.Add(Convert.ToString(row["Save_Date"]));
                }
                BtnTourList.ItemsSource = saveDates;
            }
        }
        private async void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            String OpenApiUrl = "file:///C:/Users/user/Desktop/GimhaeTourism.html";
            string result = string.Empty;

            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(OpenApiUrl);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);



            var data = jsonResult["results"];
            var jsonArray = data as JArray; // json자체에서 []안에 들어간 배열데이터만 JArray 변환 가능

            var tourinfos = new List<tourinfo>();
            foreach (var item in jsonArray)
            {
                tourinfos.Add(new tourinfo()
                {
                    idx = Convert.ToInt32(item["idx"]),
                    name = Convert.ToString(item["name"]),
                    category = Convert.ToString(item["category"]),
                    copy = Convert.ToString(item["copy"]),
                    manage = Convert.ToString(item["manage"]),
                    phone = Convert.ToString(item["phone"]),
                    homepage = Convert.ToString(item["homepage"]),
                    content = Convert.ToString(item["content"]),
                    fee = Convert.ToString(item["fee"]),
                    usehour = Convert.ToString(item["usehour"]),
                    address = Convert.ToString(item["address"]),
                    xposition = Convert.ToDouble(item["xposition"]),
                    yposition = Convert.ToDouble(item["yposition"]),
                    parking = Convert.ToString(item["parking"]),
                    images = Convert.ToString(item["images"]),
                });
            }
            this.DataContext = tourinfos;
            //StsResult.Content = $"OpenAPI {tourinfo.Count}건 조회완료!";
        }

        private void BtnTourList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var curItem = GrdResult.SelectedItem as tourinfo;

            var mapWindow = new MapWindow(curItem.xposition, curItem.yposition);
            mapWindow.Owner = this;
            mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mapWindow.ShowDialog();
        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.Items.Count == 0)
            {
                await this.ShowMessageAsync("저장오류", "실시간 조회후 저장하십시오");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var insRes = 0;
                    foreach (tourinfo item in GrdResult.Items)
                    {
                        SqlCommand cmd = new SqlCommand(Models.tourinfo.INSERT_QUERY, conn);
                        cmd.Parameters.AddWithValue("@idx", item.idx);
                        cmd.Parameters.AddWithValue("@name", item.name);
                        cmd.Parameters.AddWithValue("@category", item.category);
                        cmd.Parameters.AddWithValue("@area", item.area);
                        cmd.Parameters.AddWithValue("@copy", item.copy);
                        cmd.Parameters.AddWithValue("@manage", item.manage);
                        cmd.Parameters.AddWithValue("@phone", item.phone);
                        cmd.Parameters.AddWithValue("@homepage", item.homepage);
                        cmd.Parameters.AddWithValue("@content", item.content);
                        cmd.Parameters.AddWithValue("@fee", item.fee);
                        cmd.Parameters.AddWithValue("@usehour", item.usehour);
                        cmd.Parameters.AddWithValue("@address", item.address);
                        cmd.Parameters.AddWithValue("@address", item.xposition);
                        cmd.Parameters.AddWithValue("@address", item.yposition);
                        cmd.Parameters.AddWithValue("@address", item.parking);
                        cmd.Parameters.AddWithValue("@address", item.images);


                        insRes += cmd.ExecuteNonQuery();
                    }
                    if (insRes > 0)
                    {
                        await this.ShowMessageAsync("저장", "DB저장성공!");
                        StsResult.Content = $"DB저장 {insRes}건 성공!";
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
            }
            InitComboDateFromDB();
        }
    }
}