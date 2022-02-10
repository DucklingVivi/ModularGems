using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Recipe;

namespace ModularGems.Items.Jewels
{
    public abstract class BasicJewel : ModItem
    {

        public List<Point16> Shape { get; set; } = new List<Point16>();
        public Jewel jewel { get; set; } = null;
        public override string Texture => GetTexture();

        private string GetTexture()
        {
            string texture = "ModularGems/Items/Jewels/" + Item.ModItem.Name;
            if (!ModContent.RequestIfExists<Texture2D>(texture, out _, AssetRequestMode.ImmediateLoad))
            {
                texture = "ModularGems/Items/Jewels/BasicJewel";
            }
            return texture;
        }

        
        public override void SetDefaults()
        {
            jewel = new Jewel();

        }

        public virtual void syncJewel()
        {
            jewel.Shape = Shape;
            jewel.SetRotation(Item.GetGlobalItem<JewelGlobalItem>().rotation);
            jewel.anchor = Item.GetGlobalItem<JewelGlobalItem>().anchor;
        }

        public virtual void UpdateJewel(Player player){}
    }
}
