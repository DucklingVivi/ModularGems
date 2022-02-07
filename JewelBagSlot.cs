using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ModularGems.Items;
using ModularGems.UI;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModularGems
{
    internal class JewelBagSlot : ModAccessorySlot
    {

        private static Texture2D bagButtonTexture;
        private static Texture2D bagHighlightTexture;
        private static bool prevState = false;
        
        public override string Name => "JewelBagSlot";
        public override bool DrawDyeSlot => false;
        public override bool DrawVanitySlot => false;
        public override string FunctionalTexture => "ModularGems/Items/JewelBag";
        public override bool IsHidden()
        { 
            if ((Main.playerInventory && Main.EquipPageSelected == 0) || Main.mouseItem.type == ModContent.ItemType<JewelBag>())
                return false;
            return true;
        }
        public override Vector2? CustomLocation => GetCustomLocation();

        public static bool IsVisible { get; internal set; }

        public override void SetupContent()
        {
            SetStaticDefaults();
            base.SetupContent();
        }
        public override void SetStaticDefaults()
        {
            bagButtonTexture = ModContent.Request<Texture2D>("ModularGems/BagButton", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            bagHighlightTexture = ModContent.Request<Texture2D>("ModularGems/BagHighlight",mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (checkItem.type == ModContent.ItemType<JewelBag>())
            {
                
                return true;
            }
            return false;
        }
        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            if (item.type == ModContent.ItemType<JewelBag>())
            {
                return true;
                
            }
            return false;
        }

        public override void PostDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            if (item.type == ItemID.None)
            {
                ModularGems.JewelBagOpen = false;
                return;
            }
            Rectangle bagRect = new Rectangle((int)(position.X - 46), (int)position.Y, ((int)(bagButtonTexture.Width * 0.85f)), (int)(bagButtonTexture.Height * 0.85f));
           
            Main.spriteBatch.Draw(bagButtonTexture, bagRect, Color.White);
            if (bagRect.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                Main.blockMouse = true;
                Main.spriteBatch.Draw(bagHighlightTexture, bagRect, Color.White);
                if (!prevState)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick, -1, -1, 1);
                }
                prevState = true;
                if(Main.mouseLeft && Main.mouseLeftRelease)
                {
                    ModularGems.JewelBagOpen = !ModularGems.JewelBagOpen;
                }
            }
            else
            {
                
                prevState = false;
                Main.blockMouse = false;
            }
            
        }
        public override void OnMouseHover(AccessorySlotType context)
        {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = "Jewel Bag";
                    break;
            }
        }

        private Vector2? GetCustomLocation()
        {
            if (ModularGemsConfig.Instance.SlotLocation != ModularGemsConfig.Location.Custom)
                return new Vector2(ModularGems.jewelBagSlotPosX,ModularGems.jewelBagSlotPosY);

            UIPanel panel = ModularGems.JewelBagSlotUI.Panel;

            return new Vector2(panel.Left.Pixels + panel.PaddingLeft,
                               panel.Top.Pixels + panel.PaddingTop);
        }
    }
}
