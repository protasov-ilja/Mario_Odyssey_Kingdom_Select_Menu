using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.GameAssets.Scripts.UI
{
	public sealed class KingdomButton : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private Image _rect;
		[SerializeField] private Image _circle;

		[SerializeField] private Color _textColorWhenSelected;
		[SerializeField] private Color _rectColorMouseOver;

		private void Start()
		{
			_rect.color = Color.clear;
			_text.color = Color.white;
			_circle.color = Color.white;
		}

		public void OnDeselect(BaseEventData eventData)
		{
			_rect.DOColor(Color.clear, 0.1f);
			_text.DOColor(Color.white, 0.1f);
			_circle.DOColor(Color.white, 0.1f);
		}

		public void OnSelect(BaseEventData eventData)
		{
			_rect.DOColor(Color.white, 0.1f);
			_text.DOColor(_textColorWhenSelected, 0.1f);
			_circle.DOColor(Color.red, 0.1f);

			_rect.transform.DOComplete();
			_rect.transform.DOPunchScale(Vector3.one / 3, 0.2f, 20, 1);
		}

		public void OnSubmit(BaseEventData eventData)
		{

		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (EventSystem.current.currentSelectedGameObject != gameObject)
			{
				_rect.DOColor(_rectColorMouseOver, 0.2f);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (EventSystem.current.currentSelectedGameObject != gameObject)
			{
				_rect.DOColor(Color.clear, 0.2f);
			}
		}
	}
}
