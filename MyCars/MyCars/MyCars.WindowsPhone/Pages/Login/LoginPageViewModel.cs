namespace MyCars.Pages.Login
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight;
    using Parse;
    using MyCars.ViewModels;

    public class LoginPageViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }

        public LoginPageViewModel()
        {
            this.User = new UserViewModel()
            {
                Username = "Didaka",
                Password = "123456"
            };
        }

        public async Task<bool> Login()
        {
            try
            {
                await ParseUser.LogInAsync(this.User.Username, this.User.Password);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}