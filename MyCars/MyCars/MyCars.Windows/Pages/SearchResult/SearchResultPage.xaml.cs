// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
namespace MyCars.Pages.SearchResult
{
    using MyCars.Common;
    using MyCars.Pages.AddingCar;
    using MyCars.Pages.CarDetails;
    using MyCars.Pages.Favourites;
    using MyCars.Pages.Login;
    using MyCars.Pages.Main;
    using MyCars.Pages.Search;
    using MyCars.ViewModels;
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchResultPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SearchResultPage()
            : this(new SearchResultPageViewModel())
        {
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public SearchResultPage(SearchResultPageViewModel viewModel)
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            
            this.ViewModel = viewModel;
        }

        public SearchResultPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as SearchResultPageViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            var cars = e.Parameter as IEnumerable<CarViewModel>;

            this.ViewModel.LoadCars(cars);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public void OnSignOutCompleted(object sender, EventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void OnCarsListItemDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var eventsListView = (sender as ListView);

            if (eventsListView == null)
            {
                return;
            }

            var selectedObject = eventsListView.SelectedItem;
            this.Frame.Navigate(typeof(CarDetailsPage), selectedObject);
        }

        private void OnMainPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnAddingCarPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddingCarPage));
        }

        private void OnSearchPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchPage));
        }

        private void OnFavouritesPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FavouritesPage));
        }
    }
}
