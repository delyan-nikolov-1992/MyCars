namespace MyCars.ViewModels
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        //internal static UserViewModel FromModel(ParseUser parseUser)
        //{
        //    var userCategories = parseUser["categories"] as IEnumerable<object>;

        //    return new UserViewModel()
        //    {
        //        Username = parseUser.Username,
        //        Categories = (parseUser["categories"] as IEnumerable<object>).Select(cat => cat as CategoryModel)
        //    };
        //}

        //public IEnumerable<CategoryModel> Categories { get; set; }
    }
}