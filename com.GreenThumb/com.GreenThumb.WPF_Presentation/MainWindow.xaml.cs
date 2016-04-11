using com.GreenThumb.BusinessLogic;
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
        RoleManager roleManager = new RoleManager();
        NewUserCreation _newUser;

        public MainWindow()
        {
            InitializeComponent();
            /// Added by Trevor
            /// Checking to see if there are users in the DB----- If not prompt to create admin account
            UserManager um = new UserManager();
            int users = um.GetUserCount();
            if (users == 0)
            {
                _newUser = new NewUserCreation(true);
                _newUser.AccessTokenCreatedEvent += setAccessToken;
                _newUser.ShowDialog();
                if (_accessToken != null)
                {
                    this.btnLogin.Header = "Log Out";
                   
                }

            }


            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
            CheckPermissions();



        }

        private void CheckPermissions()
        {
            btnGardens.Visibility = Visibility.Hidden;
            btnAdmin.Visibility = Visibility.Hidden;
            btnExpert.Visibility = Visibility.Hidden;
            btnHome.Visibility = Visibility.Visible;
            btnProfile.Visibility = Visibility.Hidden;
            btnVolunteer.Visibility = Visibility.Hidden;
            if (_accessToken == null)
            {                
                btnSideBar1.Content = "";
                btnSideBar2.Content = "";
                btnSideBar3.Content = "";
                btnSideBar4.Content = "";
                btnSideBar5.Content = "";
                btnSideBar6.Content = "";
                btnSideBar7.Content = "";
                btnSideBar8.Content = "";
                btnSideBar9.Content = "";
                btnSideBar10.Content = "";
                btnSideBar11.Content = "";
                btnSideBar12.Content = "";
                btnSideBar13.Content = "";
                btnSideBar14.Content = "";
                btnSideBar15.Content = "";
            }
            if (_accessToken != null)
            {
                foreach (Role r in _accessToken.Roles)
                {
                    if (r.RoleID == "Admin")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Visible;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                    if (r.RoleID == "Expert")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Hidden;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                    if (r.RoleID == "GroupLeader")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Hidden;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                    if (r.RoleID == "GroupMember")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Hidden;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                    if (r.RoleID == "User")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Hidden;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                    if (r.RoleID == "Guest")
                    {
                        btnGardens.Visibility = Visibility.Visible;
                        btnAdmin.Visibility = Visibility.Hidden;
                        btnExpert.Visibility = Visibility.Visible;
                        btnHome.Visibility = Visibility.Visible;
                        btnProfile.Visibility = Visibility.Visible;
                        btnVolunteer.Visibility = Visibility.Visible;
                        break;
                    }
                }
            }
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
                    CheckPermissions();
                    SetHomeButtons();
                }
                else
                {
                    // clear the access token reference
                    _accessToken = null;
                    MessageBox.Show("Login Failed.");
                    lblLoggedIn.Header = "";
                    CheckPermissions();
                }
            }
            else // somebody is already logged in
            {
                _accessToken = null;
                this.btnLogin.Header = "Log In";
                // change things back to default here.
                lblLoggedIn.Header = "";
                CheckPermissions();
                btnSignUp.Visibility = System.Windows.Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for Loggged in button
        /// this button displays the person that is logged in, and will go to profile menu when clicked (when done)
        /// Date: 3/6/16
        /// </summary>
        private void lblLoggedIn_Click(object sender, RoutedEventArgs e)
        {
            btnProfile.Focus();
            mainFrame.NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
            CheckPermissions();
            SetProfileButtons();
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
            if (sender == _login || sender == _newUser) // Made changes to login when user registers By : Poonam Dubey
            {
                this._accessToken = a;
                lblLoggedIn.Header = "Logged in as " + a.FirstName + " " + a.LastName;
                btnSignUp.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Author: Ryan Taylor
        /// Click logic for New user button
        /// Date: 2/26/16
        /// </summary>-
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            // Made changes to login when user registers By : Poonam Dubey
            _newUser = new NewUserCreation();
            _newUser.AccessTokenCreatedEvent += setAccessToken;
            _newUser.ShowDialog(); 
            if (_accessToken != null)
            {
                this.btnLogin.Header = "Log Out";
                CheckPermissions();
            }
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnHome
        /// Date: 3/6/16
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new HomeContent(_accessToken));
            SetHomeButtons();
        }
        // Chris S - Had to refactor - using in two places
        private void SetHomeButtons()
        {
            btnSideBar1.Content = "Blog";
            btnSideBar2.Content = "";
            btnSideBar3.Content = "";
            btnSideBar4.Content = "";
            btnSideBar5.Content = "";
            btnSideBar6.Content = "";
            btnSideBar7.Content = "";
            btnSideBar8.Content = "";
            btnSideBar9.Content = "";
            btnSideBar10.Content = "";
            btnSideBar11.Content = "";
            btnSideBar12.Content = "";
            btnSideBar13.Content = "";
            btnSideBar14.Content = "";
            btnSideBar15.Content = "";
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnGardens
        /// Date: 3/6/16
        /// </summary>
        private void btnGardens_click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new GardenPages.GardenMain(_accessToken));
            //btnSideBar1.Content = "Create a Garden";
            btnSideBar1.Content = "DO NOT USE";
            btnSideBar2.Content = "DO NOT USE";
            btnSideBar3.Content = "DO NOT USE";
            btnSideBar4.Content = "Complete A Task";
            btnSideBar5.Content = "Create a Task";
            btnSideBar6.Content = "Sign Up for Task";

            Role role = new Role();
            role.RoleID = "Admin";
            if (_accessToken.Roles.Contains(role))
            {
                btnSideBar7.Content = "Manage Garden Group";
            }
            else
            {
                btnSideBar7.Content = "btnSideBar7";
            }
            btnSideBar7.Content = "Create Garden";
            btnSideBar8.Content = "View Tasks By Garden";
            btnSideBar9.Content = "View Garden Tasks";
            btnSideBar10.Content = "View Groups";
            btnSideBar11.Content = "Your Groups";
            btnSideBar12.Content = "Request to be a Group Leader";
            btnSideBar13.Content = "Assgin Task to a Member";
            btnSideBar14.Content = "btnSideBar14";
            btnSideBar15.Content = "btnSideBar15";
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
            btnSideBar3.Content = "Search for Questions";
            btnSideBar4.Content = _accessToken != null ? "Ask a Question" : "btnSideBar4";
            btnSideBar5.Content = roleManager.IsUserThisRole(_accessToken, "Expert") ? "Answer Questions" : "btnSideBar5";
            btnSideBar6.Content = "Upload Garden Template";
            btnSideBar7.Content = "View Garden Templates";
            btnSideBar8.Content = "View Recipes";
            btnSideBar9.Content = "Plants";
            btnSideBar10.Content = "btnSideBar10";
            btnSideBar11.Content = "btnSideBar11";
            btnSideBar12.Content = "btnSideBar12";
            btnSideBar13.Content = "btnSideBar13";
            btnSideBar14.Content = "btnSideBar14";
            btnSideBar15.Content = "btnSideBar15";
        }

        /// <summary>
        /// Author: Emily West
        /// Click logic for button btnVolunteer
        /// </summary>
        private void btnVolunteer_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new VolunteerPages.VolunteerHome(_accessToken));
            btnSideBar1.Content = "Edit Volunteer Availability";
            btnSideBar2.Content = "Volunteer Sign Up";
            btnSideBar3.Content = "btnSideBar3";
            btnSideBar4.Content = "btnSideBar4";
            btnSideBar5.Content = "btnSideBar5";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
            btnSideBar11.Content = "btnSideBar11";
            btnSideBar12.Content = "btnSideBar12";
            btnSideBar13.Content = "btnSideBar13";
            btnSideBar14.Content = "btnSideBar14";
            btnSideBar15.Content = "btnSideBar15";

        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnAdmin
        /// Date: 3/9/16
        /// </summary>
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new AdminPages.AdminHome(_accessToken));
            btnSideBar1.Content = "btnSideBar1";
            btnSideBar2.Content = "Messages";
            btnSideBar3.Content = "Expert Requests";
            btnSideBar4.Content = "User Role";
            btnSideBar5.Content = "User Region";
            btnSideBar6.Content = "btnSideBar6";
            btnSideBar7.Content = "btnSideBar7";
            btnSideBar8.Content = "btnSideBar8";
            btnSideBar9.Content = "btnSideBar9";
            btnSideBar10.Content = "btnSideBar10";
            btnSideBar11.Content = "btnSideBar11";
            btnSideBar12.Content = "btnSideBar12";
            btnSideBar13.Content = "btnSideBar13";
            btnSideBar14.Content = "btnSideBar14";
            btnSideBar15.Content = "btnSideBar15";
        }

        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for button btnProfile
        /// Date: 3/9/16
        /// </summary>
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new ProfilePages.ProfileMain(_accessToken));
            SetProfileButtons();
        }
        //Chris S - had to refactor - using in multiple places
        private void SetProfileButtons()
        {
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
            btnSideBar11.Content = "btnSideBar11";
            btnSideBar12.Content = "btnSideBar12";
            btnSideBar13.Content = "btnSideBar13";
            btnSideBar14.Content = "btnSideBar14";
            btnSideBar15.Content = "btnSideBar15";
        }




        /// <summary>
        /// Author: Sara Nanke
        /// Click logic for the btnsidebarclick event
        /// Date: 3/9/16
        /// Updated By: Chris Sheehan 3/24/16
        /// cast content to string
        /// </summary>
        private void btnSideBar1_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            if (btnSideBar1.Content.ToString() == "Edit Personal Info")
            {
                mainFrame.NavigationService.Navigate(new ProfilePages.EditPersonalInfo(_accessToken));
            }
            else if (btnSideBar1.Content.ToString() == "Edit Volunteer Availability")
            {
                mainFrame.NavigationService.Navigate(new VolunteerPages.EditVolunteerAvailability(_accessToken));
            }
            else if (btnSideBar1.Content.ToString() == "Become an Expert")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.RequestExpert(_accessToken));
            }
            else if (btnSideBar1.Content.ToString() == "Blog")
            {
                if (_accessToken == null)
                {
                    mainFrame.NavigationService.Navigate(new HomePages.ViewBlog());
                }
                else
                {
                    mainFrame.NavigationService.Navigate(new HomePages.ViewBlog(_accessToken));
                }

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
        /// Updated By: Chris Sheehan 3/24/16
        /// cast content to string
        /// </remarks>
        private void btnSideBar2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar2.Content.ToString() == "Messages")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.AdminMessages(_accessToken));
            }
            else if (btnSideBar2.Content.ToString() == "Insert Recipe")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.RecipeInput(_accessToken));
            }            
            else if (btnSideBar2.Content.ToString() == "Volunteer Sign Up")
            {
                mainFrame.NavigationService.Navigate(new VolunteerPages.VolunteerSignUp(_accessToken));
            }
            
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar3click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar3_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if ("Expert Requests" == btnSideBar3.Content.ToString())
            {
                AdminPages.AdminProcessExpertRequests processExperts
                    = new AdminPages.AdminProcessExpertRequests(_accessToken);

                mainFrame.NavigationService.Navigate(processExperts);
            }
            else if (btnSideBar3.Content.ToString() == "Search for Questions")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.SearchForQuestions(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar4click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar4_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (btnSideBar4.Content.ToString() == "Complete A Task")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.CompleteTask(_accessToken));
            }
            else if (btnSideBar4.Content.ToString() == "Ask a Question")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ExpertAdvice(_accessToken));
            }
            else if (btnSideBar4.Content.ToString().Equals("User Role"))
            {
                mainFrame.NavigationService.Navigate(new UserRole(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar5click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar5.Content.ToString() == "Answer Questions")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ExpertAdviceRespond(_accessToken));
            }
            else if (btnSideBar5.Content.ToString() == "Create a Task")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.ManageTask(_accessToken));
            }
            else if (btnSideBar5.Content.ToString().Equals("User Region"))
            {
                mainFrame.NavigationService.Navigate(new Uri("AdminPages/RegionPage.xaml", UriKind.Relative));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar6click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar6.Content.ToString() == "Upload Garden Template")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ExpertGardenTemplate(_accessToken));
            }
            else if (btnSideBar6.Content.ToString() == "Sign Up for Task")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.SelectTasks(_accessToken));
            }
            
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar7click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar7.Content.ToString() == "View Garden Templates")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ViewGardenTemplate());
            }
            //else if (btnSideBar7.Content.ToString() == "Manage Garden Group")
            if (btnSideBar7.Content.ToString().ToLowerInvariant() == "create garden")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.CreateGarden(_accessToken));
            }
            else if (btnSideBar7.Content.ToString() == "View Garden Templates")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ViewGardenTemplate());
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar8click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar8.Content.ToString() == "View Tasks By Garden")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.ViewTasks(_accessToken));
            }
            //if (btnSideBar8.Content.ToString() == "Plants")
            //{
            //    mainFrame.NavigationService.Navigate(new ExpertPages.ViewPlants());
            //}

            else if (btnSideBar8.Content.ToString() == "View Recipes")
            {
                mainFrame.NavigationService.Navigate(new ExpertPages.ViewRecipe(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar9click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar9.Content.ToString() == "Plants")
            {
                if (_accessToken != null)
                {
                    mainFrame.NavigationService.Navigate(new ExpertPages.ViewPlants(_accessToken));
                }
                else
                {
                    mainFrame.NavigationService.Navigate(new ExpertPages.ViewPlants());
                }
            }
            else if (btnSideBar9.Content.ToString() == "View Garden Tasks")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.ViewGardenTasks(_accessToken));
            }
        }
        /// <summary>
        /// Author: Chris Sheehan
        /// Click logic for the btnsidebar10click event
        /// Date: 3/9/16
        /// </summary>
        private void btnSideBar10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar10.Content.ToString().ToLowerInvariant() == "view groups")
            {
                mainFrame.NavigationService.Navigate(new GardenPages.ViewGroups(_accessToken));
            }
        }
        private void btnSideBar11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnSideBar11.Content.ToString().Equals("Your Groups"))
            {
                mainFrame.NavigationService.Navigate(new GardenPages.GroupMain(_accessToken));
            }
        }
        private void btnSideBar12_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            if (btnSideBar12.Content.ToString().Equals("Request to be a Group Leader"))
            {
                mainFrame.NavigationService.Navigate(new GardenPages.RequestGroupLeader(_accessToken));
            }
        }
        private void btnSideBar13_MouseDown(object sender, MouseButtonEventArgs e)
        {
               if (btnSideBar13.Content.ToString().Equals("Assgin Task to a Member"))
               {
                   mainFrame.NavigationService.Navigate(new GardenPages.AssignTask(_accessToken));
               }
        }
        private void btnSideBar14_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void btnSideBar15_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


    }
}
