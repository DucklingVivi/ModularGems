using ModularGems.Jewels;
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
            List<TagCompound> tag2List = new List<TagCompound>();
            foreach (JewelGridData data in grid.jewelGridData)
            {
                tagList.Add(JewelIO.Save(data.jewel));
                tag2List.Add(ItemIO.Save(data.item));
            }
            tag.Set("width", grid.Width);
            tag.Set("height", grid.Height);
            tag.Set("jewellist", tagList);
            tag.Set("itemlist", tag2List);
            return tag;
        }

        internal static JewelGrid Load(TagCompound tag)
        {
            int width = tag.GetInt("width");
            int height = tag.GetInt("height");
            JewelGrid grid = new JewelGrid(width, height);
            List<JewelGridData> jewelList = new List<JewelGridData>();
            List<TagCompound> tagList = tag.GetList<TagCompound>("jewellist").ToList();
            List<TagCompound> tag2List = tag.GetList<TagCompound>("itemlist").ToList();
            for (int i = 0; i < tagList.Count; i++)
            {
                Item item = ItemIO.Load(tag2List[i]);
                ModularGems.Instance.jewelComponents[item.GetGlobalItem<ModularGemsItem>().jewelName].AddDetailsToItem(item);
                jewelList.Add(new JewelGridData(JewelIO.Load(tagList[i]), item));
            }
            grid.jewelGridData = jewelList;

            return grid;
        }

        internal static void Send(JewelGrid grid, BinaryWriter writer)
        {
            writer.Write(grid.Width);
            writer.Write(grid.Height);
            writer.Write(grid.jewelGridData.Count);
            for (int i = 0; i < grid.jewelGridData.Count; i++)
            {
                ItemIO.Send(grid.jewelGridData[i].item, writer);
                JewelIO.Send(grid.jewelGridData[i].jewel, writer);
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
                ModularGems.Instance.jewelComponents[item.GetGlobalItem<ModularGemsItem>().jewelName].AddDetailsToItem(item);
                Jewel jewel = JewelIO.Recieve(reader);
                retgrid.jewelGridData.Add(new JewelGridData(jewel, item));
            }
            return retgrid;
        }
    }
}