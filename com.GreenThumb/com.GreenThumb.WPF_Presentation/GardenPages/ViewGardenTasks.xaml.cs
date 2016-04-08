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
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.WPF_Presentation.GardenPages
{
    /// <summary>
    /// Interaction logic for ViewGardenTasks.xaml
    /// </summary>
    public partial class ViewGardenTasks : Page
    {
        private string category = "";
        private JobManager jobManager = new JobManager();
        AccessToken accessToken = new AccessToken();



        public ViewGardenTasks(AccessToken _accessToken)
        {
            accessToken = _accessToken;
            InitializeComponent();
        }

        private void selectGarden1(object sender, RoutedEventArgs e)
        {

            category = "Roof Top garden1";
            DisplayTaskData();

        }

        private void selectGarden2(object sender, RoutedEventArgs e)
        {
            category = "Roof Top garden2";
            DisplayTaskDataAll();
        }

        private void grdTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DisplayTaskData()
        {

            try
            {
                var jobs = jobManager.FetchGarden1();

                grdTasks.ItemsSource = jobs;
            }
            catch (Exception)
            {

            }
        }
        private void DisplayTaskDataAll()
        {

            try
            {
                var jobs = jobManager.FetchGarden2();

                grdTasks.ItemsSource = jobs;
            }
            catch (Exception)
            {

            }
        }

        private void grdAdmins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
