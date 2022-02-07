using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomSlot;
using CustomSlot.UI;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;

namespace ModularGems.UI
{
    public class ModularGemsUI : AccessorySlotsUI
    {
        public override void OnInitialize()
        {
            EquipSlot = new CustomItemSlot(ItemSlot.Context.EquipAccessory, 0.85f);

            float slotSize = EquipSlot.Width.Pixels;

            Panel = new DraggableUIPanel();

            Panel.Width.Set(slotSize + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(slotSize + Panel.PaddingTop + Panel.PaddingBottom, 0);
            if (PanelLocation == Location.Custom)
                MoveToCustomPosition();
            else
                ResetPosition();

            Panel.Append(EquipSlot);
            Append(Panel);

        }
    }
}
