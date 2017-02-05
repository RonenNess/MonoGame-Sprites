#region File Description
//-----------------------------------------------------------------------------
// A basic sprite entity.
//
// Author: Ronen Ness.
// Since: 2016.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoSprite
{
    /// <summary>
    /// Basic sprite entity (renderable image).
    /// </summary>
    public class Sprite : Renderable
    {
        /// <summary>
        /// Texture source rectangle (in pixels).
        /// This also affect drawing size, unless the Size property is set.
        /// </summary>
        Rectangle _srcRect;

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
        /// Create the new sprite entity.
        /// </summary>
        public Sprite() : base()
        {
            _srcRect = new Rectangle(0, 0, 0, 0);
            Size = Point.Zero;
            Texture = null;
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
            _srcRect = new Rectangle(0, 0, 0, 0);
            Size = size ?? Point.Zero;
            Texture = texture;
            Origin = origin ?? Vector2.One * 0.5f;
            Position = position ?? Vector2.Zero;
            Color = color ?? Color.White;
            Zindex = zindex;
            if (parent != null) { parent.AddChild(this); }
        }

        /// <summary>
        /// Clone this sprite.
        /// </summary>
        /// <returns>Clonsed sprite.</returns>
        public Sprite Clone()
        {
            Sprite ret = new Sprite();
            ret._srcRect = _srcRect;
            ret.Origin = Origin;
            ret.Texture = Texture;
            ret.Size = Size;
            ret.Visible = Visible;
            ret.Zindex = Zindex;
            ret._localTrans = _localTrans.Clone();
            return ret;
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

            // draw the sprite
            spriteBatch.Draw(
                Texture,
                rotation: WorldTransformations.Rotation,
                position: WorldTransformations.Position,
                scale: scale,
                origin: origin,
                color: WorldTransformations.Color,
                layerDepth: zindex);
        }
    }
}
