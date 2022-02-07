using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;

namespace ModularGems
{
    internal class JewelGridIO
    {
        internal static TagCompound Save(JewelGrid grid)
        {
            TagCompound tag = new TagCompound();
            List<TagCompound> tagList = new List<TagCompound>();
            foreach (Jewel jewel in grid.jewelList)
            {
                tagList.Add(JewelIO.Save(jewel));
            }
            tag.Set("width", grid.Width);
            tag.Set("height", grid.Height);
            tag.Set("jewellist", tagList);
            return tag;
        }

        internal static JewelGrid Load(TagCompound tag)
        {
            int width = tag.GetInt("width");
            int height = tag.GetInt("height");
            JewelGrid grid = new JewelGrid(width, height);
            List<Jewel> jewelList = new List<Jewel>();
            List<TagCompound> tagList = tag.GetList<TagCompound>("jewellist").ToList();
            foreach(TagCompound tag2 in tagList)
            {
                jewelList.Add(JewelIO.Load(tag2));
            }
            grid.jewelList = jewelList;

            return grid;
        }
    }
}