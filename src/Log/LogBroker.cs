using System.Collections.Generic;
using UnityEngine;

namespace Log
{
    internal class Broker
    {
        private bool _isConnected = false;
        private bool _isQueueEmpty = true;
        private readonly List<Log> _queue = new List<Log>(16);
        private readonly Stash _stash;

        public Broker(Stash stash)
        {
            _stash = stash;
        }

        public void Connect()
        {
            if (_isConnected)
            {
                // something went wrong.
                return;
            }

            _isConnected = true;
            Application.logMessageReceivedThreaded += OnLogMessageReceivedThreaded;
        }

        public void Disconnect()
        {
            if (!_isConnected)
            {
                // something went wrong.
                return;
            }

            _isConnected = false;
            Application.logMessageReceivedThreaded -= OnLogMessageReceivedThreaded;
        }

        private void OnLogMessageReceivedThreaded(string message, string stacktrace, LogType type)
        {
            // TODO
            var log = new Log(type, message, stacktrace, 0, "");
            lock (_queue)
            {
                _isQueueEmpty = false;
                _queue.Add(log);
            }
        }

        public void Update()
        {
            if (_isQueueEmpty) return;
            lock (_queue)
            {
                foreach (var l in _queue)
                    _stash.Add(l);
                _isQueueEmpty = true;
                _queue.Clear();
            }
        }
    }
}

//         if (selectedLog != null)
//         {
//             int newSelectedIndex = currentLog.IndexOf(selectedLog);
//             if (newSelectedIndex == -1)
//             {
//                 Log collapsedSelected = logsDic[selectedLog.condition][selectedLog.stacktrace];
//     newSelectedIndex = currentLog.IndexOf(collapsedSelected);
//                 if (newSelectedIndex != -1)
//                     scrollPosition.y = newSelectedIndex* size.y;
// }
//             else
//             {
//                 scrollPosition.y = newSelectedIndex* size.y;
//             }
//         }
//     }
// }
