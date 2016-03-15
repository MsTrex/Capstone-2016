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

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for CreateGarden.xaml
    /// </summary>
    public partial class CreateGarden : Page
    {
        public CreateGarden(AccessToken _accessToken)
        {
            InitializeComponent();
            if (_accessToken != null)
            {
                lblMessage.Content = _accessToken.FirstName + " " + _accessToken.LastName + " Welcome to the the Create Garden page";
            }
        }
    }
}
