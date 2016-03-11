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
            this.btnEditPersonalInfo.Visibility = Visibility.Hidden;
            mainFrame.NavigationService.Navigate(new Uri("HomeContent.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for login button
        /// Date: 3/6/16
        /// </summary>
		/// <remarks>
		/// Ryan Taylor
		/// Updated: 2016/03/07
		/// Fixed the access token creation event
		/// </remarks>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (null == _accessToken)
            {
                _login = new Login();
                _login.AccessTokenCreatedEvent += setAccessToken;
                if (_login.ShowDialog() == true && _accessToken != null) // login succeeded
                {
                    this.btnLogin.Header = "Log Out";
                    this.btnEditPersonalInfo.Visibility = Visibility.Visible;
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
				this.btnEditPersonalInfo.Visibility = Visibility.Hidden;
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
        /// <summary>
        /// Author: Ryan Taylor
        /// Click logic for New user button
        /// Date: 2/26/16
        /// </summary>-
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserCreation _newUser = new NewUserCreation();
            _newUser.ShowDialog();
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnGardens
        /// Date: 3/6/16
        /// </summary>
        private void btnGardens_click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Gardens.xaml", UriKind.Relative));
            btnSideBar1.Content = "Create a Garden";
            btnSideBar2.Content = "btnSideBar2";
            btnSideBar3.Content = "btnSideBar3";
            btnSideBar4.Content = "btnSideBar4";
            btnSideBar5.Content = "btnSideBar5";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnExpert
        /// Date: 3/6/16
        /// </summary>
        private void btnExpert_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Expert.xaml", UriKind.Relative));
            btnSideBar1.Content = "Become an Expert";
            btnSideBar2.Content = "btnSideBar2";
            btnSideBar3.Content = "btnSideBar3";
            btnSideBar4.Content = "btnSideBar4";
            btnSideBar5.Content = "btnSideBar5";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnHome
        /// Date: 3/6/16
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("HomeContent.xaml", UriKind.Relative));
            btnSideBar1.Content = "Messages";
        }

        //private void btnSideBar1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (btnSideBar1.Content.ToString() == "Create a Garden")
        //    {
        //        mainFrame.NavigationService.Navigate(new Uri("GardenPages/CreateGarden.xaml", UriKind.Relative));
        //    }
        //}
            btnSideBar1.Content = "btnSideBar1";
            btnSideBar2.Content = "btnSideBar2";
            btnSideBar3.Content = "btnSideBar3";
            btnSideBar4.Content = "btnSideBar4";
            btnSideBar5.Content = "btnSideBar5";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnProfile
        /// Date: 3/9/16
        /// </summary>
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
             mainFrame.NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
        }


        /// <summary>
        /// Author: Chris Schwebach
        /// Interaction logic for UserEditPersonalInfo.xaml
        /// Date: 3/3/16
        /// ///Updated Date: 3/8/16
        /// </summary>
        /// 
        private void Button_Click_PersonalInfo(object sender, RoutedEventArgs e)
        {
            this.Hide();
            UserEditPersonalInfo _userEditPersonalInfo = new UserEditPersonalInfo(_accessToken);
            _userEditPersonalInfo.ShowDialog();
            this.Show();
        }

        private void btnSideBar1_Click(object sender, RoutedEventArgs e)
        {
            if (btnSideBar1.Content.ToString() == "Create a Garden")
            {
                mainFrame.NavigationService.Navigate(new Uri("GardenPages/CreateGarden.xaml", UriKind.Relative));
            }
        }
        private void btnSideBar2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSideBar10_Click(object sender, RoutedEventArgs e)
        {

        }



        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void btnSideBar1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar1.Content == "Create Garden")
            {
                mainFrame.NavigationService.Navigate(new Uri("GardenPages/CreateGarden.xaml", UriKind.Relative));
            }
            else if (btnSideBar1.Content == "Messages")
            {
                mainFrame.NavigationService.Navigate(new Uri("GardenPages/AdminMessages.xaml", UriKind.Relative));
            }
            
        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        
    }
}
