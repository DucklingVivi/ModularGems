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
    public class WeakHealthJewel : JewelComponent
    {



        public override void SetDefaults()
        {
            DisplayName = "Weak Health Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Increases max life by 10");
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));

            color = Color.Red;
            itemColor = Color.Red;
            rarity = 2;

        }

        internal override void Update(Player player)
        {
            player.statLifeMax2 += 10;
        }

        public override void AddRecipe(Recipe recipe)
        {

            recipe.AddIngredient(ItemID.LesserHealingPotion, 8);
            recipe.AddTile(TileID.Anvils);

        }
    }
}
