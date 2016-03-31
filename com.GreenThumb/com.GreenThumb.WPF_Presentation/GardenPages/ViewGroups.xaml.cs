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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for ViewGroups.xaml
    /// </summary>
    public partial class ViewGroups : Page
    {
        private AccessToken _accessToken;
        private GroupManager _groupMgr = new GroupManager();
        public ViewGroups(AccessToken at)
        {
            InitializeComponent();
            this._accessToken = at;
            PopulateGroupList();
        }


        private void PopulateGroupList()
        {
            try
            {
                var groupList = _groupMgr.GetGroupsForUser(_accessToken.UserID);
                dataGroupList.ItemsSource = groupList.OrderBy(s => s.Name);
            }
            catch (Exception)
            {
                dataGroupList.ItemsSource = null;
            }
        }
    }
}
