using System.Collections;
using UnityEngine;

namespace Splash
{
    /// <summary>
    /// 시스템 로그 출력
    /// @TODO :: 동적 생성으로 변경
    /// </summary>
    public class SystemLogPrint : MonoBehaviour
    {
        public GameObject[] logs;

        public float wait = 1.4f;
        public float minDuration = 0.2f;
        public float maxDuration = 1.0f;

        private void Awake()
        {
            foreach (GameObject log in logs)
            {
                log.SetActive(false);
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(wait);

            for (int i = 0; i < logs.Length; i++)
            {
                GameObject log = logs[i];

                float duration = Random.Range(minDuration, maxDuration);
                var floating = log.GetComponent<UIFloating>();

                floating.duration = duration;
                floating.destination = new Vector2(0, 0);

                log.SetActive(true);
                floating.Move();

                for (int j = 0; j <= i; j++)
                {
                    var previous = logs[i - j].GetComponent<UIFloating>();

                    previous.destination = new Vector2(0, (1 + j) * 24);
                    previous.duration = duration;
                    previous.offset = previous.GetComponent<RectTransform>().anchoredPosition;

                    previous.Move();
                }

                yield return new WaitForSeconds(duration);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("로그 오브젝트 가져오기")]
        private void GatherChild()
        {
            var childCount = transform.childCount;
            logs = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                logs[i] = transform.GetChild(i).gameObject;
            }
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
        }
        #endif
    }
}