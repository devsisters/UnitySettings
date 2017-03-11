using LogType = UnityEngine.LogType;

namespace Settings.Log
{
    public struct Mask
    {
        private struct ByteMask
        {
            private byte _mask;
            public bool IsAll1 { get { return _mask == 0xff; } }
            public bool IsAll0 { get { return _mask == 0; } }

            public bool this[byte bit]
            {
                get { return (_mask & (1 << bit)) != 0; }
                set
                {
                    if (value) _mask = (byte)(_mask | (1 << bit));
                    else _mask = (byte)(_mask & ~(1 << bit));
                }
            }

            public byte Get() { return _mask; }
            public void All1() { _mask = 0xff; }
            public void All0() { _mask = 0; }
        }

        public static Mask AllTrue;

        static Mask()
        {
            AllTrue.SetAllTrue();
        }

        private ByteMask _mask;

        public bool Log { get { return _mask[0]; } set { _mask[0] = value; } }
        public bool Warning { get { return _mask[1]; } set { _mask[1] = value; } }
        public bool Error { get { return _mask[2]; } set { _mask[2] = value; } }
        public bool Assert { get { return _mask[3]; } set { _mask[3] = value; } }
        public bool Exception { get { return _mask[4]; } set { _mask[4] = value; } }

        public bool IsAllTrue { get { return _mask.IsAll1; } }
        public bool IsAllFalse { get { return _mask.IsAll0; } }

        public bool Check(LogType type)
        {
            switch (type)
            {
                case LogType.Log: return Log;
                case LogType.Warning: return Warning;
                case LogType.Error: return Error;
                case LogType.Assert: return Assert;
                case LogType.Exception: return Exception;
                default:
                    L.SomethingWentWrong();
                    return false;
            }
        }

        public void SetAllTrue() { _mask.All1(); }
        public void SetAllFalse() { _mask.All0(); }

        public override int GetHashCode() { return _mask.Get(); }

        public bool Equals(Mask other)
        {
            return _mask.Get() == other._mask.Get();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Mask)) return false;
            return Equals((Mask)obj);
        }

        public static bool operator ==(Mask a, Mask b) { return a.Equals(b); }
        public static bool operator !=(Mask a, Mask b) { return !(a == b); }
    }
}
