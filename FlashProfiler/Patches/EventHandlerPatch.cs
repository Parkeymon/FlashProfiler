﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.Extensions;
using HarmonyLib;

namespace FlashProfiler.Patches
{
    [HarmonyPatch]
    internal static class EventHandlerPatch
    {
        public static MethodInfo TargetMethod() => typeof(Event).GetMethods().First(x => x.IsGenericMethod).MakeGenericMethod(typeof(IExiledEvent));

        [HarmonyPrefix]
        private static bool Prefix(Events.CustomEventHandler<IExiledEvent> ev, IExiledEvent arg)
        {
            if (ev == null)
                return false;

            var watch = new Stopwatch();

            var eventName = ev.GetType().FullName;

            Log.Debug($"+ {ev.Method.Name}");

            foreach (var handler in ev.GetInvocationList())
            {
                try
                {
                    watch.Restart();
                    handler.DynamicInvoke(arg);
                    watch.Stop();
                    Log.Debug($"{watch.Elapsed.TotalMilliseconds}ms {handler.Method.Name}::{handler.Method.ReflectedType?.FullName}");
                    Logic.AddEvent(watch.Elapsed.TotalMilliseconds, handler.Method.Name, handler.Method.ReflectedType?.FullName);
                }
                catch (Exception ex)
                {
                    Log.Warn($"{ex}, {handler.Method.Name}, {handler.Method.ReflectedType?.FullName}, {handler.Method.ReflectedType?.FullName}");
                } 
            }
            
            return false;
        }
    }

    [HarmonyPatch(typeof(Event), nameof(Event.InvokeSafely), typeof(Events.CustomEventHandler))]
    internal static class SecondEventHandlerPatch
    {
        [HarmonyPrefix]
        private static bool Prefix(Events.CustomEventHandler ev)
        {
            if (ev == null)
                return false;

            var watch = new Stopwatch();
            
            var eventName = ev.GetType().FullName;
            
            Log.Debug($"+ {ev.Method.Name}");

            foreach (var handler in ev.GetInvocationList())
            {
                try
                {
                    watch.Restart();
                    handler.DynamicInvoke();
                    watch.Stop();
                    Log.Debug($"{watch.Elapsed.TotalMilliseconds}ms {handler.Method.Name}::{handler.Method.ReflectedType?.FullName}, {handler.Method.ReflectedType?.Namespace}");
                    Logic.AddEvent(watch.Elapsed.TotalMilliseconds, handler.Method.Name, handler.Method.ReflectedType?.FullName);
                }
                catch (Exception ex)
                {
                    Log.Warn($"{ex}, {handler.Method.Name}, {handler.Method.ReflectedType?.FullName}, {handler.Method.ReflectedType?.FullName}");
                }
            }
            
            return false;
        }
    }
}