using System;
using System.Configuration;
using static ViceArmory.Utility.CommonEnums;

namespace ViceArmory.Utility
{
    public class ConfigReader
    {
        public static String Read(ConfigKeys key)
        {
            String value = ConfigurationManager.AppSettings[Enum.GetName(key.GetType(), key)];
            return (!String.IsNullOrEmpty(value)) ? value.Trim() : String.Empty;
        }

        public static String Read(String key)
        {
            String value = ConfigurationManager.AppSettings[key];
            return (!String.IsNullOrEmpty(value)) ? value.Trim() : String.Empty;
        }
    }
}
