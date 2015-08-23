using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Skimur.Web.Models
{
    public class FlairSettingsViewModel
    {
        public virtual Guid Id { get; set; }
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DisplayName("Enable User Flair in this Subreddit")]
        public bool EnableUserSubreddit { get; set; }
        [DisplayName("Allow Users to Assign their Own Flair")]
        public bool AllowUserAssignOwn { get; set; }
        [DisplayName("Allow Submitters to Assign their Own Link Flair")]
        public bool AllowSubmitterAssignOwnLink { get; set; }
        [DisplayName("User Flair Possition")]
        public int UserFlairPossition { get; set; }
        [DisplayName("Link Flair Possition")]
        public int LinkPossition { get; set; }

        public bool Deleted { get; set; }

        [DisplayName("Link Flair Possition")]
        [Required(ErrorMessage = "select one item")]
        public LinkFlairPossition LinkFlairPossition
        {
            get { return (LinkFlairPossition)LinkPossition; }
            set { LinkPossition = (int)value; }
        }

        [Required(ErrorMessage = "select one item")]
        [DisplayName("User Flair Possition")]
        public UserPossition Possition
        {
            get { return (UserPossition)UserFlairPossition; }
            set { UserFlairPossition = (int)value; }
        }
    }

    public enum UserPossition
    {
        Left = 1,
        Right = 0
    }
    public enum LinkFlairPossition
    {
        None = 2,
        Left = 1,
        Right = 0
    }


    public class FlairIndexModel
    {
        public List<ListFlairViewModel> UserFlair { get; set; }
        public List<ListFlairViewModel> LinkFlair { get; set; }
        public string ActiveTab { get; set; } 

        public   FlairIndexModel()
        {
            UserFlair = new  List<ListFlairViewModel>() ;
            LinkFlair = new  List<ListFlairViewModel>();
    }

        }
     
   
    public class ListFlairViewModel
    {
        public virtual Guid Id { get; set; }
        public string Text { get; set; }

        public string CssClass { get; set; }
        public bool TextEditable { get; set; }

        public int Type { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }
         
    }
  
    public enum FlairType
    {
        User = 1,
        Link = 0
    }
}
