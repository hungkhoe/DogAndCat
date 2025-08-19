using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogAndCat
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl instance;

        [SerializeField] private Camera cam;
        [SerializeField] private UIGamePlay uiGamePlay;
        [SerializeField] private Image imgPowerPlayer;
        [SerializeField] private Image imgPowerEnemy;
        [SerializeField] private Image imgWindLeft;
        [SerializeField] private Image imgWindRight;
        [SerializeField] private GameObject windLeftObj;
        [SerializeField] private GameObject windRightObj;

        [SerializeField] private GameObject powerPlayer;
        [SerializeField] private GameObject powerEnemy;

        [SerializeField] private ObjectThrow weaponPlayer;
        [SerializeField] private ObjectThrow weaponEnemy;
        [SerializeField] private Transform select;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform enemyTransform;
        [SerializeField] private Transform weaponPosPlayer;
        [SerializeField] private Transform weaponPosEnemy;

        [SerializeField] private CreateCharacter catCharacter;
        [SerializeField] private CreateCharacter dogCharacter;
        private AnimationControl playerAnim;
        private AnimationControl enemyAnim;

        internal int turnPlayer;                             //1 is PlayerTurn, 2 is EnemyTurn
        private float forceMax = 25;
        public float force = 0;
        public float aWind = 0;
        private float aWindMax = 5;
        private float speedMultiplyForce = 25;
        private bool isUpForce;
        private float angle = 60;
        private bool isTurnStart;
        private bool isTargetUI;
        private bool isEnd;
        private bool isBomb;
        private bool isPower;
        private bool isX2;
        private UserVersusInfo user;

        private int damage = 10;
        private int recover = 10;
        private ObjectThrow objectThrow;
        private DogCatClass player;
        private DogCatClass enemy;
        private Ray ray;
        private RaycastHit hit;
        private Vector3 mousePosition;

        public enum TypeUser
        { 
            Player = 1,
            Enemy = 2
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            user = new UserVersusInfo();
            user = JsonConvert.DeserializeObject<UserVersusInfo>(PlayerPrefs.GetString(Config.USER_DATA));

            player = new DogCatClass((int)TypeUser.Player);
            enemy = new DogCatClass((int)TypeUser.Enemy);
            turnPlayer = Random.Range(1, 3);
            isTurnStart = true;
            if (turnPlayer == player.idDogCat)
                select.transform.position = playerTransform.position + Vector3.up * 2;
            else
                select.transform.position = enemyTransform.position + Vector3.up * 2;

            playerAnim = playerTransform.GetComponent<AnimationControl>();
            enemyAnim = enemyTransform.GetComponent<AnimationControl>();

            UpdateWindUI();
        }

        private void Start()
        {
            uiGamePlay.abilityPlayer.SetCurrentDogCat(player);
            uiGamePlay.abilityEnemy.SetCurrentDogCat(enemy);
            if (user.playerInfo.form == 1)
            {
                playerAnim.SetSpriteCharacter(catCharacter);
                enemyAnim.SetSpriteCharacter(dogCharacter);
            }
            else
            {
                playerAnim.SetSpriteCharacter(dogCharacter);
                enemyAnim.SetSpriteCharacter(catCharacter);
            }
        }

        private void Update()
        {
            if (!isTurnStart || isEnd)
                return;

            if (Input.GetButtonDown("Fire1"))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    isTargetUI = true;
                    return;
                }
                else
                {
                    isTargetUI = false;
                    force = 0;
                    isUpForce = true;
                    switch (turnPlayer)
                    {
                        case (int)TypeUser.Player:
                            powerPlayer.SetActive(true);
                            break;
                        case (int)TypeUser.Enemy:
                            powerEnemy.SetActive(true);
                            break;
                    }
                }
            }

            if (Input.GetButton("Fire1"))
            {
                if (!isTargetUI)
                {
                    if (isUpForce)
                    {
                        force += speedMultiplyForce * Time.deltaTime;
                        if (force >= forceMax)
                        {
                            force = forceMax;
                            isUpForce = false;
                        }
                    }
                    else
                    {
                        force -= speedMultiplyForce * Time.deltaTime;
                        if (force <= 0)
                        {
                            force = 0;
                            isUpForce = true;
                        }
                    }
                    switch (turnPlayer)
                    {
                        case (int)TypeUser.Player:
                            imgPowerPlayer.fillAmount = force / forceMax;
                            playerAnim.PlayAttack();
                            break;
                        case (int)TypeUser.Enemy:
                            imgPowerEnemy.fillAmount = force / forceMax;
                            enemyAnim.PlayAttack();
                            break;
                    }
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (!isTargetUI)
                {
                    switch (turnPlayer)
                    {
                        case (int)TypeUser.Player:
                            ThrowItem(weaponPosPlayer, (int)TypeUser.Enemy);
                            powerPlayer.SetActive(false);
                            break;
                        case (int)TypeUser.Enemy:
                            ThrowItem(weaponPosEnemy, (int)TypeUser.Player);
                            powerEnemy.SetActive(false);
                            break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetThrow();
            }
        }

        private void OnEndTurn()
        {
            objectThrow.EndTurn -= OnEndTurn;
            isTurnStart = true;
            force = 0;
            isUpForce = true;
            select.gameObject.SetActive(true);
            if (turnPlayer == (int)TypeUser.Player)
            {
                select.transform.position = playerTransform.position + Vector3.up * 2;
                weaponPosEnemy.gameObject.SetActive(true);
            } else
            {
                select.transform.position = enemyTransform.position + Vector3.up * 2;
                weaponPosPlayer.gameObject.SetActive(true);
            }
            UpdateWindUI();
        }

        public void ShowMiss()
        {
            if (turnPlayer == (int)TypeUser.Player)
            {
                select.transform.position = playerTransform.position + Vector3.up * 2;
                playerAnim.PlayFun();

            }
            else
            {
                select.transform.position = enemyTransform.position + Vector3.up * 2;
                enemyAnim.PlayFun();
            }
        }

        //1 = Player , 2 = Enemy
        public void TakeDamage(int _idDogCat, int _damage)
        {
            switch (_idDogCat)
            {
                case (int)TypeUser.Player:
                    if (player.currentHealth > 0)
                    {
                        player.currentHealth -= _damage;
                        uiGamePlay.UpdateHealthPlayer(player.currentHealth, player.maxHealth);
                        playerAnim.PlayHurt();

                        
                        if (player.currentHealth <= 0)
                        { 
                            player.currentHealth = 0;
                            isEnd = true;
                            playerAnim.Dead();
                            StartCoroutine(EndCoroutine(false));
                        }
                    }
                    break;
                case (int)TypeUser.Enemy:
                    if (enemy.currentHealth > 0)
                    {
                        enemy.currentHealth -= damage;
                        uiGamePlay.UpdateHealthEnemy(enemy.currentHealth, enemy.maxHealth);
                        enemyAnim.PlayHurt();
                        
                        if (enemy.currentHealth <= 0)
                        { 
                            enemy.currentHealth = 0;
                            isEnd = true;
                            enemyAnim.Dead();
                            StartCoroutine(EndCoroutine(true));
                        }
                    }
                    break;
            }
        }

        private void ThrowItem(Transform _currentTransform, int _currentTurn)
        {
            _currentTransform.gameObject.SetActive(false);
            if (isBomb)
            {
                objectThrow = Instantiate(weaponEnemy, _currentTransform.position, Quaternion.identity);
                objectThrow.InputData(angle, aWind, force, turnPlayer, 15);
                isBomb = false;
                objectThrow.EndTurn += OnEndTurn;
                turnPlayer = _currentTurn;
            }
            else if (isPower)
            {
                objectThrow = Instantiate(weaponPlayer, _currentTransform.position, Quaternion.identity);
                objectThrow.transform.localScale = new Vector3(2, 2, 2);
                objectThrow.InputData(angle, aWind, force, turnPlayer, 20);
                isPower = false;
                objectThrow.EndTurn += OnEndTurn;
                turnPlayer = _currentTurn;
            }
            else if (isX2)
            {
                StartCoroutine(ThrowX2ItemCoroutine(_currentTransform, _currentTurn));
                isX2 = false;
            }
            else
            {
                objectThrow = Instantiate(weaponPlayer, _currentTransform.position, Quaternion.identity);
                objectThrow.InputData(angle, aWind, force, turnPlayer, 10);
                objectThrow.EndTurn += OnEndTurn;
                turnPlayer = _currentTurn;
            }

            select.gameObject.SetActive(false);
            isTurnStart = false;
            
        }

        private IEnumerator ThrowX2ItemCoroutine(Transform _currentTransform, int _currentTurn)
        {
            objectThrow = Instantiate(weaponPlayer, _currentTransform.position, Quaternion.identity);
            objectThrow.InputData(angle, aWind, force, turnPlayer, 20);
            yield return new WaitForSeconds(1f);
            objectThrow = Instantiate(weaponPlayer, _currentTransform.position, Quaternion.identity);
            objectThrow.InputData(angle, aWind, force, turnPlayer, 20);
            objectThrow.EndTurn += OnEndTurn;
            turnPlayer = _currentTurn;
        }

        public void UseHealthRecover(int _idDogCat)
        {
            switch (_idDogCat)
            {
                case (int)TypeUser.Player:
                    player.currentHealth += recover;
                    if (player.currentHealth >= player.maxHealth)
                        player.currentHealth = 100;

                    uiGamePlay.UpdateHealthPlayer(player.currentHealth, player.maxHealth);
                    
                    break;
                case (int)TypeUser.Enemy:
                    enemy.currentHealth += recover;
                    if (enemy.currentHealth >= enemy.maxHealth)
                        enemy.currentHealth = 100;

                    uiGamePlay.UpdateHealthEnemy(enemy.currentHealth, enemy.maxHealth);
                    break;
            }
        }
        public void UseBomb(int _idDogCat)
        {
            isBomb = true;
        }

        public void UsePower(int _idDogCat)
        {
            isPower = true;
        }

        public void UseX2(int _idDogCat)
        {
            isX2 = true;
        }

        private void UpdateWindUI()
        {
            aWind = Random.Range(-aWindMax, aWindMax + 1);
            if (aWind < 0)
            {
                windLeftObj.SetActive(true);
                windRightObj.SetActive(false);
                imgWindRight.fillAmount = 0;
                imgWindLeft.fillAmount = -aWind / aWindMax;
            }
            else if (aWind > 0)
            {
                windRightObj.SetActive(true);
                windLeftObj.SetActive(false);
                imgWindLeft.fillAmount = 0;
                imgWindRight.fillAmount = aWind / aWindMax;
            }
            else
            {
                windLeftObj.SetActive(false);
                windRightObj.SetActive(false);
                imgWindRight.fillAmount = 0;
                imgWindLeft.fillAmount = 0;
            }
        }

        private IEnumerator EndCoroutine(bool _isWin)
        {
            yield return new WaitForSeconds(1);
            if (_isWin)
                uiGamePlay.ShowWinUI();
            else
                uiGamePlay.ShowLoseUI();
        }

        public void SetThrow()
        {
            switch (turnPlayer)
            {
                case (int)TypeUser.Player:
                    ThrowItem(weaponPosPlayer, (int)TypeUser.Enemy);
                    powerPlayer.SetActive(false);
                    break;
                case (int)TypeUser.Enemy:
                    ThrowItem(weaponPosEnemy, (int)TypeUser.Player);
                    powerEnemy.SetActive(false);
                    break;
            }
        }
    }
}

