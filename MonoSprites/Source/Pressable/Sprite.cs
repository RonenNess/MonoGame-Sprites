using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace MonoSprites.Pressable
{
    /// <summary>
    /// Click targetable sprite. Should universally support touch, but not currently implemented.
    /// </summary>
    public class PressableSprite : Sprite
    {
        /// <summary>
        /// Whether the click targetting should cascade to the Sprite's children
        /// </summary>
        public bool TargetChildren = false;

        /// <summary>
        /// Whether the click event is active.
        /// </summary>
        public bool Active = true;


        /// <summary>
        /// Event to fire when sprite is under the mouse.
        /// </summary>
        public Action<MouseEventArgs> Under;

        /// <summary>
        /// Event to trigger when sprite is left clicked.
        /// </summary>
        public Action<MouseEventArgs> LeftClick;

        /// <summary>
        /// Event to trigger when sprite is targetted and left mouse button is pressed.
        /// </summary>
        public Action<MouseEventArgs> LeftDown;

        /// <summary>
        /// Event to trigger when sprite is targetted and left mouse button is depressed.
        /// </summary>
        public Action<MouseEventArgs> LeftUp;

        /// <summary>
        /// Event to trigger when sprite is middle clicked.
        /// </summary>
        public Action<MouseEventArgs> MiddleClick;

        /// <summary>
        /// Event to trigger when sprite is targetted and middle mouse button is pressed.
        /// </summary>
        public Action<MouseEventArgs> MiddleDown;

        /// <summary>
        /// Event to trigger when sprite is targetted and middle mouse button is depressed.
        /// </summary>
        public Action<MouseEventArgs> MiddleUp;

        /// <summary>
        /// Event to trigger when sprite is right clicked.
        /// 
        /// </summary>
        public Action<MouseEventArgs> RightClick;

        /// <summary>
        /// Event to trigger when sprite is targetted and right mouse button is pressed.
        /// </summary>
        public Action<MouseEventArgs> RightDown;

        /// <summary>
        /// Event to trigger when sprite is targetted and right mouse button is depressed.
        /// </summary>
        public Action<MouseEventArgs> RightUp;

        /// <summary>
        /// Event to trigger when sprite drag begins.
        /// </summary>
        public Action<MouseEventArgs> DragStart;

        /// <summary>
        /// Event to trigger when sprite drag begins.
        /// </summary>
        /// <remarks>If the sprite is dragged onto another pressable sprite, that will be passed as the second parameter to DragEnd.</remarks>
        public Action<MouseEventArgs, PressableSprite> DragEnd;


        /// <summary>
        /// Pressable sprite constructor.
        /// </summary>
        public PressableSprite(
            Texture2D texture,
            Point? size = null,
            Vector2? origin = null,
            Vector2? position = null,
            Color? color = null,
            float zindex = 0f,
            Renderable parent = null
        ) : base(texture, size, origin, position, color, zindex, parent)
        { }
    }
}
