using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfo
{
    public int playerhealth;
    public int playerMaxhealth;
    public int playerLevel;
    public bool doubleJumpUnlocked;
    public bool dashUnlocked;
    public int attackIncreased;
    public int coins;
    public int rubies;
    public int currentLevel;
    public int[] levelRange;


    public GameInfo(int newPlayerhealth, int newCoins, int newRubies, int newCurrentLevel, int newPlayerMaxHealth, int newPlayerLevel, bool newDoubleJumpUnlocked, bool newDashUnlocked, int newAttackIncreased, int[] newLevelRange)
    {
        playerhealth = newPlayerhealth;
        coins = newCoins;
        rubies = newRubies;
        currentLevel = newCurrentLevel;
        playerMaxhealth = newPlayerMaxHealth;
        playerLevel = newPlayerLevel;
        doubleJumpUnlocked = newDoubleJumpUnlocked;
        dashUnlocked = newDashUnlocked;
        attackIncreased = newAttackIncreased;
        levelRange = newLevelRange;
    }
}
