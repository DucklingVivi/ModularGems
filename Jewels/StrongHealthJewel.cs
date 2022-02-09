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
    public class StrongHealthJewel : JewelComponent
    {



        public override void SetDefaults()
        {
            DisplayName = "Strong Health Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Increases max life by 40");
            addTooltipLine("basicDesc3", "Increases max life by 5%");
            Shape.Add(new Point16(0, -2));
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(1, 0));
            Shape.Add(new Point16(2, 0));
            color = Color.Red;
            itemColor = Color.White;
            
            rarity = 3;

        }

        internal override void Update(Player player)
        {
            player.statLifeMax2 += player.statLifeMax / 20 / 20 * 20 + 40;
        }

        public override void AddRecipe(Recipe recipe)
        {
            recipe.AddIngredient(ItemID.LifeforcePotion,1);
            recipe.AddIngredient(ItemID.LifeFruit, 4);
            recipe.AddTile(TileID.Anvils);

        }
    }
}
