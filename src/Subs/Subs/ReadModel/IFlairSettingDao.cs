using System;
using System.Collections.Generic;

namespace Subs.ReadModel
{
    public interface IFlairSettingDao
    {
        FlairSetting GetFlairSettingById(Guid id);
        List<FlairSetting> GetAllFlairSettingForUser(string userName );
    }
}
