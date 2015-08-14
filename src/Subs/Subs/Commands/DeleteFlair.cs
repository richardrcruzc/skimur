using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Messaging;

namespace Subs.Commands
{
    public class DeleteFlair : ICommandReturns<DeleteFlairResponse>
    {
        public Guid FlairId { get; set; }

        public string UserName { get; set; }

        public DateTime DateDeleted { get; set; }
    }

    public class DeleteFlairResponse
    {
        public string Error { get; set; }
        public Guid FlairId { get; set; }
    }
}
