// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
namespace MyCars.Pages.AddingCar
{
    using MyCars.Common;
    using MyCars.Pages.Favourites;
    using MyCars.Pages.Login;
    using MyCars.Pages.Main;
    using MyCars.Pages.Search;
    using System;
    using Windows.Devices.Geolocation;
    using Windows.Services.Maps;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddingCarPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public AddingCarPage()
            : this(new AddingCarPageViewModel())
        {
        }

        public AddingCarPage(AddingCarPageViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;
        }

        public AddingCarPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as AddingCarPageViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            LoadLocation();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public void OnSignOutCompleted(object sender, EventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private async void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
            {
                // raise error
                return;
            }

            bool isCarAdded = await this.ViewModel.AddCar();

            if (isCarAdded)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void OnMainPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnSearchPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchPage));
        }

        private void OnFavouritesPageAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FavouritesPage));
        }

        private async void LoadLocation()
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.MovementThreshold = 100;
            locator.ReportInterval = 100;

            locator.PositionChanged += (snd, args) =>
            {
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var latitude = args.Position.Coordinate.Point.Position.Latitude;
                    var longitude = args.Position.Coordinate.Point.Position.Longitude;

                    ReverseGeocode(latitude, longitude);
                });
            };

            await locator.GetGeopositionAsync();
        }

        private async void ReverseGeocode(double latitude, double longitude)
        {
            // Location to reverse geocode.
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = latitude;
            location.Longitude = longitude;
            Geopoint pointToReverseGeocode = new Geopoint(location);

            // Reverse geocode the specified geographic location.
            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count > 0)
            {
                var city = result.Locations[0].Address.Town;
                this.CityLocation.Text = city;
            }
        }
    }
}