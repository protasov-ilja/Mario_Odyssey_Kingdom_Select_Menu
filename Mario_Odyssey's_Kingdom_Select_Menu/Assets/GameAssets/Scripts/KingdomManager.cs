using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameAssets.Scripts
{
	public sealed class KingdomManager : MonoBehaviour, IKingdomManager
	{
		#region Editor Fields
		[SerializeField] private List<Kingdom> _kingdoms = new List<Kingdom>();

		[Space, Header("Public Regerences")]
		[SerializeField] private GameObject _kingdomPointPrefab;
		[SerializeField] private GameObject _kingdomButtonPrefab;
		[SerializeField] private Transform _modelTransform;
		[SerializeField] private Transform _kingdomButtonContainer;

		[Space, Header("Tween Settings")]
		[SerializeField] private float _lookDuration;
		[SerializeField] private Ease _lookEase;
		[Space]
		public Vector2 visualOffset;
		#endregion

		private Camera _camera;
		private Transform _cameraParent;
		private Action<IKingdom> OnKingdomCreated;

		#region Unity Methods
		private void Start()
		{
			_camera = Camera.main;
			_cameraParent = _camera.transform.parent;
			foreach (IKingdom k in _kingdoms)
			{
				SpawnKingdomPoint(k);
				OnKingdomCreated?.Invoke(k);
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
		public void LookAtKingdom(IKingdom k)
		{
			var cameraPivot = _cameraParent.parent;

			_cameraParent.DOLocalRotate(new Vector3(k.YPosition, 0, 0), _lookDuration, RotateMode.Fast).SetEase(_lookEase);
			cameraPivot.DOLocalRotate(new Vector3(0, -k.XPosition, 0), _lookDuration, RotateMode.Fast).SetEase(_lookEase);

			FindObjectOfType<FollowTarget>().target = k.VisualPoint;
		}

		#endregion

		#region Private Methods
		private void SpawnKingdomButton(IKingdom k)
		{
			Button kingdomButton = Instantiate(_kingdomButtonPrefab, _kingdomButtonContainer).GetComponent<Button>();
			kingdomButton.onClick.AddListener(() => LookAtKingdom(k));

			kingdomButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = k.Name;
		}

		private void SpawnKingdomPoint(IKingdom k)
		{
			var kingdom = Instantiate(_kingdomPointPrefab, _modelTransform);
			kingdom.transform.localEulerAngles = new Vector3(k.YPosition + visualOffset.y, -k.XPosition - visualOffset.x, 0);
			k.VisualPoint = kingdom.transform.GetChild(0);
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
					parent.transform.eulerAngles += new Vector3(_kingdoms[i].YPosition, -_kingdoms[i].XPosition, 0);
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
