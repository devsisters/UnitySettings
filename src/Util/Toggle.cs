namespace Settings
{
    internal struct Toggle
    {
        private bool _isOn;

        public bool On()
        {
            if (_isOn) return false;
            _isOn = true;
            return true;
        }

        public bool OnWithCheck()
        {
            var ret = On();
            if (!ret) L.SomethingWentWrong();
            return ret;
        }

        public bool Off()
        {
            if (!_isOn) return false;
            _isOn = false;
            return true;
        }

        public bool OffWithCheck()
        {
            var ret = Off();
            if (!ret) L.SomethingWentWrong();
            return ret;
        }

        public static implicit operator bool(Toggle thiz)
        {
            return thiz._isOn;
        }
    }
}
