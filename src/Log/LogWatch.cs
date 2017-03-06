namespace Dashboard.Log
{
    internal class Watch : IBehaviourListener
    {
        private readonly IProvider _provider;
        private readonly Stash _stash;
        private readonly Broker _broker;

        public Watch(IProvider provider, ISampler sampler)
        {
            _provider = provider;
            _stash = new Stash();
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
            _broker.Transfer(_stash);
        }

        private void Clear()
        {
            _stash.Clear();
            _broker.Clear();
        }
    }
}

