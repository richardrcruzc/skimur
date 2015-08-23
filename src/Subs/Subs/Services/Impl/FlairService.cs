using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Utils;
using ServiceStack.OrmLite;
using Skimur;

namespace Subs.Services.Impl
{
    public class FlairService : IFlairService
    {
        private readonly IDbConnectionProvider _conn;

        public FlairService(IDbConnectionProvider conn)
        {
            _conn = conn;
        }
        public void InsertFlair(Flair flair)
        {
            _conn.Perform(conn =>
            {
                conn.Insert(flair);
            });

        }
        public void UpdateFlair(Flair flair)
        {
            _conn.Perform(conn => conn.Update(flair));
           
        }
        public void DeleteFlair(Guid id)
        {
           var flair= GetFlairById(id);
            flair.Deleted = true;
            UpdateFlair(flair);
        }

        public Flair GetFlairById(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            return _conn.Perform(conn => conn.SingleById<Flair>(id));
        }
        public List<Flair> GetAllFlairsForUser(string userName, FlairType? flairType=null)
        {
            return _conn.Perform(conn =>
            {
                var query = conn.From<Flair>().Where(x => x.UserName == userName && !x.Deleted);

                if (flairType.HasValue)
                 query = query.Where(x=>x.FlairType == flairType);                

                return conn.Select(query);
            });
        }

    }
}
