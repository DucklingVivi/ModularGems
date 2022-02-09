using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ModularGems
{
    public abstract class JewelComponent
    {

        public Mod mod { get; internal set; }
        public string Name { get; internal set; }

        public int type;
        public int rarity;
        public string DisplayName = "";
        
        internal string texture;

        public List<Point16> Shape = new List<Point16>();
        public Color color;
        public Color itemColor;

        public List<TooltipLine> tooltips = new List<TooltipLine>();
        public virtual bool Autoload(ref string name)
        {
            return true;
        }

        public virtual void SetDefaults()
        {
            
        }

        public virtual void AddRecipe(Recipe recipe)
        {

        }

        public virtual void addTooltipLine(string desc, string line)
        {
            tooltips.Add(new TooltipLine(ModularGems.Instance, desc, line));
        }

        internal virtual void Update(Player player)
        {
            
        }

        public virtual void CreateRecipe(Recipe recipe)
        {
            AddRecipe(recipe);

            Item resultitem = new Item(ModContent.ItemType<Items.BasicJewel>());
            AddDetailsToItem(resultitem);
            recipe.createItem = resultitem.Clone();
            recipe.Register();
        }
        public virtual void AddDetailsToItem(Item item)
        {

            

           
            item.color = itemColor;
            item.SetNameOverride(DisplayName);
            item.rare = rarity;
            ModularGemsItem moditem;
            if (item.TryGetGlobalItem<ModularGemsItem>(out moditem))
            {
                moditem.jewelName = Name;
                moditem.type = type;
                moditem.texture = texture;
                moditem.name = DisplayName;
                moditem.color = itemColor;
                moditem.tooltips = tooltips;
                if(moditem.jewel == null)
                {
                    moditem.jewel = new Jewel(Name);
                    moditem.jewel.color = color;
                    moditem.jewel.Shape = new List<Point16>(Shape);
                    moditem.jewel.SetRotation(0);
                }
            }
        }



    }
}
