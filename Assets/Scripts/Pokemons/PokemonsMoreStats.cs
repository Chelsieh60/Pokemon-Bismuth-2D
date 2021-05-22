using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PokemonsMoreStats
{
    [SerializeField] Pokemons _Base;
    [SerializeField] int Levels;

   public Pokemons _base
    {
        get
        {
            return _Base;
        }
    }
   public int level {
        get {
            return Levels;
        } 
    }
    public int Hp { get; set; }

    public List<MoveMore> Moves { get; set; }

    public void Init()
    {
        //_base = pbase;
        //level = plevel;
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
    public DamageDets TakDmg(MoveMore move, PokemonsMoreStats attacker)
    {
        float crit = 1;
        if (Random.value * 100 <= 6.25f)
            crit = 2;

        float type = TypeChart.TypeEffectivness(move.Base.Type, this._base.FirstType) * TypeChart.TypeEffectivness(move.Base.Type, this._base.SecondType);

        float modifiers = Random.Range(.85f, 1f) * type * crit;
        float a = (2 * attacker.level + 10) / 250;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

       var damageDets = new DamageDets()
        {
            TypeEffect = type,
            Crit = crit,
            Fainted = false

        };

    Hp -= damage;
        if(Hp <= 0)
        {
            Hp = 0;
            damageDets.Fainted = true;
        }
        return damageDets;
    }



    public MoveMore GetRandMove()
    {
        int r = Random.Range(1, Moves.Count);
        return Moves[r];
    }
    public class DamageDets
    {
        public bool Fainted { get; set; }
        public float Crit { get; set; }
        public float TypeEffect { get; set; }
    }

}
