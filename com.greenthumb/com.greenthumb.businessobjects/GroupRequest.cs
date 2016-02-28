// Added by Poonam Dubey on 02/27/2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GroupRequest
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public string RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }


        public GroupRequest() { }
        public GroupRequest(int requestID,
                             int userID,
                             string requestStatus,
                             DateTime requestDate,
                             int requestedBy,
                             DateTime approvedDate,
                             int approvedBy)
        {
            RequestID = requestID;
            UserID = userID;
            RequestStatus = requestStatus;
            RequestDate = requestDate;
            RequestedBy = requestedBy;
            ApprovedDate = approvedDate;
            ApprovedBy = approvedBy;
        }
    }
}
