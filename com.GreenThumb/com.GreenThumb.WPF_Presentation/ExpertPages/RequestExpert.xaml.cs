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
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;


namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    ///<summary>
    ///Author: Stenner Kvindlog         
    ///gets title and description for expert status request
    ///Date: 3/19/16
    ///</summary>
    public partial class RequestExpert : Page
    {
        AccessToken CurrentUser = new AccessToken();
        String Title;
        String Description;
        int userID;
        DateTime Time;

        public RequestExpert(AccessToken ax)
        {
            InitializeComponent();
            userID = CurrentUser.UserID;
            Time = DateTime.Now;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            // save and submit to database 
            Title = this.title.Text;
            Description = this.description.Text;

            AdminExpertRequestsManager myAdminExpertRequestsManager = new AdminExpertRequestsManager(CurrentUser);

            myAdminExpertRequestsManager.AddExpertApplication(Title, Description, userID, Time);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // send user back to pervious page      
            this.NavigationService.Navigate(new ExpertPages.ExpertHome(CurrentUser));
        }
 
    }
}
