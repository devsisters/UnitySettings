/*
using System.Collections.Generic;
using UnityEngine;

namespace Settings.GUI
{
    internal class ToolbarDrawer
    {
        public delegate void OnClick();
        public delegate bool Getter();
        public delegate bool Setter(bool value);
        public delegate string GetLabel();

        private class Button
        {
            public readonly GUIContent Icon;
            public readonly OnClick OnClick;
            public Button(GUIContent icon, OnClick onClick)
            {
                Icon = icon;
                OnClick = onClick;
            }
        }

        private class Toggle
        {
            public readonly GUIContent Icon;
            public readonly Getter Getter;
            public readonly Setter Setter;
            public readonly GetLabel GetLabel;

            public Toggle(GUIContent icon, Getter getter, Setter setter, GetLabel getLabel)
            {
                Icon = icon;
                Getter = getter;
                Setter = setter;
                GetLabel = getLabel;
            }
        }

        private readonly Icons _icons;
        private readonly Styles _styles;
        private float _scrollX;
        private readonly List<object> _buttons = new List<object>(16);

        public ToolbarDrawer(Icons icons, Styles styles)
        {
            _icons = icons;
            _styles = styles;
            RegisterDefaults();
        }

        private void RegisterDefaults()
        {
            var i = _icons;
            // TODO
            AddButton(i.Clear, null);
            AddToggle(i.Collapse, null, null);
            AddToggle(i.ClearOnNewScene, null, null);
            AddToggle(i.ShowTime, null, null,
                () => Time.realtimeSinceStartup.ToString("0.0"));
            AddToggle(i.ShowScene, null, null,
                () => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            AddButton(i.Search, null);
            AddButton(i.Info, null);

            AddToggle(i.Log, null, null, null);
            AddToggle(i.Warning, null, null, null);
            AddToggle(i.Error, null, null, null);
            AddToggle(i.Close, null, null, null);
        }

        public ToolbarDrawer AddButton(GUIContent icon, OnClick onClick)
        {
            _buttons.Add(new Button(icon, onClick));
            return this;
        }

        public ToolbarDrawer AddToggle(GUIContent icon, Getter getter, Setter setter, GetLabel getLabel = null)
        {
            _buttons.Add(new Toggle(icon, getter, setter, getLabel));
            return this;
        }

        private void UpdateScroll(Rect area)
        {
            Vector2 curPos;
            if (!Util.Mouse.CurPos(out curPos)) return;
            curPos = new Vector2(curPos.x, Screen.height - curPos.y);
            if (!area.Contains(curPos)) return;

            Vector2 delta;
            if (!Util.Mouse.Delta(out delta)) return;
            if (delta.x == 0) return;

            _scrollX -= delta.x;
        }

        public void Draw(Rect area, float buttonW)
        {
            UpdateScroll(area);

            var btnGUIStyle = _styles.Bar;
            var btnActiveStyle = _styles.ButtonActive;
            var btnGUIOptions = new[] {
                GUILayout.Width(buttonW),
                GUILayout.Height(area.height),
            };

            //toolbarScrollerSkin.verticalScrollbar.fixedWidth = 0f;
            //toolbarScrollerSkin.horizontalScrollbar.fixedHeight= 0f;
            // GUI.skin = toolbarScrollerSkin;

            GUILayout.BeginArea(area);
            _scrollX = GUILayout.BeginScrollView(new Vector2(_scrollX, 0)).x;
            GUILayout.BeginHorizontal(btnGUIStyle);

            foreach (var b in _buttons)
            {
                if (b is Button)
                {
                    var btn = b as Button;
                    if (GUILayout.Button(btn.Icon, btnGUIStyle, btnGUIOptions))
                        btn.OnClick();
                }
                else if (b is Toggle)
                {
                    var btn = b as Toggle;
                    var curValue = btn.Getter();
                    var style = curValue ? btnActiveStyle : btnGUIStyle;
                    if (GUILayout.Button(btn.Icon, style, btnGUIOptions))
                        btn.Setter(!curValue);
                    if (btn.GetLabel != null)
                    {
                        var tempRect = GUILayoutUtility.GetLastRect();
                        UnityEngine.GUI.Label(tempRect, btn.GetLabel(), _styles.LowerLeftFont);
                    }
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}
 */
