using _Main.Scripts.UI;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DogAndCat
{
    public class UIVersusLoading : MonoBehaviour
    {
        [SerializeField] private Sprite[] catSprArr;
        [SerializeField] private Sprite[] dogSprArr;
        [SerializeField] private Image imgPlayer;
        [SerializeField] private Image imgEnemy;
        [SerializeField] private TextMeshProUGUI txtNamePlayer;
        [SerializeField] private TextMeshProUGUI txtNameEnemy;
        private UserVersusInfo user;

        private void Awake()
        {
            user = new UserVersusInfo();
            user = JsonConvert.DeserializeObject<UserVersusInfo>(PlayerPrefs.GetString(Config.USER_DATA));
        }

        private void Start()
        {
            if (user.playerInfo.form == 1)
            {
                imgPlayer.sprite = catSprArr[user.playerInfo.idAnimal - 1];
                imgEnemy.sprite = dogSprArr[user.playerInfo.idAnimal];
            }
            else
            {
                imgPlayer.sprite = dogSprArr[user.playerInfo.idAnimal - 1];
                imgEnemy.sprite = catSprArr[user.playerInfo.idAnimal];
            }

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(2);
            FadingScreen.Instance.LoadScene("GamePlay");
        }
    }
}

