using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Splash
{
	internal class BrandingPanel : Panel
	{
		[Header(nameof(BrandingPanel))]
		public bool QuitApp = true;
		
		public Image blackLogo;
		public Image whiteLogo;

		public float beforeShow = 1.0f;
		public float blackDuration = 1.0f;
		public float interval = 1.0f;
		public float whiteDuration = 2.0f;
		public float fadeOut = 1.0f;

		public override IEnumerator Sequence()
		{
			var transparent = Color.black;
			transparent.a = 0;

			blackLogo.color = transparent;
			whiteLogo.color = transparent;

			yield return new WaitForSeconds(beforeShow);

			Color color = Color.white;

			// yield return Show();
			float elpased = 0f;
			while (elpased < blackDuration)
			{
				color.a = elpased / blackDuration;
				blackLogo.color = color;

				elpased += Time.deltaTime;
				yield return null;
			}

			blackLogo.color = Color.white;

			yield return new WaitForSeconds(interval);

			var inverseColor = Color.white;

			elpased = 0f;
			while (elpased < whiteDuration)
			{
				float alpha = elpased / whiteDuration;

				color.a = alpha;
				inverseColor.a = 1 - alpha;

				whiteLogo.color = color;
				blackLogo.color = inverseColor;

				elpased += Time.deltaTime;
				yield return null;
			}

			whiteLogo.color = Color.white;
			blackLogo.color = transparent;

			yield return new WaitForSeconds(interval);

			elpased = 0f;
			while (elpased < fadeOut)
			{
				float alpha = elpased / fadeOut;
				color.a = 1 - alpha;

				whiteLogo.color = color;

				elpased += Time.deltaTime;
				yield return null;
			}

			whiteLogo.color = transparent;
			
			yield return new WaitForSeconds(interval);
		}
	}
}