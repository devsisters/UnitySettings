using UnityEngine;

namespace Settings.GUI
{
    public static class Helper
    {
        public static Texture2D Solid(Color color)
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color); tex.Apply();
            return tex;
        }

        public static Texture2D Solid(uint color)
        {
            Color32 c;
            c.r = (byte)((color >> 24) & 0xFF);
            c.g = (byte)((color >> 16) & 0xFF);
            c.b = (byte)((color >> 8) & 0xFF);
            c.a = (byte)((color) & 0xFF);
            return Solid(c);
        }

    }
}
