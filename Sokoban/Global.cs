using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Global
    {

        private static Settings _CurrentSettings;
        public static Settings CurrentSettings
        {
            get
            {

                if (_CurrentSettings == null)
                {
                    if (!System.IO.File.Exists(Util.FileUtil.SettingsPath))
                    {
                        Util.SerializeUtility.CreateNewSettings(Util.FileUtil.SettingsPath);
                    }
                    _CurrentSettings = Util.SerializeUtility.DeserializeSettings(Util.FileUtil.SettingsPath);
                }
                return _CurrentSettings;
            }
        }
        public static void SaveSettings()
        {
         
            Util.SerializeUtility.SerializeSettings(CurrentSettings, Util.FileUtil.SettingsPath);
            _CurrentSettings = null;
        }
    }
}
