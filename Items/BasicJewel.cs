using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ModularGems.Items
{
    public class BasicJewel : ModItem
    {

        public override void SetDefaults()
        {
            Item.color = Color.AliceBlue;
            Item.width = 18;
            Item.height = 18;
        }
    }
}
