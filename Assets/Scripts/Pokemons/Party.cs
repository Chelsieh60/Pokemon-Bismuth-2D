using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
   [SerializeField] List<PokemonsMoreStats> pokemons;
    private void Start()
    {
        foreach (var pokemon in pokemons)
        {
            pokemon.Init();
        }
    }
    public PokemonsMoreStats GoodPoke()
    {
       return pokemons.Where(x => x.Hp > 0).FirstOrDefault();
    }
}
