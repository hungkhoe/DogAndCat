using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.UI
{
    public class FadingScreen : MonoBehaviour
    {
        private static FadingScreen _instance;
        public static FadingScreen Instance => _instance;

        private AsyncOperation m_Operator;
        private CanvasGroup c_Group;
        private GameObject g_ImgFading;
        private float m_Alpha;
        private bool _isCompleted;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            c_Group = GetComponent<CanvasGroup>();
            g_ImgFading = transform.Find("imgFading").gameObject;
            m_Alpha = c_Group.alpha;
        }

        public void LoadScene(string sceneName)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadSceneRoutine(sceneName));
        }


        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            yield return FadeInRoutine();

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            yield return FadeOutRoutine();
        }

        private IEnumerator FadeInRoutine(float duration = 0.5f)
        {
            while (m_Alpha < 1)
            {
                m_Alpha += Time.deltaTime / duration;
                c_Group.alpha = m_Alpha;
                yield return null;
            }
        }

        private IEnumerator FadeOutRoutine(float duration = 0.5f)
        {
            while (m_Alpha > 0)
            {
                m_Alpha -= Time.deltaTime / duration;
                c_Group.alpha = m_Alpha;
                yield return null;
            }

            if (!g_ImgFading.activeSelf)
            {
                g_ImgFading.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}