using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;
using System.Data.Entity;

namespace SoftwareProjectManagementSystemWebApp.Gateaway
{
    public class CommentGateway:BaseGateway
    {
        // add comment
        public int AddComment(Comment comment)
        {
            Context.Comments.Add(comment);
            return Context.SaveChanges();
        }

        // get all comment by task id
        public List<CommentViewModel> GetAllCommentByTaskId(int taskId)
        {
            var query = Context.Comments.Include(c => c.ProjectDetail).Include(c => c.User).Where(c=>c.TaskId == taskId);

            List<CommentViewModel> commentViewModels = new List<CommentViewModel>();

            foreach (var result in query)
            {
                CommentViewModel commentViewModel = new CommentViewModel();

                commentViewModel.Comment = result.CommentDescription;
                commentViewModel.CommentBy = result.User.Name;
                commentViewModel.DateTime = result.ActionTime;

                commentViewModels.Add(commentViewModel);
            }

            return commentViewModels;
        }
    }
}