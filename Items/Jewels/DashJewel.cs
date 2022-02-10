using Microsoft.Xna.Framework;
using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Player;

namespace ModularGems.Items.Jewels
{
    public class DashJewel : BasicJewel
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Dash Jewel");
            Tooltip.SetDefault("Gem\nGives you a weak dash");

            Shape.Add(new Point16(-1, 1));
            Shape.Add(new Point16(-1, 0));
            Shape.Add(new Point16(-1, -1));
            Shape.Add(new Point16(0, -1));
            Shape.Add(new Point16(0, 0));
            Shape.Add(new Point16(0, 1));
            Shape.Add(new Point16(1, 1));
            Shape.Add(new Point16(1, 0));
            Shape.Add(new Point16(1, -1));


            jewel.color = new Color(25, 25, 25);
            Item.color = new Color(25, 25, 25);
            Item.rare = ItemRarityID.Orange;
            syncJewel();
        }

        public override void UpdateJewel(Player player)
        {
            player.dashType = 10;


            if (player.dashDelay > 0)
            {
                player.dashDelay--;
            }
            else if (player.dashDelay == 0)
            {
                int dir;
                bool dashing;
                DoCommonDashHandle(out dir, out dashing, player);
                if (dashing)
                {

                    player.velocity.X = 10.9f * (float)dir;
                    Point point = (player.Center + new Vector2(dir * player.width / 2 + 2, player.gravDir * (float)(-player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                    Point point2 = (player.Center + new Vector2(dir * player.width / 2 + 2, 0f)).ToTileCoordinates();
                    if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                    {
                        player.velocity.X /= 2f;
                    }
                    player.dashDelay = 120;

                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EoCShield, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        private void DoCommonDashHandle(out int dir, out bool dashing,Player player)
        {
            dir = 0;
            dashing = false;
            if(player.dashTime > 0)
            {
                player.dashTime--;
            }
            if (player.dashTime < 0)
            {
                player.dashTime++;
            }
            if (player.controlRight && player.releaseRight)
            {
                if (player.dashTime > 0)
                {
                    dir = 1;
                    dashing = true;
                    player.dashTime = 0;
                    player.timeSinceLastDashStarted = 0;
                }
                else
                {
                    player.dashTime = 15;
                }
            }
            else if (player.controlLeft && player.releaseLeft)
            {
                if (player.dashTime < 0)
                {
                    dir = -1;
                    dashing = true;
                    player.dashTime = 0;
                    player.timeSinceLastDashStarted = 0;
                }
                else
                {
                    player.dashTime = -15;
                }
            }
        }
    }
}
