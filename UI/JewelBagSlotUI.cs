using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace ModularGems.UI
{
    public class JewelBagSlotUI : UIState
    {
        public static readonly float SlotSize = TextureAssets.InventoryBack.Value.Height * Main.inventoryScale;
        public DraggableUIPanel Panel { get; protected set; }

        public virtual bool IsVisible => Main.playerInventory && ModularGemsConfig.Instance.ShowCustomLocationPanel;

        public override void OnInitialize()
        {
            Panel = new DraggableUIPanel();
            Panel.Width.Set(SlotSize + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(SlotSize + Panel.PaddingTop + Panel.PaddingBottom, 0);

            ResetPosition();
            Append(Panel);
        }

        public void ResetPosition()
        {
            Panel.Left.Set(Main.screenWidth / 2.0f, 0);
            Panel.Top.Set(Main.screenWidth / 2.0f, 0);
        }
        
        
    }
}
