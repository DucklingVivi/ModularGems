using Microsoft.Xna.Framework;
using ModularGems.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ModularGems
{
	public class ModularGems : Mod
	{
        public static JewelBagSlotUI JewelBagSlotUI;
        public static JewelBagUI JewelBagUI;
        internal static int jewelBagSlotPosX;
        internal static int jewelBagSlotPosY;
        internal static bool JewelBagOpen;
        public class ModularGemsSystem : ModSystem
        {
            private UserInterface jewelBagSlotInterface;
            private UserInterface jewelBagInterface;
            public override void Load()
            {
                if (!Main.dedServ)
                {
                    jewelBagSlotInterface = new UserInterface();
                    jewelBagInterface = new UserInterface();

                    JewelBagSlotUI = new JewelBagSlotUI();
                    JewelBagUI = new JewelBagUI();

                    JewelBagSlotUI.Activate();
                    JewelBagUI.Activate();
                    
                    jewelBagSlotInterface.SetState(JewelBagSlotUI);
                    jewelBagInterface.SetState(JewelBagUI);
                }
            }
            public override void Unload()
            {
                JewelBagSlotUI = null;
            }
            public override void UpdateUI(GameTime gameTime)
            {
                if (JewelBagSlotUI.IsVisible)
                {
                    jewelBagSlotInterface?.Update(gameTime);
                }
                if (JewelBagUI.IsVisible)
                {
                    jewelBagInterface?.Update(gameTime);
                }
                int mapH = 0;
                Main.inventoryScale = 0.85f;

                if (Main.mapEnabled)
                {
                    if (!Main.mapFullscreen && Main.mapStyle == 1)
                    {
                        mapH = 256;
                    }
                }

                if(ModContent.GetInstance<ModularGemsConfig>().SlotLocation == ModularGemsConfig.Location.Accessories)
                {
                    if (Main.mapEnabled)
                    {
                        int adjustY = 600;
                        if (Main.player[Main.myPlayer].extraAccessory)
                        {
                            adjustY = 610 + PlayerInput.UsingGamepad.ToInt() * 30;
                        }

                        if ((mapH + adjustY) > Main.screenHeight)
                        {
                            mapH = Main.screenHeight - adjustY;
                        }
                        

                    }
                    int slotCount = 6 + Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow();
                    if ((Main.screenHeight < 900) && (slotCount >= 8))
                    {
                        slotCount = 6;
                    }
                    jewelBagSlotPosX = Main.screenWidth - 82 - 14 - (47 * 3) - (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale);
                    jewelBagSlotPosY = (int)(mapH + 174 + 4 + slotCount * 56 * Main.inventoryScale);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        jewelBagSlotPosX -= 47;
                    }
                }


            }
            public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
            {
                int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

                if (inventoryLayer != -1)
                {
                    layers.Insert(
                        inventoryLayer,
                        new LegacyGameInterfaceLayer(
                            "ModularGems: Custom Slot UI",
                            () => {
                                if (JewelBagSlotUI.IsVisible)
                                {
                                    jewelBagSlotInterface.Draw(Main.spriteBatch, new GameTime());
                                }

                                return true;
                            },
                            InterfaceScaleType.UI));
                    layers.Insert(
                        inventoryLayer,
                        new LegacyGameInterfaceLayer(
                            "ModularGems: JewelBag UI",
                            () => {
                                if (JewelBagUI.IsVisible)
                                {
                                    jewelBagInterface.Draw(Main.spriteBatch, new GameTime());
                                }

                                return true;
                            },
                            InterfaceScaleType.UI));
                }
            }
        }
    }
}