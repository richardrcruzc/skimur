

using Infrastructure.Messaging;
using System;

namespace Subs.Commands
{
    public class CreateFlairSetting: ICommandReturns<CreateFlairSettingResponse>
    {
        public virtual Guid? FlairSettingId { get; set; }

        public bool EnableUserSubreddit { get; set; }
        public bool AllowUserAssignOwn { get; set; }
        public bool AllowSubmitterAssignOwnLink { get; set; }
        public int UserFlairPossition { get; set; }
        public int LinkPossition { get; set; }

        public string UserName { get; set; }

        public bool Deleted { get; set; }

    }
    public class CreateFlairSettingResponse
    {
        public string Error { get; set; }

        public Guid? FlairSettingId { get; set; }
         
    }
}
