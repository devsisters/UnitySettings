using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class Config
    {
        private const string _prefKey = "UnitySettings_Config";

        private struct ViewConfig
        {
            public string View;
            public string Data;
        }

        public string StartView;
        private List<ViewConfig> _viewConfigs = new List<ViewConfig>();

        public static Config LoadFromPrefs()
        {
            var json = PlayerPrefs.GetString(_prefKey);
            if (string.IsNullOrEmpty(json)) return new Config();
            return JsonUtility.FromJson<Config>(json);
        }

        public void SaveToPrefs()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_prefKey, json);
        }
    }
}
