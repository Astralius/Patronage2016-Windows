using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PatronagePhotographer.View
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(PhotosListPage));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerMenu.IsPaneOpen = !HamburgerMenu.IsPaneOpen;
        }

        private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var item = (ListBoxItem)listBox?.SelectedItem;
            switch (item?.Name)
            {
                case "SinglePhoto":
                    MainFrame.Navigate(typeof(SinglePhotoPage));
                    break;
                case "PhotosList":
                    MainFrame.Navigate(typeof(PhotosListPage));
                    break;
                default:
                    break;
            }
            HamburgerMenu.IsPaneOpen = false;
        }

        /// <summary>
        /// Ukrywa pasek stanu urządzenia mobilnego z chwilą załadowania głównej strony aplikacji.
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                return;
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
        }
    }
}
