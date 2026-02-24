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
    internal class BounceSpeed : BaseSpeed
    {
        protected override float Speed => Mathf.Max(0.01f, Config.BounceSpeedMult?.Value ?? 1);
        public override void SetSpeed()
        {
            if (Controller == null) return;

            var fsm = Controller.GetFsm("Game Specific Control")!;
            DoSpeedMult(fsm.GetFloatVariable("Flea Speed"));
            DoSpeedMult(fsm.GetFloatVariable("Flea Speed Initial"));
            DoSpeedMult(fsm.GetFloatVariable("Spawn Frequency"));

            SetAction(fsm, "Start Pause", 2);
            SetAction(fsm, "Increase Difficulty", 1);

            DoSpeedMult(fsm.GetAction<FloatClamp>("Increase Difficulty", 3).maxValue);
            
            DoSpeedMult(fsm.GetAction<FloatAdd>("Shorten Pause?", 1).add);
            DoSpeedMult(fsm.GetAction<FloatClamp>("Shorten Pause?", 2).maxValue);

            DoSpeedMult(fsm.GetAction<FloatAdd>("Lengthen Pause", 0).add);
            DoSpeedMult(fsm.GetAction<FloatClamp>("Lengthen Pause", 1).minValue);
            DoSpeedMult(fsm.GetAction<FloatClamp>("Lengthen Pause", 1).maxValue);
        }
    }
}
