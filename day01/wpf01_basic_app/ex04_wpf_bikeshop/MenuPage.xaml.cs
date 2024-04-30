﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex04_wpf_bikeshop
{
    /// <summary>
    /// MenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            // 메뉴클릭시 화면전환, NavigationService & Frame 컨트롤
            NavigationService.Navigate(new Uri("/ProductPage.xaml", 
                UriKind.RelativeOrAbsolute));
        }

        private void BtnLiveSupport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SupportPage.xaml", 
                UriKind.RelativeOrAbsolute));
        }

        private void BtnEmailSupport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ContactPage.xaml", 
                UriKind.RelativeOrAbsolute));
        }
    }
}
