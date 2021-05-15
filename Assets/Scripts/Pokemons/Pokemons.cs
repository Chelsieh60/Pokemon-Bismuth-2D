using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class Pokemons : ScriptableObject
{
    //name of pokemon
   [SerializeField] string names;

    //description of pokemon
    [TextArea]
    [SerializeField] string description;

    //direction of pokemon
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;

    //type of pokemon (can have 1 or 2)
    [SerializeField] Types firstType;
    [SerializeField] Types secondType;

    //stats
    [SerializeField] int maxHealth;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<Learnable> learnables;

    public Sprite Front
    {
        get { return front; }
    }
    public Sprite Back
    {
        get { return back; }
    }
    public string Names
    {
        get { return names;  }
    }
    public string Description
    {
        get { return description; }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int SpAttack
    {
        get { return spAttack; }
    }
    public int SpDefense
    {
        get { return spDefense; }
    }
    public int Speed
    {
        get { return speed; }
    }
    public List<Learnable> Learnables
    {
        get { return learnables; }
    }


}
[System.Serializable]
public class Learnable{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }
    public int Level
    {
        get { return level; }
    }
    
}
public enum Types
{
    Normal,
Fire,
Water,
Grass,
Electric,
Ice,
Fighting,
Poison,
Ground,
Flying,
Psychic,
Bug,
Rock,
Ghost,
Dark,
Dragon,
Steel,
Fairy,
}
