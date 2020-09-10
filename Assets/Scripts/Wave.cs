using UnityEngine;

[System.Serializable]
public class Wave {

    // class for making an wave of enemies

    [Header("Difficult")]
    public string Difficulty;

    [Header("Zombie")]
    public int NumOFZombies;
    public int ZombieHealth = 100;
    public int ZombieDamage = 15;

    [Header("Beast")]
    public int NumOFBeast;
    public int BeastHealth = 100;
    public int BeastDamage = 13;



}
