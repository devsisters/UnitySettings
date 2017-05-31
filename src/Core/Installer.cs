using UnityEngine;
using SystemInfo = Settings.Extension.SystemInfo;
using Log = Settings.Extension.Log;
using Scene = Settings.Extension.Scene;

namespace Settings
{
    [AddComponentMenu("Settings/Settings.Installer")]
    public class Installer : MonoBehaviour
    {
        public bool EnableGesture = true;
        public KeyCode KeyboardShortcutForShow = KeyCode.BackQuote;

        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                Install();
                return _instance;
            }
        }

        private static bool InstantiateSettings()
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

            // for readability
            var isInstantiated = shouldInstantiate;
            return isInstantiated;
        }

        public static void Install()
        {
            if (!Application.isPlaying) return;
            if (!InstantiateSettings()) return;
            var installer = Object.FindObjectOfType<Installer>();
            if (installer == null) new GameObject().AddComponent<Installer>();
        }

        private void Awake()
        {
            var isInstantiated = InstantiateSettings();

            // if there's another Installer already, just destroy.
            if (transform.parent != _instance.transform.parent
                && _instance.transform.childCount > 0)
            {
                Destroy(gameObject);
                return;
            }

            gameObject.name = "Installer (For Recompile)";

            // change parent to Settings.
            if (transform.parent != _instance.transform.parent)
                transform.SetParent(_instance.transform, false);

            // initialize settings
            if (isInstantiated)
            {
                _instance.EnableGesture = EnableGesture;
                _instance.KeyboardShortcutForShow = KeyboardShortcutForShow;
            }
        }

        private static void InjectDependency(Settings settings)
        {
            // inject SystemInfo
            {
                settings.AddView(new SystemInfo.View());
            }

            // inject Log
            {
                var provider = new Log.Provider();
                var sampler = new Log.Sampler();
                var watch = new Log.Watch(provider, sampler);
                settings.AddBehaviourListener(watch);

                var viewConfig = new Log.View.Config(); // TODO
                var stash = watch.Stash;
                var organizer = stash.Organizer;
                var view = new Log.View(viewConfig, organizer);
                view.OnClickClear += () => stash.Clear();
                settings.AddView(view);
            }

            // inject scene
            {
                settings.AddView(new Scene.View());
            }
        }
    }
}
