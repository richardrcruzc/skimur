

using System;
using System.Collections.Generic;

namespace Subs.Services
{
    public interface IFlairService
    {
        void InsertFlair(Flair flair);
        void UpdateFlair(Flair flair);
        void DeleteFlair(Guid id);
        Flair GetFlairById(Guid id);
        List<Flair> GetAllFlairsForUser(string userName, FlairType? flairType=null); 
         
    }
}
