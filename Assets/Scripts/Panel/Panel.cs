using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Splash
{
	[RequireComponent(typeof(CanvasGroup))]
	internal abstract class Panel : UIBehaviour
	{
		[Serializable]
		internal class Registry
		{
			[SerializeField] internal string name;
			[SerializeField] internal int order;
			[SerializeField] internal EMethod method;
			[SerializeField] internal Panel panel;

			internal enum EMethod
			{
				Undefined,
				Execute,
				Await,
			};
		}
		
		[Header("Panel")] 
		[SerializeField] protected CanvasGroup canvasGroup = default;
		[SerializeField] protected float fadeDuration = 1.0f;
		[SerializeField] protected bool hideOnAwake = true;

		[Header("Panel/Canvas Group")] 
		[SerializeField] protected bool controlBlockRaycasts = true;
		[SerializeField] protected bool controlInteractable = true;

		protected override void Awake()
		{
			if (hideOnAwake)
			{
				canvasGroup.alpha = 0;

				if (controlBlockRaycasts)
				{
					canvasGroup.blocksRaycasts = false;
				}

				if (controlInteractable)
				{
					canvasGroup.interactable = false;
				}
			}
		}

		public virtual IEnumerator Sequence()
		{
			yield break;
		}

		protected virtual IEnumerator Show()
		{
			float elpased = 0f;
			while (elpased < fadeDuration)
			{
				canvasGroup.alpha = elpased / fadeDuration;
				
				elpased += Time.deltaTime;
				yield return null;
			}
			
			canvasGroup.alpha = 1;

			if (controlBlockRaycasts)
			{
				canvasGroup.blocksRaycasts = true;
			}

			if (controlInteractable)
			{
				canvasGroup.interactable = true;
			}
		}

		protected virtual IEnumerator Hide()
		{
			if (controlBlockRaycasts)
			{
				canvasGroup.blocksRaycasts = false;
			}

			if (controlInteractable)
			{
				canvasGroup.interactable = false;
			}
			
			float elpased = 1f;
			while (elpased < fadeDuration)
			{
				canvasGroup.alpha = 1 - elpased / fadeDuration;
				
				elpased += Time.deltaTime;
				yield return null;
			}
			
			canvasGroup.alpha = 0;
		}

#if UNITY_EDITOR
		protected override void Reset()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
#endif
	}
}