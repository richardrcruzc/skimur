using System;
 
using Infrastructure.Messaging;

namespace Subs.Commands
{
    public class DeleteFlairSetting : ICommandReturns<DeleteFlairSettingResponse>
    {
        public Guid FlairSettingId { get; set; }

        public string UserName { get; set; }

        public DateTime DateDeleted { get; set; }
    }

    public class DeleteFlairSettingResponse
    {
        public string Error { get; set; }
        public Guid FlairSettingId { get; set; }
    }
}
