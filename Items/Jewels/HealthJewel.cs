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
    public class HealthJewel : BasicJewel
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Health Jewel");
            Tooltip.SetDefault("Gem\nIncreases max life by 20");
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 1));

            jewel.color = new Color(255, 78, 66);
            Item.color = Color.White;
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statLifeMax2 += 20;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.HealingPotion, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
