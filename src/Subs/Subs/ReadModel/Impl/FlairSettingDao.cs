using Infrastructure.Data;
using Subs.Services.Impl;

namespace Subs.ReadModel.Impl
{
    public class FlairSettingDao
         // this class temporarily implements the service, until we implement the proper read-only layer
        : FlairSettingService, IFlairSettingDao
    {
        public FlairSettingDao(IDbConnectionProvider conn)
            :base(conn)
        {
            
        }
    }
}
