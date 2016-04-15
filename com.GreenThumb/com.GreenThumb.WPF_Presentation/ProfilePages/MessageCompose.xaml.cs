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

        public MessageCompose(AccessToken accessToken)
        {
            _accessToken = accessToken;
            InitializeComponent();
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
            if (!this.txtTo.Text.Equals(""))
            {
                Message msg = new Message()
                {
                    MessageSender = _accessToken.UserName,
                    MessageReceiver = this.txtTo.Text,
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
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Messages(_accessToken));
        }


    }
}
