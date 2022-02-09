using Microsoft.Xna.Framework;
using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModularGems.Jewels
{
    public class SlimeJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Slime King's Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Slimes become friendly");
            Shape.Add(new Point16(0,0));
            Shape.Add(new Point16(1,1));
            Shape.Add(new Point16(-1,1));
            Shape.Add(new Point16(1,-1));
            color = Color.Blue;
            itemColor = Color.White;
            rarity = 3;
            
        }

        internal override void Update(Player player)
        {
            player.npcTypeNoAggro[1] = true;
            player.npcTypeNoAggro[16] = true;
            player.npcTypeNoAggro[59] = true;
            player.npcTypeNoAggro[71] = true;
            player.npcTypeNoAggro[81] = true;
            player.npcTypeNoAggro[138] = true;
            player.npcTypeNoAggro[121] = true;
            player.npcTypeNoAggro[122] = true;
            player.npcTypeNoAggro[141] = true;
            player.npcTypeNoAggro[147] = true;
            player.npcTypeNoAggro[183] = true;
            player.npcTypeNoAggro[184] = true;
            player.npcTypeNoAggro[204] = true;
            player.npcTypeNoAggro[225] = true;
            player.npcTypeNoAggro[244] = true;
            player.npcTypeNoAggro[302] = true;
            player.npcTypeNoAggro[333] = true;
            player.npcTypeNoAggro[335] = true;
            player.npcTypeNoAggro[334] = true;
            player.npcTypeNoAggro[336] = true;
            player.npcTypeNoAggro[537] = true;
        }

        public override void AddRecipe(Recipe recipe)
        {

            recipe.AddIngredient(ItemID.RoyalGel,3);
            recipe.AddTile(TileID.Solidifier);

        }
    }
}
