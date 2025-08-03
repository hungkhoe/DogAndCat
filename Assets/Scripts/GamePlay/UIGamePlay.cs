using _Main.Scripts.UI;
using DOG.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogAndCat
{
    public class UIGamePlay : MonoBehaviour
    {
        [SerializeField] private Image progressHpPlayer; 
        [SerializeField] private Image progressHpEnemy;
        [SerializeField] private GameObject victoryObj;
        [SerializeField] private GameObject loseObj;
        [SerializeField] private GameObject uiEndGame;
        [SerializeField] private Image imgWinFace;
        [SerializeField] private Image imgLoseFace;
        [SerializeField] private Button btnHome;
        internal AbilityItem abilityPlayer;
        internal AbilityItem abilityEnemy;

        private void Awake()
        {
            abilityPlayer = transform.FindTransform("AbilityPlayer").gameObject
                    .AddComponent<AbilityItem>();
            abilityEnemy = transform.FindTransform("AbilityEnemy").gameObject
                    .AddComponent<AbilityItem>();

            btnHome.SetAction(HomeButton);
        }

        public void UpdateHealthPlayer(float _currentHp, float _maxHp)
        {
            progressHpPlayer.fillAmount = _currentHp / _maxHp;
        }

        public void UpdateHealthEnemy(float _currentHp, float _maxHp)
        {
            progressHpEnemy.fillAmount = _currentHp / _maxHp;
        }

        public void ShowWinUI()
        {
            uiEndGame.SetActive(true);
            victoryObj.SetActive(true);
        }

        public void ShowLoseUI()
        {
            uiEndGame.SetActive(true);
            loseObj.SetActive(true);
        }

        private void HomeButton()
        {
            FadingScreen.Instance.LoadScene("ChooseCharacter");
        }
    }

    
}

