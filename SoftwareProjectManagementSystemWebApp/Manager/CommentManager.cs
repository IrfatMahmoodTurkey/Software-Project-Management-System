using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareProjectManagementSystemWebApp.Gateaway;
using SoftwareProjectManagementSystemWebApp.Models;
using SoftwareProjectManagementSystemWebApp.Models.ViewModels;

namespace SoftwareProjectManagementSystemWebApp.Manager
{
    public class CommentManager
    {
        private CommentGateway commentGateway;

        public CommentManager()
        {
            commentGateway = new CommentGateway();
        }

        // add comment
        public string AddComment(Comment comment)
        {
            int rowsAffected = commentGateway.AddComment(comment);

            if (rowsAffected > 0)
            {
                return "Added Comment Successfully";
            }
            else
            {
                return "Failed to Add Comment";
            }
        }

        // get all comment by task id
        public List<CommentViewModel> GetAllCommentByTaskId(int taskId)
        {
            return commentGateway.GetAllCommentByTaskId(taskId);
        }

    }
}