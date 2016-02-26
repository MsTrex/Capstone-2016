using BusinessLogic;
using BusinessObjects;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for NewUserCreation.xaml
    /// </summary>
    public partial class NewUserCreation : Window
    {
        static AccessToken _accessToken;
        public NewUserCreation()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string username = this.txtnewUsername.Text;
            string password = this.txtnewPassword.Text;
            string passConfirm = this.txtPassConfirm.Text;
            try
            {

                _accessToken = Security.ValidateNewUser(username, password);
                this.DialogResult = true;
                MessageBox.Show("Created New User. Please Log in.");

                if (password == passConfirm)
                {
                    this.DialogResult = true;
                    NewUserInformation _newInfo = new NewUserInformation();
                    _newInfo.ShowDialog();
                    _accessToken = Security.ValidateNewUser(username, password);
                    this.DialogResult = true;
                    MessageBox.Show("Created New User. Please Log in.");
                    this.Close();

                }
                else
                {
                    txtnewPassword.Text = "Passwords Don't Match!";
                    txtPassConfirm.Text = "Passwords Don't Match!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
