using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Extension.FPS
{
    internal class Counter : IBehaviourListener
    {
        private const float _interval = 1f;

        public int FPS { get; private set; }
        private int _lastFrameCount = 0;
        private float _nextTimeToSample = 0;

        public override void OnEnable()
        {
            FPS = Application.targetFrameRate;
            _lastFrameCount = Time.frameCount;
            _nextTimeToSample = Time.realtimeSinceStartup + _interval;
        }

        public override void Update(bool isShowingGUI)
        {
            if (Time.realtimeSinceStartup > _nextTimeToSample)
            {
                FPS = Time.frameCount - _lastFrameCount;
                _lastFrameCount = Time.frameCount;
                _nextTimeToSample += _interval;
            }
        }
    }
}
