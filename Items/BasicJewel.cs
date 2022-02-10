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

namespace ModularGems.Items
{
    public abstract class BasicJewel : ModItem
    {

        public override string Texture => GetTexture();

        public virtual string GetTexture()
        {

            string texture = "ModularGems/Jewels/" + Item.Name;

            Asset<Texture2D> dummyAsset;
            if (!ModContent.RequestIfExists<Texture2D>(texture, out dummyAsset, AssetRequestMode.ImmediateLoad))
            {
                texture = "ModularGems/Items/BasicJewel";
            }


            return texture;
        }

        public override void SetDefaults()
        {
            Item.color = Color.AliceBlue;
            Item.width = 18;
            Item.height = 18;
        }


        public virtual void UpdateJewel(){}
    }
}
