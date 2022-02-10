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
    public class WeakHealthJewel : BasicJewel
    {



        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Weak Health Jewel");
            Tooltip.SetDefault("Gem\nIncreases max life by 10");
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));

            jewel.color = new Color(255, 86, 74);
            Item.color = Color.White;
            Item.rare = ItemRarityID.Green;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statLifeMax2 += 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LesserHealingPotion, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
