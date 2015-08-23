using System;
using System.Collections.Generic;
using System.Linq;  
using Infrastructure.Data; 
using ServiceStack.OrmLite;
using Skimur;

namespace Subs.Services.Impl
{
    public class FlairSettingService : IFlairSettingService
    {
        private readonly IDbConnectionProvider _conn;

        public FlairSettingService(IDbConnectionProvider conn)
        {
            _conn = conn;
        }
        public void InsertFlairSetting(FlairSetting flairSetting)
        {
            _conn.Perform(conn =>
            {
                conn.Insert(flairSetting);
            });

        }
        public void UpdateFlairSetting(FlairSetting flairSetting)
        {
            _conn.Perform(conn => conn.Update(flairSetting));
           
        }
        public void DeleteFlairSetting(Guid id)
        {
            //var flairSetting= GetFlairSettingById(id);
            // flairSetting.Deleted = true;
            // UpdateFlairSetting(flairSetting);

            _conn.Perform(conn => conn.DeleteById<FlairSetting>(id));
        }

        public FlairSetting GetFlairSettingById(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            return _conn.Perform(conn => conn.SingleById<FlairSetting>(id));
        }
        public List<FlairSetting> GetAllFlairSettingForUser(string userName )
        {
            return _conn.Perform(conn =>
            {
                var query = conn.From<FlairSetting>().Where(x => x.UserName == userName && !x.Deleted);

                 
                return conn.Select(query);
            });
        }

    }
}
