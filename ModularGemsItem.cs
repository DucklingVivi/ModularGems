using Microsoft.Xna.Framework;
using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ModularGems
{
    public class ModularGemsItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public JewelGrid grid;
        public Jewel jewel;

        public override void SetDefaults(Item item)
        {
            if(item.type == ModContent.ItemType<JewelBag>())
            {
                this.grid = new JewelGrid(10, 10);
            }
            if (item.type == ModContent.ItemType<BasicJewel>())
            {
                this.jewel = new Jewel();
                this.jewel.anchor = new Point16(0, 0);
                this.jewel.color = Color.Red;
                this.jewel.Shape.Add(new Point16(0, 0));
                this.jewel.Shape.Add(new Point16(0, 1));
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ModularGemsItem clone = (ModularGemsItem)base.Clone(item, itemClone);
            clone.grid = grid?.Clone() ?? null;
            clone.jewel = jewel?.Clone() ?? null;
            return clone;
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            if (jewel != null)
            {
                tag.Set("jewel", JewelIO.Save(jewel));
            }
            if(grid != null)
            {
                tag.Set("grid", JewelGridIO.Save(grid));
            }
            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.ContainsKey("jewel"))
            {
                jewel = JewelIO.Load(tag.GetCompound("jewel"));
            }
            if (tag.ContainsKey("grid"))
            {
                grid = JewelGridIO.Load(tag.GetCompound("grid"));
            }
            base.LoadData(item, tag);
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {
            base.NetSend(item, writer);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            base.NetReceive(item, reader);
        }
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if(item.type == ModContent.ItemType<JewelBag>() && slot < 20 && modded == false)
            {
                return false;
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
    }
}
