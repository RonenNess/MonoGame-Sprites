using System;
using System.Collections.Generic;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework;

namespace MonoSprites.Pressable
{
    /// <summary>
    /// Extension of container to handle click targetting.
    /// </summary>
    public class PressableContainer : Container
    {
        /// <summary>
        /// The container's mouse event listener.
        /// </summary>
        protected MouseListener listener;

        /// <summary>
        /// The subject of the current drag event.
        /// </summary>
        protected PressableSprite dragSubject;

        /// <summary>
        /// Pressable container's constructor.
        /// </summary>
        public PressableContainer(MouseListener listener) : base()
        {
            this.listener = listener;

            listener.MouseClicked += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (target == null)
                {
                    return;
                }

                var actions = new Dictionary<MouseButton, Action<MouseEventArgs>>()
                {
                    { MouseButton.Left, target.LeftClick },
                    { MouseButton.Right, target.RightClick },
                    { MouseButton.Middle, target.MiddleClick }
                };

                if (actions[args.Button] != null)
                {
                    actions[args.Button](args);
                }
            };

            listener.MouseDown += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (target == null)
                {
                    return;
                }

                var actions = new Dictionary<MouseButton, Action<MouseEventArgs>>()
                {
                    { MouseButton.Left, target.LeftDown },
                    { MouseButton.Right, target.RightDown },
                    { MouseButton.Middle, target.MiddleDown }
                };

                if (actions[args.Button] != null)
                {
                    actions[args.Button](args);
                }
            };

            listener.MouseUp += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (target == null)
                {
                    return;
                }

                var actions = new Dictionary<MouseButton, Action<MouseEventArgs>>()
                {
                    { MouseButton.Left, target.LeftUp },
                    { MouseButton.Right, target.RightUp },
                    { MouseButton.Middle, target.MiddleUp }
                };

                if (actions[args.Button] != null)
                {
                    actions[args.Button](args);
                }
            };

            listener.MouseDragStart += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (target == null)
                {
                    return;
                }

                if (target.DragStart != null)
                {
                    target.DragStart(args);

                    dragSubject = target;
                }
            };

            listener.MouseDragEnd += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (dragSubject != null && dragSubject.DragEnd != null)
                {
                    dragSubject.DragEnd(args, target);
                }

                dragSubject = null;

                if (target == null)
                {
                    return;
                }

                if (target.DragStart != null)
                {
                    target.DragStart(args);
                }
            };

            listener.MouseMoved += (object sender, MouseEventArgs args) => {
                var target = FindTarget(args.Position.ToVector2());

                if (target != null && target.Under != null)
                {
                    target.Under(args);
                }
            };
        }

        /// <summary>
        /// Finds the top level sprite under the specified world position.
        /// </summary>
        protected PressableSprite FindTarget(Vector2 position)
        {
            var under = Under(position);

            // Should give us a front-to-back list.
            under.Reverse();

            // Find the first pressable or child of a cascading pressable under the mouse.
            foreach (var current in under)
            {
                var target = BubblePress(current);

                if (target != null)
                {
                    return target;
                }
            }

            return null;
        }

        /// <summary>
        /// Handler for mouse under event.
        /// </summary>
        public List<PressableSprite> Under(Vector2 position)
        {
            var ret = new List<PressableSprite>();

            Traverse(
                (Renderable node) => {
                    if (node.Visible && ((PressableSprite)node).Contains(position))
                    {
                        ret.Add((PressableSprite)node);
                    }
                },
                typeof(PressableSprite)
            );

            return ret;
        }

        /// <summary>
        /// Handles bubbling up the target event throughout the descendant tree.
        /// </summary>
        public static PressableSprite BubblePress(Renderable target, bool bubbling = false)
        {
            if (target.GetType() == typeof(PressableSprite))
            {
                var pressTarget = (PressableSprite)target;

                if ((bubbling && !pressTarget.TargetChildren) || !pressTarget.Active)
                {
                    return null;
                }

                return (PressableSprite)target;
            }

            if (target.Parent != null)
            {
                return BubblePress(target.Parent, true);
            }

            return null;
        }
    }
}
