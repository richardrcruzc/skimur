

using System;
using System.Collections.Generic;

namespace Subs.Services
{
    public interface IFlairSettingService
    {
        void InsertFlairSetting(FlairSetting flair);
        void UpdateFlairSetting(FlairSetting flair);
        void DeleteFlairSetting(Guid id);
        FlairSetting GetFlairSettingById(Guid id);
        List<FlairSetting> GetAllFlairSettingForUser(string userName); 
         
    }
}
