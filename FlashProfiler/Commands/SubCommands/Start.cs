using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace FlashProfiler.Commands.SubCommands
{
    public class Start : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("flashprofiler.start"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }
            
            FlashProfiler.Harmony.PatchAll();
            Logic.StartTime = DateTime.Now;
            response = "Profiler started. Run 'fp stop' to stop the profiler and write logs to file.";
            return true;
        }

        public string Command { get; } = "start";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Starts the profiler.";
    }
}