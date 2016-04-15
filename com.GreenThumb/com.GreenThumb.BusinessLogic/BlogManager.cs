using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Added by Sara Nanke on 03/22/2016
    /// This Class manages blog objects to send to the view
    /// </summary>
    public class BlogManager
    {
        List<Blog> blogs;
        BlogAccessor blogAccessor = new BlogAccessor();

        public List<Blog> GetBlogs()
        {
            blogs = blogAccessor.retrieveBlogs();
            return blogs;
        }

        public Blog GetBlogById(int blogId)
        {
            Blog blogReturn = null;
            blogs = blogAccessor.retrieveBlogs();
            foreach (Blog blog in blogs)
            {
                if (blog.BlogID == blogId)
                {
                    return blog;
                }
            }
            return blogReturn;
        }
        public List<Blog> GetBlogByDate()
        {
            blogs = blogAccessor.retrieveBlogs();
            //sortByDate
            return blogs;
        }

        public List<Blog> GetBlogByName()
        {
            //blogs = blogAccessor.fetchBlogsByName();

            return blogs;
        }
        public bool CreateBlog(Blog blog)
        {
            bool created;
            try
            {
                if (BlogAccessor.InsertBlog(blog) == 1)
                {
                    created = true;
                }
                else
                {
                    created = false;
                }
            }
            catch
            {
                created = false;
            }
            return created;
        }

    }
}
