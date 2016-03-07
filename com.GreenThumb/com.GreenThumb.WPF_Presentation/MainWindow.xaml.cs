using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccessToken _accessToken = null;
        Login _login;

        public MainWindow()
        {
            InitializeComponent();
            mainFrame.NavigationService.Navigate(new Uri("HomeContent.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/04/2016
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (null == _accessToken)
            {
                _login = new Login();
                _login.AccessTokenCreatedEvent += setAccessToken;
                if (_login.ShowDialog() == true && _accessToken != null) // login succeeded
                {
                    this.btnLogin.Header = "Log Out";
                    // this is where we will set the initial privilages based on roles

                }
                else
                {
                    // clear the access token reference
                    _accessToken = null;
                    MessageBox.Show("Login Failed.");
                }
            }
            else // somebody is already logged in
            {
                _accessToken = null;
                this.btnLogin.Header = "Log In";
                // change things back to default here.
            }
            
            
            
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/05/2016
        /// A method to subscribe to the login event that sets access token.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a">The access token being sent</param>
        private void setAccessToken(object sender, AccessToken a)
        {
            if (sender == _login)
            {
                this._accessToken = a;
            }
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserCreation _newUser = new NewUserCreation();
            _newUser.ShowDialog();
        }

        private void btnGardens_click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Gardens.xaml", UriKind.Relative));
            btnSideBar1.Content = "Create a Garden";
        }

        private void btnExpert_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Expert.xaml", UriKind.Relative));
            btnSideBar1.Content = "btnSideBar1";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("HomeContent.xaml", UriKind.Relative));
            btnSideBar1.Content = "btnSideBar1";
        }

        private void btnSideBar1_Click(object sender, RoutedEventArgs e)
        {
            if (btnSideBar1.Content.ToString() == "Create a Garden")
            {
                mainFrame.NavigationService.Navigate(new Uri("GardenPages/CreateGarden.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Author: Chris Schwebach
        /// Interaction logic for UserEditPersonalInfo.xaml
        /// Date: 3/3/16
        /// </summary>
        private void Button_Click_PersonalInfo(object sender, RoutedEventArgs e)
        {
            UserEditPersonalInfo _userEditPersonalInfo = new UserEditPersonalInfo(_accessToken);
            _userEditPersonalInfo.ShowDialog();
            Close();
        }
        
    }
}
