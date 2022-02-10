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
    public class DefenceJewel : BasicJewel
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Defence Jewel");
            Tooltip.SetDefault("Gem\nIncreases your defence by 5");
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 0));


            jewel.color = Color.Orange;
            Item.color = Color.Orange;
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.statDefense += 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
