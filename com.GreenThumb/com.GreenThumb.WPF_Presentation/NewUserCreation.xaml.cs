using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
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
using com.GreenThumb.BusinessLogic.Interfaces;
using System.Text.RegularExpressions;

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for NewUserCreation.xaml
    /// </summary>
    public partial class NewUserCreation : Window
    {
        private UserManager _userManagerObj = new UserManager();
        public NewUserCreation()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string fName = this.txtFName.Text;
            string lName = this.txtLName.Text;
            string username = this.txtnewUsername.Text;
            string password = this.txtnewPassword.Password;
            string passConfirm = this.txtPassConfirm.Password;
            bool isActive = true;
            bool regexFnameFailed = false;
            bool regexLnameFailed = false;
            bool regexPasswordFailed = false;
            try
            {
                if (!string.IsNullOrEmpty(fName) && !string.IsNullOrEmpty(lName) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(passConfirm))
                {
                    if (Regex.IsMatch(fName, @"(?i)^[a-z]+"))
                    {
                        regexFnameFailed = false;
                    }
                    else
                    {
                        regexFnameFailed = true;
                    }

                    if (Regex.IsMatch(lName, @"(?i)^[a-z]+"))
                    {
                        regexLnameFailed = false;
                    }
                    else
                    {
                        regexLnameFailed = true;
                    }

                    if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$"))
                    {
                        regexPasswordFailed = false;
                    }
                    else
                    {
                        regexPasswordFailed = true;
                    }

                    if (!regexFnameFailed && !regexLnameFailed && !regexPasswordFailed)
                    {
                        if (password != passConfirm)
                            MessageBox.Show("Passwords dont match!");
                        else
                        {
                            if (username.Length < 5)
                            {
                                MessageBox.Show("Username should be atleast 5 characters long");
                            }
                            else
                            {
                                if (_userManagerObj.AddNewUser(fName, lName, string.Empty, string.Empty, username, password.HashSha256(), isActive, null) == 1)
                                {
                                    ClearControls();
                                    MessageBox.Show("User has been created successfully!!");
                                }
                                else
                                {
                                    txtnewUsername.Text = string.Empty;
                                    MessageBox.Show("Username entered already exists. Please try a different username.");

                                }
                            }
                        }
                    }
                    else
                    {
                        if (regexFnameFailed)
                            MessageBox.Show("Please enter only characters in first name");
                        else if (regexLnameFailed)
                            MessageBox.Show("Please enter only characters in last name");
                        else
                            MessageBox.Show("Password should contain 1 uppercase, 1 lowercase, 1 digit and a special character and should be minimum 6 characters long.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter all the fields");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            ClearControls();
        }

        private void ClearControls()
        {
            this.txtFName.Text = string.Empty;
            this.txtLName.Text = string.Empty;
            this.txtnewUsername.Text = string.Empty;
            this.txtnewPassword.Password = string.Empty;
            this.txtPassConfirm.Password = string.Empty;
        }
    }
}
