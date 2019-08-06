using UnityEngine;

namespace ProjectName
{
	[RequireComponent(typeof(LineRenderer))]
	public class Bezier : MonoBehaviour
	{
		#region Editor Fields
		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform _endPoint;
		[SerializeField] private int _resolutionPoints = 50;
		#endregion

		private Vector3[] _positions;
		private LineRenderer _lineRenderer;
		

		#region Unity Methods
		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
			_positions = new Vector3[_resolutionPoints];
		}

		private void Start()
		{
			_lineRenderer.positionCount = _resolutionPoints;

			DrawLinearCurve();
		}
		#endregion

		#region Public Methods

		#endregion

		#region Private Methods
		private void DrawLinearCurve()
		{
			for (var i = 0; i < _resolutionPoints; ++i)
			{
				var t = (i) / (float)(_resolutionPoints - 1);
				_positions[i] = CalculateLinearBezierPoint(t, _startPoint.position, _endPoint.position);
			}

			_lineRenderer.SetPositions(_positions);
		}

		private Vector3 CalculateLinearBezierPoint(double t, Vector3 startPoint, Vector3 endPoint)
		{
			// P = P0 + t(P1 - P0)
			return startPoint + (float)t * (endPoint - startPoint);
		}
		#endregion
	}
}

