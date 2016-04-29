﻿using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace com.GreenThumb.WPF_Presentation.HomePages
{
    /// <summary>
    /// Added by Sara Nanke on 03/22/2016
    /// Interaction logic for ViewBlog.xaml
    /// </summary>
    public partial class ViewBlog : Page
    {
        Blog blog = new Blog();
        List<Blog> blogs = new List<Blog>();
        BlogManager blogManager = new BlogManager();
        List<DateTime> dates = new List<DateTime>();
        AccessToken accessToken = null;
        List<String> roles = new List<String>();

        // page variables
        PageDetails pageDetails = new PageDetails();
        Paginate<Blog> paginate = new Paginate<Blog>();
        List<Blog> fullList = new List<Blog>();
        public ViewBlog()
        {
            InitializeComponent();
            blogs = blogManager.GetBlogs();
            icBlogs.ItemsSource = blogs;

            pageDetails = InitializePageDetails();
            fullList = blogManager.GetBlogs();
        }

        public ViewBlog(AccessToken accessToken)
        {
            this.accessToken = accessToken;
            InitializeComponent();

            pageDetails = InitializePageDetails();
            fullList = blogManager.GetBlogs();

            blogs = paginate.GetList(pageDetails, blogManager.GetBlogs());

            icBlogs.ItemsSource = blogs;
            foreach (Role role in accessToken.Roles)
            {
                roles.Add(role.RoleID);
            }
            if (roles.Contains("Admin"))
            {
                btnCreateBlog.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public string UserCreated(int userId)
        {
            string userCreated = "";
            UserManager userManager = new UserManager();
            User user = userManager.GetUser(userId);
            userCreated = user.FirstName + " " + user.LastName;
            return userCreated;
        }

        private void scrBlogs_Initialized(object sender, EventArgs e)
        {
        }

        private void btnCreateBlog_Click(object sender, RoutedEventArgs e)
        {
            if (accessToken != null)
            {
                this.NavigationService.Navigate(new HomePages.CreateBlog(accessToken));
            }
        }

        private void stpnlBlogs_Initialized(object sender, EventArgs e)
        {
            pageDetails = InitializePageDetails();
            CreateBlogStack();
        }

        private void CreateBlogStack()
        {
            stpnlBlogs.Children.Clear();
            blogs = paginate.GetList(pageDetails, blogManager.GetBlogs());
            foreach (Blog blog in blogs)
            {
                Button button = new Button();
                button.Style = (Style) Resources["button1"];
                button.Margin.Equals(10);
                button.Name = "btn" + blog.BlogID.ToString();
                button.Content = blog.BlogTitle;
                button.Click += new RoutedEventHandler(btnRpter_Clicked);
                stpnlBlogs.Children.Add(button);
            }
        }

        private void stpnlBlogs_MouseDown(object sender, MouseButtonEventArgs e)
        { }

        private void btnRpter_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                pageDetails.CurrentPage = 1;
                lblPage.Content = "Page " + pageDetails.CurrentPage;
                int blogId = Int32.Parse(button.Name.Substring(3));
                Blog currentBlog = new Blog();
                currentBlog = blogManager.GetBlogById(blogId);
                blogs = new List<Blog>();
                blogs.Add(currentBlog);
                icBlogs.ItemsSource = blogs;
            }
            catch (Exception ex)
            {
                //blog could not be found
            }
        }

        private void allBlogs_Click(object sender, RoutedEventArgs e)
        {
            SelectBlogs();
            icBlogs.ItemsSource = blogs;
        }

        /* Rhett Allen - adding pages */
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage > 1)
            {
                pageDetails.CurrentPage--;
            }

            SelectBlogs();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (pageDetails.CurrentPage < pageDetails.MaxPages)
            {
                pageDetails.CurrentPage++;
            }

            SelectBlogs();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = 1;
            SelectBlogs();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            pageDetails.CurrentPage = pageDetails.MaxPages;
            SelectBlogs();
        }

        private void SelectBlogs()
        {
            try
            {
                icBlogs.ItemsSource = paginate.GetList(pageDetails, fullList);
                lblPage.Content = "Page " + pageDetails.CurrentPage;
                CreateBlogStack();
            }
            catch (Exception)
            {
                icBlogs.ItemsSource = new List<Blog>();
            }
        }

        private PageDetails InitializePageDetails()
        {
            PageDetails p = new PageDetails();
            p.Count = blogManager.GetBlogs().Count;
            p.CurrentPage = 1;
            p.PerPage = 5;

            return p;
        }
    }
}
