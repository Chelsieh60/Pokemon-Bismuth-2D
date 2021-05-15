using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonsMoreStats
{
   public Pokemons _base { get; set; }
   public int level { get; set; }
    public int Hp { get; set; }

    public List<MoveMore> Moves { get; set; }

    public PokemonsMoreStats(Pokemons pbase, int plevel)
    {
        _base = pbase;
        level = plevel;
        Hp = MaxHealth;

        Moves = new List<MoveMore>();
        foreach (var move in _base.Learnables)
        {
            if (move.Level <= level)
            {
                Moves.Add(new MoveMore(move.Base));
            }
            if (Moves.Count > 4)
            {
                break;
            }
        }
    }
    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5;  }

    }
    public int Defense
    {
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }

    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }

    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5; }

    }
    public int Speed
    {
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }

    }
    public int MaxHealth
    {
        get { return Mathf.FloorToInt((_base.MaxHealth * level) / 100f) + 10; }

    }
    public bool TakDmg(MoveMore move, PokemonsMoreStats attacker)
    {
        float modifiers = Random.Range(.85f, 1f);
        float a = (2 * attacker.level + 10) / 250;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        Hp -= damage;
        if(Hp <= 0)
        {
            Hp = 0;
            return true;
        }
        return false;
    }
    public MoveMore GetRandMove()
    {
        int r = Random.Range(1, Moves.Count);
        return Moves[r];
    }
}
