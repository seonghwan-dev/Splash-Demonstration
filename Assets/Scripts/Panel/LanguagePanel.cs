using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Splash
{
	internal class LanguagePanel : Panel
	{
		[Header(nameof(LanguagePanel))]
		public Button prevButton;
		public Button nextButton;

		private int index;
		private int localeCount;
		
		protected override void Awake()
		{
			base.Awake();

			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
		}

		public override IEnumerator Sequence()
		{
			// Wait for the localization system to initialize
			yield return LocalizationSettings.InitializationOperation;

			yield return Show();

			// Generate list of available Locales
			index = 0;
			localeCount = LocalizationSettings.AvailableLocales.Locales.Count;
			
			for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
			{
				var locale = LocalizationSettings.AvailableLocales.Locales[i];
				if (LocalizationSettings.SelectedLocale == locale)
				{
					index = i;
					break;
				}
			}
			
			prevButton.onClick.AddListener(OnPrev);
			nextButton.onClick.AddListener(OnNext);
		}

		private void OnNext()
		{
			index++;
			index %= localeCount;
			
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
		}

		private void OnPrev()
		{
			index--;
			index += localeCount;
			index %= localeCount;
			
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
		}
	}
}