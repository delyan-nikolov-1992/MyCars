// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
namespace MyCars.Views
{
    using Parse;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class HeaderView : UserControl
    {
        public event EventHandler SignOut;

        public HeaderView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public string Username
        {
            get
            {
                return ParseUser.CurrentUser.Username;
            }
        }

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(HeaderView), new PropertyMetadata(null));

        private void OnSignOutButtonClick(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();

            if (this.SignOut != null)
            {
                this.SignOut(this, null);
            }
        }
    }
}