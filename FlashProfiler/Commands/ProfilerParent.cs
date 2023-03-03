using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using FlashProfiler.Commands.SubCommands;

namespace FlashProfiler.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class ProfilerParent : ParentCommand
    {
        // Parent command logic stuff stolen from serpents hand
        public ProfilerParent() => LoadGeneratedCommands();

        public override string Command => "flashprofiler";
        public override string[] Aliases { get; } = new[] {"fp"};
        public override string Description => "The parent command for Flash Profiler.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Start());
            RegisterCommand(new Stop());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "\nPlease enter a valid subcommand:\n";
            foreach (var command in AllCommands)
                if (sender.CheckPermission($"fp.{command.Command}"))
                    response += $"- {command.Command} ({string.Join(", ", Aliases)})";
            return false;
        }
    }
}