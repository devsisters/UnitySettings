using System;
using UnityEngine;

namespace Settings.GUI
{
    public class Table
    {
        private readonly ScrollView _scroll;
        private float _scrollY
        {
            get { return _scroll.ScrollY; }
            set { _scroll.ScrollY = value; }
        }

        private readonly float _height;
        private readonly GUIStyle _bg;

        public Table(float height, float initScrollY, GUIStyle bg)
        {
            _scroll = new ScrollView(new Vector2(0, initScrollY), false, true);
            _height = height;
            _bg = bg;
        }

        public void Update()
        {
            _scroll.Update();
        }

        public void OnGUI(Rect area, int count, Action<int> drawer)
        {
            UnityEngine.GUI.Box(area, "", _bg);

            var viewRect = new Rect(0, 0, area.width, count * _height);
            _scroll.Begin(area, viewRect);

            var startIdx = Mathf.Clamp((int)(_scrollY / _height), 0, count);
            var visibleCount = (int)(area.height / _height) + 2;
            var drawedRows = 0;
            for (var i = startIdx; i < count && drawedRows < visibleCount; ++i, ++drawedRows)
            {
                var rowArea = new Rect(0, i * _height, area.width, _height);
                GUILayout.BeginArea(rowArea);
                drawer(i);
                GUILayout.EndArea();
            }

            _scroll.End();
        }

        public void SetScrollToKeepIn(Rect area, int idx)
        {
            var scrollMax = idx * _height;
            var scrollMin = scrollMax + _height - area.height;
            _scrollY = Mathf.Clamp(_scrollY, scrollMin, scrollMax);
        }
    }
}
