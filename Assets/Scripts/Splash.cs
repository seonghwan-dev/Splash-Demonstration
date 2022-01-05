using System.Collections;
using System.Linq;
using UnityEngine;

namespace Splash
{
	public class Splash : MonoBehaviour
	{
		[SerializeField] internal Panel.Registry[] registries;
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit(0);
			}
		}

		private IEnumerator Start()
		{
			var sorted = registries.OrderBy(e => e.order);
			foreach (Panel.Registry registry in sorted)
			{
				if (registry.method == Panel.Registry.EMethod.Execute)
				{
					StartCoroutine(registry.panel.Sequence());
				}
				else if (registry.method == Panel.Registry.EMethod.Await)
				{
					yield return registry.panel.Sequence();
				}
			}
		}
	}
}