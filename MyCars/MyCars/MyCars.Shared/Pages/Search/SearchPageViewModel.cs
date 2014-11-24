namespace MyCars.Pages.Search
{
    using GalaSoft.MvvmLight;
    using MyCars.Models;
    using Parse;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Linq;
    using MyCars.ViewModels;
    using MyCars.Models.ParseModels;

    public class SearchPageViewModel : ViewModelBase
    {
        private ObservableCollection<int> allowedYears;
        private string model;
        private double minPrice;
        private double maxPrice;
        private int minYear;
        private string city;
        private bool initializing;
        private string errorMessage;

        public SearchPageViewModel()
        {
            this.allowedYears = new ObservableCollection<int>();

            for (int i = 1930; i < 2014; i++)
            {
                this.allowedYears.Add(i);
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

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                this.model = value;
                this.RaisePropertyChanged(() => this.Model);
            }
        }

        public string City
        {
            get
            {
                return this.city;
            }

            set
            {
                this.city = value;
                this.RaisePropertyChanged(() => this.City);
            }
        }

        public double MinPrice
        {
            get
            {
                return this.minPrice;
            }

            set
            {
                this.minPrice = value;
                this.RaisePropertyChanged(() => this.MinPrice);
            }
        }

        public double MaxPrice
        {
            get
            {
                return this.maxPrice;
            }

            set
            {
                this.maxPrice = value;
                this.RaisePropertyChanged(() => this.MaxPrice);
            }
        }

        public int MinYear
        {
            get
            {
                return this.minYear;
            }

            set
            {
                this.minYear = value;
                this.RaisePropertyChanged(() => this.MinYear);
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

        public async Task<IEnumerable<CarViewModel>> SearchCar()
        {
            this.Initializing = true;

            if (!IsDataValid())
            {
                this.Initializing = false;

                return null;
            }

            var cars = await new ParseQuery<CarParseModel>()
                    .OrderBy(c => c.Price)
                    .Where(c => (this.MinPrice <= c.Price) && (c.Price <= this.MaxPrice) &&
                        (this.MinYear <= c.YearOfManufacture))
                    .FindAsync();

            var resultCars = cars.AsQueryable()
                .Where(c => (c.Vendor.Contains(this.Model) || c.Model.Contains(this.Model)) &&
                (c.CityLocation.Contains(this.City))).Select(CarViewModel.FromParseModel).ToList();

            if (!resultCars.Any())
            {
                this.ErrorMessage = "There are no such cars!";
                this.Initializing = false;

                return null;
            }

            this.Initializing = false;

            return resultCars;
        }

        private bool IsDataValid()
        {
            if (this.MaxPrice <= 0)
            {
                this.ErrorMessage = "Max price should be positive!";

                return false;
            }

            if (this.MinPrice > this.MaxPrice)
            {
                this.ErrorMessage = "Min price greater than max price!";

                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Model))
            {
                this.Model = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(this.City))
            {
                this.City = string.Empty;
            }

            return true;
        }
    }
}