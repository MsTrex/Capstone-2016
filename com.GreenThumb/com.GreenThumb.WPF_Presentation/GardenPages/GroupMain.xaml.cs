﻿using com.GreenThumb.BusinessLogic;
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
    /// Ryan Taylor
    /// Created: 03/22/16
    /// </summary>
    public partial class GroupMain : Page
    {
        private AccessToken _accessToken;
        private GroupManager _groupMgr = new GroupManager();
        public GroupMain(AccessToken at)
        {
            InitializeComponent();
            this._accessToken = at;
            PopulateGroupList();
        }

        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            string groupName = this.txtGroupName.Text;

            if (groupName.Length > 1 && groupName.Length <= 100)
            {
                try
                {
                    if (_groupMgr.AddGroup(_accessToken.UserID, groupName))
                    {
                        PopulateGroupList();
                        this.txtGroupName.Clear();
                        this.lblSuccess.Content = "Success!";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// This method will populate the data grid with all the groups the current user in in
        /// </summary>
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

        private void txtGroupName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.lblSuccess.Content = "";
        }
    }
}
