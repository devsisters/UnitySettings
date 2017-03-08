using UnityEngine;
using View = System.Collections.Generic.Dictionary<string, Settings.GUI.IView>;

namespace Settings.GUI
{
    internal class Drawer
    {
        private readonly View _views = new View(4);

        public void Add(string key, IView view)
        {
            _views.Add(key, view);
        }

        public void Update(string key)
        {
            IView view;
            if (!_views.TryGetValue(key, out view))
            {
                L.SomethingWentWrong();
                return;
            }

            view.Update();
        }

        public void OnGUI(string key)
        {
            IView view;
            if (!_views.TryGetValue(key, out view))
            {
                L.SomethingWentWrong();
                return;
            }

            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            view.Draw(screenRect);
        }
    }
}

// logDate = System.DateTime.Now.ToString();
// float toolbarOldDrag = 0;
// float oldDrag;
// float oldDrag2;
// float oldDrag3;

/*
public void INit(){
GUISkin skin = PNGs.reporterScrollerSkin;

toolbarScrollerSkin = (GUISkin)GameObject.Instantiate(skin);
toolbarScrollerSkin.verticalScrollbar.fixedWidth = 0f;
toolbarScrollerSkin.horizontalScrollbar.fixedHeight = 0f;
toolbarScrollerSkin.verticalScrollbarThumb.fixedWidth = 0f;
toolbarScrollerSkin.horizontalScrollbarThumb.fixedHeight = 0f;

logScrollerSkin = (GUISkin)GameObject.Instantiate(skin);
logScrollerSkin.verticalScrollbar.fixedWidth = size.x * 2f;
logScrollerSkin.horizontalScrollbar.fixedHeight = 0f;
logScrollerSkin.verticalScrollbarThumb.fixedWidth = size.x * 2f;
logScrollerSkin.horizontalScrollbarThumb.fixedHeight = 0f;

graphScrollerSkin = (GUISkin)GameObject.Instantiate(skin);
graphScrollerSkin.verticalScrollbar.fixedWidth = 0f;
graphScrollerSkin.horizontalScrollbar.fixedHeight = size.x * 2f;
graphScrollerSkin.verticalScrollbarThumb.fixedWidth = 0f;
graphScrollerSkin.horizontalScrollbarThumb.fixedHeight = size.x * 2f;
*/

// private readonly InfoDrawer _infoDrawer;
// private readonly ToolbarDrawer _toolbarDrawer;
// private readonly LogDrawer _logDrawer;
// private readonly Log.Stash _logStash;
// _infoDrawer = new InfoDrawer(icons, styles);
// _toolbarDrawer = new ToolbarDrawer(icons, styles);
// _logDrawer = new LogDrawer(icons, styles);
// _logStash = logStash;

// switch (view)
// {
//     case DashboardView.Info:
//         {
//             var screenRect = new Rect(0, 0, Screen.width, Screen.height);
//             _infoDrawer.Draw(screenRect, _sizeY * 2, _sizeX * 2);
//         }
//         break;
//     case DashboardView.Logs:
//         {
//             var toolbarHeight = _sizeY * 2;
//             var toolbarRect = new Rect(0, 0,
//                 Screen.width, toolbarHeight);
//             _toolbarDrawer.Draw(toolbarRect, _sizeX * 2);
//             var logRect = new Rect(0, toolbarHeight,
//                 Screen.width, Screen.height * 0.75f - toolbarHeight);
//             // TODO
//             _logDrawer.Draw(logRect, _sizeY, _sizeX, _logStash.Danger_All(), 0, default(Log.Mask), false, false);
//         }
//         break;
//     default:
//         L.Som // TODO
//         break;
// }
