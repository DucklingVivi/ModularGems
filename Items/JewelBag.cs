using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModularGems.Items
{
    class JewelBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.color = Color.LightBlue;
            Item.width = 26;
            Item.height = 26;
            Item.accessory = true;
            
        }
        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (this.Item.GetGlobalItem<ModularGemsItem>() != null)
            {
                this.Item.GetGlobalItem<ModularGemsItem>().grid.Update(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
