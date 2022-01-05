using System;
using System.Collections;
using UnityEngine;

namespace Splash
{
	public class Splash : MonoBehaviour
	{
		public LogoFade logoFade;
		public SystemLogPrint logger;
		public PanelLanguage language;
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit(0);
			}
		}

		private IEnumerator Start()
		{
			StartCoroutine(logger.Load());
			yield return logoFade.Load();
			// yield return logger.Load();

			yield return language.Load();
		}
	}
}