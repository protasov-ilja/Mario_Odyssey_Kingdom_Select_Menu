using ProjectName;
using UnityEngine;

namespace Assets.GameAssets.Scripts.BezierCurves
{
	public sealed class BezierLinear : Bezier
	{
		private Vector3 _startPoint;
		private Vector3 _endPoint;

		public BezierLinear(int resolutionPoints, Vector3 startPoint, Vector3 endPoint) 
			: base(resolutionPoints)
		{
			_startPoint = startPoint;
			_endPoint = endPoint;
		}

		public override void GenerateCurvePoints()
		{
			positions = new Vector3[resolutionPoints];
			for (var i = 0; i < resolutionPoints; ++i)
			{
				var t = (i) / (float)(resolutionPoints - 1);
				positions[i] = CalculateLinearBezierPoint(t, _startPoint, _endPoint);
			}
		}

		private Vector3 CalculateLinearBezierPoint(float t, Vector3 startPoint, Vector3 endPoint)
		{
			// P = P0 + t(P1 - P0)
			return startPoint + t * (endPoint - startPoint);
		}
	}
}
