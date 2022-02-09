using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace ModularGems
{
    internal class JewelIO
    {
        internal static TagCompound Save(Jewel jewel)
        {

            TagCompound tag = new TagCompound();
            tag.Set("name", jewel.Name);
            tag.Set("color", jewel.color);
            tag.Set("shape", jewel.Shape);
            tag.Set("anchor", jewel.anchor);
            tag.Set("rotation", jewel.rotation);
            return tag;
        }

        internal static Jewel Load(TagCompound tag)
        {
           
            Jewel jewel = new Jewel(tag.GetString("name"));
            jewel.color = tag.Get<Color>("color");
            jewel.anchor = tag.Get<Point16>("anchor");
            jewel.Shape = tag.GetList<Point16>("shape").ToList();
            jewel.SetRotation(tag.GetAsInt("rotation"));
            return jewel;
        }

        internal static void Send(Jewel jewel, BinaryWriter writer)
        {
            writer.Write(jewel.Name);
            writer.Write(jewel.type);
            writer.WriteRGB(jewel.color);
            writer.WriteVector2(jewel.anchor.ToVector2());
            writer.Write(jewel.Shape.Count);
            for (int i = 0; i < jewel.Shape.Count; i++)
            {
                writer.WriteVector2(jewel.Shape[i].ToVector2());
            }
            writer.Write(jewel.rotation);
        }

        internal static Jewel Recieve(BinaryReader reader)
        {
            
            Jewel retjewel = new Jewel(reader.ReadString());
            retjewel.type = reader.ReadInt32();
            retjewel.color = reader.ReadRGB();
            retjewel.anchor = reader.ReadVector2().ToPoint16();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                retjewel.Shape.Add(reader.ReadVector2().ToPoint16());
            }
            retjewel.SetRotation(reader.ReadInt32());
            return retjewel;
        }
    }
}
