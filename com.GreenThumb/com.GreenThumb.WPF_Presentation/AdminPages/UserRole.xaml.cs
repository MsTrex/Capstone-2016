using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BussinessLogic;
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
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for UserRole.xaml
    /// </summary>
    public partial class UserRole : Window
    {
        AccessToken _accessToken = new AccessToken();
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>

        UserManager myUserManager = new UserManager();
        RoleManager myRoleManager = new RoleManager();
        UserRoleManager myUserRoleManager = new UserRoleManager();

     /*   public UserRole(AccessToken accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
            ValidateAccessToken();
            
        } */
     /*   public void ValidateAccessToken()
        {
            while (_accessToken == null)
            {
                _errorMesage = "You are not logged in.";
            }
        } */

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopulateUserGrid(); // Displays User Table data
            PopulateRoleGrid(); // Displays Roles Table Data
            PopulateUserRoleGrid(); // Displays UserRole Table data
        }
        private void PopulateUserGrid()
        {
            try
            {
                var users = myUserManager.GetUserList(Active.active);
                grdUserList.ItemsSource = users;

                var count = myUserManager.GetUserCount(Active.active);
                lblUserCount.Content = "Count: " + count.ToString();
            }
            catch (Exception)
            {
                grdUserList.ItemsSource = null;
                lblUserCount.Content = "Count: 0";
            }
        }

        private void PopulateRoleGrid()
        {
            try
            {
                var roles = myRoleManager.GetRoleList();
                grdRoleList.ItemsSource = roles;

                var count = myRoleManager.GetRoleCount();
                lblRoleCount.Content = "Count: " + count.ToString();
            }
            catch (Exception)
            {
                grdRoleList.ItemsSource = null;
                lblRoleCount.Content = "Count: 0";
            }
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PopulateUserRoleGrid()
        {
            try
            {
                var userRoles = myUserRoleManager.GetUserRoleList();
                grdUserRoleList.ItemsSource = userRoles;

                var count = myUserRoleManager.GetUserRoleCount();
                lblUserRoleCount.Content = "Count: " + count.ToString();
            }
            catch (Exception)
            {
                grdUserRoleList.ItemsSource = null;

                lblUserRoleCount.Content = "Count: 0";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
