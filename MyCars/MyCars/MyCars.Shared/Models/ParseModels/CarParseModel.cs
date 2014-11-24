namespace MyCars.Models.ParseModels
{
    using Parse;

    [ParseClassName("Car")]
    public class CarParseModel : ParseObject
    {
        [ParseFieldName("vendor")]
        public string Vendor
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("model")]
        public string Model
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("ownerUsername")]
        public string OwnerUsername
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("ownerPhone")]
        public string OwnerPhone
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("yearOfManufacture")]
        public int YearOfManufacture
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("cityLocation")]
        public string CityLocation
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("price")]
        public double Price
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        [ParseFieldName("imageUrl")]
        public string ImageUrl
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}