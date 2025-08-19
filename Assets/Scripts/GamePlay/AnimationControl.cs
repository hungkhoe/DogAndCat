using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogAndCat
{
    public class AnimationControl : MonoBehaviour
    {
        private Animator anim;
        private int hashFun;
        private int hashHurt;
        private int hashNormal;
        private int hashAttack;
        private CreateCharacter spriteCharacter;

        [SerializeField] private SpriteRenderer head;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            hashFun = Animator.StringToHash("Miss");
            hashHurt = Animator.StringToHash("Hurt");
            hashNormal = Animator.StringToHash("Normal");
            hashAttack = Animator.StringToHash("Attack");      
        }

        public void PlayFun()
        {
            head.sprite = spriteCharacter.headFun;
            anim.Play(hashFun, 0, 0);
            StartCoroutine(ChangeNormalAnim(1));
        }

        public void PlayHurt()
        {
            head.sprite = spriteCharacter.headHurt;
            anim.Play(hashHurt, 0, 0);
            StartCoroutine(ChangeNormalAnim(1));
        }

        public void PlayAttack()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                head.sprite = spriteCharacter.headAttack;
                anim.Play(hashAttack, 0, 0);
                StartCoroutine(ChangeNormalAnim(1));
            }
        }

        public void PlayNormal()
        {
            head.sprite = spriteCharacter.headNormal;
            anim.Play(hashNormal, 0, 0);
        }

        public void Dead()
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Dead" || child.name == "Shadow")
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }

        public void SetSpriteCharacter(CreateCharacter _char)
        {
            spriteCharacter = _char;
            transform.Find("Head").GetComponent<SpriteRenderer>().sprite = spriteCharacter.headNormal;
            transform.Find("Body").GetComponent<SpriteRenderer>().sprite = spriteCharacter.body;
            transform.Find("HandRight").GetComponent<SpriteRenderer>().sprite = spriteCharacter.rightHand;
            transform.Find("HandLeft").GetComponent<SpriteRenderer>().sprite = spriteCharacter.leftHand;
            transform.Find("Leg").GetComponent<SpriteRenderer>().sprite = spriteCharacter.leg;
            transform.Find("Dead").GetComponent<SpriteRenderer>().sprite = spriteCharacter.dead;
        }

        private IEnumerator ChangeNormalAnim(float _time)
        {
            yield return new WaitForSeconds(_time);
            PlayNormal();
        }
    }
}

