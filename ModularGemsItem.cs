using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ModularGems
{
    class ModularGemsItem : GlobalItem
    {
        public override bool InstancePerEntity => true;


        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if(item.type == ModContent.ItemType<JewelBag>() && slot < 20 && modded == false)
            {
                return false;
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
    }
}
