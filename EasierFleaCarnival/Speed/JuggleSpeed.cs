using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FleaFestivalDifficulty.Speed
{
    internal class JuggleSpeed : BaseSpeed
    {
        protected override float Speed => Mathf.Max(0.01f, Config.JuggleSpeedMult?.Value ?? 1);
        public override void SetSpeed()
        {
            if (Controller == null) return;

            var fsm = Controller.GetFsm("Game Specific Control")!;
            DoSpeedMult(fsm.GetFloatVariable("Bounce Time"), true);

            SetAction(fsm, "Start Pause", 1, true);
            SetAction(fsm, "Single", 1, true);
            SetAction(fsm, "Double", 1, true);
            SetAction(fsm, "Triple", 1, true);
            SetAction(fsm, "Quad", 1, true);


            DoSpeedMult(fsm.GetAction<SetFsmFloat>("Quick Flea", 2).setValue, true);
            DoSpeedMult(fsm.GetAction<SetFsmFloat>("Quick Flea", 4).setValue, true);

            // Stage Check
            SetAction(fsm, "Stage Check", 1, true);
            SetAction(fsm, "Stage Check", 4, true);
            SetAction(fsm, "Stage Check", 6, true);
            SetAction(fsm, "Stage Check", 10, true);
            SetAction(fsm, "Stage Check", 12, true);
            SetAction(fsm, "Stage Check", 16, true);
            SetAction(fsm, "Stage Check", 18, true);
            SetAction(fsm, "Stage Check", 22, true);
            SetAction(fsm, "Stage Check", 24, true);
            SetAction(fsm, "Stage Check", 26, true);
            SetAction(fsm, "Stage Check", 30, true);
            SetAction(fsm, "Stage Check", 35, true);

            //fsm.GetAction<FloatAdd>("Stage Check", 33).add.Value /= speed;


            // Special fleas

            // Varga and Grishkin
            SetSpecialFleaSpeed(4);
            SetSpecialFleaSpeed(5);

            var bigboi = Controller.transform.GetChild(10).GetChild(0);
            fsm = bigboi.gameObject.GetFsm("Control")!;
            DoSpeedMult(fsm.GetFloatVariable("Bounce Time"), true);

            DoSpeedMult(fsm.GetAction<FloatMultiply>("BounceAway Start", 7).multiplyBy, true);
            DoSpeedMult(fsm.GetAction<FloatMultiply>("GiantAway Start", 4).multiplyBy, true);
        }

        void SetSpecialFleaSpeed(int index)
        {
            var flea = Controller.transform.GetChild(index);
            var fsm = flea.gameObject.GetFsm("Control")!;

            DoSpeedMult(fsm.GetFloatVariable("Bounce Time"), true);
            SetAction(fsm, "Varga Setup", 15, true);
        }
    }
    internal static class JuggleBkp
    {
        static GameObject controller;
        internal static float JuggleSpeed => Config.JuggleSpeedMult?.Value ?? 1;

        public static void Init(Scene scene)
        {
            controller = scene.FindGameObject("Caravan_States/Flea Festival/Flea Game - Juggling");
            if (controller == null)
            {
                Log.LogError("Couldn't find game controller");
                return;
            }
            else
            {
                Log.LogWarning("Found game controller");
                SetJugglerSpeed();
            }
        }

        public static void SetJugglerSpeed(object sender, EventArgs e)
        {
            SetJugglerSpeed();
        }

        public static void SetJugglerSpeed()
        {
        }

        static void SetSpecialFleaSpeed(int index)
        {
            var flea = controller.transform.GetChild(index);
            var fsm = flea.GetComponentInChildren<PlayMakerFSM>(true);

            fsm.GetFloatVariable("Bounce Time").value *= JuggleSpeed;
            SetAction(fsm, "Varga Setup", 15);
        }

        static void SetAction(PlayMakerFSM fsm, string state, int actionIndex)
        {
            var action = fsm.GetAction<SetFloatValue>(state, actionIndex);
            if (action == null) return;
            action.floatValue = JuggleSpeed * action.floatValue.Value;
        }
    }
}
