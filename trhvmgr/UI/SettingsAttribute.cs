using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;

namespace trhvmgr.UI
{
    public class SettingsAttribute
    {
        public static void SetAttribute(string propName, params Attribute[] attr)
        {
            var _settings = Properties.Settings.Default;
            PropertyOverridingTypeDescriptor ctd = new PropertyOverridingTypeDescriptor(TypeDescriptor.GetProvider(_settings).GetTypeDescriptor(_settings));
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(_settings))
            {
                if (pd.Name == propName)
                {
                    PropertyDescriptor pd2 = TypeDescriptor.CreateProperty(_settings.GetType(), pd, attr);
                    ctd.OverrideProperty(pd2);
                }
            }
            TypeDescriptor.AddProvider(new TypeDescriptorOverridingProvider(ctd), _settings);
        }
    }
}
