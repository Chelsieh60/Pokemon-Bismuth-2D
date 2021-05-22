using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
   [SerializeField] List<PokemonsMoreStats> Pokemons;
    private void Start()
    {
        foreach (var pokemons in Pokemons)
        {
            pokemons.Init();
        }
    }
    public PokemonsMoreStats GoodPoke()
    {
       return Pokemons.Where(x => x.Hp > 0).FirstOrDefault();
    }
}
