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
        public ProfileMain(AccessToken _accessToken)
        {
            InitializeComponent();
            if (_accessToken != null)
            {
                lblTest.Content = "Hello " + _accessToken.FirstName + "welcome to the profile tab main page!";
            }
            lblTitle.Content = "Profile Main Page " + _accessToken.FirstName; 
        }
    }
}
