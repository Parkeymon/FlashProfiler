using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace FlashProfiler.Commands.SubCommands
{
    public class Stop : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("flashprofiler.log"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            Logic.WriteToFile();
            FlashProfiler.Harmony.UnpatchAll(FlashProfiler.Harmony.Id);
            response = "Profiler stopped and logs written to file.";
            return true;
        }

        public string Command { get; } = "stop";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Stops the profiler and puts logs into a file.";
    }
}