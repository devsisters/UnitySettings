using L = UnityEngine.LogType;

namespace Log
{
    public struct Mask
    {
        private struct ByteMask
        {
            private byte _mask;
            public bool this[byte bit]
            {
                get { return (_mask & (1 << bit)) != 0; }
                set
                {
                    if (value) _mask = (byte)(_mask | (1 << bit));
                    else _mask = (byte)(_mask & ~(1 << bit));
                }
            }
        }

        private ByteMask _mask;

        public bool Log { get { return _mask[0]; } set { _mask[0] = value; } }
        public bool Warning { get { return _mask[1]; } set { _mask[1] = value; } }
        public bool Error { get { return _mask[2]; } set { _mask[2] = value; } }
        public bool Assert { get { return _mask[3]; } set { _mask[3] = value; } }
        public bool Exception { get { return _mask[4]; } set { _mask[4] = value; } }

        public bool Check(L type)
        {
            switch (type)
            {
                case L.Log: return Log;
                case L.Warning: return Warning;
                case L.Error: return Error;
                case L.Assert: return Assert;
                case L.Exception: return Exception;
                default:
                    // something went wrong.
                    return false;
            }
        }
    }
}
