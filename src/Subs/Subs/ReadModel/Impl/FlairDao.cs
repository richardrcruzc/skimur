using Infrastructure.Data;
using Subs.Services.Impl;

namespace Subs.ReadModel.Impl
{
    public class FlairDao
         // this class temporarily implements the service, until we implement the proper read-only layer
        : FlairService, IFlairDao
    {
        public FlairDao(IDbConnectionProvider conn)
            :base(conn)
        {
            
        }
    }
}
