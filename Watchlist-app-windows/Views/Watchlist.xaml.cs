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
using System.IO;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Watchlist_app_windows.DataFetchers;
using System.Windows.Controls;
using System;
using System.Threading;
using System.Windows.Forms;
using Watchlist_app_windows.ViewControllers;

namespace Watchlist_app_windows
{
    /// <summary>
    /// Interaction logic for Watchlist.xaml
    /// </summary>
    public partial class Watchlist : Page
    {
        public Watchlist()
        {           
            Data.EventHandler = new Data.MyEvent(toDataGrid);
            InitializeComponent();
        }

        private void GoToMain(object sender, RoutedEventArgs e)
        {

            WindowsList Singleton = WindowsList.GetInstance();
            this.NavigationService.Navigate(Singleton.page1);
        }


        private void SearchPopular(object sender, RoutedEventArgs e)
        {
            Get request = new Get("http://api.themoviedb.org/3/movie/popular?api_key=86afaae5fbe574d49418485ca1e58803");
            ThreadClass tc = new ThreadClass(request);
            Thread searchThread = new Thread(new ThreadStart(tc.func));
            searchThread.Start();
          
        }

        private void SearchByTitle(object sender, RoutedEventArgs e)
        {
            string temp = searchBox.Text;
            Get request = new Get("http://api.themoviedb.org/3/search/movie?query=" + temp + "&api_key=86afaae5fbe574d49418485ca1e58803");
            ThreadClass tc = new ThreadClass(request);
            Thread searchThread = new Thread(new ThreadStart(tc.func));
            searchThread.Start();
        }
        public void toDataGrid(Movies myMovies)
        {
            if (dataGrid1.Dispatcher.Thread == Thread.CurrentThread)
            {
                dataGrid1.ItemsSource = myMovies.results;
                dataGrid1.Items.Refresh();
            }
            else
            {
                dataGrid1.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                {
                dataGrid1.ItemsSource = myMovies.results;
                dataGrid1.Items.Refresh();
            }));
            }
            
        }
 

    }
}
