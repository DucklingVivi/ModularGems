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
    public class RocketJewel : BasicJewel
    {

       

        
        internal override void Update(Player player)
        {
            player.accRunSpeed = 6f;
        }

        public override void AddRecipes()
        {
            
        }
    }
}
