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
    internal class DodgeSpeed : BaseSpeed
    {
        protected override float Speed => Mathf.Max(0.01f, Config.DodgeSpeedMult?.Value ?? 1);
        public override void SetSpeed()
        {
            if (Controller == null) return;

            var fsm = Controller.GetFsm("Game Specific Control")!;

            DoSpeedMult(fsm.GetFloatVariable("Flea Speed"));
            DoSpeedMult(fsm.GetFloatVariable("Flea Speed Initial"));
            DoSpeedMult(fsm.GetFloatVariable("Max Difficulty Score"), true);

            //fsm.FsmVariables.floatVariables[0].Value *= Speed;

            SetAction(fsm, "Start Pause", 1);
            DoSpeedMult(fsm.GetAction<IntCompare>("Quicken?", 1).integer2);
            DoSpeedMult(fsm.GetAction<IntCompare>("Choice", 3).integer2);
            DoSpeedMult(fsm.GetAction<IntTestToBool>("Choice", 4).int2);
            DoSpeedMult(fsm.GetAction<IntTestToBool>("Choice", 5).int2);
            DoSpeedMult(fsm.GetAction<IntCompare>("Choice", 3).integer2);

            SetTimings(fsm, 3, true);
            SetTimings(fsm, 4);
            SetTimings(fsm, 5);

            SetAction(fsm, "Min Difficulty", 0);
            SetAction(fsm, "Max Difficulty", 0);

            var fleas = Controller.transform.GetChild(5);
            var fleaCount = fleas.childCount;
            for (var i = 0; i < fleaCount; i++)
            {
                var flea = fleas.GetChild(i).gameObject;
                var fleaFsm = flea.GetFsm("Control");
                DoSpeedMult(fleaFsm.GetFloatVariable("Charge Speed"));
                DoSpeedMult(fleaFsm.GetFloatVariable("Max Difficulty Score"), true);
                DoSpeedMult(fleaFsm.GetAction<FloatOperator>("Set Speed", 6).float1);
                SetAction(fleaFsm, "Min Speed", 0);
                SetAction(fleaFsm, "Max Speed", 0);
            }


            //SetAction(fsm, "3 Even", 0, true);
            //SetAction(fsm, "3 Even", 1, true);
            //SetAction(fsm, "3 Early", 0, true);
            //SetAction(fsm, "3 Early", 1, true);
            //SetAction(fsm, "3 Late", 0, true);
            //SetAction(fsm, "3 Late", 1, true);

            //SetAction(fsm, "4 Even", 0, true);
            //SetAction(fsm, "4 Early", 0, true);
            //SetAction(fsm, "4 Even", 1, true);
            //SetAction(fsm, "4 Even", 2, true);
            //SetAction(fsm, "4 Early", 1, true);
            //SetAction(fsm, "4 Early", 2, true);

            //SetAction(fsm, "5 Even", 0, true);
            //SetAction(fsm, "5 Even", 1, true);
            //SetAction(fsm, "5 Even", 2, true);
            //SetAction(fsm, "5 Even", 3, true);
            //SetAction(fsm, "5 Early", 0, true);
            //SetAction(fsm, "5 Early", 1, true);
            //SetAction(fsm, "5 Early", 2, true);
            //SetAction(fsm, "5 Early", 3, true);





            //SetAction(fsm, "Single", 1);
            //SetAction(fsm, "Double", 1);
            //SetAction(fsm, "Triple", 1);
            //SetAction(fsm, "Quad", 1);
            //fsm.GetAction<SetFsmFloat>("Quick Flea", 2).setValue = Speed;
            //fsm.GetAction<SetFsmFloat>("Quick Flea", 4).setValue = Speed;
        }

        void SetTimings(PlayMakerFSM fsm, int fleaCount, bool hasLate = false)
        {
            for (var i = 0; i < fleaCount - 1; i++)
            {
                SetAction(fsm, $"{fleaCount} Even", i, true);
                if (hasLate)
                {
                    SetAction(fsm, $"{fleaCount} Early", i, true);
                    SetAction(fsm, $"{fleaCount} Late", i, true);
                }
                else
                {
                    SetAction(fsm, $"{fleaCount} Split", i, true);
                }
            }
        }
    }
}
