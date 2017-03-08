using System;
using UnityEngine;

namespace Settings.GUI
{
    public class Table
    {
        private float _scrollY;
        private readonly float _height;
        private readonly GUIStyle _bg;

        public Table(float height, float initScrollY, GUIStyle bg)
        {
            _scrollY = initScrollY;
            _height = height;
            _bg = bg;
        }

        private void UpdateScroll(Rect area)
        {
            Vector2 curPos;
            if (!Util.Mouse.CurPos(out curPos)) return;
            curPos = new Vector2(curPos.x, Screen.height - curPos.y);
            if (!area.Contains(curPos)) return;

            Vector2 delta;
            if (!Util.Mouse.Delta(out delta)) return;
            if (delta.y == 0) return;

            // TODO: why scale 4?
            _scrollY += delta.y / 4;
        }

        public void Draw(Rect area, int count, Action<Rect, int> drawer)
        {
            UpdateScroll(area);

            _scrollY = UnityEngine.GUI.BeginScrollView(
                area, new Vector2(0, _scrollY),
                new Rect(0, 0, area.width, count * _height)).y;

            var startIdx = (int)(_scrollY / _height);
            var visibleCount = (int)(area.height / _height) + 2;
            var drawedRows = 0;
            for (var i = startIdx; i < count && drawedRows < visibleCount; ++i, ++drawedRows)
            {
                var rowArea = new Rect(0, i * _height, area.width, _height);
                GUILayout.BeginArea(rowArea);
                drawer(rowArea, i);
                GUILayout.EndArea();
            }

            UnityEngine.GUI.EndScrollView();
        }

        public void SetScrollToKeepIn(Rect area, int idx)
        {
            var scrollMax = idx * _height;
            var scrollMin = scrollMax + _height - area.height;
            _scrollY = Mathf.Clamp(_scrollY, scrollMin, scrollMax);
        }
    }
}
