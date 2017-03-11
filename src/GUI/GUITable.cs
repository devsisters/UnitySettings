using System;
using UnityEngine;

namespace Settings.GUI
{
    public class Table
    {
        public enum Direction { Horizontal, Vertical, }

        private readonly Direction _dir;
        private readonly ScrollView _scroll;
        private readonly float _rowSize;
        private readonly GUIStyle _bg;

        private bool _isHorizontal { get { return _dir == Direction.Horizontal; } }
        private bool _isVertical { get { return _dir == Direction.Vertical; } }
        private float _scrollAmount
        {
            get { return ScrollSide(_scroll.Scroll); }
            set
            {
                if (_isHorizontal) _scroll.ScrollX = value;
                else _scroll.ScrollY = value;
            }
        }

        public Table(Direction dir, float rowSize, float initScroll, GUIStyle bg)
        {
            _dir = dir;
            _rowSize = rowSize;
            _bg = bg;
            _scroll = new ScrollView(Align(0, initScroll), _isHorizontal, _isVertical);
        }

        private Vector2 Align(float fixedSide, float scrollSide)
        {
            if (_isHorizontal) return new Vector2(scrollSide, fixedSide);
            else return new Vector2(fixedSide, scrollSide);
        }

        private float FixedSide(Rect rect)
        {
            if (_isHorizontal) return rect.height;
            else return rect.width;
        }

        private float ScrollSide(Vector2 v)
        {
            if (_isHorizontal) return v.x;
            else return v.y;
        }

        private float ScrollSide(Rect rect)
        {
            if (_isHorizontal) return rect.width;
            else return rect.height;
        }

        public void Update()
        {
            _scroll.Update();
        }

        public void OnGUI(Rect area, int count, Action<int> drawer)
        {
            UnityEngine.GUI.Box(area, "", _bg);

            var fixedSide = FixedSide(area);
            var scrollSide = ScrollSide(area);

            var viewSize = Align(fixedSide, count * _rowSize);
            var viewRect = new Rect(0, 0, viewSize.x, viewSize.y);
            _scroll.Begin(area, viewRect);

            var startIdx = Mathf.Clamp((int)(_scrollAmount / _rowSize), 0, count);
            var visibleCount = (int)(scrollSide / _rowSize) + 2;
            var rowSize = Align(fixedSide, _rowSize);
            var drawedRows = 0;
            for (var i = startIdx; i < count && drawedRows < visibleCount; ++i, ++drawedRows)
            {
                var rowPos = Align(0, i * _rowSize);
                var rowArea = new Rect(rowPos.x, rowPos.y, rowSize.x, rowSize.y);
                GUILayout.BeginArea(rowArea);
                drawer(i);
                GUILayout.EndArea();
            }

            _scroll.End();
        }

        public void SetScrollToKeepIn(Rect area, int idx)
        {
            var scrollMax = idx * _rowSize;
            var scrollMin = scrollMax + _rowSize - ScrollSide(area);
            _scrollAmount = Mathf.Clamp(_scrollAmount, scrollMin, scrollMax);
        }
    }
}
