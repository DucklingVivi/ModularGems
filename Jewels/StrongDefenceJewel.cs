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
    public class StrongDefenceJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Strong Defence Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Increases your defence by 10");
            Shape.Add(new Point16(0,0));
            Shape.Add(new Point16(0,1));
            Shape.Add(new Point16(1,0));

            color = Color.Orange;
            itemColor = Color.Orange;
            rarity = 2;
            
        }

        internal override void Update(Player player)
        {
            player.statDefense += 10;
            player.noKnockback = true;
        }

        public override void AddRecipe(Recipe recipe)
        {
            recipe.AddIngredient(ItemID.CobaltShield);
            recipe.AddRecipeGroup("ModularGems:CobaltBars");
            recipe.AddTile(TileID.Anvils);

        }
    }
}
