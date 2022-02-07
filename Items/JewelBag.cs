using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ModularGems.Items
{
    class JewelBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.color = Color.LightBlue;
            Item.width = 26;
            Item.height = 26;

        }

    }
}
