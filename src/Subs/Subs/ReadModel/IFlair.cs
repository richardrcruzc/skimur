using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subs.ReadModel
{
    public interface IFlairDao
    {
        void InsertFlair(Flair flair);
        void UpdateFlair(Flair flair);
        void DeleteFlair(Guid id);
        Flair GetFlairById(Guid id);
        List<Flair> GetAllFlairsForUser(string userName, FlairType? flairType = null);
    }
}
