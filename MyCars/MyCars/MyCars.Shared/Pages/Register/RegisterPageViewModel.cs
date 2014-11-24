namespace MyCars.Pages.Register
{
    using GalaSoft.MvvmLight;
    using MyCars.ViewModels;
    using Parse;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class RegisterPageViewModel : ViewModelBase
    {
        private const int ValidLength = 4;

        private string errorMessage;
        private bool initializing;
        private UserViewModel user;
        private string confirmationPassword;

        public UserViewModel User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
                this.RaisePropertyChanged(() => this.User);
            }
        }

        public string ConfirmationPassword
        {
            get
            {
                return this.confirmationPassword;
            }
            set
            {
                this.confirmationPassword = value;
                this.RaisePropertyChanged(() => this.ConfirmationPassword);
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

        public RegisterPageViewModel()
        {
            this.User = new UserViewModel();
        }

        public async Task<bool> Register()
        {
            this.Initializing = true;

            if (!IsDataValid())
            {
                return false;
            }

            try
            {
                var user = new ParseUser()
                {
                    Username = this.User.Username,
                    Password = this.User.Password
                };

                user["phone"] = this.User.Phone;

                await user.SignUpAsync();

                await ParseUser.LogInAsync(this.User.Username, this.User.Password);

                this.ErrorMessage = string.Empty;

                this.Initializing = false;

                return true;
            }
            catch (Exception)
            {
                this.ErrorMessage = "Username already exists!";

                this.Initializing = false;

                return false;
            }
        }

        private bool IsDataValid()
        {
            if (string.IsNullOrWhiteSpace(this.User.Password))
            {
                this.ErrorMessage = "Password should not be empty!";

                return false;
            }

            if (this.User.Password != this.ConfirmationPassword)
            {
                this.ErrorMessage = "Passwords should be equal!";

                return false;
            }

            if (string.IsNullOrWhiteSpace(this.User.Username))
            {
                this.ErrorMessage = "Username should not be empty!";

                return false;
            }

            if (this.User.Username.Length < ValidLength)
            {
                this.ErrorMessage = string.Format("Username should consist of at least {0} symbols!", ValidLength);

                return false;
            }

            if (this.User.Password.Length < ValidLength)
            {
                this.ErrorMessage = string.Format("Password should consist of at least {0} symbols!", ValidLength);

                return false;
            }

            return true;
        }
    }
}