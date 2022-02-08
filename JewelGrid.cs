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
        public void addJewelToGrid(Jewel jewel)
        {
            Jewel toadd = jewel.Clone();
            jewelList.Add(toadd);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            
            foreach(Jewel jewel in jewelList)
            {
                jewel.Draw(spriteBatch, Position);
            }
        }

        internal bool tryAddJewelToGrid(Jewel previewJewel)
        {
            if (canFitJewelInGrid(previewJewel))
            {
                addJewelToGrid(previewJewel);
                return true;
            }
            return false;
            
        }
        internal void removeJewelFromGrid(Jewel jewel)
        {
            jewelList.Remove(jewel);
        }
        internal bool canFitJewelInGrid(Jewel jewel)
        {
            List<Point16> points = new List<Point16>();
            foreach(Jewel existingJewel in jewelList)
            {
                foreach (Point16 point in existingJewel.Shape)
                {
                    points.Add(point + existingJewel.anchor);
                }
            }
            foreach(Point16 point1 in jewel.Shape)
            {
                if (points.Contains(point1 + jewel.anchor))
                {
                    return false;
                }
                if((point1.X + jewel.anchor.X) < 0 || (point1.Y + jewel.anchor.Y) < 0 || (point1.X + jewel.anchor.X + 1) > Width || (point1.Y + jewel.anchor.Y + 1) > Height){
                    return false;
                }
            }
            return true;
        }
        internal bool isHoveringOverJewel(out Jewel jewel, Point16 position)
        {
            jewel = null;
            foreach(Jewel checkingJewel in jewelList)
            {
                if (checkingJewel.Shape.Contains(position - checkingJewel.anchor))
                {
                    jewel = checkingJewel; 
                    return true;
                }
            }
            return false;
        }
    }
}
