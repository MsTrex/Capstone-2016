using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public short RegionID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Question() { }

        public Question(int questionID, string title, string category, string content, short regionID, int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate)
        {
            QuestionID = questionID;
            Title = title;
            Category = category;
            Content = content;
            RegionID = regionID;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
        }
    }
}
