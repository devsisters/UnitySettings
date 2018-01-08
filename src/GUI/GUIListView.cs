using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.GUI
{
    public struct ListViewItem
    {
        public string Text;
        public bool ShouldHighlight;

        public ListViewItem(string text, bool shouldHighlight)
        {
            Text = text;
            ShouldHighlight = shouldHighlight;
        }
    }

    public interface IListViewDelegate
    {
        int Count { get; }
        ListViewItem GetItem(int index);
        void OnSelect(int index);
    }

    public class ListView : IView
    {
        private static class Styles
        {
            public static readonly GUIStyle ButtonEven;
            public static readonly GUIStyle ButtonOdd;
            public static readonly GUIStyle ButtonSelected;
            public static readonly GUIStyle ButtonFocusOverlay;

            static Styles()
            {
                ButtonEven = new GUIStyle(GUI.Styles.ButtonLightGray);
                ButtonEven.alignment = TextAnchor.MiddleCenter;
                ButtonOdd = new GUIStyle(GUI.Styles.ButtonGray);
                ButtonOdd.alignment = TextAnchor.MiddleCenter;
                ButtonSelected = new GUIStyle(GUI.Styles.ButtonGraySelected);
                ButtonSelected.alignment = TextAnchor.MiddleCenter;
                ButtonFocusOverlay = new GUIStyle();
                ButtonFocusOverlay.normal.background = GUI.Helper.Solid(0xffff0060);
            }
        }

        private readonly int _cellHeight;
        private readonly IListViewDelegate _delegate;
        private int? _focus;

        public ListView(string title, GUIContent toolbarIcon, int cellHeight, IListViewDelegate dataProvider)
            : base(title, toolbarIcon)
        {
            _cellHeight = cellHeight;
            _delegate = dataProvider;
        }

        public override void Update()
        {
            UpdateFocus();
        }

        private void UpdateFocus()
        {
            // up down focus
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!_focus.HasValue) _focus = -1;
                else --_focus;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_focus.HasValue) _focus = 0;
                else ++_focus;
            }

            if (!_focus.HasValue) return;

            // cull focus
            var max = _delegate.Count;
            _focus = (_focus.Value + max) % max;

            // on hit space
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _delegate.OnSelect(_focus.Value);
                _focus = null;
            }
        }

        public override void OnGUI(Rect area)
        {
            GUILayout.BeginArea(area, GUI.Styles.BG);

            for (var i = 0; i != _delegate.Count; ++i)
            {
                var item = _delegate.GetItem(i);

                // select style
                var style = item.ShouldHighlight
                    ? Styles.ButtonSelected
                    : (i % 2 == 0 ? Styles.ButtonEven : Styles.ButtonOdd);

                // draw button
                var rect = new Rect(0, i * _cellHeight, area.width, _cellHeight);
                if (UnityEngine.GUI.Button(rect, item.Text, style))
                    _delegate.OnSelect(i);

                // draw overlay
                if (i == _focus)
                    UnityEngine.GUI.Box(rect, "", Styles.ButtonFocusOverlay);
            }

            GUILayout.EndArea();
        }
    }
}