using System;
using System.Collections.Generic;
using System.Linq;


public class Disk
{
    public readonly Vector2 position;
    public readonly Vector2 velocity;
    public readonly double radius;

    public Disk(Vector2 position, Vector2 velocity, double radius)
    {
        this.position = position;
        this.velocity = velocity;
        this.radius = radius;
    }

    public override int GetHashCode() {
        unchecked
        {
            return 17 * position.GetHashCode() + 23 * velocity.GetHashCode() + 31 * radius.GetHashCode();
        }
    }

    public static override bool operator==(Disk d1, Disk d2){
        return d1.Equals(d2);
    }

    public static override bool operator !=(Disk d1, Disk d2)
    {
        return d1.Equals(d2) == false;
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
        if(position == otherDisk.position
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
        return new Disk(position + velocity, velocity, radius);
    }

    public Disk Accelerate(Vector2 acceleration)
    {
        return new Disk(position, velocity + acceleration, radius);
    }

    public Disk Accelerate(double factor)
    {
        return new Disk(position, velocity*factor, radius);
    }


    /**
     * <summary> Identify if the disk will collide with each other assuming that both
     * disks will remain with a constant speed. A collision occurs when the two
     * circles touch each other </summary>
     * 
     */
    public bool WillCollide(Disk other)
    {
        Vector2 toOther = other.position - position;
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

