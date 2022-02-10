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
    public class FishJewel : JewelComponent
    {

       

        public override void SetDefaults()
        {
            DisplayName = "Fish Jewel";
            addTooltipLine("basicDesc", "Gem");
            addTooltipLine("basicDesc2", "Gives you gills, but you can't breathe air");
            Shape.Add(new Point16(0,0));


            color = Color.Green;
            itemColor = Color.White;
            rarity = 3;
            
        }

        internal override void Update(Player player)
        {
            player.GetModPlayer<ModularGemsPlayer>().isFish = true;
        }

        public override void AddRecipe(Recipe recipe)
        {
 
            recipe.AddIngredient(ItemID.Bass, 21);
            recipe.AddTile(TileID.TinkerersWorkbench);

        }
    }
}
