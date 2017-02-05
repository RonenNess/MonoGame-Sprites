﻿#region File Description
//-----------------------------------------------------------------------------
// Basic renderable entity class.
//
// Author: Ronen Ness.
// Since: 2017.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoSprites
{
    /// <summary>
    /// MonoSprites is the main namespace that contains all the MonoGame-Sprites entities.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// A renderable entity.
    /// </summary>
    public class Renderable
    {
        /// <summary>
        /// Parent entity.
        /// </summary>
        protected Renderable _parent { get; set; }

        /// <summary>
        /// Local transformations (color, position, rotation..).
        /// </summary>
        protected Transformation _localTrans = new Transformation();

        /// <summary>
        /// World transformations (local transformations + parent's world transformations).
        /// These are the actual transformations that will apply when drawing the entity.
        /// </summary>
        private Transformation _worldTrans = new Transformation();

        /// <summary>
        /// Get final world transformations.
        /// </summary>
        public Transformation WorldTransformations { get { return _worldTrans; } }

        /// <summary>
        /// Is the entity currently visible?
        /// </summary>
        public bool Visible { get; set; }

        // currently calculated z-index, including parents.
        private float _finalZindex = 0f;

        // do we need to update transformations?
        private bool _needUpdateTransformations = false;

        // children entities of this renderable.
        private List<Renderable> _children = new List<Renderable>();

        /// <summary>
        /// String identifier you can attach to renderable entities.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Should we flip drawing on X axis?
        /// </summary>
        public bool FlipX
        {
            set
            {
                _localTrans.Scale.X = System.Math.Abs(_localTrans.Scale.X) * (value ? -1f : 1f);
                UpdateTransformations();
            }
        }

        /// <summary>
        /// Should we flip drawing on Y axis?
        /// </summary>
        public bool FlipY
        {
            set
            {
                _localTrans.Scale.Y = System.Math.Abs(_localTrans.Scale.Y) * (value ? -1f : 1f);
                UpdateTransformations();
            }
        }

        /// <summary>
        /// Renderable position.
        /// </summary>
        public Vector2 Position { get { return _localTrans.Position; } set { _localTrans.Position = value; UpdateTransformations(); } }

        /// <summary>
        /// Renderable scale.
        /// </summary>
        public Vector2 Scale { get { return _localTrans.Scale; } set { _localTrans.Scale = value; UpdateTransformations(); } }

        /// <summary>
        /// Renderable scale as a single scalar (in oppose to a vector).
        /// </summary>
        public float ScaleScalar { get { return _localTrans.Scale.X; } set { Scale = Vector2.One * value; } }

        /// <summary>
        /// Renderable rotation (radians).
        /// </summary>
        public float Rotation { get { return _localTrans.Rotation; } set { _localTrans.Rotation = value; UpdateTransformations(); } }

        /// <summary>
        /// Renderable z-index (relative to parent).
        /// </summary>
        public float Zindex { get { return _zindex; } set { _zindex = value; UpdateTransformations(); } }
        private float _zindex;

        /// <summary>
        /// Renderable tint color.
        /// </summary>
        public Color Color { get { return _localTrans.Color; } set { _localTrans.Color = value; UpdateTransformations(); } }

        /// <summary>
        /// Create the new renderable entity with default values.
        /// </summary>
        public Renderable()
        {
            _parent = null;
            Position = Vector2.Zero;
            ScaleScalar = 1f;
            Zindex = 0f;
            Visible = true;
            Color = Color.White;
        }

        /// <summary>
        /// Called whenever one of the transformations properties change and we need to update world transformations.
        /// </summary>
        protected void UpdateTransformations()
        {
            _needUpdateTransformations = true;
        }

        /// <summary>
        /// Add a child entity to this renderable.
        /// </summary>
        /// <param name="child">Child entity to add.</param>
        public void AddChild(Renderable child)
        {
            // if child already got a parent throw exception
            if (child._parent != null)
            {
                throw new System.Exception("Renderable to add as child already have a parent!");
            }

            // add child
            _children.Add(child);
            child._parent = this;

            // update child transformations (since now it got a new parent)
            child.UpdateTransformations();
        }

        /// <summary>
        /// Remove a child entity from this renderable.
        /// </summary>
        /// <param name="child">Child entity to remove.</param>
        public void RemoveChild(Renderable child)
        {
            // if child don't belong to this entity throw exception
            if (child._parent != this)
            {
                throw new System.Exception("Renderable to remove is not a child of this renderable!");
            }

            // remove child
            _children.Remove(child);
            child._parent = null;

            // update child transformations (since now it no longer got a parent)
            child.UpdateTransformations();
        }

        /// <summary>
        /// Remove this renderable from its parent.
        /// </summary>
        public void RemoveFromParent()
        {
            _parent.RemoveChild(this);
        }

        /// <summary>
        /// Draw this renderable entity.
        /// This will also update transformations if needed.
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to use for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // not visible? skip!
            if (!Visible)
            {
                return;
            }

            // if need to update transformations:
            if (_needUpdateTransformations)
            {
                // create world transformations (merged with parent)
                _worldTrans = _parent != null ? Transformation.Compose(_parent._worldTrans, _localTrans) : _localTrans;

                // calculate final zindex
                _finalZindex = (_parent != null ? _parent._finalZindex + Zindex : Zindex);

                // notify all childrens that they also need update
                foreach (var child in _children)
                {
                    child.UpdateTransformations();
                }

                // no longer need to update transformations
                _needUpdateTransformations = false;
            }

            // draw the entity
            DoDraw(spriteBatch, _finalZindex);

            // draw children
            foreach (var child in _children)
            {
                child.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Do the object-specific drawing function.
        /// Implement per renderable type.
        /// </summary>
        /// <remarks>When this function is called, transformation is already applied (position / scale / rotation).</remarks>
        /// <param name="spriteBatch">Spritebatch to use for drawing.</param>
        /// <param name="zindex">Final rendering zindex.</param>
        protected virtual void DoDraw(SpriteBatch spriteBatch, float zindex)
        {
        }
    }
}