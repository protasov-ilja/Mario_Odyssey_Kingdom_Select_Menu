using UnityEngine;

namespace Assets.GameAssets.Scripts.BezierCurves
{
	[RequireComponent(typeof(LineRenderer))]
	public class BezierCurve : MonoBehaviour
	{
		#region Editor Fields
		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform _endPoint;
		[SerializeField] private Transform _middlePoint;
		[SerializeField] private int _resolutionPoints = 50;
		[SerializeField] private BezierType _type = BezierType.Quadratic;
		#endregion

		private LineRenderer _lineRenderer;
		private Bezier _bezier;

		#region Unity Methods
		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
		}

		private void Start()
		{
			_lineRenderer.positionCount = _bezier.ResolutionPoints;
			_lineRenderer.SetPositions(_bezier.Positions);
		}
		#endregion

		#region Public Methods
		#endregion

		#region Private Methods
		private void InitializeBezier()
		{
			switch (_type)
			{
				case BezierType.Linear:
					_bezier = new BezierLinear(_resolutionPoints, _startPoint.position, _endPoint.position);
					break;
				case BezierType.Quadratic:
					_bezier = new BezierQuadratic(_resolutionPoints, _startPoint.position, _endPoint.position, _middlePoint.position);
					break;
				default:
					_bezier = new BezierLinear(_resolutionPoints, _startPoint.position, _endPoint.position);
					break;
			}

			_bezier.GenerateCurvePoints();
		}
		#endregion
	}
}
