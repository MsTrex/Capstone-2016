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
using System.Windows.Shapes;

namespace com.GreenThumb.WPF_Presentation.AdminPages
{
    /// <summary>
    /// Interaction logic for ProfileMenu.xaml
    /// Added by Ibrahim Abuzaid 04-15-2016
    /// </summary>
    public partial class AdminUserProfile : Page
    {
        UserManager usrMgr = new UserManager();
        UserRoleManager usrRoleMgr = new UserRoleManager();
        GroupManager grpMgr = new GroupManager();
        int userId;

        private AccessToken _accessToken;

        public AdminUserProfile(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;
            //      ProfileMenu profMenu = new ProfileMenu(_accessToken);
            InitializeComponent();
            listUsers();
            populateUser();
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
        }

        private void listUsers()
        {
           frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdUserNames.Visibility = Visibility.Visible;
            try
            {
                
                var users = usrMgr.GetUserList(Active.active);
                grdUserNames.ItemsSource = users;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Users table.... failure here : " + ex.Message + ex.StackTrace);
            }
        
        }

        private void populateUser()
        {

            try
            {

                var users = usrMgr.GetPersonalInfo(userId);
          

                if (users == null)
                {
                    lblMessage.Foreground = Brushes.Red;
                    lblMessage.Content = "Users NO: " + userId + "  Not Found in DataBase, try again";
                }
                else
                {
                    lblFirstName.Content = users.FirstName;
                    lblLastName.Content = users.LastName;
                    lblZip.Content = users.Zip;
                    lblMail.Content = users.EmailAddress;
                    lblUserName.Content = users.UserName;
                    lblRegion.Content = users.RegionId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER..... failure here : " + ex.Message + ex.StackTrace);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var users = usrMgr.GetPersonalInfo(userId);
            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmEdit.Visibility = Visibility.Visible;
            txtFirstName.Text = users.FirstName;
            txtLastName.Text = users.LastName;
            txtZip.Text = users.Zip;
            txtEmail.Text = users.EmailAddress;
            txtRegion.Text = users.RegionId.ToString();
            txtRegion.IsEnabled = false;
            txtUserName.Text = users.UserName;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var users = usrMgr.GetPersonalInfo(userId);
            User user = new User();
            frmEdit.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Visible;

            if (txtOldPassword.Password != null && txtOldPassword.Password != users.Password)
            {
                lblMessage.Content = "Invalid old Password";
            }

            if (txtNewPassword2.Password == null || txtNewPassword1.Password == null)
            {
                lblMessage.Content = "enter new Password twice";
            }

            if (txtNewPassword2.Password != txtNewPassword1.Password)
            {
                lblMessage.Content = "new Password doesn't match!";
            }

        }
        private void btnPasswordSave_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            try
            {
                var res = usrMgr.EditPasssword(txtUserName.Text, txtOldPassword.Password, txtNewPassword1.Password);
                lblMessage.Content = txtUserName.Text + "  " + txtOldPassword.Password + "   " + txtNewPassword1.Password;
                if (res == true)
                {
                    lblMessage.Content = "Operation Succeeded. ";

                }
                else
                {
                    lblMessage.Content = "Operation failed. ";
                }
            }
            catch (Exception)
            {
                lblMessage.Content = "Operation Failed, check out!";
            }
            finally
            {
                txtOldPassword.Password = "";
                txtNewPassword1.Password = "";
                txtNewPassword2.Password = "";
                frmPassword.Visibility = Visibility.Hidden;
                populateUser();
            }
        }

        private void btnUserRoles_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Visible;
            try
            {
                
                var userRoles = usrRoleMgr.GetUserRoleListByUser(userId);
                grdUserRoleList.ItemsSource = userRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER Role..... failure here : " + ex.Message + ex.StackTrace);
            }
        }


        private void btnGarden_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;
            frmPassword.Visibility = Visibility.Hidden;
            frmRole.Visibility = Visibility.Hidden;
            grdMap.Visibility = Visibility.Hidden;
            grdGarden.Visibility = Visibility.Visible;
            try
            {
                
                var groupManager = grpMgr.GetGroupsForUser(userId);
                grdGarden.ItemsSource = groupManager;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Garden..... failure here : " + ex.Message + ex.StackTrace);
            }
            grdMap.Visibility = Visibility.Visible;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.UserID = userId;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Zip = txtZip.Text;
            user.EmailAddress = txtEmail.Text;
            user.UserName = txtUserName.Text;

            if (txtRegion.Text.Trim() == "" || txtRegion.Text == null)
            {
                user.RegionId = null;
            }
            else
            {
                user.RegionId = int.Parse(txtRegion.Text);
            }

            try
            {
                var res = usrMgr.EditUserPersonalInfo(user.UserID, user.FirstName, user.LastName,
                         user.Zip, user.EmailAddress, user.RegionId);
                if (res == true)
                {

                    lblMessage.Content = "Operation Succeeded. ";
                    //_accessToken.FirstName = user.FirstName;
                    //_accessToken.LastName = user.LastName;
                    //_accessToken.UserName = user.UserName;
                    //_accessToken.EmailAddress = user.EmailAddress;
                    //_accessToken.Zip = user.Zip;
                    //_accessToken.RegionId = user.RegionId;
                    populateUser();
                }
                else
                {
                    lblMessage.Content = "Operation failed. ";
                }


            }
            catch (Exception)
            {
                lblMessage.Content = "Operation Failed, check out!";
            }
            finally
            {


            }
        }
        public void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frmEdit.Visibility = Visibility.Hidden;
        }
        public void btnPasswordBack_Click(object sender, RoutedEventArgs e)
        {
            frmPassword.Visibility = Visibility.Hidden;
        }
        public void DataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            frmPassword.Visibility = Visibility.Hidden;
        }

      
   
        private void btnShowProfile_Click(object sender, RoutedEventArgs e)
        {
            bool res = Int32.TryParse(txtUserId.Text, out userId);
            populateUser();
        }

        private void txtUserId_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool res = Int32.TryParse(txtUserId.Text, out userId);
        }

        private void grdUserNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                    
    //            txtUserId.Text = (grdUserNames.SelectedIndex + 1).ToString();

                bool res = Int32.TryParse(txtUserId.Text, out userId);
        }

        private void btnSelectUser_Click(object sender, RoutedEventArgs e)
        {
   //         txtUserId.Text = (grdUserNames.SelectedIndex + 1).ToString();
           
    //        bool res = Int32.TryParse(txtUserId.Text, out userId);
            
    //        lblMessage.Content = "display: " + userId + " " + txtUserId.Text;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (regions.SelectedIndex + 1 == 11)
            {
                txtRegion.Text = "";
            }
            else
            {
                txtRegion.Text = (regions.SelectedIndex + 1).ToString();
            }
        }

        private void grdUserNames_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView lv = (ListView)sender;
                User test = (User)lv.SelectedItem;
                userId = test.UserID;
                txtUserId.Text = userId.ToString();
                populateUser();
                frmEdit.Visibility = Visibility.Hidden;
                frmPassword.Visibility = Visibility.Hidden;
                frmRole.Visibility = Visibility.Hidden;
                grdMap.Visibility = Visibility.Hidden;
                grdGarden.Visibility = Visibility.Hidden;

            }
            catch (Exception ex) { }
        }

    }
}
