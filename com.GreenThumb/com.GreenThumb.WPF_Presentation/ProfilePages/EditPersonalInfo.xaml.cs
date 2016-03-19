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

namespace com.GreenThumb.WPF_Presentation.ProfilePages
{

    ///<summary>
    ///Author: Chris Schwebach
    ///Interaction logic for EditPersonalInfo.xaml
    ///Date: 3/3/16
    ///Updated Date: 3/19/16
    ///Changed from a window to a page
    ///</summary>
    public partial class EditPersonalInfo : Page
    {

        private UserManager myUserManager = new UserManager();

        private AccessToken _accessToken;

        private int? regionId;

        public EditPersonalInfo(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;

            InitializeComponent();

            DisplayPersonalInfo();

            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();

            txtFirstName.Text = _accessToken.FirstName;
            txtLastName.Text = _accessToken.LastName;
            txtZip.Text = _accessToken.Zip;
            txtEmailAddress.Text = _accessToken.EmailAddress;

        }

        private void Na_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 10;
        }

        private void None_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = null;
        }

        private void one_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 1;
        }

        private void two_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 2;
        }

        private void three_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 3;
        }

        private void four_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 4;
        }

        private void five_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 5;
        }

        private void six_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 6;
        }

        private void seven_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 7;
        }

        private void eight_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 8;
        }

        private void nine_Item_Selected(object sender, RoutedEventArgs e)
        {
            regionId = 9;
        }
        private void DisplayPersonalInfo()
        {
            try
            {
                var user = myUserManager.GetPersonalInfo(_accessToken.UserID);
                grdPersonalInfo.ItemsSource = user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();

        }

        private void btnEditPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string zip = txtZip.Text;
            string emailAddress = txtEmailAddress.Text;

            try
            {

                myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, regionId);
                DisplayPersonalInfo();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtZip.Clear();
                txtEmailAddress.Clear();
                MessageBox.Show("Profile changed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
