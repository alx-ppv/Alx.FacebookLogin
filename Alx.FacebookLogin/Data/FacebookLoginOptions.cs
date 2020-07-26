using System.Collections.Generic;

namespace Alx.FacebookLogin.Data
{
    public class FacebookLoginOptions
    {
        public string Route { get; set; }
        public List<string> Fields { get; set; }
        public ConfigurationKeys ConfigurationKeys { get; set; }

        public FacebookLoginOptions()
        {
            Fields = new List<string> { "email", "name", "picture.width(300).height(300)" };
            Route = "/api/session/facebook";
            ConfigurationKeys = new ConfigurationKeys
            {
                AppId = "Authentication:Facebook:AppId",
                AppSecret = "Authentication:Facebook:AppSecret",
                RedirectUri = "Authentication:Facebook:RedirectUri"
            };
        }
    }
}
