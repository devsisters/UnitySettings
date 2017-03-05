using UnityEngine.SceneManagement;

namespace Dashboard.Log
{
    internal class Watch : IBehaviourListener
    {
        private readonly IProvider _provider;
        private readonly Stash _stash;
        private readonly Broker _broker;
        public bool ClearOnSceneLoad;

        public Watch(IProvider provider)
        {
            _provider = provider;
            _stash = new Stash();
            _broker = new Broker();
        }

        public override void OnEnable()
        {
            _broker.Connect(_provider);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            _broker.Disconnect();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public override void Update()
        {
            _broker.Transfer(_stash);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (ClearOnSceneLoad)
                Clear();
        }

        private void Clear()
        {
            _stash.Clear();
        }
    }
}

