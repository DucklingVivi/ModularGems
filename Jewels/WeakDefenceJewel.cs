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
    public class WeakDefenceJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Weak Defence Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Increases your defence by 2");
            Shape.Add(new Point16(0,0));
            Shape.Add(new Point16(0,1));
            Shape.Add(new Point16(1,0));

            color = Color.Orange;
            itemColor = Color.Orange;
            rarity = 2;
            
        }

        internal override void Update(Player player)
        {
            player.statDefense += 2;
        }

        public override void AddRecipe(Recipe recipe)
        {

            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddTile(TileID.Anvils);

        }
    }
}
