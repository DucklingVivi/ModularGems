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
    public class RunJewel : BasicJewel
    {


        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Run Jewel");
            Tooltip.SetDefault("Gem\nLets you run super fast");
            Shape.Add(new Point16(-1, 0));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(1, 0));
            Shape.Add(new Point16(1, 1));
            Shape.Add(new Point16(2, 0));


            jewel.color = Color.LightGreen;
            Item.color = Color.White;
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.accRunSpeed = 6f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("ModularGems:Boots");
            recipe.AddIngredient(ItemID.SwiftnessPotion, 2);
            recipe.AddIngredient(ItemID.Aglet);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
