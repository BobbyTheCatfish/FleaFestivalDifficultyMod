using HarmonyLib;
using System;
using System.Collections.Generic;
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
                        __result = Scores.Juggle.Score(Scores.Juggle.Champ);
                        Log.LogInfo($"champ juggle: {__result}");
                        break;
                    case "FLEA_FESTIVAL_SETH_JUGGLE":
                        __result = Scores.Juggle.Score(Scores.Juggle.Seth);
                        break;
                    case "FLEA_FESTIVAL_CHAMP_DODGE":
                        __result = Scores.Dodge.Score(Scores.Dodge.Champ);
                        break;
                    case "FLEA_FESTIVAL_SETH_DODGE":
                        __result = Scores.Dodge.Score(Scores.Dodge.Seth);
                        break;
                    case "FLEA_FESTIVAL_CHAMP_BOUNCE":
                        __result = Scores.Bounce.Score(Scores.Bounce.Champ);
                        break;
                    case "FLEA_FESTIVAL_SETH_BOUNCE":
                        __result = Scores.Bounce.Score(Scores.Bounce.Seth);
                        break;
                }

                return false;
            }
            return true;
        }
    }
}
