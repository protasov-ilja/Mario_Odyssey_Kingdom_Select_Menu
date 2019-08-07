using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameAssets.Scripts.UI
{
	public sealed class SelectionCursor : MonoBehaviour
	{
		[SerializeField] private RectTransform _rect;
		[Space]
		[SerializeField] private float _duration;
		[SerializeField] private float _delay;

		private Image _image;
		private Vector2 _originalSize;

		private void Start()
		{
			_image = _rect.GetComponent<Image>();
			_image.DOFade(0, 0);

			_originalSize = _rect.sizeDelta;
			_rect.sizeDelta = _originalSize / 4f;

			StartCoroutine(Delay());
		}

		IEnumerator Delay()
		{
			yield return new WaitForSeconds(_delay);
			Animate();
		}

		public void Animate()
		{
			Sequence sequence = DOTween.Sequence().SetLoops(-1);
			sequence.Append(_rect.DOSizeDelta(_originalSize, _duration).SetEase(Ease.OutCirc));
			sequence.Join(_image.DOFade(1, _duration / 3));
			sequence.Join(_image.DOFade(0, _duration / 4).SetDelay(_duration / 1.5f));
		}
	}
}
