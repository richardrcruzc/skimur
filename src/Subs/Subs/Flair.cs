using Infrastructure;
using ServiceStack.DataAnnotations;
using System;

namespace Subs
{
    [Alias("Flairs")]
    public class Flair : IAggregateRoot
    {
        public virtual Guid Id { get; set; }

        public string Text { get; set; }

        public string CssClass { get; set; }

        public bool TextEditable { get; set; }

        public int Type { get; set; } 
        public string UserName { get; set; }

        public bool Deleted { get; set; }
         

        [Ignore]
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
