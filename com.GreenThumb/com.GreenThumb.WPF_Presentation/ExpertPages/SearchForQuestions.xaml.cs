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

namespace com.GreenThumb.WPF_Presentation.ExpertPages
{
    /// <summary>
    /// Interaction logic for SearchForQuestions.xaml
    /// </summary>
    public partial class SearchForQuestions : Page
    {
        AccessToken _accessToken = new AccessToken();
        QuestionManager questionManager = new QuestionManager();
        ResponseManager responseManager = new ResponseManager();
        UserManager userManager = new UserManager();
        bool hasChangedQuestion = false;
        public SearchForQuestions(AccessToken accessToken)
        {
            InitializeComponent();

            _accessToken = accessToken;
            ValidateAccessToken();
        }

        public SearchForQuestions(AccessToken accessToken, Question question)
        {
            InitializeComponent();

            _accessToken = accessToken;
            ValidateAccessToken();
            cmbMyQuestions.SelectedIndex = cmbMyQuestions.Items.Count - 1;
            ChangeQuestionAndResponses(question.QuestionID);
            lblNoReplies.Content = "Your question has been successfully submitted. Come back later to check replies.";
            lblContent.Text = question.Content;
            lblQuestion.Content = userManager.RetrieveUser(question.CreatedBy).UserName + " asks...";
        }

        private void ValidateAccessToken()
        {
            List<Question> questions = new List<Question>();

            if (_accessToken != null)
            {
                questions = questionManager.GetQuestionsByUserID(_accessToken.UserID);
                if (questions.Count > 0)
                {
                    gridMyQuestions.Visibility = System.Windows.Visibility.Visible;
                    cmbMyQuestions.ItemsSource = questions;
                    cmbMyQuestions.SelectedIndex = 0;
                }
                else
                {
                    gridMyQuestions.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                gridMyQuestions.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void txtKeywords_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                gridQuestions.ItemsSource = questionManager.GetQuestionsWithKeyword(txtKeywords.Text);
            }
            catch (Exception)
            {
                
            }

            ChangeGridVisibility();
        }

        private void gridQuestions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Question question = (Question)gridQuestions.SelectedItem;
                ChangeQuestionAndResponses(question.QuestionID);
            }
            catch(Exception)
            {

            }
        }

        private void ChangeGridVisibility()
        {
            if (gridQuestions.Items.Count > 0)
            {
                gridQuestions.Visibility = System.Windows.Visibility.Visible;
                lblNoMatch.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridQuestions.Visibility = System.Windows.Visibility.Collapsed;
                lblNoMatch.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangeQuestionAndResponses(int questionID)
        {
            gridQuestion.Visibility = System.Windows.Visibility.Visible;
            Question question = questionManager.GetQuestionByID(questionID);
            lblContent.Text = question.Content;
            lblQuestion.Content = userManager.RetrieveUser(question.CreatedBy).UserName + " asks...";
            List<Response> responses = new List<Response>();

            try
            {
                responses = responseManager.GetResponsesByQuestionID(questionID);
            }
            catch(Exception)
            {
                lblNoReplies.Visibility = System.Windows.Visibility.Visible;
            }


            if (responses.Count > 0)
            {
                gridResponses.Visibility = System.Windows.Visibility.Visible;
                gridNoResponses.Visibility = System.Windows.Visibility.Collapsed;

                List<UIElement> elements = new List<UIElement>();

                foreach(UIElement e in gridResponses.Children)
                {
                    elements.Add(e);
                }

                foreach (UIElement e in elements)
                {
                    gridResponses.Children.Remove(e);
                }

                int i = 0;
                foreach (Response response in responses)
                {
                    Label lblName = new Label();
                    lblName.Content = userManager.RetrieveUser(response.UserID).UserName + " posted...";
                    lblName.SetValue(Grid.ColumnProperty, 0);
                    lblName.SetValue(Grid.RowProperty, i);
                    lblName.Margin = new Thickness(20.0, 20.0, 20.0, 0.0);

                    TextBlock lblResponse = new TextBlock();
                    lblResponse.Text = response.UserResponse;
                    lblResponse.TextWrapping = TextWrapping.Wrap;

                    Border border = new Border();
                    border.BorderThickness = new Thickness(1.0);
                    border.BorderBrush = System.Windows.Media.Brushes.Black;
                    border.MaxHeight = 90;
                    border.Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
                    border.SetValue(Grid.ColumnProperty, 0);
                    i++;
                    border.SetValue(Grid.RowProperty, i);
                    border.Background = System.Windows.Media.Brushes.White;
                    border.Padding = new Thickness(5.0);

                    border.Child = lblResponse;

                    gridResponses.RowDefinitions.Add(new RowDefinition());
                    gridResponses.RowDefinitions.Add(new RowDefinition());
                    gridResponses.Children.Add(lblName);
                    gridResponses.Children.Add(border);
                    i++;
                }
            }
            else
            {
                gridResponses.Visibility = System.Windows.Visibility.Collapsed;
                gridNoResponses.Visibility = System.Windows.Visibility.Visible;
                if(hasChangedQuestion)
                {
                    lblNoReplies.Content = "There are no replies for this question";
                }
            }

            hasChangedQuestion = true;
        }

        private void cmbMyQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtKeywords.Text = "";
            Question question = (Question)cmbMyQuestions.SelectedItem;
            ChangeQuestionAndResponses(question.QuestionID);
            gridQuestions.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
