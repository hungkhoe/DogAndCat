using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DogCatClass
{
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    public int recoverHealth { get; set; }
    public int bomb { get; set; }
    public int power { get; set; }
    public int x2 { get; set; }

    public int idDogCat { get; set; }           //1 : Player, 2 : Enemy

    public DogCatClass(int _idDogCat)
    {
        maxHealth = 100;
        currentHealth = 100;
        recoverHealth = 1;
        bomb = 1;
        power = 1;
        x2 = 1;
        idDogCat = _idDogCat;
    }
        
}

[System.Serializable]
public class UserInfoData
{
    [JsonProperty("animal")]
    public int form { get; set; }

    [JsonProperty("id")]
    public int idAnimal { get; set; }
    
}

[System.Serializable]
public class UserVersusInfo
{
    [JsonProperty("player")]
    public UserInfoData playerInfo { get; set; }

    [JsonProperty("enemy")]
    public UserInfoData enemyInfo { get; set; }
    public UserVersusInfo()
    {
        playerInfo = new UserInfoData();
        enemyInfo = new UserInfoData();
    }
}

public class Config
{
    public const string USER_DATA = "USER_DATA";
}

