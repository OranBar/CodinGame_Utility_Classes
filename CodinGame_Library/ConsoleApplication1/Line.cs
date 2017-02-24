using System;
using System.Collections.Generic;

public class Line
{
    public double Slope { get; private set; }
    public double Offset { get; private set; }

    private Vector2 pointOnLine = null;

    public Line(Vector2 point1, Vector2 point2)
    {
        this.Slope = (point2.Y - point1.Y) / (point2.X - point1.X);
        this.pointOnLine = point1;

        this.Offset = pointOnLine.Y - Slope * pointOnLine.X;
    }

    public Line(Vector2 point1, double slope)
    {
        this.pointOnLine = point1;

        this.Offset = pointOnLine.Y - Slope * pointOnLine.X;
    }

    public double GetY(double x)
    {
        return Slope * x + Offset;
    }

	public double GetX(double y) {
		return (y - Offset) / Slope;
	}

	public Vector2 GetIntersection(Line other) {
        //TODO:
        return null;
    }

}
