namespace MyCars.Pages.SearchResult
{
    using GalaSoft.MvvmLight;
    using MyCars.ViewModels;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class SearchResultPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarViewModel> cars;
        private bool initializing;

        public SearchResultPageViewModel()
        {
        }

        public void LoadCars(IEnumerable<CarViewModel> cars)
        {
            this.Initializing = true;

            this.Cars = cars;

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