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

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Interaction logic for ViewPlants.xaml
    /// </summary>
    public partial class ViewPlants : Page
    {
        AccessToken accessToken = null;
        PlantManager plantManager = new PlantManager();
        List<Plant> plants = new List<Plant>();
        List<Plant> minPlants = new List<Plant>();
        bool hasAuthority = false;
        RoleManager roleManager = new RoleManager();

        public ViewPlants()
        {
            InitializeComponent();
            setupPage();
        }

        public ViewPlants(AccessToken accessToken)
        {
            this.accessToken = accessToken;
            InitializeComponent();
            setupPage();
            foreach (Role role in accessToken.Roles) {
                if (role.RoleID == "Admin")
                {
                    btnCreatePlant.Visibility = Visibility.Visible;
                }
            }
        }

        public void setupPage()
        {
            //Name Search
            plants = plantManager.FetchPlantList(Active.all);
            //plants = plantManager.CreateTestPlants();
                //List<String> plantNames = new List<String>();
                //foreach(Plant plant in plants){
                //    plantNames.Add(plant.Name);
                //}
                //acPlants.ItemsSource = plantNames;
            dgrdNameSearch.ItemsSource = plants;

            //Category Search
            newImage(img00, "Fruit");
            newImage(img01, "Vegetables");
            newImage(img02, "Herbs");
            newImage(img03, "Flowers");
            //newImage(img10, "Trees");
            //newImage(img11, "Annuals");
            //newImage(img12, "Perennials");
            //newImage(img13, "Bushes");

            //Region Search
            newImage(imgRegion, "RegionMap");
        }

        private void newImage(Image image, String imageName)
        {
            if (imageName == null)
            {
                image.Source = null;
            }
            else
            {
                try { 
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/Plants/"+imageName+".png"));
                }
                catch
                {
                    image.Source = null;
                }
            }
        }

        public void showGrid(Grid visibleGrid)
        {
            grdCategories.Visibility = Visibility.Hidden;
            grdName.Visibility = Visibility.Hidden;
            grdPlantList.Visibility = Visibility.Hidden;
            grdRegion.Visibility = Visibility.Hidden;

            visibleGrid.Visibility = Visibility.Visible;
        }

        public void setUpMinPlants(String plant, String type="Category")
        {
            switch (type)
            {
                case "Name":
                    minPlants = plants.Where(p => p.Name.Equals(plant)).ToList();
                    break;
                case "Region":
                    //minPlants = plants.Where(p => p..Equals(plant));
                    break;
                default: //"Category":
                    //minPlants = from plant in plants where plant.Category == plantCategory select plant;
                    minPlants = plants.Where(p => p.Category.Equals(plant)).ToList(); //.OrderBy(p => p);
                    break;
            }

            icPlants.ItemsSource = minPlants;
            showGrid(grdPlantList);
        }

        private void btnCategory0_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Fruit");
        }

        private void btnCategory1_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Vegetable");
        }

        private void btnCategory2_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Herb");
        }

        private void btnCategory3_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Flower");
        }

        private void btnCategory4_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Tree");
        }

        private void btnCategory5_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Annual");
        }

        private void btnCategory6_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Perennial");
        }

        private void btnCategory7_Click(object sender, RoutedEventArgs e)
        {
            setUpMinPlants("Bush");
        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            showGrid(grdName);
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            showGrid(grdCategories);
        }

        private void btnRegion_Click(object sender, RoutedEventArgs e)
        {
            showGrid(grdRegion);
        }

        private void btnCreatePlant_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExpertPages.CreatePlant(accessToken));
        }

        //private void btnNameSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    setUpMinPlants(acPlants.Text,"Name");
        //    showGrid(grdPlantList);
        //}

        //Copied from SearchForQuestions page
        private void txtNamePlantSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                minPlants = new List<Plant>();
                foreach(Plant plant in plants){
                    if (plant.Name.ToLower().Contains(txtNamePlantSearch.Text.ToLower()))
                    {
                        minPlants.Add(plant);
                    }
                }
                dgrdNameSearch.ItemsSource = minPlants;
                //Question question = (Question)gridQuestions.SelectedItem;
                //ChangeQuestionAndResponses(question.QuestionID);
            }
            catch (Exception) { }

            ChangeGridVisibility();
        }

        //Copied from SearchForQuestions page
        private void ChangeGridVisibility()
        {
            if (dgrdNameSearch.Items.Count > 0)
            {
                dgrdNameSearch.Visibility = System.Windows.Visibility.Visible;
                //lblNoMatch.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                dgrdNameSearch.Visibility = System.Windows.Visibility.Collapsed;
                //lblNoMatch.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void dgrdNameSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            minPlants = new List<Plant>();
            minPlants.Add((Plant) e.AddedItems[0]);
            icPlants.ItemsSource = minPlants;
            showGrid(grdPlantList);
        }

        private void btnAddNutrient_Click(object sender, RoutedEventArgs e)
        {
            Plant plant = ((Button)sender).Tag as Plant;
            this.NavigationService.Navigate(new ExpertPages.AddNutrientsToPlant(accessToken, plant));
        }

        private void btnAddNutrient_Loaded(object sender, RoutedEventArgs e)
        {
            CheckForAuthority();

            if (hasAuthority)
            {
                Button button = ((Button)sender) as Button;
                button.Content = "Add/View Nutrients";
            }
            else
            {
                Button button = ((Button)sender) as Button;
                button.Content = "View Nutrients";
            }
        }

        private void CheckForAuthority()
        {
            if (roleManager.IsUserThisRole(accessToken, "Expert") ||
                roleManager.IsUserThisRole(accessToken, "Admin"))
            {
                hasAuthority = true;
            }
            else
            {
                hasAuthority = false;
            }
        }
    }
}
