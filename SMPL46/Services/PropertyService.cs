using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SMPL46.Services
{
    public class WebConfigurationPropertyService : IPropertyService
    {
        public WebConfigurationPropertyService() {

        } // end constructor

        public virtual bool HasProperty(string key) {
            return !String.IsNullOrWhiteSpace(key) && ConfigurationManager.AppSettings.AllKeys.Select((string x) => x).Contains(key);
        } // end method HasProperty

        public virtual string ReadProperty(string key) {
            string returnValue = String.Empty;
            if(this.HasProperty(key)) {
                returnValue = ConfigurationManager.AppSettings[key];
            } // end if
            return returnValue;
        } // end method ReadProperty    
    }
}