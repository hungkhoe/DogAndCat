using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DogAndCat.GameControl;

namespace DogAndCat
{
    public class ObjectThrow : MonoBehaviour
    {
        [SerializeField] Transform sprite;

        private float gravity = -20f;
        private Vector3 velocity;
        private float angle;
        private float aWind;
        private float rotationChange;
        //private float range;
        //private float tHit;
        private int damage = 10;
        private int idDogCat;
       
        float tHitBot;
        float tHitTop;
        float rangeTop;
        float rangeBot;

        public delegate void EndTurnEvent();
        public event EndTurnEvent EndTurn;

        public void InputData(float _angle, float _aWind, float _velocity, int _idDogCat, int _damage)
        {
            idDogCat = _idDogCat;
            if (idDogCat == (int)TypeUser.Player)
                angle = _angle;
            else
                angle = 180 - _angle;

            aWind = _aWind;
            velocity = new Vector3(_velocity * Mathf.Cos(angle * Mathf.PI / 180), _velocity * Mathf.Sin(angle * Mathf.PI / 180), 0);
            damage = _damage;

            float heightRangeTop = -2.114f;
            float heightRangeBot = 0.47f;
            float rangeMax = 12.984f;
            float rangeMin = 11.514f;
          
            tHitTop = Mathf.Abs(velocity.y / gravity) + Mathf.Sqrt(2 * ((velocity.y * velocity.y) / (2 * Mathf.Abs(gravity)) + heightRangeTop) / Mathf.Abs(gravity));
            tHitBot = Mathf.Abs(velocity.y / gravity) + Mathf.Sqrt(2 * ((velocity.y * velocity.y) / (2 * Mathf.Abs(gravity)) + heightRangeBot) / Mathf.Abs(gravity));
          
            rangeTop = Mathf.Abs(velocity.x * tHitTop + aWind * tHitTop * tHitTop / 2);
            rangeBot = Mathf.Abs(velocity.x * tHitBot + aWind * tHitBot * tHitBot / 2);

            if ((rangeTop >= rangeMin && rangeTop <= rangeMax) || rangeBot >= rangeMin && rangeBot <= rangeMax)
            {
                Debug.Log("true");
            }
            else if ((rangeTop > rangeMax && rangeBot < rangeMin) || rangeTop < rangeMin && rangeBot > rangeMax)
            {
                Debug.Log("true");
            }         
        }

        private void OnDestroy()
        {
            EndTurn?.Invoke();
        }

        private void Update()
        {
            UpdateGravity();
            UpdatePosition();
            RotateObject();

            if (transform.position.x > 13 || transform.position.x < -13 || transform.position.y < -7)
            {
                GameControl.instance.ShowMiss();
                Destroy(gameObject);
            }
        }

        private void UpdateGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            velocity.x += aWind * Time.deltaTime;
        }

        private void UpdatePosition()
        {
            transform.position += velocity * Time.deltaTime;
        }

        private void RotateObject()
        {
            rotationChange += 1;
            sprite.transform.eulerAngles = new Vector3(0, 0, rotationChange);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (idDogCat == 1)
            {
                if (collision.tag == "Enemy")
                {
                    GameControl.instance.TakeDamage(2, damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.tag == "Player")
                {
                    GameControl.instance.TakeDamage(1, damage);
                    Destroy(gameObject);
                }
            }
        }

    }
}

