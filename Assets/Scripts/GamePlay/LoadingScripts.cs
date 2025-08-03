using _BaseFeatures.Audios;
using _Main.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogAndCat
{
    public class LoadingScripts : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 120;
            Screen.SetResolution(1080, (int)(1080 / Camera.main.aspect), true);
        }

        private IEnumerator Start()
        {
            bool isLoadingCompleted = false;
            GameManager.Awaken();
            AudioManager.Initialize();
            AudioManager.OnCompleted += () => isLoadingCompleted = true;
            yield return new WaitForSeconds(3);
            yield return new WaitUntil(() => isLoadingCompleted);
            FadingScreen.Instance.LoadScene("ChooseCharacter");
        }
    }
}

