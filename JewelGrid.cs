using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace ModularGems
{
    public class JewelGrid
    {
        public int Width;
        public int Height;
        public List<Jewel> jewelList = new List<Jewel>();
        public JewelGrid()
        {

        }
        public JewelGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        internal JewelGrid Clone()
        {
            JewelGrid clone = new JewelGrid();
            clone.Width = Width;
            clone.Height = Height;
            foreach (Jewel jewel in jewelList)
            {
                clone.jewelList.Add(jewel.Clone());
            }

            return clone;
            throw new NotImplementedException();
        }
        public void addJewelToGrid(Jewel jewel, Point16 anchor)
        {
            Jewel toadd = jewel.Clone();
            toadd.anchor = anchor;
            jewelList.Add(toadd);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            foreach(Jewel jewel in jewelList)
            {
                jewel.Draw(spriteBatch, Position);
            }
        }
    }
}
