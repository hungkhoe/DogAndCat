using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogAndCat
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class CreateCharacter : ScriptableObject
    {
        public Sprite headNormal;
        public Sprite headHurt;
        public Sprite headFun;
        public Sprite headAttack;
        public Sprite body;
        public Sprite rightHand;
        public Sprite leftHand;
        public Sprite leg;
        public Sprite weapon;
        public Sprite dead;
    }
}

