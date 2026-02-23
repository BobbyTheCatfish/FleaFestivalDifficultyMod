using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasierFleaCarnival
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
                        __result = Values.Juggle.Score(Values.Juggle.Champ);
                        Log.LogInfo($"champ juggle: {__result}");
                        break;
                    case "FLEA_FESTIVAL_SETH_JUGGLE":
                        __result = Values.Juggle.Score(Values.Juggle.Seth);
                        break;
                    case "FLEA_FESTIVAL_CHAMP_DODGE":
                        __result = Values.Dodge.Score(Values.Dodge.Champ);
                        break;
                    case "FLEA_FESTIVAL_SETH_DODGE":
                        __result = Values.Dodge.Score(Values.Dodge.Seth);
                        break;
                    case "FLEA_FESTIVAL_CHAMP_BOUNCE":
                        __result = Values.Bounce.Score(Values.Bounce.Champ);
                        break;
                    case "FLEA_FESTIVAL_SETH_BOUNCE":
                        __result = Values.Bounce.Score(Values.Bounce.Seth);
                        break;
                }

                return false;
            }
            return true;
        }
    }
}
