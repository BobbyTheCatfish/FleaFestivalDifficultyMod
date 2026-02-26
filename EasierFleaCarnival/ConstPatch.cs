using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FleaFestivalDifficulty
{
    internal class ConstPatch
    {
        public ConstPatch()
        {
            var harmony = new Harmony("festival");

            Type generic = typeof(int);

            MethodInfo methodDef = typeof(Constants).GetMethod(nameof(Constants.GetConstantValue));
            MethodInfo specificMethod = methodDef.MakeGenericMethod(generic);

            MethodInfo prefix = typeof(ConstPatch).GetMethod(nameof(GetConstantValue));

            harmony.Patch(specificMethod, new HarmonyMethod(prefix));
            harmony.PatchAll(typeof(ConstPatch));
        }

        public static bool GetConstantValue(string variableName, ref int __result)
        {
            if (variableName.StartsWith("FLEA_F"))
            {
                //if (typeof(T) != typeof(int)) return true;

                //Log.LogInfo(variableName);
                switch (variableName)
                {
                    case "FLEA_FESTIVAL_CHAMP_JUGGLE":
                        __result = Scores.Juggle.Champ;
                        //Log.LogInfo($"champ juggle: {__result}");
                        break;
                    case "FLEA_FESTIVAL_SETH_JUGGLE":
                        __result = Scores.Juggle.Seth;
                        break;
                    case "FLEA_FESTIVAL_CHAMP_DODGE":
                        __result = Scores.Dodge.Champ;
                        break;
                    case "FLEA_FESTIVAL_SETH_DODGE":
                        __result = Scores.Dodge.Seth;
                        break;
                    case "FLEA_FESTIVAL_CHAMP_BOUNCE":
                        __result = Scores.Bounce.Champ;
                        break;
                    case "FLEA_FESTIVAL_SETH_BOUNCE":
                        __result = Scores.Bounce.Seth;
                        break;
                    default:
                        return true;
                }

                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsJugglingChampion), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool FleaGamesIsJugglingChampion(PlayerData __instance, ref bool __result)
        {
            Log.LogInfo("TESTING JUGGLE CHAMP");
            __result = __instance.fleaGames_juggling_highscore > Scores.Juggle.Champ;
            return false;
        }
        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsJugglingSethChampion), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool FleaGamesIsJugglingSethChampion(PlayerData __instance, ref bool __result)
        {
            __result = __instance.fleaGames_juggling_highscore > Scores.Juggle.Seth;
            return false;
        }

        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsBouncingChampion), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool FleaGamesIsBouncingChampion(PlayerData __instance, ref bool __result)
        {
            __result = __instance.fleaGames_bouncing_highscore > Scores.Bounce.Champ;
            return false;
        }
        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsBouncingSethChampion), MethodType.Getter)]
        public static bool FleaGamesIsBouncingSethChampion(PlayerData __instance, ref bool __result)
        {
            __result = __instance.fleaGames_bouncing_highscore > Scores.Bounce.Seth;
            return false;
        }


        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsDodgingChampion), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool FleaGamesIsDodgingChampion(PlayerData __instance, ref bool __result)
        {
            __result = __instance.fleaGames_dodging_highscore > Scores.Dodge.Champ;
            return false;
        }
        [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.FleaGamesIsDodgingSethChampion), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool FleaGamesIsDodgingSethChampion(PlayerData __instance, ref bool __result)
        {
            __result = __instance.fleaGames_dodging_highscore > Scores.Dodge.Seth;
            return false;
        }
    }
}
