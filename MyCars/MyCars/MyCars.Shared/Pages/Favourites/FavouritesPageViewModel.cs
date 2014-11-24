namespace MyCars.Pages.Favourites
{
    using GalaSoft.MvvmLight;
    using MyCars.Models.ParseModels;
    using MyCars.SQLiteHelper;
    using MyCars.ViewModels;
    using Parse;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class FavouritesPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarViewModel> cars;
        private bool initializing;

        public FavouritesPageViewModel()
        {
            this.LoadCars();
        }

        private async void LoadCars()
        {
            this.Initializing = true;

            SQLiteManager manager = new SQLiteManager();

            var cars = await manager.SearchCarsAsync();

            this.Cars = cars.AsQueryable()
                    .Select(CarViewModel.FromSQLiteModel);

            this.Initializing = false;
        }

        public bool Initializing
        {
            get
            {
                return this.initializing;
            }
            set
            {
                this.initializing = value;
                this.RaisePropertyChanged(() => this.Initializing);
            }
        }

        public IEnumerable<CarViewModel> Cars
        {
            get
            {
                if (this.cars == null)
                {
                    this.cars = new ObservableCollection<CarViewModel>();
                }

                return this.cars;
            }
            set
            {
                if (this.cars == null)
                {
                    this.cars = new ObservableCollection<CarViewModel>();
                }

                this.cars.Clear();

                foreach (var currentCar in value)
                {
                    this.cars.Add(currentCar);
                }
            }
        }

        public async Task DeleteCar(CarViewModel selectedObject)
        {
            SQLiteManager manager = new SQLiteManager();
            await manager.DeleteCarAsync(selectedObject.ObjectId);
        }
    }
}