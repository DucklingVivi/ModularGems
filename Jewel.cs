


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ModularGems
{
    public class Jewel
    {
        public static Texture2D jewelTexture;

        public static void Load()
        {
            jewelTexture = ModContent.Request<Texture2D>("ModularGems/UI/GemTileSet", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        public List<Point16> Shape = new List<Point16>();
        public Color color;
        public Point16 anchor = new Point16(-1, -1);
        
        public Jewel()
        {
            color = Color.White;
        }
        public Jewel(List<Point16> shape, Color color)
        {
            this.Shape = new List<Point16>(shape);
            this.color = color;
        }
        
        internal Jewel Clone()
        {
            Jewel clone = new Jewel();
            clone.Shape = new List<Point16>(Shape);
            clone.color = color;
            clone.anchor = anchor;
            return clone;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {

            Vector2 truePos = new Vector2(anchor.X, anchor.Y);
            truePos *= 50f;
            truePos += Position;

            
            foreach (Point16 point in Shape)
            {
                                                                                                                            
                Vector2 tempVec = truePos + (point.ToVector2() * 50f);

                Rectangle target = new Rectangle((int)tempVec.X, (int)tempVec.Y, 50, 50);
                int count = 0;
                if(Shape.Contains(new Point16(point.X, point.Y + 1)))
                {
                    count += 1;
                }
                if (Shape.Contains(new Point16(point.X, point.Y -1)))
                {
                    count += 2;
                }
                if (Shape.Contains(new Point16(point.X + 1, point.Y)))
                {
                    count += 4;
                }
                if (Shape.Contains(new Point16(point.X - 1, point.Y)))
                {
                    count += 8;
                }
                Rectangle sourceRect = new Rectangle(0,0,50,50);

                sourceRect.X = count % 4 * 52;
                sourceRect.Y = count/4*52;
                spriteBatch.Draw(jewelTexture, target ,sourceRect, color);
            }
        }
        public void rotateRight()
        {
            for (int i = 0; i < Shape.Count; i++)
            {
                Shape[i] = new Point16(Shape[i].Y, -Shape[i].X);
            }
        }
        public void rotateLeft()
        {
            for (int i = 0; i < Shape.Count; i++)
            {
                Shape[i] = new Point16(-Shape[i].Y, Shape[i].X);
            }
        }
    }
    public enum Direction
    {
        Center = -1,
        Up,
        Right,
        Down,
        Left
    }
}