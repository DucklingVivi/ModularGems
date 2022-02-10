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
    public class FishJewel : BasicJewel
    {

       public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Fish Jewel");
            Tooltip.SetDefault("Gem\nGives you gills, but you can't breathe air");
            Shape.Add(new Point16(0, 0));


            jewel.color = Color.Green;
            Item.color = Color.White;
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.GetModPlayer<ModularGemsPlayer>().isFish = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bass, 21);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

    }
}
