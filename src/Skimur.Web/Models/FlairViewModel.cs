using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skimur.Web.Models
{
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

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public string IpAddress { get; set; }


        public FlairType FlairType
        {
            get { return (FlairType)Type; }
            set { Type = (int)value; }
        }
    }
    public class ListFlairViewModel
    {
        public string Text { get; set; }

        public string CssClass { get; set; }
        public bool TextEditable { get; set; }

        public int Type { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }
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

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public string IpAddress { get; set; }


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
