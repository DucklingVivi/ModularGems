
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader.IO;
using static ModularGems.JewelGrid;

namespace ModularGems
{
    internal class JewelGridIO
    {
        internal static TagCompound Save(JewelGrid grid)
        {
            TagCompound tag = new TagCompound();
            List<TagCompound> tagList = new List<TagCompound>();
            foreach (Item item in grid.grid)
            {
                tagList.Add(ItemIO.Save(item));
            }
            tag.Set("width", grid.Width);
            tag.Set("height", grid.Height);
            tag.Set("gridlist", tagList);
            return tag;
        }

        internal static JewelGrid Load(TagCompound tag)
        {
            int width = tag.GetInt("width");
            int height = tag.GetInt("height");
            JewelGrid grid = new JewelGrid(width, height);
            List<Item> itemList = new List<Item>();
            List<TagCompound> tagList = tag.GetList<TagCompound>("gridlist").ToList();

            for (int i = 0; i < tagList.Count; i++)
            {
                Item item = ItemIO.Load(tagList[i]);
                itemList.Add(item);
            }
            grid.grid = itemList;

            return grid;
        }

        internal static void Send(JewelGrid grid, BinaryWriter writer)
        {
            writer.Write(grid.Width);
            writer.Write(grid.Height);
            writer.Write(grid.grid.Count);
            for (int i = 0; i < grid.grid.Count; i++)
            {
                ItemIO.Send(grid.grid[i], writer);
              
            }
        }

        internal static JewelGrid Recieve(BinaryReader reader)
        {
            JewelGrid retgrid = new JewelGrid();
            retgrid.Width = reader.ReadInt32();
            retgrid.Height = reader.ReadInt32();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                Item item = ItemIO.Receive(reader);
                retgrid.grid.Add(item);
                
            }
            return retgrid;
        }
    }
}