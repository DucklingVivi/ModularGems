


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;

namespace ModularGems
{
    public class Jewel
    {
        public List<Point16> Shape = new List<Point16>();
        public Color color;
        public Point16 anchor = new Point16(-1, -1);
        public Jewel()
        {
            color = Color.White;
        }
        public Jewel(List<Point16> shape, Color color)
        {
            this.Shape = new List<Point16>(shape);
            this.color = color;
        }

        internal Jewel Clone()
        {
            Jewel clone = new Jewel();
            clone.Shape = new List<Point16>(Shape);
            clone.color = color;
            clone.anchor = anchor;
            return clone;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            Main.NewText(color.ToString());
        }
    }
}