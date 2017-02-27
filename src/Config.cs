using UnityEngine;

[System.Serializable]
public class Config
{
    public ReportView StartView = ReportView.Logs;
    public bool Collapse = false;
    public bool ClearOnSceneLoad = false;
    public bool ShowTime = false;
    public bool ShowScene = false;
    public Log.Mask LogMask;
    public string FilterText = string.Empty;
    public float Size = 32;

    private const string _prefKey = "UnityReporter_Config";

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
