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
    public class WeakDefenceJewel : BasicJewel
    {

       



        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Weak Defence Jewel");
            Tooltip.SetDefault("Gem\nIncreases your defence by 2");
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 0));

            jewel.color = Color.Orange;
            Item.color = Color.Orange;
            Item.rare = ItemRarityID.Green;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statDefense += 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
