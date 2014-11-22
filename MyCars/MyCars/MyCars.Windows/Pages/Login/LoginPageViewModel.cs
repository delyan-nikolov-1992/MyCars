namespace MyCars.Pages.Login
{
    using GalaSoft.MvvmLight;
    using MyCars.ViewModels;
    using Parse;
    using System;
    using System.Threading.Tasks;

    public class LoginPageViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }

        public LoginPageViewModel()
        {
            this.User = new UserViewModel()
            {
                Username = "Doncho",
                Password = "123456q"
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