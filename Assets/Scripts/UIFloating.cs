using System.Collections;
using UnityEngine;

namespace Splash
{
	public class UIFloating : MonoBehaviour
	{
		public float duration;
		public Vector2 offset = new Vector2(-20, 80);
		public Vector2 destination;

		private void Start()
		{
			// Move();
		}

		public void Move()
		{
			StartCoroutine(MoveRoutine());
		}

		private IEnumerator MoveRoutine()
		{
			var rect = GetComponent<RectTransform>();

			rect.anchoredPosition = offset;

			float elpased = 0f;
			while (elpased < duration)
			{
				rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, destination, elpased);

				elpased += Time.deltaTime;
				yield return null;
			}

			rect.anchoredPosition = destination;
		}
	}
}