using System;
using System.Collections.Generic;
using System.Linq;


public class Disk : Vector2
{
    public readonly Vector2 velocity;
    public readonly double radius;

    public Disk(Vector2 position, Vector2 velocity, double radius) : base(position.X, position.Y)
    {
        this.velocity = velocity;
        this.radius = radius;
    }

    public override int GetHashCode() {
        unchecked
        {
            return 17 * this.GetHashCode() + 23 * velocity.GetHashCode() + 31 * radius.GetHashCode();
        }
    }

    public static bool operator==(Disk d1, Disk d2){
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(d1, d2))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)d1 == null) || ((object)d2 == null))
        {
            return false;
        }
        return d1.Equals(d2);
    }

    public static bool operator !=(Disk d1, Disk d2)
    {
        return (d1==d2) == false;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Disk otherDisk = obj as Disk;
        if (otherDisk == null)
        {
            return false;
        }
        if(this == otherDisk
            && velocity == otherDisk.velocity
            && radius == otherDisk.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /** <summary>Move the disk by its speed vector</summary> 
     */
    public Disk Move()
    {
        return new Disk(this + velocity, velocity, radius);
    }

    public Disk Accelerate(Vector2 acceleration)
    {
        return new Disk(this, velocity + acceleration, radius);
    }

    public Disk Accelerate(double factor)
    {
        return new Disk(this, velocity*factor, radius);
    }


    /**
     * <summary> Identify if the disk will collide with each other assuming that both
     * disks will remain with a constant speed. A collision occurs when the two
     * circles touch each other </summary>
     * 
     */
    public bool WillCollide(Disk other)
    {
        Vector2 toOther = other - this;
        Vector2 relativeSpeed = velocity - (other.velocity);
        if (relativeSpeed.LengthSquared() <= 0) // No relative movement
        {
            return false;
        }
        if (toOther.Dot(relativeSpeed) < 0) // Opposite directions
        {
            return false;
        }
        return Math.Abs(relativeSpeed.Normalize().Orthogonal().Dot(toOther)) <= radius + other.radius;
    }
}

