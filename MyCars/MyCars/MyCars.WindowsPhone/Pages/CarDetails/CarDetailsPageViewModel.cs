namespace MyCars.Pages.CarDetails
{
    using GalaSoft.MvvmLight;
    using MyCars.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CarDetailsPageViewModel : ViewModelBase
    {
        private CarViewModel car;

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
    }
}