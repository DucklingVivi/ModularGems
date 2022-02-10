using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularGems.ILEdits;
using ModularGems.Items;
using ModularGems.Jewels;
using ModularGems.UI;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ModularGems
{
	public class ModularGems : Mod
	{

        public readonly IDictionary<string,JewelComponent> jewelComponents = new Dictionary<string,JewelComponent>();
        public static JewelBagSlotUI JewelBagSlotUI;
        public static JewelBagUI JewelBagUI;
        internal static int jewelBagSlotPosX;
        internal static int jewelBagSlotPosY;
        internal static bool JewelBagOpen;
        public static ModularGems Instance;
        public static ModKeybind rotateHotkey { get; private set; }
        public override void Load()
        {
            Instance = this;
            rotateHotkey = KeybindLoader.RegisterKeybind(this, "Rotate Held Jewel", Keys.Y);
            AutoLoad();
        }

        private void AutoLoad()
        {
            foreach (Type item in Code.GetTypes().OrderBy((Type type) => type.FullName, StringComparer.InvariantCulture))
            {
                if (item.IsSubclassOf(typeof(JewelComponent)))
                {
                    JewelComponent jewelComponent = (JewelComponent)Activator.CreateInstance(item);
                    jewelComponent.mod = this;
                    string name = item.Name;
                    if(jewelComponent.Autoload(ref name))
                    {
                        jewelComponent.Name = name;
                        jewelComponent.type = JewelComponentLoader.ReserveComponentID();
                        jewelComponent.texture = "ModularGems/Jewels/" + item.Name;

                        Asset<Texture2D> dummyAsset;
                        if(!ModContent.RequestIfExists<Texture2D>(jewelComponent.texture,out dummyAsset, AssetRequestMode.ImmediateLoad))
                        {
                            jewelComponent.texture = "ModularGems/Items/BasicJewel";
                        }

                        jewelComponent.SetDefaults();
                        jewelComponents[name] = jewelComponent;
                        JewelComponentLoader.jewelComponents.Add(jewelComponent);
                        ContentInstance.Register(jewelComponent);

                    }
                }else if (item.IsSubclassOf(typeof(ILEdit)))
                {
                    ((ILEdit)Activator.CreateInstance(item)).Apply();
                }
            }
        }
        public override void AddRecipes()
        {
            foreach(JewelComponent jewelComponent in jewelComponents.Values)
            {
                jewelComponent.CreateRecipe(CreateRecipe(ModContent.ItemType<BasicJewel>()));
            }
        }
        public class ModularGemsSystem : ModSystem
        {
            private UserInterface jewelBagSlotInterface;
            private UserInterface jewelBagInterface;
            public override void Load()
            {
                
                if (!Main.dedServ)
                {

                    
                    Jewel.Load();
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
            public override void AddRecipeGroups()
            {

                RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Bar", new int[]
                {
                    ItemID.CobaltBar,
                     ItemID.PalladiumBar
                });
                RecipeGroup.RegisterGroup("ModularGems:CobaltBars", group);
                RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Boots", new int[]
                {
                    ItemID.HermesBoots,
                    ItemID.FlurryBoots,
                    ItemID.SailfishBoots,
                    ItemID.SandBoots
                });
                RecipeGroup.RegisterGroup("ModularGems:Boots", group2);
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