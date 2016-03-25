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

/// <summary>
/// Retrieve and select a task for a garden
/// Created By: Nasr Mohammed 3/4/2016 
/// </summary>

namespace com.GreenThumb.WPF_Presentation
{
    /// <summary>
    /// Interaction logic for ManageTask.xaml
    /// </summary>
    public partial class ManageTask : Page
    {
        private JobManager jobManager = new JobManager();

        public ManageTask()
        {
            InitializeComponent();
        }

        public bool saveDetails()
        {
            bool myBool = false;

            try
            {
                Job newJob = new Job();

                newJob.GardenID = int.Parse(this.txtGardenID.Text);
                newJob.Description = this.txtTaskDescription.Text;
                newJob.DateAssigned = DateTime.Now;
                newJob.DateCompleted = DateTime.Now;
                newJob.AssignedTo = int.Parse(this.txtAssignedTo.Text);
                newJob.AssignedFrom = int.Parse(this.txtAssignedFrom.Text);
                newJob.UserNotes = this.txtuserNotes.Text;

                myBool = jobManager.AddNewTask(newJob);

            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }

            return myBool;
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {



            bool myBool = saveDetails();
            if (myBool == true)
            {
                MessageBox.Show("Your record created succssfully!");
                DisplayTaskData();
                txtAssignedFrom.Clear();
                txtAssignedTo.Clear();
                txtDateAssigned.Clear();
                txtDateCompleted.Clear();
                txtGardenID.Clear();
                txtTaskDescription.Clear();
                txtuserNotes.Clear();

            }
            else if (myBool == false)
            {
                MessageBox.Show("Your record has not been created succssfully, something went wrong!");
            }


        }

        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            DisplayTaskData();
        }

        private void grdTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void DisplayTaskData()
        {

            try
            {
                var job = jobManager.GetTaskList();

                grdTasks.ItemsSource = job;


            }
            catch (Exception)
            {

            }
        }
    }
}
