using System;
using System.IO;
using Exiled.API.Features;
using FlashProfiler.Patches;
using HarmonyLib;

namespace FlashProfiler
{
    public class FlashProfiler : Plugin<PluginConfig>
    {
        public override string Name { get; } = "FlashProfiler";
        public override string Prefix { get; } = "flash_profiler";
        public override string Author { get; } = "Jesus-QC / Parkeymon";
        public override Version Version { get; } = new Version(0, 0, 2);
        public override Version RequiredExiledVersion { get; } = new Version(6,0,0);

        internal static Harmony Harmony;

        public override void OnEnabled()
        {
            Harmony = new Harmony($"com.jesusqc.flash");
            
            if (!Directory.Exists($"{Paths.Configs}/FlashProfiler"))
            {
                Log.Warn("Profiler logs folder does not exist! Creating...");
                Directory.CreateDirectory($"{Paths.Configs}/FlashProfiler");
            }
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Harmony.UnpatchAll(Harmony.Id);
            Harmony = null;
            
            base.OnDisabled();
        }
    }
}