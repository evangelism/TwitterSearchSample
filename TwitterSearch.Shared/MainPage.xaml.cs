using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TwitterSearch.MobileServices;
using TwitterSearch.PCL;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TwitterSearch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string _searchTerm = string.Empty;

        TwitterSearchResult TRes;

        public MainPage()
        {
            InitializeComponent();
            TRes = new TwitterSearchResult();

            this.DataContext = TRes;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.SelectAll();
        }

        private async void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                LoadData();
            }
        }

        private async void LoadData()
        {
#if WINDOWS_PHONE_APP
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.ProgressIndicator.Text = "Загрузка новых твитов...";
            await statusBar.ProgressIndicator.ShowAsync();
#endif
            _searchTerm = SearchTextBox.Text.Trim();
            MobileService.Update(_searchTerm.ToLower());
            await TRes.Search(_searchTerm);
        }

        private async void resultListBox_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
#if WINDOWS_PHONE_APP
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.ProgressIndicator.HideAsync();
#endif
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var s = await MobileService.GetPopular();
            if (s!=null)
            {
                tbMostPopular.Text = string.Format("Popular: {0} ({1} times)",s.id,s.count);
            }
        }
    }
}
