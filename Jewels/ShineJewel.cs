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
    public class ShineJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Shine Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Makes you bright");
            Shape.Add(new Point16(0,-1));
            Shape.Add(new Point16(0,0));
            Shape.Add(new Point16(0,1));
            Shape.Add(new Point16(0,2));

            color = Color.Yellow;
            itemColor = Color.Yellow;
            rarity = 2;
            
        }

        internal override void Update(Player player)
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

        public override void AddRecipe(Recipe recipe)
        {
 
            recipe.AddIngredient(ItemID.ShinePotion, 3);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddTile(TileID.Anvils);

        }
    }
}
