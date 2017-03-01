using UnityEngine;

namespace Dashboard
{
    [System.Serializable]
    public class Config
    {
        public string StartView = "Log";
        public bool Collapse = false;
        public bool ClearOnSceneLoad = false;
        public bool ShowTime = false;
        public bool ShowScene = false;
        public Log.Mask LogMask;
        public string FilterText = string.Empty;
        public float Size = 32;

        private const string _prefKey = "UnityDashboard_Config";

        public static Config LoadFromPrefs()
        {
            var json = PlayerPrefs.GetString(_prefKey);
            if (string.IsNullOrEmpty(json)) return default(Config);
            return JsonUtility.FromJson<Config>(json);
        }

        public void SaveToPrefs()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_prefKey, json);
        }
    }
}
