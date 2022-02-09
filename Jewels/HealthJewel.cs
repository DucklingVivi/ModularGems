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
    public class HealthJewel : JewelComponent
    {



        public override void SetDefaults()
        {
            DisplayName = "Health Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Increases max life by 20");
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 1));
            color = new Color(255, 78, 66);
            itemColor = Color.White;
            rarity = 3;

        }

        internal override void Update(Player player)
        {
            player.statLifeMax2 += 20;
        }

        public override void AddRecipe(Recipe recipe)
        {
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.HealingPotion, 2);
            recipe.AddTile(TileID.Anvils);

        }
    }
}
