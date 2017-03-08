namespace Settings.Log
{
    internal class Watch : IBehaviourListener
    {
        public readonly Stash Stash;

        private readonly IProvider _provider;
        private readonly Broker _broker;

        public Watch(IProvider provider, ISampler sampler)
        {
            _provider = provider;
            Stash = new Stash();
            _broker = new Broker(sampler);
        }

        public override void OnEnable()
        {
            _broker.Connect(_provider);
        }

        public override void OnDisable()
        {
            _broker.Disconnect();
        }

        public override void Update()
        {
            _broker.Transfer(Stash);
        }

        private void Clear()
        {
            Stash.Clear();
            _broker.Clear();
        }
    }
}

