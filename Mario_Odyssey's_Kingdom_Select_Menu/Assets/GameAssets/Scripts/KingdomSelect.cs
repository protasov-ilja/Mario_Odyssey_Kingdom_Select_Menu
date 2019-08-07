using Assets.GameAssets.Scripts;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		[SerializeField] private Ease _lookEase;
		[Space]
		public Vector2 visualOffset;
		#endregion

		private Camera _camera;
		private Transform _cameraParent;

		#region Unity Methods
		private void Start()
		{
			_camera = Camera.main;
			_cameraParent = _camera.transform.parent;
			foreach (Kingdom k in _kingdoms)
			{
				SpawnKingdomPoint(k);
				SpawnKingdomButton(k);
			}

			if (_kingdoms.Count > 0)
			{
				LookAtKingdom(_kingdoms[0]);
				UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_kingdomButtonContainer.GetChild(0).gameObject);
			}
		}

		#endregion

		#region Public Methods
		public void LookAtKingdom(Kingdom k)
		{
			var cameraPivot = _cameraParent.parent;

			_cameraParent.DOLocalRotate(new Vector3(k.y, 0, 0), _lookDuration, RotateMode.Fast).SetEase(_lookEase);
			cameraPivot.DOLocalRotate(new Vector3(0, -k.x, 0), _lookDuration, RotateMode.Fast).SetEase(_lookEase);	

			FindObjectOfType<FollowTarget>().target = k.visualPoint;
		}

		#endregion

		#region Private Methods
		private void SpawnKingdomButton(Kingdom k)
		{
			Button kingdomButton = Instantiate(_kingdomButtonPrefab, _kingdomButtonContainer).GetComponent<Button>();
			kingdomButton.onClick.AddListener(() => LookAtKingdom(k));

			kingdomButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = k.name;
		}

		private void SpawnKingdomPoint(Kingdom k)
		{
			var kingdom = Instantiate(_kingdomPointPrefab, _modeltransform);
			kingdom.transform.localEulerAngles = new Vector3(k.y + visualOffset.y, -k.x - visualOffset.x, 0);
			k.visualPoint = kingdom.transform.GetChild(0);
		}

		private void OnDrawGizmos()
		{
#if UNITY_EDITOR
			Gizmos.color = Color.red;

			//only draw if there is at least one stage
			if (_kingdoms.Count > 0)
			{
				for (int i = 0; i < _kingdoms.Count; i++)
				{
					//creat two empty objects
					GameObject point = new GameObject();
					GameObject parent = new GameObject();
					//move the point object to the front of the world sphere
					point.transform.position += -new Vector3(0, 0, .5f);
					//parent the point to the "parent" object in the center
					point.transform.parent = parent.transform;
					//set the visual offset
					parent.transform.eulerAngles = new Vector3(visualOffset.y, -visualOffset.x, 0);

					if (!Application.isPlaying)
					{
						Gizmos.DrawWireSphere(point.transform.position, 0.02f);
					}

					//spint the parent object based on the stage coordinates
					parent.transform.eulerAngles += new Vector3(_kingdoms[i].y, -_kingdoms[i].x, 0);
					//draw a gizmo sphere // handle label in the point object's position
					Gizmos.DrawSphere(point.transform.position, 0.07f);
					//destroy all
					DestroyImmediate(point);
					DestroyImmediate(parent);
				}
			}
#endif
		}
		#endregion
	}
}

