﻿using System;
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

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 4/7/16
    /// Interaction logic for AddNutrientsToPlant.xaml
    /// </summary>
    public partial class AddNutrientsToPlant : Page
    {
        private Plant _plant;
        private Nutrient _nutrient;
        private AccessToken _accessToken;
        private NutrientManager nutrientManager = new NutrientManager();
        private RoleManager roleManager = new RoleManager();
        private bool hasAuthority = false;
        public AddNutrientsToPlant(AccessToken accessToken, Plant plant)
        {
            InitializeComponent();

            _accessToken = accessToken;
            _plant = plant;
            lblNutrients.Text = "Nutrients for " + plant.Name;

            CheckForAuthority();
            ManageAddVisibility();
            FillNutrients();
            FillPlantNutrients();
        }

        private void FillNutrients()
        {
            try
            {
                cmbNutrients.ItemsSource = nutrientManager.RetrieveNutrients();
                cmbNutrients.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                cmbNutrients.Items.Add(ex.Message);
                cmbNutrients.SelectedIndex = 0;
            }

        }

        private void FillPlantNutrients()
        {
            try
            {
                icNutrients.ItemsSource = nutrientManager.RetrievePlantNutrients(_plant.PlantID);
            }
            catch (Exception ex)
            {
                icNutrients.ItemsSource = new List<Object>() { new { Name = ex.Message } };
            }
        }

        private void ManageAddVisibility()
        {
            if (hasAuthority)
            {
                grdAdd.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                grdAdd.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void CheckForAuthority()
        {
            if (roleManager.IsUserThisRole(_accessToken, "Expert") ||
                roleManager.IsUserThisRole(_accessToken, "Admin"))
            {
                hasAuthority = true;
            }
            else
            {
                hasAuthority = false;
            }
        }

        private void cmbNutrients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblError.Content = "";
            try
            {
                _nutrient = (Nutrient)cmbNutrients.SelectedItem;
            }
            catch (Exception) { }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!nutrientManager.AddNutrientToPlant(_nutrient.NutrientID, _plant.PlantID))
                {
                    lblError.Content = "Nutrient could not be added";
                }
                else
                {
                    lblError.Content = "";
                }
            }
            catch
            {
                lblError.Content = "Nutrient could not be added";
            }

            FillPlantNutrients();
        }
    }
}