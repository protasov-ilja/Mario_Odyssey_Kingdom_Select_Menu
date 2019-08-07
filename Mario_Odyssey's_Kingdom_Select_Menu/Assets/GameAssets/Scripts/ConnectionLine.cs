using Assets.GameAssets.Scripts.BezierCurves;
using UnityEngine;

namespace Assets.GameAssets.Scripts
{
	[RequireComponent(typeof(LineRenderer))]
	public sealed class ConnectionLine : MonoBehaviour
	{
		[SerializeField] private Transform[] _points = new Transform[3];
		[SerializeField] private Vector3[] _positions = new Vector3[50];
		[SerializeField] private float _distance;
		[Range(0, 1)]
		[SerializeField] private float _distanceAmount = 1;

		private int _numPoints = 50;
		private LineRenderer _line;
		private Bezier _bezier;

		private void Start()
		{
			_line = GetComponent<LineRenderer>();
			_line.positionCount = _numPoints;

			Transform nextPoint;
			if (transform.GetSiblingIndex() + 1 < transform.parent.childCount)
			{
				nextPoint = transform.parent.GetChild(transform.GetSiblingIndex() + 1);
				SetMidPoint(nextPoint);
				DrawQuadraticCurve();
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				DrawQuadraticCurve();
			}
		}

		private void SetMidPoint(Transform nextPoint)
		{
			_distance = Vector3.Distance(transform.GetChild(0).position, nextPoint.GetChild(0).position);

			var pivot = new GameObject();
			var point = new GameObject();

			pivot.transform.parent = transform.parent.parent;

			point.transform.parent = pivot.transform;
			point.transform.position += (Vector3.forward / 2) + Vector3.forward * _distance * _distanceAmount;

			pivot.transform.localEulerAngles = new Vector3((transform.localEulerAngles.x + nextPoint.localEulerAngles.x) / 2,
				(transform.localEulerAngles.y + nextPoint.localEulerAngles.y) / 2, 0);

			_points[0] = transform.GetChild(0);
			_points[1] = point.transform;
			_points[2] = nextPoint.GetChild(0);
		}

		private void DrawQuadraticCurve()
		{
			_bezier = new BezierQuadratic(_numPoints, _points[0].position, _points[2].position, _points[1].position);
			_bezier.GenerateCurvePoints();
			_positions = _bezier.Positions;
			_line.SetPositions(_positions);
		}
	}
}
