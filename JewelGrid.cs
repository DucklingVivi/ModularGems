using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularGems.Jewels;
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

        public struct JewelGridData
        {
            public Jewel jewel;
            public Item item;
            public JewelGridData(Jewel jewel, Item item)
            {
                this.jewel = jewel;
                this.item = item;
            }
            public JewelGridData Clone()
            {
                return new JewelGridData(this.jewel.Clone(), this.item.Clone());
            }
        }

        public int Width;
        public int Height;
        public List<JewelGridData> jewelGridData = new List<JewelGridData>();

        internal void Update(Player player)
        {
            foreach(JewelGridData data in jewelGridData)
            {
                JewelComponentLoader.GetComponent(data.jewel.type).Update(player);
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
            foreach (JewelGridData data in jewelGridData)
            {
                clone.jewelGridData.Add(data.Clone());
            }

            return clone;
            throw new NotImplementedException();
        }
        public void addJewelToGrid(Jewel jewel, Item item)
        {
            Jewel toaddJ = jewel.Clone();
            Item toaddI = item.Clone();
            jewelGridData.Add(new JewelGridData(toaddJ,toaddI));
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            
            foreach(JewelGridData data in jewelGridData)
            {
                data.jewel.Draw(spriteBatch, Position);
            }
        }

        internal bool tryAddJewelToGrid(Jewel previewJewel, Item item)
        {
            if (canFitJewelInGrid(previewJewel))
            {
                addJewelToGrid(previewJewel, item.Clone());
                return true;
            }
            return false;
            
        }
        internal Item removeJewelFromGrid(Jewel jewel)
        {
            foreach(JewelGridData data in jewelGridData)
            {
                if (data.jewel.Equals(jewel))
                {
                    Item item = data.item.Clone();
                    item.GetGlobalItem<ModularGemsItem>().jewel = data.jewel;
                    jewelGridData.Remove(data);
                    
                    return item;
                }
            }
            
            return new Item();
        }
        internal bool canFitJewelInGrid(Jewel jewel)
        {
            List<Point16> points = new List<Point16>();
            foreach(JewelGridData data in jewelGridData)
            {
                foreach (Point16 point in data.jewel._shape)
                {
                    points.Add(point + data.jewel.anchor);
                }
            }
            foreach(Point16 point1 in jewel._shape)
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
        internal bool isHoveringOverJewel(out JewelGridData jewelData, Point16 position)
        {
            jewelData = new JewelGridData();
            foreach(JewelGridData data in jewelGridData)
            {
                if (data.jewel._shape.Contains(position - data.jewel.anchor))
                {
                    jewelData = data; 
                    return true;
                }
            }
            return false;
        }
    }
}
