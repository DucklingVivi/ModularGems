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

namespace ModularGems.Items.Jewels
{
    public class StrongDefenceJewel : BasicJewel
    {

      
        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Strong Defence Jewel");
            Tooltip.SetDefault("Gem\nIncreases your defence by 10\nMakes you immune to knockback");
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 0));


            jewel.color = Color.Orange;
            Item.color = Color.Orange;
            Item.rare = ItemRarityID.LightRed;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statDefense += 10;
            player.noKnockback = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltShield);
            recipe.AddRecipeGroup("ModularGems:CobaltBars");
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
