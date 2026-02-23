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

namespace EasierFleaCarnival
{
    internal static class Speed
    {
        static GameObject controller;
        internal static float speed = 2;

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

        public static void SetJugglerSpeed()
        {
            if (controller == null) return;

            var fsm = controller.GetComponents<PlayMakerFSM>()[1];
            fsm.FsmVariables.floatVariables[0].Value = speed;

            SetAction(fsm, "Start Pause", 1);
            SetAction(fsm, "Single", 1);
            SetAction(fsm, "Double", 1);
            SetAction(fsm, "Triple", 1);
            SetAction(fsm, "Quad", 1);
            fsm.GetAction<SetFsmFloat>("Quick Flea", 2).setValue = speed;
            fsm.GetAction<SetFsmFloat>("Quick Flea", 4).setValue = speed;
            //SetAction(fsm, "Quick Flea", 2);
            //SetAction(fsm, "Quick Flea", 4);
            //SetAction(fsm, "Spawn FLea", 2);

            //var bTime = fsm.GetFloatVariable("Bounce Time");
            //bTime.Value

            //fsm.FsmStates[3].SaveActions();
            //fsm.FsmStates[6].SaveActions();
            //fsm.FsmStates[7].SaveActions();
            //fsm.FsmStates[8].SaveActions();
            //fsm.FsmStates[9].SaveActions();

            // Stage Check
            //var stage = fsm.FsmStates.First(s => s.name == "Stage Check");

            SetAction(fsm, "Stage Check", 1);
            SetAction(fsm, "Stage Check", 4);
            SetAction(fsm, "Stage Check", 6);
            SetAction(fsm, "Stage Check", 10);
            SetAction(fsm, "Stage Check", 12);
            SetAction(fsm, "Stage Check", 16);
            SetAction(fsm, "Stage Check", 18);
            SetAction(fsm, "Stage Check", 22);
            SetAction(fsm, "Stage Check", 24);
            SetAction(fsm, "Stage Check", 26);
            SetAction(fsm, "Stage Check", 30);
            SetAction(fsm, "Stage Check", 35);

            //fsm.GetAction<FloatAdd>("Stage Check", 33).add.Value /= speed;



            //stage.SaveActions();

        }

        static void SetAction(PlayMakerFSM fsm, string state, int actionIndex)
        {
            var action = fsm.GetAction<SetFloatValue>(state, actionIndex);
            if (action == null) return;
            action.floatValue = speed * action.floatValue.Value;
        }
    }
}
