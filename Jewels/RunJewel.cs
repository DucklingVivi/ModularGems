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
    public class RunJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Run Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Lets you run super fast");
            Shape.Add(new Point16(-1, 0));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(1, 0));
            Shape.Add(new Point16(1, 1));
            Shape.Add(new Point16(2, 0));
            color = Color.LightGreen;
            itemColor = Color.White;
            rarity = 3;
            
        }

        internal override void Update(Player player)
        {
            player.accRunSpeed = 6f;
        }

        public override void AddRecipe(Recipe recipe)
        {

            recipe.AddRecipeGroup("ModularGems:Boots");
            recipe.AddIngredient(ItemID.SwiftnessPotion, 2);
            recipe.AddIngredient(ItemID.Aglet);
            recipe.AddTile(TileID.TinkerersWorkbench);

        }
    }
}
