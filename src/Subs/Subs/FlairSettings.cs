using Infrastructure;
using ServiceStack.DataAnnotations;
using System;

namespace Subs
{
    [Alias("FlairSettings")]
    public class FlairSetting : IAggregateRoot
    {
        public virtual Guid Id { get; set; }

        public bool EnableUserSubreddit { get; set; }
        public bool AllowUserAssignOwn { get; set; }
        public bool AllowSubmitterAssignOwnLink { get; set; }
        public int UserFlairPossition { get; set; }
        public int LinkPossition { get; set; }  

        public string UserName { get; set; }

        public bool Deleted { get; set; }


        [Ignore]
        public LinkFlairPossition LinkFlairPossition
        {
            get { return (LinkFlairPossition)LinkPossition; }
            set { LinkPossition = (int)value; }
        }
        [Ignore]
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
        None=2,
        Left = 1,
        Right = 0
    }
}