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
            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
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
                    // this is where we will set the initial privilages based on roles

                }
                else
                {
                    // clear the access token reference
                    _accessToken = null;
                    MessageBox.Show("Login Failed.");
                    lblLoggedIn.Header = "";
                }
            }
            else // somebody is already logged in
            {
                _accessToken = null;
                this.btnLogin.Header = "Log In";
                // change things back to default here.
                lblLoggedIn.Header = "";
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
                lblLoggedIn.Header = "Logged in as " + a.FirstName + " " + a.LastName;
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
        /// Click logic for button btnHome
        /// Date: 3/6/16
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
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
        /// Click logic for button btnGardens
        /// Date: 3/6/16
        /// </summary>
        private void btnGardens_click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new GardenPages.GardenMain(_accessToken));
            btnSideBar1.Content = "Create a Garden";
            btnSideBar2.Content = "btnSideBar2";
            btnSideBar3.Content = "Request to be a Group Leader";
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
            mainFrame.NavigationService.Navigate(new ExpertPages.ExpertHome(_accessToken));
            btnSideBar1.Content = "Become an Expert";
            btnSideBar2.Content = "Insert Recipe";
            btnSideBar3.Content = "btnSideBar3";
            btnSideBar4.Content = "Upload Garden Template";
            btnSideBar5.Content = "View Garden Templates";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
        }
        


        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new AdminPages.AdminHome(_accessToken));
            btnSideBar1.Content = "btnSideBar1";
            btnSideBar2.Content = "Messages";
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
            btnSideBar1.Content = "Edit Personal Info";
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
        /// Author: Sara Nanke
        /// Click logic for the btnsidebarclick event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar1.Content.ToString().ToLowerInvariant() == "create a garden")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.CreateGarden(_accessToken));
            }
            else if (btnSideBar1.Content == "Edit Personal Info")
            {
                mainFrame.NavigationService.Navigate(new ProfilePages.EditPersonalInfo(_accessToken));
            }

        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar2click event
        /// Date: 3/9/16
        /// </summary>
        /// <remarks>
        /// Updater Chris Schwebach
        /// Updated: 2016/03/15
        /// Changed btnSideBar2 event Insert Recipe 
        /// </remarks>
        private void btnSideBar2_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            if (btnSideBar2.Content.ToString() == "Messages")
            {
                mainFrame.NavigationService.Navigate(new Uri("GardenPages/AdminMessages.xaml", UriKind.Relative));
            }
            else if (btnSideBar2.Content == "Insert Recipe")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.RecipeInput(_accessToken));
            }
            
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar3click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar3.Content.ToString() == "Request to be a Group Leader")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.RequestGroupLeader(_accessToken));
            }
            
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar4click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar4.Content.ToString() == "Upload Garden Template")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ExpertGardenTemplate(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar5click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar5.Content.ToString() == "View Garden Templates")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ViewGardenTemplate());
            }
            
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar6click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar6_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar7click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar7_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar8click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar8_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar9click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar9_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar10click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar10_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
