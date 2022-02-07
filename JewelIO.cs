using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace ModularGems
{
    internal class JewelIO
    {
        internal static TagCompound Save(Jewel jewel)
        {

            TagCompound tag = new TagCompound();
            tag.Set("color", jewel.color);
            tag.Set("anchor", jewel.anchor);
            tag.Set("shape", jewel.Shape);
            return tag;
        }

        internal static Jewel Load(TagCompound tag)
        {
            Jewel jewel = new Jewel();
            jewel.anchor = tag.Get<Point16>("anchor");
            jewel.color = tag.Get<Color>("color");
            jewel.Shape = tag.GetList<Point16>("shape").ToList();
            return jewel;
        }
    }
}
