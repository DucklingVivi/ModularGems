using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularGems.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ModularGems
{
    public class ModularGemsItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public JewelGrid grid;
        public override void SetDefaults(Item item)
        {
            if(item.type == ModContent.ItemType<JewelBag>())
            {
                this.grid = new JewelGrid(4, 4);
            }            
        }
        
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ModularGemsItem clone = (ModularGemsItem)base.Clone(item, itemClone);
            clone.grid = grid?.Clone() ?? null;
            
            return clone;
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            
            if(grid != null)
            {
                tag.Set("grid", JewelGridIO.Save(grid));
            }
        }
        
        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.ContainsKey("grid"))
            {
                grid = JewelGridIO.Load(tag.GetCompound("grid"));
            }

            base.LoadData(item, tag);
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {

            if (grid != null)
            {
                writer.Write("grid");
                JewelGridIO.Send(grid, writer);
            }
            writer.Write("end");
            base.NetSend(item, writer);
            
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            string nextString = reader.ReadString();
           
            if(nextString == "grid")
            {
                grid = JewelGridIO.Recieve(reader);
                nextString = reader.ReadString();
            }

            base.NetReceive(item, reader);
            
        }

        //public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //    if(texture != null)
        //    {

        //        Texture2D value5 = TextureAssets.Item[item.type].Value;
        //        Texture2D value7 = ModContent.Request<Texture2D>(texture, mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        //        Vector2 vector = TextureAssets.InventoryBack.Value.Size() * Main.inventoryScale;
        //        Rectangle rectangle2 = ((Main.itemAnimations[item.type] == null) ? value7.Frame() : Main.itemAnimations[item.type].GetFrame(value7));
        //        float scale2 = 1f;
        //        //DO STUFF HERE FOR PULSING
        //        float num13 = 1f;
        //        if (rectangle2.Width > 32 || rectangle2.Height > 32)
        //        {
        //            num13 = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width));
        //        }
        //        num13 *= Main.inventoryScale;
        //        Vector2 num14 = value5.Size() * num13 / 2f;
        //        Vector2 position2 = position + num14 - (rectangle2.Size() * num13) / 2f;
        //        Vector2 origin2 = rectangle2.Size() * (scale2 / 2f - 0.5f);
        //        spriteBatch.Draw(value7, position2, rectangle2, drawColor, 0f, origin2,num13 * scale2, SpriteEffects.None, 0f);
        //        if (item.color != Color.Transparent)
        //        {
        //            spriteBatch.Draw(value7, position2, rectangle2, itemColor, 0f, origin2, num13 * scale2, SpriteEffects.None, 0f);
        //        }
        //        return false;
        //    }
        //    return true;
        //}

        //public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //    if (texture != null)
        //    {
        //        Texture2D value7 = ModContent.Request<Texture2D>(texture, mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        //        Rectangle frame;
        //        Rectangle glowmaskFrame;

        //        if (Main.itemAnimations[item.type] != null)
        //        {
        //            frame = (glowmaskFrame = Main.itemAnimations[item.type].GetFrame(value7, Main.itemFrameCounter[whoAmI]));
        //        }
        //        else
        //        {
        //            frame = (glowmaskFrame = value7.Frame());
        //        }

        //        Vector2 vector = frame.Size() / 2f;
        //        Vector2 vector2 = new Vector2((float)(item.width / 2) - vector.X, item.height - frame.Height);
        //        Vector2 vector3 = item.position - Main.screenPosition + vector + vector2;
        //        float num = item.velocity.X * 0.2f;
        //        float scale2 = 1f;
        //        Color color = Lighting.GetColor(item.Center.ToTileCoordinates());
        //        Color currentColor = item.GetAlpha(color);
        //        //DO STUFF HERE FOR PULSING
        //        int num2 = item.glowMask;


        //        spriteBatch.Draw(value7, vector3, frame, currentColor, num, vector, scale2, SpriteEffects.None, 0f);
        //        if (item.color != Color.Transparent)
        //        {
        //            spriteBatch.Draw(value7, vector3, frame, item.GetColor(color), num, vector, scale2, SpriteEffects.None, 0f);
        //        }
        //        return false;
        //    }
        //    return true;
        //}


        
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if(item.type == ModContent.ItemType<JewelBag>() && slot < 20 && modded == false)
            {
                return false;
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //if (this.tooltips.Count > 0)
            //{
            //    foreach (TooltipLine line in this.tooltips)
            //    {
            //        tooltips.Add(line);
            //    }
            //}
            if(item.type == ModContent.ItemType<JewelBag>())
            {


                tooltips.Add(new TooltipLine(ModularGems.Instance, "Size", grid.Width + "x" + grid.Height));
            }
        }
    }
}
