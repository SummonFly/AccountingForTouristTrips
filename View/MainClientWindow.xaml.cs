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
using AccountingForTouristTrips.Model;

namespace AccountingForTouristTrips.View
{
    /// <summary>
    /// Логика взаимодействия для MainClientWindow.xaml
    /// </summary>
    public partial class MainClientWindow : Page
    {
        public MainClientWindow()
        {
            InitializeComponent();
        }

        private void ViewToursButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\TourView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void MyToursButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\MyTourList.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
