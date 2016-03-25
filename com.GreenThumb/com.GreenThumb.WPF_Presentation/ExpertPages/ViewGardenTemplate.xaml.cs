using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ViewGardenTemplate.xaml
    /// </summary>
    public partial class ViewGardenTemplate : Page
    {
        private GardenTemplateManager manager = new GardenTemplateManager();
        private List<GardenTemplate> templateList;
        private string selectedTemplate;
        public ViewGardenTemplate()
        {
            templateList = manager.GetTemplateList();
            InitializeComponent();
            for (int i = 0; i < templateList.Count; i++)
            {
                cmbTemplateName.Items.Add(templateList[i].TemplateName);
            }

        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (selectedTemplate != "" && selectedTemplate != null)
            {
                try
                {
                    var data = manager.LoadTemplate(selectedTemplate);
                    var stream = new MemoryStream(data);

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    ImgTemplate.Source = bitmap;
                }
                catch(Exception ex)
                {
                    lblError.Content = "Error loading image.";
                }
            }


        }



        private void cmbTemplateName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTemplate = cmbTemplateName.SelectedValue.ToString();
        }
    }
}
