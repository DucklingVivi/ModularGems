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
    public class StrongHealthJewel : BasicJewel
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Strong Health Jewel");
            Tooltip.SetDefault("Gem\nIncreases max life by 40\nIncreases max life by 5%");
            Shape.Add(new Point16(0, -2));
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(1, 0));
            Shape.Add(new Point16(2, 0));

            jewel.color = Color.Red;
            Item.color = Color.White;
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statLifeMax2 += player.statLifeMax / 20 / 20 * 20 + 40;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeforcePotion, 1);
            recipe.AddIngredient(ItemID.LifeFruit, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
