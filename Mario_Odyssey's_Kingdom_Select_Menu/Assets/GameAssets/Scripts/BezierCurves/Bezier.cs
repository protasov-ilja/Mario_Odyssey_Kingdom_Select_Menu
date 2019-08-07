using UnityEngine;

namespace Assets.GameAssets.Scripts.BezierCurves
{
	public abstract class Bezier
	{
		protected Vector3[] positions;
		protected int resolutionPoints = 30;

		public Vector3[] Positions => positions;

		public Bezier(int resolutionPoints)
		{
			this.resolutionPoints = resolutionPoints;
		}

		public int ResolutionPoints
		{
			get => resolutionPoints;
			set
			{
				resolutionPoints = value;
				GenerateCurvePoints();
			}
		}

		public abstract void GenerateCurvePoints();
	}
}