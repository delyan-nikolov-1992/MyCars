namespace MyCars.Pages.Main
{
    using GalaSoft.MvvmLight;
    using MyCars.Models;
    using MyCars.ViewModels;
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class MainPageViewModel : ViewModelBase
    {
        private const string DefaultUrl = 
            "http://nancyharmonjenkins.com/wp-content/plugins/nertworks-all-in-one-social-share-tools/images/no_image.png";

        private ObservableCollection<CarViewModel> cars;
        private bool initializing;

        public MainPageViewModel()
        {
            this.LoadCars();
        }

        private async void LoadCars()
        {
            this.Initializing = true;

            var cars = await new ParseQuery<Car>()
                    .FindAsync();

            this.Cars = cars.AsQueryable()
                    .Select(CarViewModel.FromModel);

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
                    try
                    {
                        this.cars.Add(currentCar);
                    }
                    catch (Exception)
                    {
                        currentCar.ImageUrl = DefaultUrl;
                        this.cars.Add(currentCar);
                    }
                }
            }
        }
    }
}