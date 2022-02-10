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
    public class ShineJewel : BasicJewel
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Shine Jewel");
            Tooltip.SetDefault("Gem\nMakes you bright");
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(0, 2));


            jewel.color = Color.Yellow;
            Item.color = Color.Yellow;
            Item.rare = ItemRarityID.Green;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            float num = 0.92f;
            float num2 = 0.8f;
            float num3 = 0.65f;
            float num10 = 1f;

            Vector2 spinningpoint = new Vector2(player.width / 2 + 8 * player.direction, 2f);
            int i = (int)(player.position.X + spinningpoint.X) / 16;
            int j = (int)(player.position.Y + spinningpoint.Y) / 16;
            Lighting.AddLight(i, j, num * num10, num2 * num10, num3 * num10);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShinePotion, 3);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }



    }
}
