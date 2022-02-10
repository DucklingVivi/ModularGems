using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ModularGems
{
    public class ModularGemsPlayer : ModPlayer
    {

        public bool isFish = false;

        public override void PreUpdate()
        {
            isFish = false;
        }


    }
}
