using UnityEngine;

namespace Settings.GUI
{
    public class ScrollView
    {
        private Vector2 _scroll;
        public Vector2 Scroll
        {
            get { return _scroll; }
            set { _scroll = value; }
        }

        public float ScrollX
        {
            get { return _scroll.x; }
            set { _scroll.x = value; }
        }

        public float ScrollY
        {
            get { return _scroll.y; }
            set { _scroll.y = value; }
        }

        private readonly bool _horizontal;
        private readonly bool _vertical;
        private Rect _lastArea;

        public ScrollView(Vector2 initScroll, bool horizontal, bool vertical)
        {
            _scroll = initScroll;
            _horizontal = horizontal;
            _vertical = vertical;
        }

        public void Update()
        {
            UpdateTouch();
        }

        public void Begin(Rect area, Rect viewRect)
        {
            _lastArea = area;
            var afterScroll = UnityEngine.GUI.BeginScrollView(
                area, _scroll, viewRect);
            if (_horizontal) _scroll.x = afterScroll.x;
            if (_vertical) _scroll.y = afterScroll.y;
            _scroll.x = Mathf.Clamp(_scroll.x, 0, viewRect.width - area.width);
            _scroll.y = Mathf.Clamp(_scroll.y, 0, viewRect.height - area.height);
        }

        public void End()
        {
            UnityEngine.GUI.EndScrollView();
        }

        public void BeginLayout(Rect area)
        {
            _lastArea = area;
            GUILayout.BeginArea(area);
            var afterScroll = GUILayout.BeginScrollView(_scroll);
            if (_horizontal) _scroll.x = afterScroll.x;
            if (_vertical) _scroll.y = afterScroll.y;
        }

        public void EndLayout()
        {
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void UpdateTouch()
        {
            // check collision
            Vector2 curPos;
            if (!Util.Mouse.CurPos(out curPos)) return;
            curPos = new Vector2(curPos.x, Screen.height - curPos.y);
            if (!_lastArea.Contains(curPos)) return;

            // check delta
            Vector2 delta;
            if (!Util.Mouse.Delta(out delta)) return;

            // update scroll
            if (_horizontal && delta.x != 0)
                _scroll.x -= delta.x;
            if (_vertical && delta.y != 0)
                _scroll.y += delta.y;
        }
    }
}
