using UnityEngine;

namespace Assets.GameAssets.Scripts.BezierCurves
{
	public sealed class BezierQuadratic : Bezier
	{
		private Vector3 _startPoint;
		private Vector3 _endPoint;
		private Vector3 _middlePoint;

		public BezierQuadratic(int resolutionPoints, Vector3 startPoint, Vector3 endPoint, Vector3 middlePoint) 
			: base(resolutionPoints)
		{
			_startPoint = startPoint;
			_endPoint = endPoint;
			_middlePoint = middlePoint;
		}

		public override void GenerateCurvePoints()
		{
			positions = new Vector3[resolutionPoints];
			for (var i = 0; i < resolutionPoints; ++i)
			{
				var t = (i) / (float)(resolutionPoints - 1);
				positions[i] = CalculateQuadraticBezierPoint(t, _startPoint, _endPoint, _middlePoint);
			}
		}

		private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 startPoint, Vector3 endPoint, Vector3 middlePoint)
		{
			// P = (1 - t)^2P0 + 2(1 - t)tP1 + t^2P2
			float u = 1 - t;
			float tt = t * t;
			float uu = u * u;

			return Mathf.Pow(1 - t, 2) * startPoint + 2 * (1 - t) * t * middlePoint + Mathf.Pow(t, 2) * endPoint;
		}
	}
}
