namespace MyCars.Pages.AddingCar
{
    using GalaSoft.MvvmLight;
    using MyCars.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class AddingCarPageViewModel : ViewModelBase
    {
        private CarViewModel car;
        private ObservableCollection<int> allowedYears;
        private string errorMessage;
        private bool initializing;

        public AddingCarPageViewModel()
        {
            this.car = new CarViewModel();
            this.allowedYears = new ObservableCollection<int>();

            for (int i = 1930; i < 2015; i++)
            {
                this.allowedYears.Add(i);
            }
        }

        public CarViewModel Car
        {
            get
            {
                return this.car;
            }
            set
            {
                this.car = value;
                this.RaisePropertyChanged(() => this.Car);
            }
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

        public IEnumerable<int> AllowedYears
        {
            get
            {
                return this.allowedYears;
            }
            set
            {
                if (this.allowedYears == null)
                {
                    this.allowedYears = new ObservableCollection<int>();
                }

                this.allowedYears.Clear();

                foreach (var year in value)
                {
                    this.allowedYears.Add(year);
                }

                this.RaisePropertyChanged(() => this.AllowedYears);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }

            set
            {
                this.errorMessage = value;
                this.RaisePropertyChanged(() => this.ErrorMessage);
            }
        }

        public async Task<bool> AddCar()
        {
            this.Initializing = true;

            if (!IsDataValid())
            {
                this.Initializing = false;

                return false;
            }

            var carModel = this.Car.ToParseModel;

            await carModel.SaveAsync();

            this.Initializing = false;

            return true;
        }

        private bool IsDataValid()
        {
            if (string.IsNullOrWhiteSpace(this.Car.Vendor))
            {
                this.ErrorMessage = "Vendor should not be empty!";

                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Car.Model))
            {
                this.ErrorMessage = "Model should not be empty!";

                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Car.Description))
            {
                this.Car.Description = "No Description";
            }

            if (string.IsNullOrWhiteSpace(this.Car.CityLocation))
            {
                this.Car.Description = "Unknown";
            }

            if (this.Car.Price <= 0)
            {
                this.ErrorMessage = "The price should be positive!";

                return false;
            }

            if (this.Car.YearOfManufacture == 0)
            {
                this.ErrorMessage = "Year of manufacture should not be empty!";

                return false;
            }

            var result = CheckUri();

            return result;
        }

        private bool CheckUri()
        {
            Uri uri;
            var result = Uri.TryCreate(this.Car.ImageUrl, UriKind.Absolute, out uri);

            if (!result)
            {
                this.ErrorMessage = "Invalid image URL!";
            }

            return result;
        }
    }
}