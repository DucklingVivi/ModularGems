using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace ModularGems
{
    internal class ModularGemsConfig : ModConfig
    {
        public enum Location {
            Accessories,
            Custom
        }

        private Location lastSlotLocation = Location.Accessories;
        public override ConfigScope Mode => ConfigScope.ClientSide;


        public static ModularGemsConfig Instance;

        [Header("Modular Gems Settings")]
        [DefaultValue(Location.Accessories)]
        [Label("Slot Location")]
        
        public Location SlotLocation;

        [DefaultValue(false)]
        [Tooltip("Show the draggable panel for a custom location")]
        [Label("Show custom location panel")]
        public bool ShowCustomLocationPanel;

        [DefaultValue(false)]
        [Tooltip("Reset custom panel location if it disappears from the screen")]
        [Label("Reset custom location")]
        public bool ResetCustomSlotLocation;

        public override void OnChanged()
        {
            if (ModularGems.JewelBagSlotUI == null)
                return;

            if (lastSlotLocation == Location.Custom && SlotLocation != Location.Custom)
                ShowCustomLocationPanel = false;

            if (ShowCustomLocationPanel)
                SlotLocation = Location.Custom;

            ModularGems.JewelBagSlotUI.Panel.Visible = ShowCustomLocationPanel;
            ModularGems.JewelBagSlotUI.Panel.CanDrag = ShowCustomLocationPanel;

            if (ResetCustomSlotLocation)
            {
                ModularGems.JewelBagSlotUI.ResetPosition();
                ResetCustomSlotLocation = false;
            }

            lastSlotLocation = SlotLocation;
        }

    }
}
