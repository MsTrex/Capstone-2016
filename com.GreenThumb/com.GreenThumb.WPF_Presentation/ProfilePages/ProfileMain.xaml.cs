using System.Windows.Controls;
﻿using System;
﻿using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for ProfileMain.xaml
    /// </summary>
    public partial class ProfileMain : Page
    {
        /// <summary>
        /// Author: Chris Sheehan
        /// Logic for ProfileMain page
        /// Date: 4/14/16
        /// </summary>
        public ProfileMain(AccessToken _accessToken)
        {
            InitializeComponent();
            if (_accessToken != null)
            {
                //_accessToken.Roles
                grdRoles.ItemsSource = _accessToken.Roles;
                grdRoles.IsHitTestVisible = false;
                //lblTest.Content = "Hello " + _accessToken.FirstName + " " + _accessToken.LastName + ", welcome to the profile tab main page!";
            }
            else
            {
                //lblTest.Content = "Hello welcome to the profile tab main page!";
            }
            //lblTitle.Content = "Profile Main Page "; 
        }
    }
}
