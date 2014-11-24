namespace MyCars.ViewModels
{
    using GalaSoft.MvvmLight;
    using MyCars.Models;
    using MyCars.Models.ParseModels;
    using MyCars.Models.SQLiteModels;
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Media.Imaging;

    public class CarViewModel : ViewModelBase
    {
        private string objectId;
        private string vendor;
        private string model;
        private double price;
        private string imageUrl;
        private string fullName;
        private BitmapImage bitmap;
        private string description;
        private int yearOfManufacture;
        private string cityLocation;
        private string ownerUsername;
        private string ownerPhone;

        public static Expression<Func<CarParseModel, CarViewModel>> FromParseModel
        {
            get
            {
                return model => new CarViewModel()
                {
                    ObjectId = model.ObjectId,
                    Vendor = model.Vendor,
                    Model = model.Model,
                    Description = model.Description,
                    OwnerUsername = model.OwnerUsername,
                    OwnerPhone = model.OwnerPhone,
                    YearOfManufacture = model.YearOfManufacture,
                    CityLocation = model.CityLocation,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl,
                    FullName = model.Vendor + " " + model.Model,
                    Bitmap = new BitmapImage(new Uri(model.ImageUrl, UriKind.Absolute))
                };
            }
        }

        public CarParseModel ToParseModel
        {
            get
            {
                return new CarParseModel()
                {
                    Vendor = this.Vendor,
                    Model = this.Model,
                    Description = this.Description,
                    OwnerUsername = ParseUser.CurrentUser.Username,
                    OwnerPhone = ParseUser.CurrentUser["phone"].ToString(),
                    YearOfManufacture = this.YearOfManufacture,
                    CityLocation = this.CityLocation,
                    Price = this.Price,
                    ImageUrl = this.ImageUrl
                };
            }
        }

        public static Expression<Func<CarSQLiteModel, CarViewModel>> FromSQLiteModel
        {
            get
            {
                return model => new CarViewModel()
                {
                    ObjectId = model.ParseObjectId,
                    Vendor = model.Vendor,
                    Model = model.Model,
                    Description = model.Description,
                    OwnerUsername = model.OwnerUsername,
                    OwnerPhone = model.OwnerPhone,
                    YearOfManufacture = model.YearOfManufacture,
                    CityLocation = model.CityLocation,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl,
                    FullName = model.Vendor + " " + model.Model,
                    Bitmap = new BitmapImage(new Uri(model.ImageUrl, UriKind.Absolute))
                };
            }
        }

        public CarSQLiteModel ToSQLiteModel
        {
            get
            {
                return new CarSQLiteModel()
                {
                    ParseObjectId = this.ObjectId,
                    Vendor = this.Vendor,
                    Model = this.Model,
                    Description = this.Description,
                    OwnerUsername = ParseUser.CurrentUser.Username,
                    OwnerPhone = ParseUser.CurrentUser["phone"].ToString(),
                    YearOfManufacture = this.YearOfManufacture,
                    CityLocation = this.CityLocation,
                    Price = this.Price,
                    ImageUrl = this.ImageUrl
                };
            }
        }

        public BitmapImage Bitmap
        {
            get
            {
                return this.bitmap;
            }

            set
            {
                this.bitmap = value;
                this.RaisePropertyChanged(() => this.Bitmap);
            }
        }

        public string ObjectId
        {
            get
            {
                return this.objectId;
            }
            set
            {
                this.objectId = value;
                this.RaisePropertyChanged(() => this.ObjectId);
            }
        }

        public string Vendor
        {
            get
            {
                return this.vendor;
            }
            set
            {
                this.vendor = value;
                this.RaisePropertyChanged(() => this.Vendor);
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

        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
                this.RaisePropertyChanged(() => this.FullName);
            }
        }

        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
                this.RaisePropertyChanged(() => this.ImageUrl);
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.RaisePropertyChanged(() => this.Description);
            }
        }

        public string OwnerUsername
        {
            get
            {
                return this.ownerUsername;
            }
            set
            {
                this.ownerUsername = value;
                this.RaisePropertyChanged(() => this.OwnerUsername);
            }
        }

        public string OwnerPhone
        {
            get
            {
                return this.ownerPhone;
            }
            set
            {
                this.ownerPhone = value;
                this.RaisePropertyChanged(() => this.OwnerPhone);
            }
        }

       

        public int YearOfManufacture
        {
            get
            {
                return this.yearOfManufacture;
            }
            set
            {
                this.yearOfManufacture = value;
                this.RaisePropertyChanged(() => this.YearOfManufacture);
            }
        }

        public string CityLocation
        {
            get
            {
                return this.cityLocation;
            }
            set
            {
                this.cityLocation = value;
                this.RaisePropertyChanged(() => this.CityLocation);
            }
        }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
                this.RaisePropertyChanged(() => this.Price);
            }
        }
    }
}