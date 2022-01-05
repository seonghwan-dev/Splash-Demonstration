using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Splash
{
	public class PanelLanguage : UIBehaviour
	{
		public float fadeDuration = 0.8f;
		
		public Button prevButton;
		public Button nextButton;
		
		public CanvasGroup canvasGroup;

		private int index;
		private int localeCount;
		
		protected override void Awake()
		{
			base.Awake();

			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
		}

		public IEnumerator Load()
		{
			// Wait for the localization system to initialize
			yield return LocalizationSettings.InitializationOperation;

			float elpased = 0f;
			while (elpased < fadeDuration)
			{
				canvasGroup.alpha = elpased / fadeDuration;
				
				elpased += Time.deltaTime;
				yield return null;
			}
			
			canvasGroup.alpha = 1;
			
			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;

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
				
				// options.Add(new TMP_Dropdown.OptionData(locale.name));
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