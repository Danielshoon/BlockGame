﻿///REpresents a point in 3D space that has integer co-ordinates
///© 2013 Spine Games

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BlockGame.Utilities;

namespace BlockGame.Utilities
{
    /// <summary>
    /// Represents a 3D co-ordinate with integer x,y,z
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]    
    public class Point3
    {
        /// <summary>
        /// A point with all co-ords set to 1
        /// </summary>
        public static Point3 One
        {
            get { return new Point3(1, 1, 1); }
        }
        /// <summary>
        /// A point with all co-ords set to 0
        /// </summary>
        public static Point3 Zero
        {
            get { return new Point3(0,0,0); }
        }

        /// <summary>
        /// The x co-ords of this point
        /// </summary>
        public int X;
        /// <summary>
        /// The y co-ords of this point
        /// </summary>
        public int Y;
        /// <summary>
        /// The z co-ords of this point
        /// </summary>
        public int Z;

        /// <summary>
        /// Creates a new point with all co-ords set to one value
        /// </summary>
        /// <param name="size">The value to set all the coords to</param>
        public Point3(int size)
        {
            this.X = size;
            this.Y = size;
            this.Z = size;
        }

        /// <summary>
        /// Creates a new 3D point with different unit vectors
        /// </summary>
        /// <param name="x">The x co-ord</param>
        /// <param name="y">The y co-ord</param>
        /// <param name="z">The z co-ord</param>
        public Point3(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Methods
        /// <summary>
        /// Checks if this point is inside a cuboid
        /// </summary>
        /// <param name="min">The minimum point in the cuboid</param>
        /// <param name="max">The maximum point in the cuboid</param>
        /// <returns>True if this point is intersecting or within the cuboid</returns>
        public bool IsInCuboid(Point3 min, Point3 max)
        {
            return IsInCuboid(this, min, max);
        }

        /// <summary>
        /// Checks if this point is inside a cuboid
        /// </summary>
        /// <param name="cuboid">The cuboid to check against</param>
        /// <returns>True if this point is intersecting or within the cuboid</returns>
        public bool IsInCuboid(Cuboid cuboid)
        {
            return IsInCuboid(this, cuboid);
        }

        /// <summary>
        /// Clamps this point to be equal to or between two other points
        /// </summary>
        /// <param name="min">The min value to clamp to</param>
        /// <param name="max">The max value to clamp to</param>
        public void Clamp(Point3 min, Point3 max)
        {
            Point3 t = Clamp(this, min, max);
            this.X = t.X;
            this.Y = t.Y;
            this.Z = t.Z;
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        /// <param name="p2">The point to check this point against</param>
        /// <returns>The distance from this point to P2</returns>
        public float Distance(Point3 p2)
        {
            return (float)Math.Sqrt(X * p2.X + Y * p2.Y + Z * p2.Z);
        }

        /// <summary>
        /// Casts this point to a string
        /// </summary>
        /// <returns>The string representation of this point</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", X, Y, Z);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Clamps a point between teo other points
        /// </summary>
        /// <param name="p">The point to clamp</param>
        /// <param name="min">The min value to clamp to</param>
        /// <param name="max">The max value to clamp to</param>
        /// <returns>A new point representing p as a clamped value</returns>
        public static Point3 Clamp(Point3 p, Point3 min, Point3 max)
        {
            return new Point3(
                (int)MathHelper.Clamp(p.X, min.X, max.X),
                (int)MathHelper.Clamp(p.Y, min.Y, max.Y),
                (int)MathHelper.Clamp(p.Z, min.Z, max.Z));
        }

        /// <summary>
        /// Checks if a point is in a cuboid
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <param name="cuboid">The cuboid to compare against</param>
        /// <returns>True if point is instersecting or inside of cuboid</returns>
        public static bool IsInCuboid(Point3 p, Cuboid cuboid)
        {
            return (p.X >= cuboid.Min.X & p.X <= cuboid.Max.X) & (p.Y >= cuboid.Min.Y & p.Y <= cuboid.Max.Y) &
                (p.Z >= cuboid.Min.Z & p.Z <= cuboid.Max.Z);
        }

        /// <summary>
        /// Checks if a point is in a cuboid
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <param name="min">The minimum value to check against</param>
        /// <param name="max">The maximum value to check against</param>
        /// <returns>True if point is instersecting or inside of cuboid</returns>
        public static bool IsInCuboid(Point3 p, Point3 min, Point3 max)
        {
            return (p.X >= min.X & p.X <= max.X) & (p.Y >= min.Y & p.Y <= max.Y) & 
                (p.Z >= min.Z & p.Z <= max.Z);
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        /// <param name="p1">The first point to check</param>
        /// <param name="p2">The second point to check against</param>
        /// <returns></returns>
        public static float Distance(Point3 p1, Point3 p2)
        {
            return (float)Math.Sqrt(p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z);
        }
        #endregion

        #region Static Operators
        /// <summary>
        /// Handles subtracting one point from another
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p2">The point to subtract</param>
        /// <returns>p1 - p2</returns>
        public static Point3 operator -(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        /// <summary>
        /// Handles adding one point to another
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p2">The point to add</param>
        /// <returns>p1 + p2</returns>
        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        /// <summary>
        /// Handles multiplying two points
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p2">The point to multiply by</param>
        /// <returns>p1 * p2</returns>
        public static Point3 operator *(Point3 p1, Point3 p2)
        {
            return new Point3(p2.X * p1.X, p2.Y * p1.Y, p2.Z * p1.Z);
        }

        /// <summary>
        /// Handles dividing two points
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p2">The point to divide by</param>
        /// <returns>p1 / p2</returns>
        public static Point3 operator /(Point3 p1, Point3 p2)
        {
            return new Point3(p2.X / p1.X, p2.Y / p1.Y, p2.Z / p1.Z);
        }

        /// <summary>
        /// Handles multiplying a point by an integer value
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p">The value to multiply all unit vectors by</param>
        /// <returns>p1 * p</returns>
        public static Point3 operator *(Point3 p1, int p)
        {
            return new Point3(p* p1.X, p * p1.Y, p * p1.Z);
        }

        /// <summary>
        /// Handles dividing a point by an integer value
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="p">The value to divide all unit vectors by</param>
        /// <returns>p1 / p</returns>
        public static Point3 operator /(Point3 p1, int p)
        {
            return new Point3(p1.X / p, p1.Y / p, p1.Z / p);
        }

        /// <summary>
        /// Handles subtracting one point from another
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="v">The distance to add</param>
        /// <returns>p1 - v</returns>
        public static Point3 operator -(Point3 p1, int v)
        {
            return new Point3(p1.X - v, p1.Y - v, p1.Z - v);
        }

        /// <summary>
        /// Handles adding one point to another
        /// </summary>
        /// <param name="p1">The source point</param>
        /// <param name="v">The distance to add</param>
        /// <returns>p1 + v</returns>
        public static Point3 operator +(Point3 p1, int v)
        {
            return new Point3(p1.X + v, p1.Y + v, p1.Z + v);
        }

        /// <summary>
        /// Handles casting a 3D point to a Vector3
        /// </summary>
        /// <param name="point">The point to cast</param>
        /// <returns>The Vector3 version of point</returns>
        public static implicit operator Vector3(Point3 point)
        {
            return new Vector3(point.X, point.Y, point.Z);
        }
        #endregion
    }
}
