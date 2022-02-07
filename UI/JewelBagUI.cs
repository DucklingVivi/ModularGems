using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
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
            Panel.Width.Set(500f, 0f);
            Panel.Height.Set(525f, 0f);
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
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            JewelGrid grid = ModContent.GetInstance<JewelBagSlot>().FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
            grid.Draw(spriteBatch, GetDimensions().Position());
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)))
            {
                ModularGemsItem item;
                if (Main.mouseItem.TryGetGlobalItem<ModularGemsItem>(out item))
                {
                    if (item.jewel != null)
                    {
                        JewelGrid grid = ModContent.GetInstance<JewelBagSlot>().FunctionalItem.GetGlobalItem<ModularGemsItem>().grid;
                        grid.addJewelToGrid(item.jewel, new Point16(0, 0));
                        Main.mouseItem.TurnToAir();
                    }
                }
            }
        }
    }
}
