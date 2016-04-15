using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;


namespace com.GreenThumb.MVC.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        /// <summary>
        /// Created by: Trent Cullinan 03/31/2016
        /// Displays list of groups a User belongs to 
        /// 
        /// Modified by: Nicholas King 04/03/2016
        /// Merged grouplist display and create group
        /// added join group list table
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            GroupIndexViewModel model = new GroupIndexViewModel();
            int userId = RetrieveUserId();

            if (0 != userId)
            {
                var groups = new GroupManager().RetrieveUserGroups(userId);
                model.UserGroupList = new List<GroupIndexViewModel.UserGroupViewModel>(groups.Count());

                foreach (Group group in groups)
                {
                    model.UserGroupList.Add(new GroupIndexViewModel.UserGroupViewModel()
                    {
                        GroupId
                            = group.GroupID,
                        Name
                            = group.Name,
                        LeaderUserName
                            = group.GroupLeader.User.UserName,
                        LeaderEmail
                            = group.GroupLeader.User.EmailAddress,
                        CreatedDate
                            = group.CreatedDate
                    });
                }

                var joinableGroups = new GroupManager().FetchGroupsToJoin(userId);
                model.NonUserGroupList = new List<GroupIndexViewModel.UserGroupViewModel>(joinableGroups.Count());

                foreach (Group group in joinableGroups)
                {
                    model.NonUserGroupList.Add(new GroupIndexViewModel.UserGroupViewModel()
                    {
                        GroupId
                            = group.GroupID,
                        Name
                            = group.Name,
                        LeaderUserName
                            = group.GroupLeader.User.UserName,
                        LeaderEmail
                            = group.GroupLeader.User.EmailAddress,
                        CreatedDate
                            = group.CreatedDate
                    });
                }


                return View(model);
            }

            return View("Error");
        }

        /// <summary>
        /// Logged in user will leave group.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="group">Group Id that is being left.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LeaveGroup(int? group)
        {
            if (group.HasValue)
            {
                int userId = RetrieveUserId();

                if (0 != userId)
                {
                    if (new GroupManager().LeaveGroup(userId, group.Value))
                    {
                        return RedirectToAction("Index", "Group");
                    }
                }
            }

            return View("Error");
        }

        /// <summary>
        /// Logged in user will view group details
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            ActionResult view = RedirectToAction("Index", "Group");

            if (id.HasValue)
            {
                var group
                    = new GroupManager().RetrieveGroup(id.Value);

                var gardens
                    = new GardenManager().RetrieveGroupGardens(id.Value);

                var viewModel = new GroupDetailViewModel()
                {
                    GroupID
                        = group.GroupID,
                    GroupName
                        = group.Name,
                    GroupLeader
                        = ConvertGroupMember(group.GroupLeader),
                    GroupMembers
                        = ConvertGroupMemberCollection(group.UserList),
                    Gardens
                        = ConvertGardenCollection(gardens)
                };

                view = View(viewModel);
            }

            return view;
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            var userName = User.Identity.GetUserName();
            var model = new com.GreenThumb.BusinessLogic.UserManager().RetrieveUserByUserName(userName);
            return View(model);
        }

        // POST: Group/Create
        [HttpPost]
       
        
        //take in userid and group name/create something to bind these too.
       public ActionResult Create([Bind(Include="GroupName")]string groupName)
         {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                var user = userManager.RetrieveUserByUserName(User.Identity.GetUserName());
                
                try
                {
                    com.GreenThumb.BusinessLogic.GroupManager groupManager = new BusinessLogic.GroupManager();
                    groupManager.AddGroup(user.UserID, groupName); //hard coded garbage data-need to replace supplied by request
                    ViewBag.StatusMessage = "Your group was created!";
                    
                }
                catch
                {
                    return View();
                }
                
                
            }
            return RedirectToAction("Index", "Group");
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Created by: Trent Cullinan 04/05/2016
        private IEnumerable<GroupMemberViewModel> ConvertGroupMemberCollection(IEnumerable<GroupMember> groupMembers)
        {
            var groupMembersView = new List<GroupMemberViewModel>();

            foreach (GroupMember groupMember in groupMembers)
            {
                groupMembersView.Add(ConvertGroupMember(groupMember));
            }

            return groupMembersView;
        }

        // Created by: Trent Cullinan 04/05/2016
        private GroupMemberViewModel ConvertGroupMember(GroupMember groupMember)
        {
            return new GroupMemberViewModel()
            {
                UserID
                    = groupMember.User.UserID,
                Email
                    = groupMember.User.EmailAddress,
                UserName
                    = groupMember.User.UserName,
                FirstName
                    = groupMember.User.FirstName,
                LastName
                    = groupMember.User.LastName,
                DateCreated
                    = groupMember.CreatedDate
            };
        }

        // Created by: Trent Cullinan 04/05/2016
        private IEnumerable<GroupGardenViewModel> ConvertGardenCollection(IEnumerable<Garden> gardens)
        {
            var gardensView = new List<GroupGardenViewModel>();

            foreach (Garden garden in gardens)
            {
                gardensView.Add(ConvertGarden(garden));
            }

            return gardensView;
        }

        // Created by: Trent Cullinan 04/05/2016
        private GroupGardenViewModel ConvertGarden(Garden garden)
        {
            return new GroupGardenViewModel()
            {
                GardenID
                    = garden.GardenID,
                GardenName
                    = garden.GardenName,
                Description
                    = garden.GardenDescription
            };
        }

        #region Helper Methods

        // Created by: Trent Cullinan 03/31/2016
        private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new UserManager().RetrieveUserId(userName);
            }

            return userId;
        }






        #endregion
    }
}
