using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasierFleaCarnival
{
    internal static class Config
    {
        public static ConfigEntry<float> JuggleMult;
        public static ConfigEntry<float> DodgeMult;
        public static ConfigEntry<float> BounceMult;
        public static void Init(ConfigFile config)
        {
            JuggleMult = config.Bind("Score Multipliers", "Juggle", 1f);
            DodgeMult = config.Bind("Score Multipliers", "Dodge", 1f);
            BounceMult = config.Bind("Score Multipliers", "Bounce", 1f);
        }
    }

    internal static class Log
    {
        static ManualLogSource logger;
        public static void SetLogger(ManualLogSource log)
        {
            logger = log;
        }

        public static void LogInfo(params object[] data)
        {
            foreach (object obj in data)
                logger.LogInfo(obj);
        }
        public static void LogWarning(params object[] data)
        {
            foreach (object obj in data)
                logger.LogWarning(obj);
        }
        public static void LogError(params object[] data)
        {
            foreach (object obj in data)
                logger.LogError(obj);
        }
        public static void LogFatal(params object[] data)
        {
            foreach (object obj in data)
                logger.LogFatal(obj);
        }
        public static void LogDebug(params object[] data)
        {
            foreach (object obj in data)
                logger.LogDebug(obj);
        }
        public static void LogMessage(params object[] data)
        {
            foreach (object obj in data)
                logger.LogMessage(obj);
        }
    }
}
