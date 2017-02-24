using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InfluenceMap
{

	protected float[][] InfluenceMap_Arr { get; set; }
	protected int Width { get; set; }
	protected int Height { get; set; }


	public InfluenceMap(int width, int height) {
		this.Width = width;
		this.Height = height;
		this.InfluenceMap_Arr = new float[width][];
		for (int i = 0; i < width; i++) {
			this.InfluenceMap_Arr[i] = new float[height];
		}
	}

	public float Get(int x, int y) {
		return InfluenceMap_Arr[x][y];
	}

	public int[][] GetNeighbours(int x, int y) {
		int noOfNeighbours = 8;
		if (x == 0 || x == Width - 1) {
			noOfNeighbours -= 3;
		}
		if (y == 0 || y == Width - 1) {
			if (noOfNeighbours < 8) {
				noOfNeighbours -= 2;
			}
			noOfNeighbours -= 3;
		}

		int currNeighbours = 0;
		int[][] neighbours = new int[noOfNeighbours][];
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				if (i == 0 && j == 0) { continue; }

				int xNeighbour = x - i, yNeighbour = y - j;
				if (xNeighbour >= 0 && xNeighbour <= Width - 1
				&& yNeighbour >= 0 && yNeighbour <= Height - 1) {
					neighbours[currNeighbours] = new int[] { xNeighbour, yNeighbour };
					currNeighbours++;
				}
			}
		}
		return neighbours;
	}

	public void ApplyInfluence(int x, int y, float amount, int fullAmountDistance, int reducedAmountDistance, float distanceDecay) {
		ApplyInfluenceRecursive(x, y, amount, fullAmountDistance, reducedAmountDistance, distanceDecay);
	}

	public void ApplyInfluenceRecursive(int x, int y, float amount, int fullAmountDistance, int reducedAmountDistance, float distanceDecay) {
		if (fullAmountDistance < 0 && reducedAmountDistance < 0) { Console.Error.Write("Error Applying Influence! Fix the recursion"); }

		if (fullAmountDistance > 0) {
			InfluenceMap_Arr[x][y] = amount;
			foreach(int[] neighbour in GetNeighbours(x, y)) {
				ApplyInfluenceRecursive(neighbour[0], neighbour[1], amount, fullAmountDistance - 1, reducedAmountDistance, distanceDecay);
			}
		}

		if (reducedAmountDistance > 0) {
			InfluenceMap_Arr[x][y] = amount;
			foreach (int[] neighbour in GetNeighbours(x, y)) {
				ApplyInfluenceRecursive(neighbour[0], neighbour[1], amount * distanceDecay, fullAmountDistance, reducedAmountDistance - 1, distanceDecay);
			}
		}
	}

	public void ApplyInfluence(float amount, int fullAmountDistance, int reducedAmountDistance, float distanceDecay, params int[] points) {
		if (points.Length % 2 == 1) {
			Console.Error.WriteLine("invalid number of points args");
		}

		int noOfPoints = points.Length / 2;

		amount /= noOfPoints;

		for (int i = 0; i < noOfPoints; i++) {
			int pointX = points[i];
			int pointY = points[i + 1];

			ApplyInfluence(pointX, pointY, amount, fullAmountDistance, reducedAmountDistance, distanceDecay);
		}

	}
}
