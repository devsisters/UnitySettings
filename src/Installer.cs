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

        private static bool InstallWithoutCreateInstaller()
        {
            if (_instance != null)
                return false;

            _instance = Object.FindObjectOfType<Settings>();
            var shouldInstantiate = _instance == null;
            if (shouldInstantiate)
            {
                var singleton = new GameObject("Settings (Singleton)");
                _instance = singleton.AddComponent<Settings>();
                DontDestroyOnLoad(singleton);
            }

            InjectDependency(_instance);
            return shouldInstantiate;
        }

        public static void Install()
        {
            if (!InstallWithoutCreateInstaller()) return;
            new GameObject("Installer (For Recompile)")
                .AddComponent<Installer>();
        }

        private void Awake()
        {
            InstallWithoutCreateInstaller();
            var isRecompiled = _instance.transform.childCount > 0;
            if (isRecompiled) Destroy(gameObject);
            else transform.SetParent(_instance.transform, false);
        }

        private void OnEnable()
        {
            InstallWithoutCreateInstaller();
        }

        private static void InjectDependency(Settings settings)
        {
            var icons = settings.Icons;

            // inject log
            {
                var provider = new Log.Provider();
                var sampler = new Log.Sampler();
                var watch = new Log.Watch(provider, sampler);
                settings.AddBehaviourListener(watch);

                var viewConfig = new Log.GUIView.Config(); // TODO
                var stash = watch.Stash;
                var organizer = stash.Organizer;
                var view = new Log.GUIView(viewConfig, icons, organizer);
                view.OnClickClear += () => stash.Clear();
                settings.AddGUIView("Log", view);
            }
        }
    }
}