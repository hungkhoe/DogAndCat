using DOG.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogAndCat
{
    public class AbilityItem : MonoBehaviour
    {
        [SerializeField] internal Button m_btnHealth;
        [SerializeField] internal Button m_btnBomb;
        [SerializeField] internal Button m_btnPower;
        [SerializeField] internal Button m_btnX2;

        private DogCatClass currentDogCat;

        private void Awake()
        {          
            m_btnHealth.SetAction(HealthButton);
            m_btnBomb.SetAction(BombButton);
            m_btnPower.SetAction(PowerButton);
            m_btnX2.SetAction(X2Button);
        }

        public void SetCurrentDogCat(DogCatClass _dogcat)
        {
            currentDogCat = _dogcat;
        }

        public void HealthButton()
        {
            if (currentDogCat.idDogCat != GameControl.instance.turnPlayer)
                return;

            if (currentDogCat.recoverHealth > 0)
            { 
                currentDogCat.recoverHealth--;
                if (currentDogCat.recoverHealth <= 0)
                    m_btnHealth.gameObject.SetActive(false);
                GameControl.instance.UseHealthRecover(currentDogCat.idDogCat);
            }
        }

        public void BombButton()
        {
            if (currentDogCat.idDogCat != GameControl.instance.turnPlayer)
                return;

            if (currentDogCat.bomb > 0)
            {
                currentDogCat.bomb--;
                if (currentDogCat.bomb <= 0)
                    m_btnBomb.gameObject.SetActive(false);
                GameControl.instance.UseBomb(currentDogCat.idDogCat);
            }
        }

        public void PowerButton()
        {
            if (currentDogCat.idDogCat != GameControl.instance.turnPlayer)
                return;

            if (currentDogCat.power > 0)
            {
                currentDogCat.power--;
                if (currentDogCat.power <= 0)
                    m_btnPower.gameObject.SetActive(false);
                GameControl.instance.UsePower(currentDogCat.idDogCat);
            }
        }

        public void X2Button()
        {
            if (currentDogCat.idDogCat != GameControl.instance.turnPlayer)
                return;

            if (currentDogCat.x2 > 0)
            {
                currentDogCat.x2--;
                if (currentDogCat.x2 <= 0)
                    m_btnX2.gameObject.SetActive(false);
                GameControl.instance.UseX2(currentDogCat.idDogCat);
            }
               
        }
    }
}

