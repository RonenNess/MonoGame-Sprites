﻿#region File Description
//-----------------------------------------------------------------------------
// A basic sprite entity.
//
// Author: Ronen Ness.
// Since: 2017.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoSprites
{
    /// <summary>
    /// Basic sprite entity (renderable image).
    /// </summary>
    public class Sprite : Renderable
    {
        /// <summary>
        /// Sprite origin / source, eg pivot point for rotation etc.
        /// </summary>
        public Vector2 Origin = Vector2.One * 0.5f;

        /// <summary>
        /// Texture to draw.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Size, in pixels, we want this sprite to be when rendered.
        /// </summary>
        public Point Size { get; set; }

        /// <summary>
        /// Optional texture source rectangle.
        /// </summary>
        public Rectangle? SourceRectangle;

        /// <summary>
        /// Create the new sprite entity.
        /// </summary>
        public Sprite() : base()
        {
        }

        /// <summary>
        /// Create the new sprite entity with params.
        /// </summary>
        /// <param name="texture">Texture to use for this sprite.</param>
        /// <param name="size">Sprite starting size.</param>
        /// <param name="origin">Origin of the sprite (also known as anchor point) relative to drawing size.</param>
        /// <param name="position">Sprite local position.</param>
        /// <param name="color">Sprite color.</param>
        /// <param name="zindex">Sprite zindex.</param>
        /// <param name="parent">Parent container.</param>
        public Sprite(Texture2D texture, Point? size = null, Vector2? origin = null, Vector2? position = null, Color? color = null, float zindex = 0f, Renderable parent = null) : base()
        {
            Size = size ?? Point.Zero;
            Texture = texture;
            Origin = origin ?? Vector2.One * 0.5f;
            Position = position ?? Vector2.Zero;
            Color = color ?? Color.White;
            Zindex = zindex;
            if (parent != null) { parent.AddChild(this); }
        }

        /// <summary>
        /// Clone this sprite object.
        /// </summary>
        /// <param name="includeChildren">If true, will include children in clone.</param>
        /// <returns>Cloned object.</returns>
        override public Renderable Clone(bool includeChildren)
        {
            return new Sprite(this, includeChildren);
        }

        /// <summary>
        /// Clone an existing Sprite object.
        /// </summary>
        /// <param name="copyFrom">Sprite to copy properties from.</param>
        /// <param name="includeChildren">If true, will also clone children.</param>
        public Sprite(Sprite copyFrom, bool includeChildren) : base(copyFrom, includeChildren)
        {
            SourceRectangle = copyFrom.SourceRectangle;
            Origin = copyFrom.Origin;
            Texture = copyFrom.Texture;
            Size = copyFrom.Size;
        }

        /// <summary>
        /// Set a source rectangle from spritesheet.
        /// </summary>
        /// <param name="index">Sprite index to pick.</param>
        /// <param name="spritesCount">Number of sprites on X and Y axis.</param>
        public void SetSourceFromSpritesheet(Point index, Point spritesCount)
        {
            Point size = Texture.Bounds.Size / spritesCount;
            SourceRectangle = new Rectangle(index * size, size);
        }

        /// <summary>
        /// Draw the sprite itself.
        /// </summary>
        /// <remarks>When this function is called, transformation is already applied (position / scale / rotation).</remarks>
        /// <param name="spriteBatch">Spritebatch to use for drawing.</param>
        /// <param name="zindex">Final rendering zindex.</param>
        protected override void DoDraw(SpriteBatch spriteBatch, float zindex)
        {
            // no texture? skip
            if (Texture == null)
            {
                return;
            }

            // if source rect is 0,0, set to texture default size
            var _srcRect = SourceRectangle ?? new Rectangle(0, 0, 0, 0);
            if (_srcRect.Width == 0) { _srcRect.Width = Texture.Width; }
            if (_srcRect.Height == 0) { _srcRect.Height = Texture.Height; }

            // calculate origin point
            Vector2 origin = new Vector2(_srcRect.Width * Origin.X, _srcRect.Height * Origin.Y);

            // get scale from transformations
            Vector2 scale = WorldTransformations.Scale;

            // take desired size into consideration
            if (Size.X != 0)
            {
                scale.X *= (float)Size.X / Texture.Width;
                scale.Y *= (float)Size.Y / Texture.Height;
            }

            // set flips
            var effects = SpriteEffects.None;
            if (scale.X < 0) effects |= SpriteEffects.FlipHorizontally;
            if (scale.Y < 0) effects |= SpriteEffects.FlipVertically;

            // draw the sprite
            spriteBatch.Draw(
                texture: Texture,
                position: WorldTransformations.Position,
                sourceRectangle: _srcRect,
                color: WorldTransformations.Color,
                rotation: WorldTransformations.Rotation,
                origin: origin,
                scale: new Vector2(System.Math.Abs(scale.X), System.Math.Abs(scale.Y)),
                effects: effects,
                layerDepth: zindex);
        }
    }
}
