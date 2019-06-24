using System.Configuration;

namespace WebApi.Template.Modules.Api
{
    public class ModuleSection : ConfigurationSection
    {
        [ConfigurationProperty("baseAddress", IsRequired = true)]
        public string BaseAddress => (string)this["baseAddress"];
    }
}