using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

namespace EasierFleaCarnival
{

    class Score
    {
        public int juggle = 0;
        public int bounce = 0;
        public int dodge = 0;
    }
    // TODO - adjust the plugin guid as needed
    [BepInAutoPlugin(id: "io.github.bobbythecatfish.easierfleacarnival")]
    public partial class EasierFleaCarnivalPlugin : BaseUnityPlugin
    {
        

        static ConstPatch constPatch;

        private void Awake()
        {
            // Put your initialization logic here
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");

            Harmony.CreateAndPatchAll(typeof(EasierFleaCarnivalPlugin));
            constPatch = new ConstPatch();
            SceneManager.sceneLoaded += OnSceneChange;
            Log.SetLogger(Logger);

            EasierFleaCarnival.Config.Init(Config);
            Values.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Log.LogInfo(Constants.GetConstantValue<int>("FLEA_FESTIVAL_CHAMP_JUGGLE"));
                //var fields = Constants.fieldCache.fieldInfos;
                //Logger.LogInfo($"There are {fields.Count} fields");
                //foreach (var fieldInfo in fields)
                //{
                //    Log.LogInfo(fieldInfo.Key);
                //    Log.LogInfo(fieldInfo.Value.fieldInfo.Name);
                //    Log.LogInfo(fieldInfo.Value.fieldInfo.GetRawConstantValue());
                //}
            }

        }

        private void OnSceneChange(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive) return;
            if (scene.name != "Aqueduct_05_festival") return;

            Speed.Init(scene);
            Log.LogInfo("Loaded the festival");
        }

        [HarmonyPatch(typeof(ScoreBoardUI), "Awake")]
        [HarmonyPrefix]
        static void ScoreboardAwake(ScoreBoardUI __instance)
        {
            Values.LoadScoreboard(__instance.gameObject);
            Values.SetScoreboard(__instance.gameObject);
        }

        [HarmonyPatch(typeof(ScoreBoardUI), "Refresh")]
        [HarmonyPrefix]
        static void ScoreboardRefresh(ScoreBoardUI __instance)
        {
            Values.LoadScoreboard(__instance.gameObject);
            Values.SetScoreboard(__instance.gameObject);
        }
    }
}
