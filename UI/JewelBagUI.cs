using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ModularGems.UI
{
    public class JewelBagUI : UIState
    {
        public virtual bool IsVisible => Main.playerInventory && ModularGems.JewelBagOpen;
        public JewelBagUIMainPanel Panel { get; protected set; }
        
        public override void OnInitialize()
        {
            Panel = new JewelBagUIMainPanel();
            Panel.Width.Set(520f, 0f);
            Panel.Height.Set(545f, 0f);
            Panel.CanDrag = true;
            Panel.Visible = true;
            Panel.SetPadding(0f);
            Panel.BackgroundColor = Color.Transparent;
            Panel.BorderColor = Color.Transparent;
            ResetPosition();
            Append(Panel);
        }

        public void ResetPosition()
        {
            Panel.Left.Set(Main.screenWidth / 2.0f, 0);
            Panel.Top.Set(Main.screenWidth / 2.0f, 0);
        }
    }

    public class JewelBagUIMainPanel : DraggableUIPanel
    {
        private const float draggingPanelHeight = 25f;
        private UIPanel draggingPanel;

        public JewelBagMainContentUI mainContentPanel;
        public override void OnInitialize()
        {
            draggingPanel = new UIPanel();
            draggingPanel.Top.Set(0f, 0f);
            draggingPanel.Left.Set(0f, 0f);
            draggingPanel.Height.Set(draggingPanelHeight, 0f);
            draggingPanel.Width.Set(0f, 1f);

            mainContentPanel = new JewelBagMainContentUI();
            mainContentPanel.Top.Set(draggingPanelHeight, 0f);
            mainContentPanel.Left.Set(0f, 0f);
            mainContentPanel.Height.Set(-draggingPanelHeight, 1f);
            mainContentPanel.Width.Set(0f, 1f);
            mainContentPanel.SetPadding(10f);
            Append(mainContentPanel);
            Append(draggingPanel);
        }
        public override Rectangle GetInnerRectangle()
        {
            Rectangle rectangle = GetDimensions().ToRectangle();
            rectangle.Y += (int)draggingPanelHeight;
            rectangle.Height -= (int)draggingPanelHeight;
            return rectangle;
        }
        
    }

    public class JewelBagMainContentUI : UIPanel
    {

        public Jewel previewJewel;
        public Jewel hoverJewel;

        public static MouseState oldMouse;
        public static MouseState curMouse;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            JewelGrid grid = ModContent.GetInstance<JewelBagSlot>().FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
            grid.Draw(spriteBatch, GetInnerDimensions().Position());
            if(previewJewel != null)
            {
                previewJewel.Draw(spriteBatch, GetInnerDimensions().Position());
            }
           
        }
        public override void Update(GameTime gameTime)
        {
            oldMouse = curMouse;
            curMouse = Mouse.GetState();
            previewJewel = null;
            JewelGrid grid = ModContent.GetInstance<JewelBagSlot>().FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
            ModularGemsItem item;
            if (Main.mouseItem.TryGetGlobalItem<ModularGemsItem>(out item))
            {
                if (item.jewel != null)
                {
                    previewJewel = item.jewel;
                    Point16 anchor = new Point16((int)Math.Floor((Main.mouseX - GetInnerDimensions().X) / 50f), (int)Math.Floor((Main.mouseY - GetInnerDimensions().Y) / 50f));
                    previewJewel.anchor = anchor;
                    if (ModularGems.rotateHotkey.JustPressed)
                    {
                        previewJewel.rotateRight();
                    }
                    if (curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                    {

                        if (grid.tryAddJewelToGrid(previewJewel))
                        {
                            SoundEngine.PlaySound(SoundID.CoinPickup, -1, -1, 1);
                            Main.mouseItem.TurnToAir();
                        }
                    }
                }
            }
            else if (Main.mouseItem.IsAir)
            {
                Jewel jewel;
                Point16 anchor = new Point16((int)Math.Floor((Main.mouseX - GetInnerDimensions().X) / 50f), (int)Math.Floor((Main.mouseY - GetInnerDimensions().Y) / 50f));
                if (grid.isHoveringOverJewel(out jewel, anchor))
                {
                    hoverJewel = jewel;
                    if (curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                    {
                        Main.mouseItem = new Item(ModContent.ItemType<BasicJewel>());
                        ModularGemsItem moditem;
                        if (Main.mouseItem.TryGetGlobalItem<ModularGemsItem>(out moditem))
                        {
                            moditem.jewel = hoverJewel.Clone();
                        }
                        grid.removeJewelFromGrid(hoverJewel);
                    }
                }
            }
            
            
            
           

            

        }
    }
}
