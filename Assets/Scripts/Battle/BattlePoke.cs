using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePoke : MonoBehaviour
{
    [SerializeField] Pokemons _base;
    [SerializeField] int level;
    [SerializeField] bool playerPoke;

    public PokemonsMoreStats pokemon { get; set; }

    public void SetUp()
    {
       pokemon = new PokemonsMoreStats(_base, level);
        if (playerPoke)
        {
            GetComponent<Image>().sprite = pokemon._base.Back;
        }
        else
        {
            GetComponent<Image>().sprite = pokemon._base.Front;
        }
    }
}
