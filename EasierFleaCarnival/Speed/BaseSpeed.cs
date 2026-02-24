using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FleaFestivalDifficulty.Speed
{
    internal abstract class BaseSpeed
    {
        protected abstract float Speed {  get; }
        protected GameObject Controller;
        public void Init(Scene scene, string gameName)
        {
            Controller = scene.FindGameObject($"Caravan_States/Flea Festival/Flea Game - {gameName}");
            if (Controller == null)
            {
                Log.LogError("Couldn't find game controller");
                return;
            }
            else
            {
                Log.LogWarning("Found game controller");
                SetSpeed();
            }
        }
        public void SetSpeed(object sender, EventArgs e)
        {
            SetSpeed();
        }
        public abstract void SetSpeed();

        
        protected void SetAction(PlayMakerFSM fsm, string state, int actionIndex, bool inverse = false)
        {
            var action = fsm.GetAction<SetFloatValue>(state, actionIndex);
            if (action == null) return;
            DoSpeedMult(action.floatValue, inverse);
        }

        protected void DoSpeedMult(FsmFloat fsmFloat, bool inverse = false)
        {
            if (inverse) fsmFloat.Value /= Speed;
            else fsmFloat.Value *= Speed;
        }

        protected void DoSpeedMult(FsmInt fsmInt, bool inverse = false)
        {
            if (inverse) fsmInt.Value = (int)(fsmInt.Value / Speed);
            else fsmInt.Value = (int)(fsmInt.Value * Speed);
        }
    }
}
