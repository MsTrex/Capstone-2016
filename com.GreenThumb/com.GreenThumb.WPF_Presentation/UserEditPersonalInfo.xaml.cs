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

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Author: Chris Schwebach
    /// Interaction logic for UserEditPersonalInfo.xaml
    /// Date: 3/3/16
    /// </summary>
    public partial class UserEditPersonalInfo : Window
    {
        private UserManager myUserManager = new UserManager();

        private AccessToken _accessToken;
        public UserEditPersonalInfo(AccessToken _accessToken)
        {
            this._accessToken = _accessToken;

            InitializeComponent();

            DisplayPersonalInfo();

            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();
            txtRegionID.Clear();
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

        private void btnReturnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow _mainWindow = new MainWindow();
            _mainWindow.Show();
            Close();
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow _mainWindow = new MainWindow();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtZip.Clear();
            txtEmailAddress.Clear();
            txtRegionID.Clear();
            _mainWindow.Show();
            Close();
        }

        private void btnEditPersonalInfo_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string zip = txtZip.Text;
            string emailAddress = txtEmailAddress.Text;
            string regionIDText = txtRegionID.Text;
            int? regionID;
            int numRegionID;

            if (regionIDText == "")
            {
                try
                {
                    myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, null);
                    DisplayPersonalInfo();
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtZip.Clear();
                    txtEmailAddress.Clear();
                    txtRegionID.Clear();
                    lblRegionIDError.Content = "";
                    MessageBox.Show("Profile changed!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            if (int.TryParse(regionIDText, out numRegionID))
            {
                try
                {
                    regionID = numRegionID;
                    myUserManager.EditUserPersonalInfo(_accessToken.UserID, firstName, lastName, zip, emailAddress, regionID);
                    DisplayPersonalInfo();
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtZip.Clear();
                    txtEmailAddress.Clear();
                    txtRegionID.Clear();
                    lblRegionIDError.Content = "";
                    MessageBox.Show("Profile changed!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (regionIDText != "")
            {
                lblRegionIDError.Content = "RegionID must be a numeric value!";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayPersonalInfo();
        }


    }
}
