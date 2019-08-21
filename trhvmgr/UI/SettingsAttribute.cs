using System;
using System.ComponentModel;
using System.Linq;
using trhvmgr.Lib;
using trhvmgr.Properties;

namespace trhvmgr.UI
{
    /// <summary>
    /// Class to annotate application settings for
    /// user friendly display on a PropertyGrid
    /// </summary>
    public class SettingsAttribute
    {
        public static void SetAttribute(string propName, params Attribute[] attr)
        {
            var _settings = Settings.Default;
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

        public static void SetAttribute(string prop, string category, string display, string description) =>
            SetAttribute(prop, new CategoryAttribute(category), new DisplayNameAttribute(display), new DescriptionAttribute(description));

        public static void SetAttribute(string prop, string category, string display, string description, params Attribute[] attr)
        {
            var l = attr.ToList();
            l.AddRange(new Attribute[] {
                new CategoryAttribute(category), new DisplayNameAttribute(display), new DescriptionAttribute(description),
                new DefaultValueAttribute(Settings.Default.Properties[prop].DefaultValue)
            });
            SetAttribute(prop, l.ToArray());
        }
    }
}
