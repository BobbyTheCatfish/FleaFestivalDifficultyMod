using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleaFestivalDifficulty
{
    internal static class Config
    {
        public static ConfigEntry<float> JuggleScoreMult;
        public static ConfigEntry<float> DodgeScoreMult;
        public static ConfigEntry<float> BounceScoreMult;

        public static ConfigEntry<float> JuggleSpeedMult;
        public static ConfigEntry<float> DodgeSpeedMult;
        public static ConfigEntry<float> BounceSpeedMult;
        public static void Init(ConfigFile config)
        {
            JuggleScoreMult = config.Bind("Score Multipliers", "Juggle", 1f, "Multiplies all flea/seth scores for this game");
            DodgeScoreMult = config.Bind("Score Multipliers", "Dodge", 1f, "Multiplies all flea/seth scores for this game");
            BounceScoreMult = config.Bind("Score Multipliers", "Bounce", 1f, "Multiplies all flea/seth scores for this game");

            JuggleSpeedMult = config.Bind("Speed Multipliers", "Juggle", 1f, "Higher number = faster fleas");
            JuggleSpeedMult.SettingChanged += FFDPlugin.JuggleSpeed.SetSpeed;

            DodgeSpeedMult = config.Bind("Speed Multipliers", "Dodge", 1f, "Higher number = faster fleas");
            DodgeSpeedMult.SettingChanged += FFDPlugin.DodgeSpeed.SetSpeed;

            BounceSpeedMult = config.Bind("Speed Multipliers", "Bounce", 1f, "Higher number = faster fleas");
            BounceSpeedMult.SettingChanged += FFDPlugin.BounceSpeed.SetSpeed;
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
