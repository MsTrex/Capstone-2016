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
        private AccessToken _accessToken;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.NavigationService.Navigate(new Uri("HomeContent.xaml", UriKind.Relative));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            _login.ShowDialog();
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

        

        /// <summary>
        /// Author: Chris Schwebach
        /// Interaction logic for UserEditPersonalInfo.xaml
        /// Date: 3/3/16
        /// </summary>
        /// 


        private void Button_Click_PersonalInfo(object sender, RoutedEventArgs e)
        {
            UserEditPersonalInfo _userEditPersonalInfo = new UserEditPersonalInfo(_accessToken);
            _userEditPersonalInfo.ShowDialog();
            Close();
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
    }
}
