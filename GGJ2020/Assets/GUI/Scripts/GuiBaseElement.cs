using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GUI
{
    public class GuiBaseElement
    {
        public Rectangle Rect { get; set; } = new Rectangle(0, 0, 1, 1);
        public virtual void Update() { }
        public virtual void Initialize() { }
        public virtual void Destroy() { }
    }

    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void SetToRect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int GetXPosition() => X;
        public int GetYPosition() => Y;
        public int GetWidth() => Width;
        public int GetHeight() => Height;

    }
}
