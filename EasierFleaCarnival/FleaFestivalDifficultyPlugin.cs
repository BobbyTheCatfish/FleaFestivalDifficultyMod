using BepInEx;
using BepInEx.Logging;
using FleaFestivalDifficulty.Speed;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

namespace FleaFestivalDifficulty
{
    // TODO - adjust the plugin guid as needed
    [BepInAutoPlugin(id: "io.github.bobbythecatfish.FleaFestivalDifficulty")]
    public partial class FFDPlugin : BaseUnityPlugin
    {

        internal static JuggleSpeed JuggleSpeed = new();
        internal static BounceSpeed BounceSpeed = new();
        internal static DodgeSpeed DodgeSpeed = new();

        static ConstPatch constPatch;

        private void Awake()
        {
            // Put your initialization logic here
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");

            Harmony.CreateAndPatchAll(typeof(FFDPlugin));
            constPatch = new ConstPatch();
            SceneManager.sceneLoaded += OnSceneChange;
            Log.SetLogger(Logger);

            FleaFestivalDifficulty.Config.Init(Config);
            Scores.Init();
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

            JuggleSpeed.Init(scene, "Juggling");
            DodgeSpeed.Init(scene, "Dodging");
            BounceSpeed.Init(scene, "Bouncing");

            Log.LogInfo("Loaded the festival");
        }

        [HarmonyPatch(typeof(ScoreBoardUI), "Awake")]
        [HarmonyPrefix]
        static void ScoreboardAwake(ScoreBoardUI __instance)
        {
            Scores.LoadScoreboard(__instance.gameObject);
            Scores.SetScoreboard(__instance.gameObject);
        }

        [HarmonyPatch(typeof(ScoreBoardUI), "Refresh")]
        [HarmonyPrefix]
        static void ScoreboardRefresh(ScoreBoardUI __instance)
        {
            Scores.LoadScoreboard(__instance.gameObject);
            Scores.SetScoreboard(__instance.gameObject);
        }
    }
}
