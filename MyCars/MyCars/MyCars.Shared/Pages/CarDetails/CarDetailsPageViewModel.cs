namespace MyCars.Pages.CarDetails
{
    using GalaSoft.MvvmLight;
    using MyCars.Models.SQLiteModels;
    using MyCars.SQLiteHelper;
    using MyCars.ViewModels;
    using SQLite;
    using System;
    using System.Threading.Tasks;

    public class CarDetailsPageViewModel : ViewModelBase
    {
        private CarViewModel car;
        private SQLiteManager manager;

        public CarDetailsPageViewModel()
        {
            this.manager = new SQLiteManager();
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

        public async Task<bool> AddToFavourites()
        {
            var result = await this.manager.ExistCarAsync(this.Car.ObjectId);

            if (!result)
            {
                await manager.AddCarAsync(this.Car.ToSQLiteModel);
            }

            return result;
        }
    }
}