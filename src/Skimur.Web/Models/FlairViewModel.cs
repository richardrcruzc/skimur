using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skimur.Web.Models
{
    public class FlairSettingsViewModel
    {
        public virtual Guid Id { get; set; }
        public string UserName { get; set; }
        public bool enable_user_subreddit { get; set; }
        public bool allow_user_assign_own { get; set; }
        public bool allow_submitter_assign_own_link { get; set; }
        public bool user_flair_possition { get; set; }
        public bool link_possition { get; set; }

        public bool Deleted { get; set; }

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
        public class CreateFlairViewModel
    {

        public string Text { get; set; }

        public string CssClass { get; set; }

        public bool TextEditable { get; set; }

        public int Type { get; set; }
        public FlairType FlairType
        {
            get { return (FlairType)Type; }
            set { Type = (int)value; }
        }
    }
   
    public class EditFlairViewModel
    {
        public virtual Guid Id { get; set; }

        public string Text { get; set; }

        public string CssClass { get; set; }

        public bool TextEditable { get; set; }

        public int Type { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }
         

        public FlairType FlairType
        {
            get { return (FlairType)Type; }
            set { Type = (int)value; }
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
    public class  FlairViewModel
    {
        public virtual Guid Id { get; set; }

        public string Text { get; set; }

        public string CssClass { get; set; }

        public bool TextEditable { get; set; }

        public int Type { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }

       


        public FlairType FlairType
        {
            get { return (FlairType)Type; }
            set { Type = (int)value; }
        }
    }
    public enum FlairType
    {
        User = 1,
        Link = 0
    }
}
