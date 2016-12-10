using System;


/** Vector2 Class
 * 
 * Author: Oran Bar
 */
[Serializable]
public class Vector2 : IEquatable<Vector2>
{
    #region Static Variables
    public static double COMPARISON_TOLERANCE = 0.0000001;

    private readonly static Vector2 zeroVector = new Vector2(0);
    private readonly static Vector2 unitVector = new Vector2(1);

    public static Vector2 Zero
    {
        get { return zeroVector; }
        private set { }
    }
    public static Vector2 One
    {
        get { return unitVector; }
        private set { }
    }
    #endregion

    public virtual double X { get; set; }
    public virtual double Y { get; set; }

    public Vector2(double val)
    {
        this.X = val;
        this.Y = val;
    }

    public Vector2(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector2(Vector2 v)
    {
        this.X = v.X;
        this.Y = v.Y;
    }

    #region Operators
    public static Vector2 operator +(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2 operator *(Vector2 v1, double mult)
    {
        return new Vector2(v1.X * mult, v1.Y * mult);
    }

    public static bool operator ==(Vector2 a, Vector2 b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(Vector2 a, Vector2 b)
    {
        return (a == b) == false;
    }
    #endregion

    #region Object Class Overrides
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return Equals(obj as Vector2);
    }

    public bool Equals(Vector2 other)
    {
        if ((object)other == null)
        {
            return false;
        }
        if (Math.Abs(X - other.X) > COMPARISON_TOLERANCE)
        {
            return false;
        }
        if (Math.Abs(Y - other.Y) > COMPARISON_TOLERANCE)
        {
            return false;
        }
        return true;

    }


    public override int GetHashCode()
    {
        unchecked
        {
            return 17 * X.GetHashCode() + 23 * Y.GetHashCode();
        }
    }


    public override string ToString()
    {
        return String.Format("[{0}, {1}] ", X, Y);
    }
    #endregion

    #region Vector2 Methods
    public static double Distance(Vector2 v1, Vector2 v2)
    {
        return Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
    }

    public static double DistanceSquared(Vector2 v1, Vector2 v2)
    {
        return Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2);
    }

    public double Distance(Vector2 other)
    {
        return Vector2.Distance(this, other);
    }

    public double DistanceSquared(Vector2 other)
    {
        return Vector2.DistanceSquared(this, other);
    }

    public Vector2 Closest(Vector2 v1, Vector2 v2)
    {
        double distanceToV1 = this.DistanceSquared(v1);
        double distanceToV2 = this.DistanceSquared(v2);
        return (distanceToV1 <= distanceToV2) ? v1 : v2;
    }

    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    public double LengthSquared()
    {
        return X * X + Y * Y;
    }

    public Vector2 Normalize()
    {
        double length = LengthSquared();
        return new Vector2(X / length, Y / length);
    }

    public double Dot(Vector2 v)
    {
        return X * v.X + Y * v.Y;
    }

    public double Cross(Vector2 v)
    {
        return X * v.Y + Y * v.X;
    }

    public Vector2 Orthogonal()
    {
        return new Vector2(-Y, X);
    }
    #endregion
}

