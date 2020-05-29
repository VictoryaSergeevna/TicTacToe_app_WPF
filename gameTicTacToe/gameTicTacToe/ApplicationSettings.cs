using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gameTicTacToe
{
    public class ApplicationSettings:ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("99999")]
        public int BestTime
        {
            get { return (int)this["BestTime"]; }
            set { this["BestTime"] = (int)value; }
        }
    }
}
