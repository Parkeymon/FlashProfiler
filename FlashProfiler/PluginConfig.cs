using System.ComponentModel;
using Exiled.API.Interfaces;

namespace FlashProfiler
{
    public class PluginConfig : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Should the plugin log to the console? (This will heavily spam your console!)")]
        public bool Debug { get; set; }
    }
}