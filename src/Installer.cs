using UnityEngine;

namespace Settings
{
    public class Installer : MonoBehaviour
    {
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                Install();
                return _instance;
            }
        }

        public static void Install()
        {
            if (_instance != null) return;
            var singleton = new GameObject("Settings (Singleton)");
            _instance = singleton.AddComponent<Settings>();
            InjectDependency(_instance);
            DontDestroyOnLoad(singleton);
        }

        private void Awake()
        {
            Install();
            Destroy(gameObject);
        }

        private static void InjectDependency(Settings settings)
        {
            var icons = GUI.Icons.Load();

            var logProvider = new Log.Provider();
            var logSampler = new Log.Sampler();
            var logWatch = new Log.Watch(logProvider, logSampler);
            settings.AddBehaviourListener(logWatch);
            settings.AddGUIView("Log", new Log.GUIView(icons, logWatch.Stash));
        }
    }
}