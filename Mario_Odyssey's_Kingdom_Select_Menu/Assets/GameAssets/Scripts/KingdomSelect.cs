using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName
{
	public class KingdomSelect : MonoBehaviour
	{
		#region Editor Fields
		[SerializeField] private List<Kingdom> _kingdoms = new List<Kingdom>();

		[Space, Header("Public Regerences")]
		[SerializeField] private GameObject _kingdomPointPrefab;
		[SerializeField] private GameObject _kingdomButtonPrefab;
		[SerializeField] private Transform _modeltransform;
		[SerializeField] private Transform _kingdomButtonContainer;

		[Space, Header("Tween Settings")]
		[SerializeField] private float _lookDuration;
		//private Ease lookEase;
		#endregion

		#region Unity Methods
		private void Start()
		{
			foreach (Kingdom k in _kingdoms)
			{
				SpawnKingdomPoint(k);
			}
		}


		#endregion

		#region Public Methods
		public void LookAtKingdom(Kingdom k)
		{
			var cameraParent = Camera.main.transform.parent;
			var cameraPivot = cameraParent.parent;

			cameraParent.localEulerAngles = new Vector3(k.x, 0, 0);
			cameraPivot.localEulerAngles = new Vector3(cameraPivot.localEulerAngles.x, k.y, 0);
		}
		#endregion

		#region Private Methods
		private void SpawnKingdomPoint(Kingdom k)
		{
			var kingdom = Instantiate(_kingdomPointPrefab, _modeltransform);
			kingdom.transform.localEulerAngles = new Vector3(k.x, k.y, 0);
		}

		
		#endregion
	}
}

