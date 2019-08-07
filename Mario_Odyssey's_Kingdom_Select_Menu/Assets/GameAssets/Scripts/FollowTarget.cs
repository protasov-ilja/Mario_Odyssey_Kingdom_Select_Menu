using UnityEngine;

namespace Assets.GameAssets.Scripts
{
	public sealed class FollowTarget : MonoBehaviour
	{
		public Transform target;

		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			if (target != null)
			{
				transform.position = _camera.WorldToScreenPoint(target.position);
			}
		}
	}
}
