using System.Configuration;
using System.Collections.Specialized;

namespace FloodApp
{
    public static class Utility
    {
    

        /// <summary>
        /// This is used to Read the valus from app.config file
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadSetting(string key)
        {
            string result = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key];

            }
            catch (ConfigurationErrorsException ex)
            {
                throw;

            }
            return result;
        }
       
       
    }
}
