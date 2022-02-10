using Microsoft.Xna.Framework;
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

namespace ModularGems.Items.Jewels
{
    internal class JewelGlobalItem : GlobalItem
    {
        public int rotation;
        public Point16 anchor = Point16.NegativeOne;
        public override bool InstancePerEntity => true;

        
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            JewelGlobalItem clone = (JewelGlobalItem)base.Clone(item, itemClone);
            clone.rotation = rotation;
            clone.anchor = anchor;
            return clone;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            if(rotation > 0)
            {
                tag.Set("rotation", rotation);
            }
            if(anchor != Point16.NegativeOne)
            {
                tag.Set("anchor", anchor.ToVector2());
            }

        }

        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.ContainsKey("rotation"))
            {
                rotation = tag.GetInt("rotation");
            }
            if (tag.ContainsKey("anchor"))
            {
                anchor = tag.Get<Vector2>("anchor").ToPoint16();
            }
            if (item.ModItem is BasicJewel)
            {
                BasicJewel jewel = item.ModItem as BasicJewel;
                jewel.syncJewel();
            }
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(rotation);
            writer.WriteVector2(anchor.ToVector2());
        }


        public override void NetReceive(Item item, BinaryReader reader)
        {
            rotation = reader.ReadInt32();
            anchor = reader.ReadVector2().ToPoint16();
            if(item.ModItem is BasicJewel)
            {
                BasicJewel jewel = item.ModItem as BasicJewel;
                jewel.syncJewel();
            }
        }



    }
}
