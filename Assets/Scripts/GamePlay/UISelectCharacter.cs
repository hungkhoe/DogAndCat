using _BaseFeatures.DOAnimations;
using _Main.Scripts.UI;
using DOG.Extension;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogAndCat
{
    public class UISelectCharacter : MonoBehaviour
    {
        [SerializeField] private Button btnCat;
        [SerializeField] private Button btnDog;
        [SerializeField] private RectTransform uiSelectCharacter;
        [SerializeField] private RectTransform uiSelectForm;
        [SerializeField] private GameObject uiCat;
        [SerializeField] private GameObject uiDog;
        [SerializeField] private Button btnBack;
        [SerializeField] private Button btnFind;

        private CanvasGroup groupCurrent;
        private CanvasGroup groupNext;

        [SerializeField] private Button btnCat1;
        [SerializeField] private Button btnCat2;
        [SerializeField] private Button btnDog1;
        [SerializeField] private Button btnDog2;
        [SerializeField] private Transform dog1;
        [SerializeField] private Transform dog2;
        [SerializeField] private Transform cat1;
        [SerializeField] private Transform cat2;

        private Transform currentCat;
        private Transform currentDog;
        private UserVersusInfo userVersusInfo = new UserVersusInfo();

        private void Awake()
        {
            btnCat.SetAction(()=> ChooseDogOrCatButton(1));
            btnDog.SetAction(()=> ChooseDogOrCatButton(2));
            btnBack.SetAction(() => Fading(uiSelectForm, uiSelectCharacter));
            btnFind.SetAction(FindButton);
            btnCat1.SetAction(() => ChooseFormDogOrCat(cat2, cat1, 1, 1));
            btnCat2.SetAction(() => ChooseFormDogOrCat(cat1, cat2, 1, 2));
            btnDog1.SetAction(() => ChooseFormDogOrCat(dog2, dog1, 2, 1));
            btnDog2.SetAction(() => ChooseFormDogOrCat(dog1, dog2, 2, 2));
            currentCat = cat1;
            currentDog = dog1;
            userVersusInfo.playerInfo.form = 1;
            userVersusInfo.playerInfo.idAnimal = 1;
        }

        #region Choose Dog Cat
        //1 = Cat 2 = Dog
        private void ChooseDogOrCatButton(int _idDogCat)
        {
            Fading(uiSelectCharacter, uiSelectForm);
            switch (_idDogCat)
            {
                case 1:
                    uiCat.SetActive(true);
                    uiDog.SetActive(false);
                    userVersusInfo.playerInfo.form = 1;
                    break;
                case 2:
                    uiCat.SetActive(false);
                    uiDog.SetActive(true);
                    userVersusInfo.playerInfo.form = 2;
                    break;  
            }
        }

        private void Fading(RectTransform currentUI, RectTransform nextUI)
        {
            groupCurrent = currentUI.GetComponent<CanvasGroup>();
            groupNext = nextUI.GetComponent<CanvasGroup>();
            currentUI.DOFloatProgression(1, 0, alphaCanvasGroupCurrent)
                .SetCurve(PresetCurves.InOutEase)
                .SetDuration(0.25f)
                .OnComplete(() =>{ currentUI.gameObject.SetActive(false);
                                    nextUI.gameObject.SetActive(true);
                                    nextUI.DOFloatProgression(0, 1, alphaCanvasGroupNext)
                                   .SetCurve(PresetCurves.InOutEase)
                                   .SetDuration(0.25f)
                                   .Play(); })
                   
                .Play();
        }

        private void alphaCanvasGroupCurrent(float _value)
        {
            if (groupCurrent != null)
                groupCurrent.alpha = _value;
        }

        private void alphaCanvasGroupNext(float _value)
        {
            if (groupNext != null)
                groupNext.alpha = _value;
        }
        #endregion

        private void ChooseFormDogOrCat(Transform current, Transform target, int _formDogCat, int _idDogCat)
        {

            if (_formDogCat == 1 && currentCat == target)
            {
                return;
            }
            else if (_formDogCat == 2 && currentDog == target)
            {
                return;
            }
                

            current.DOScale(new Vector3(1.2f, 1.2f, 1.2f), new Vector3(0.8f, 0.8f, 0.8f))
                .Play();

            target.DOScale(new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.2f, 1.2f, 1.2f))
                .Play();

            current.FindDeepChilds<Transform>("btnChar").DOColorImage(Color.white, new Color(0.3f, 0.3f, 0.3f))
                .Play();

            target.FindDeepChilds<Transform>("btnChar").DOColorImage(new Color(0.3f, 0.3f, 0.3f), Color.white)
                .Play();

            if (_formDogCat == 1)
            {
                currentCat = target;
                userVersusInfo.playerInfo.idAnimal = _idDogCat;
                userVersusInfo.enemyInfo.form = 2;
                userVersusInfo.enemyInfo.idAnimal = Random.Range(0, 2);
            }
            else
            {
                currentDog = target;
                userVersusInfo.playerInfo.idAnimal = _idDogCat;
                userVersusInfo.enemyInfo.form = 1;
                userVersusInfo.enemyInfo.idAnimal = Random.Range(0, 2);
            }
        }

        private void FindButton()
        {
            PlayerPrefs.SetString(Config.USER_DATA, JsonConvert.SerializeObject(userVersusInfo));
            Debug.Log(PlayerPrefs.GetString(Config.USER_DATA));
            FadingScreen.Instance.LoadScene("VersusLoading");
        }
    }
}

