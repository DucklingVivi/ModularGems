using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Recipe;

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
