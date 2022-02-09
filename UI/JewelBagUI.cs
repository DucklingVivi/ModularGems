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
using static ModularGems.JewelGrid;

namespace ModularGems.UI
{
    public class JewelBagUI : UIState
    {
        public virtual bool IsVisible => Main.playerInventory && ModularGems.JewelBagOpen;
        public JewelBagUIMainPanel Panel { get; protected set; }

        public static int jewelSize = 50;
        public int bagHeight = 0;
        public int bagWidth = 0;
        public const int padding = 10;
        public override void OnInitialize()
        {
            Panel = new JewelBagUIMainPanel();
            Panel.Width.Set(bagWidth * jewelSize + padding*2, 0f);
            Panel.Height.Set(bagWidth * jewelSize + padding*2 + JewelBagUIMainPanel.draggingPanelHeight, 0f);
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
        public override void Update(GameTime gameTime)
        {
            if (!JewelBagSlot.Instance.FunctionalItem.IsAir)
            {
                if (JewelBagSlot.Instance.FunctionalItem.GetGlobalItem<ModularGemsItem>().grid != null)
                {
                    JewelGrid grid = JewelBagSlot.Instance.FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
                    bagHeight = grid.Height;
                    bagWidth = grid.Width;
                    Panel.Width.Set(bagWidth * jewelSize + padding * 2, 0f);
                    Panel.Height.Set(bagWidth * jewelSize + padding * 2 + JewelBagUIMainPanel.draggingPanelHeight, 0f);
                }
               
            }
            base.Update(gameTime);
        }
    }

    public class JewelBagUIMainPanel : DraggableUIPanel
    {
        public static float draggingPanelHeight = 25f;
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
        public JewelGridData hoverData;

        public static Texture2D backTexture;
        public static Texture2D CornersTexture;
        public static Texture2D BorderVertical;
        public static Texture2D BorderHorizontal;

        public static MouseState oldMouse;
        public static MouseState curMouse;

        public override void OnInitialize()
        {
            CornersTexture = ModContent.Request<Texture2D>("ModularGems/UI/BorderCorners",ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            BorderVertical = ModContent.Request<Texture2D>("ModularGems/UI/BorderVertical", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            BorderHorizontal = ModContent.Request<Texture2D>("ModularGems/UI/BorderHorizontal", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            backTexture = ModContent.Request<Texture2D>("ModularGems/UI/CrystalBackdrop", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = GetInnerDimensions().Position();
            JewelBagUI parent = this.Parent.Parent as JewelBagUI;
            for (int i = 0; i < parent.bagWidth; i++)
            {
                for (int j = 0; j < parent.bagHeight; j++)
                {

                    Rectangle target = new Rectangle((int)Position.X + (int)JewelBagUI.jewelSize * i, (int)Position.Y + (int)JewelBagUI.jewelSize * j, (int)JewelBagUI.jewelSize, (int)JewelBagUI.jewelSize);
                    spriteBatch.Draw(backTexture, target, new Color(245,245,245));
                }
            }
            Rectangle size = GetDimensions().ToRectangle();

            Rectangle border1 = size;
            border1.Height = 10;
            spriteBatch.Draw(BorderHorizontal, border1, Color.White);
            border1.Y += size.Height - 10;
            spriteBatch.Draw(BorderHorizontal, border1, Color.White);

            Rectangle border2 = size;
            border2.Width = 10;
            spriteBatch.Draw(BorderVertical, border2, Color.White);
            border2.X += size.Width - 10;
            spriteBatch.Draw(BorderVertical, border2, Color.White);


            Rectangle rectangle = new Rectangle(size.X, size.Y, 10,10);

            spriteBatch.Draw(CornersTexture, new Rectangle(size.X, size.Y, 10,10), new Rectangle(0,0,10,10), Color.White);
            spriteBatch.Draw(CornersTexture, new Rectangle(size.X + size.Width - 10, size.Y, 10, 10), new Rectangle(12, 0, 10, 10), Color.White);
            spriteBatch.Draw(CornersTexture, new Rectangle(size.X, size.Y + size.Height - 10, 10, 10), new Rectangle(0, 12, 10, 10), Color.White);
            spriteBatch.Draw(CornersTexture, new Rectangle(size.X + size.Width - 10, size.Y + size.Height -10, 10, 10), new Rectangle(12, 12, 10, 10), Color.White);
            JewelGrid grid = ModContent.GetInstance<JewelBagSlot>().FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
            grid.Draw(spriteBatch, GetInnerDimensions().Position());
            if(previewJewel != null)
            {
                previewJewel.DrawHover(spriteBatch, GetInnerDimensions().Position());
            }
            if(hoverData.item!= null && hoverData.jewel != null)
            {
                Main.HoverItem = hoverData.item.Clone();
                Main.hoverItemName = hoverData.item.HoverName;
                hoverData.jewel.DrawHighlight(spriteBatch, GetInnerDimensions().Position());
            }
            

        }
        public override void Update(GameTime gameTime)
        {
            hoverData = new JewelGridData();
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
                        previewJewel.SetRotation((previewJewel.rotation + 1) % 4);
                    }
                    if (curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                    {

                        if (grid.tryAddJewelToGrid(previewJewel, Main.mouseItem))
                        {
                            SoundEngine.PlaySound(SoundID.CoinPickup, -1, -1, 1);
                            Main.mouseItem.TurnToAir();
                        }
                    }
                }
            }
            else if (Main.mouseItem.IsAir)
            {
                JewelGridData data;
                Point16 anchor = new Point16((int)Math.Floor((Main.mouseX - GetInnerDimensions().X) / 50f), (int)Math.Floor((Main.mouseY - GetInnerDimensions().Y) / 50f));
                if (grid.isHoveringOverJewel(out data, anchor))
                {
                    hoverData = data;
                    Main.HoverItem = data.item.Clone();
                    Main.hoverItemName = data.item.HoverName;
                    
                    if (curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                    {
                        Main.mouseItem = grid.removeJewelFromGrid(hoverData.jewel).Clone();
                    }
                }
            }
            
            
            
           

            

        }
    }
}
