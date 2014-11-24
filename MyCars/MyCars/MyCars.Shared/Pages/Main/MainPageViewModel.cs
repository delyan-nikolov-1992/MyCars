namespace MyCars.Pages.Main
{
    using GalaSoft.MvvmLight;
    using MyCars.Models;
    using MyCars.Models.ParseModels;
    using MyCars.ViewModels;
    using Parse;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarViewModel> cars;
        private bool initializing;

        public MainPageViewModel()
        {
            this.LoadCars();
        }

        private async void LoadCars()
        {
            this.Initializing = true;

            var cars = await new ParseQuery<CarParseModel>()
                    .OrderBy(c => c.Price)
                    .FindAsync();

            this.Cars = cars.AsQueryable()
                    .Select(CarViewModel.FromParseModel);

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
    }
}