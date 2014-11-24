using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pong.DebugTypes
{
    class PathMapper : FontEffectsLib.SpriteTypes.ComplexSprite
    {
        /// <summary>
        /// Sides of a bounding box
        /// </summary>
        [Flags]
        public enum BoundingBoxSide
        { 
            Top = 1,
            Bottom = 2,
            Left = 4,
            Right = 8
        }

        /// <summary>
        /// Calculate and visually represent the path of an object traveling in a predefined bounding box
        /// </summary>
        /// <param name="startingPosition">Starting position of the object whose path is being calculated</param>
        /// <param name="initialDirection">The direction in which the object starts its movement</param>
        /// <param name="boundingBox">The predefined bounding box in which the object is traveling</param>
        /// <param name="finalDestination">The side of the bounding box where the travel ends. Reaching this side stops further calculations</param>
        public PathMapper(Vector2 startingPosition, Vector2 initialDirection, Rectangle boundingBox, BoundingBoxSide finalDestination)
            : base(Vector2.Zero, null)
        {
            //Check for invalid finalDestination value            
            if (finalDestination.HasFlag(BoundingBoxSide.Bottom) && finalDestination.HasFlag(BoundingBoxSide.Top))
            { 
                throw new ArgumentOutOfRangeException("Final Destination cannot be \"Bottom Top\"");
            }

            if (finalDestination.HasFlag(BoundingBoxSide.Left) && finalDestination.HasFlag(BoundingBoxSide.Right))
            { 
                throw new ArgumentOutOfRangeException("Final Destination cannot be \"Left Right\"");
            }

            calculatePath(startingPosition, initialDirection, boundingBox, finalDestination);   
        }

        private void calculatePath(Vector2 startingPosition, Vector2 initialDirection, Rectangle boundingBox, BoundingBoxSide finalDestination)
        {  
            //Define a variable to keep track of "exit border" - that is, what bounding box border is the object hitting first
            BoundingBoxSide? exitBorder = null;

            //Define a variable to keep track of the exact position where the object hits the "exit border"
            Vector2 exitPosition = Vector2.Zero;

            //Define a variable to keep track of the new direction the object will travel in as it reflects from the "exit border"
            Vector2 newDirection = Vector2.Zero;
            
            //TODO: calculations... see "BulletPath" example ... and Stan

            //Recursion and exit condition
            if (exitBorder != finalDestination)
            {
                calculatePath(exitPosition, newDirection, boundingBox, finalDestination);
            }
        }
    }
}
