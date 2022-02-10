


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularGems.Items.Jewels;
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
        public static Texture2D jewelHighlightTexture;
        public static void Load()
        {
            jewelTexture = ModContent.Request<Texture2D>("ModularGems/UI/GemTileSet", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            jewelHighlightTexture = ModContent.Request<Texture2D>("ModularGems/UI/GemTileSet_Highlight", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        public List<Point16> _shape = new List<Point16>();
        public List<Point16> Shape = new List<Point16>();
        public Color color;
        public int rotation { get; private set; }
        public Point16 anchor = new Point16(-1, -1);

        
        public Jewel()
        {

            this.SetRotation(0);
        }
        
        internal Jewel Clone()
        {
            Jewel clone = new Jewel();
            clone.Shape = Shape;
            clone.color = color;
            clone.anchor = anchor;
            
            clone.rotation = rotation;
            clone._shape = _shape;
            return clone;
        }


        public void SetShape(List<Point16> shape)
        {
            Shape = new List<Point16>(shape);
            processRotation();
        }

        public void SetRotation(int rot)
        {
            rotation = rot;
            processRotation();
        }

        public void processRotation()
        {
            _shape.Clear();
            _shape = new List<Point16>(Shape);
            
            for (int i = 0; i < rotation; i++)
            {
                for (int j = 0; j < Shape.Count; j++)
                {
                    _shape[j] = (new Point16(_shape[j].Y, -_shape[j].X));
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {


            
            Vector2 truePos = new Vector2(anchor.X, anchor.Y);
            truePos *= 50f;
            truePos += Position;

            
            foreach (Point16 point in _shape)
            {
                                                                                                                            
                Vector2 tempVec = truePos + (point.ToVector2() * 50f);

                Rectangle target = new Rectangle((int)tempVec.X, (int)tempVec.Y, 50, 50);
                int count = 0;
                if(_shape.Contains(new Point16(point.X, point.Y + 1)))
                {
                    count += 1;
                }
                if (_shape.Contains(new Point16(point.X, point.Y -1)))
                {
                    count += 2;
                }
                if (_shape.Contains(new Point16(point.X + 1, point.Y)))
                {
                    count += 4;
                }
                if (_shape.Contains(new Point16(point.X - 1, point.Y)))
                {
                    count += 8;
                }
                Rectangle sourceRect = new Rectangle(0,0,50,50);

                sourceRect.X = count % 4 * 52;
                sourceRect.Y = count/4*52;
                spriteBatch.Draw(jewelTexture, target ,sourceRect, color);
            }
        }
        public void DrawHover(SpriteBatch spriteBatch, Vector2 Position)
        {



            Vector2 truePos = new Vector2(anchor.X, anchor.Y);
            truePos *= 50f;
            truePos += Position;


            foreach (Point16 point in _shape)
            {

                Vector2 tempVec = truePos + (point.ToVector2() * 50f);

                Rectangle target = new Rectangle((int)tempVec.X, (int)tempVec.Y, 50, 50);
                int count = 0;
                if (_shape.Contains(new Point16(point.X, point.Y + 1)))
                {
                    count += 1;
                }
                if (_shape.Contains(new Point16(point.X, point.Y - 1)))
                {
                    count += 2;
                }
                if (_shape.Contains(new Point16(point.X + 1, point.Y)))
                {
                    count += 4;
                }
                if (_shape.Contains(new Point16(point.X - 1, point.Y)))
                {
                    count += 8;
                }
                Rectangle sourceRect = new Rectangle(0, 0, 50, 50);

                sourceRect.X = count % 4 * 52;
                sourceRect.Y = count / 4 * 52;
                spriteBatch.Draw(jewelTexture, target, sourceRect, color * 0.75f);
            }
        }
        public void DrawHighlight(SpriteBatch spriteBatch, Vector2 Position)
        {
            Vector2 truePos = new Vector2(anchor.X, anchor.Y);
            truePos *= 50f;
            truePos += Position;
            foreach (Point16 point in _shape)
            {
                Vector2 tempVec = truePos + (point.ToVector2() * 50f);
                Rectangle target = new Rectangle((int)tempVec.X, (int)tempVec.Y, 50, 50);
                int count = 0;
                if (_shape.Contains(new Point16(point.X, point.Y + 1)))
                {
                    count += 1;
                }
                if (_shape.Contains(new Point16(point.X, point.Y - 1)))
                {
                    count += 2;
                }
                if (_shape.Contains(new Point16(point.X + 1, point.Y)))
                {
                    count += 4;
                }
                if (_shape.Contains(new Point16(point.X - 1, point.Y)))
                {
                    count += 8;
                }
                Rectangle sourceRect = new Rectangle(0, 0, 50, 50);
                sourceRect.X = count % 4 * 52;
                sourceRect.Y = count / 4 * 52;
                spriteBatch.Draw(jewelHighlightTexture, target, sourceRect, Color.Yellow);
            }
        }
        private void rotateRight()
        {

            
        }
        private void rotateLeft()
        {
            _shape.Clear();
            for (int i = 0; i < Shape.Count; i++)
            {
                _shape.Add(new Point16(-Shape[i].Y, Shape[i].X));
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