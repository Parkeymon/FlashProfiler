using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace FlashProfiler.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ProfilerLog : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("flashprofiler.log"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            Logic.WriteToFile();
            response = "Profiler log written to file.";
            return true;
        }

        public string Command { get; } = "profilerlog";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Writes the profiler log to a file.";
    }
}