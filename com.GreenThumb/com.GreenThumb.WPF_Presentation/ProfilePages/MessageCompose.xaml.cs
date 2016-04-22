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
    /// <summary>
    /// Ryan Taylor
    /// Created: 4/14/2016
    /// Interaction logic for MessageCompose.xaml
    /// </summary>
    public partial class MessageCompose : Page
    {
        private AccessToken _accessToken;
        private MessageManager _mgr = new MessageManager();
        private List<string> _userList;
        private string _user;
        public MessageCompose(AccessToken accessToken)
        {
            _accessToken = accessToken;
            InitializeComponent();
            FillUserList();
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 4/14/2016
        /// 
        /// Create A message and send it to a user if username is found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (_userList.Contains(_user))
            {
                Message msg = new Message()
                {
                    MessageSender = _accessToken.UserName,
                    MessageReceiver = _user,
                    MessageContent = this.txtContent.Text,
                    MessageSubject = this.txtSubject.Text
                };

                try
                {
                    if (_mgr.SendMessage(msg.MessageContent, msg.MessageSubject, msg.MessageSender, msg.MessageReceiver))
                    {
                        MessageBox.Show("Message Sent Successfully.");
                        this.NavigationService.Navigate(new Messages(_accessToken));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.txtTo.Focus();
                }

            }
            else
            {
                lblError.Content = "Not a valid username";
            }
        }

        private void FillUserList()
        {
            try
            {
                _userList = _mgr.GetUserNames();
                txtTo.ItemsSource = _userList;
                txtTo.ToolTip = "Select a username";
            }
            catch (Exception)
            {
                txtTo.ToolTip = "No users added yet";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Messages(_accessToken));
        }

        private void txtTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblError.Content = "";
            try
            {
                _user = txtTo.SelectedItem.ToString();
            }
            catch (Exception)
            {
                _user = null;
            }
        }

        private void txtTo_LostFocus(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            if (!_userList.Contains(_user))
            {
                lblError.Content = "Not a valid username";
            }
        }

    }
}
