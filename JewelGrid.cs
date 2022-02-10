using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularGems.Items.Jewels;
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
        public List<Item> grid = new List<Item>();

        internal void Update(Player player)
        {
            foreach(Item item in grid)
            {
                BasicJewel jewel = item.ModItem as BasicJewel;
                jewel.UpdateJewel(player);
            }
        }

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
            foreach (Item item in grid)
            {
                clone.grid.Add(item.Clone());
            }

            return clone;
        }
        public void addJewelToGrid(Item item)
        {
            Item toaddI = item.Clone();
            grid.Add(toaddI);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            
            foreach(Item item in grid)
            {
                BasicJewel jewel = item.ModItem as BasicJewel;
                jewel.jewel.Draw(spriteBatch, Position);
            }
        }

        internal bool tryAddJewelToGrid(Item item)
        {
            if (canFitJewelInGrid(item))
            {
                addJewelToGrid(item);
                return true;
            }
            return false;
            
        }
        internal Item removeJewelFromGrid(Item toremove)
        {
            foreach(Item item in grid)
            {
                
                if (item.Equals(toremove))
                {
                    Item give = item.Clone();
                    grid.Remove(item);
                    
                    return give;
                }
            }
            
            return new Item();
        }
        internal bool canFitJewelInGrid(Item canFit)
        {
            List<Point16> points = new List<Point16>();
            BasicJewel canFitJewel = canFit.ModItem as BasicJewel;
            foreach (Item item in grid)
            {
                BasicJewel jewel = item.ModItem as BasicJewel;
                foreach (Point16 point in jewel.jewel._shape)
                {
                    points.Add(point + jewel.jewel.anchor);
                }
            }
            foreach(Point16 point1 in canFitJewel.jewel._shape)
            {
                if (points.Contains(point1 + canFitJewel.jewel.anchor))
                {
                    return false;
                }
                if((point1.X + canFitJewel.jewel.anchor.X) < 0 || (point1.Y + canFitJewel.jewel.anchor.Y) < 0 || (point1.X + canFitJewel.jewel.anchor.X + 1) > Width || (point1.Y + canFitJewel.jewel.anchor.Y + 1) > Height){
                    return false;
                }
            }
            return true;
        }
        internal bool isHoveringOverJewel(out Item item, Point16 position)
        {
            item = new Item();
            foreach(Item item1 in grid)
            {
                BasicJewel jewel = item1.ModItem as BasicJewel;
                if (jewel.jewel._shape.Contains(position - jewel.jewel.anchor))
                {
                    item = item1; 
                    return true;
                }
            }
            return false;
        }
    }
}
