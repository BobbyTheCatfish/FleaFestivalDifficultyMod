using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TMProOld;
using UnityEngine;

namespace EasierFleaCarnival
{
    class Game
    {
        public ConfigEntry<float> _multiplier;
        public float Multiplier => _multiplier?.Value ?? 1;
        public int Seth = 0;
        public int Champ = 0;
        public int Grish = 0;
        public int Vog = 0;
        public int Moosh = 0;
        public int Varg = 0;
        public int Kratt = 0;
        int index;

        public Game(ConfigEntry<float> mult, int seth, int champ, int index)
        {
            _multiplier = mult;
            Seth = seth;
            Champ = champ;
            this.index = index;
        }

        public void Init(GameObject ui)
        {
            var category = ui.transform.GetChild(2).GetChild(index);
            //Seth = seth;
            Grish = GetScore(category, "Score Badge Short");
            Vog = GetScore(category, "Score Badge Hunter");
            Moosh = GetScore(category, "Score Badge Leader");
            Varg = GetScore(category, "Score Badge Tall Variant");
            Kratt = GetScore(category, "Score Badge Lech");
            //Champ = champ;
            //Log.LogInfo("Grish:", Grish);
        }

        static GameObject GetBadge(Transform root, string name)
        {
            for (var i = 0; i < root.childCount; i++)
            {
                var child = root.GetChild(i);
                if (child.name == name) return child.gameObject;
            }

            return null;
        }

        static int GetScore(Transform root, string badgeName)
        {
            var badge = GetBadge(root, badgeName);
            if (badge == null) return 0;

            var scoreBadge = badge.GetComponent<ScoreBoardUIBadge>();
            return scoreBadge.score;
        }

        public int Score(int raw)
        {
            return (int)(raw * Multiplier);
        }

        public void SetScoreboard(GameObject ui)
        {
            var category = ui.transform.GetChild(2).GetChild(index);
            //Seth = seth;
            SetScore(category, "Score Badge Short", Grish);
            SetScore(category, "Score Badge Hunter", Vog);
            SetScore(category, "Score Badge Leader", Moosh);
            SetScore(category, "Score Badge Tall Variant", Varg);
            SetScore(category, "Score Badge Lech", Kratt);
        }

        void SetScore(Transform root, string badgeName, int score)
        {
            var badge = GetBadge(root, badgeName);
            if (badge == null) return;

            score = Score(score);
            //Log.LogInfo(score);

            var scoreBadge = badge.GetComponent<ScoreBoardUIBadge>();
            scoreBadge.score = score;

            var text = badge.transform.GetChild(2).GetComponent<TextMeshPro>();
            text?.text = score.ToString();
        }
    }
    internal static class Values
    {
        public static Game Juggle;
        public static Game Dodge;
        public static Game Bounce;

        static bool initialized = false;

        public static void Init()
        {
            Juggle = new Game(
                Config.JuggleMult,
                Constants.FLEA_FESTIVAL_SETH_JUGGLE,
                Constants.FLEA_FESTIVAL_CHAMP_JUGGLE,
                5
            );

            Dodge = new Game(
                Config.DodgeMult,
                Constants.FLEA_FESTIVAL_SETH_DODGE,
                Constants.FLEA_FESTIVAL_CHAMP_DODGE,
                7
            );

            Bounce = new Game(
                Config.BounceMult,
                Constants.FLEA_FESTIVAL_SETH_BOUNCE,
                Constants.FLEA_FESTIVAL_CHAMP_BOUNCE,
                9
            );
        }

        public static void LoadScoreboard(GameObject root)
        {
            if (initialized) return;

            Juggle.Init(root);
            Dodge.Init(root);
            Bounce.Init(root);
            
            initialized = true;
        }

        public static void SetScoreboard(GameObject root)
        {
            Juggle.SetScoreboard(root);
            Dodge.SetScoreboard(root);
            Bounce.SetScoreboard(root);
        }
    }
}
