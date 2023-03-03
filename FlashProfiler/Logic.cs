using System;
using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace FlashProfiler
{
    public static class Logic
    {
        private static List<ProfilerEvent> EventProfiles { get; set; } = new List<ProfilerEvent>();
        public static DateTime StartTime { get; set; }
        
        public static void AddEvent(double time, string methodName, string className)
        {
            EventProfiles.Add(new ProfilerEvent()
            {
                Time = time,
                Method = methodName,
                Class = className
            });
        }

        public static void WriteToFile()
        {
            File.WriteAllText($"{Paths.Configs}/FlashProfiler/FlashProfiler-{DateTime.Now.ToBinary()}-{(DateTime.Now - StartTime).Seconds}.json", 
                JsonConvert.SerializeObject(EventProfiles));
            
            EventProfiles.Clear();
        }
    }
}