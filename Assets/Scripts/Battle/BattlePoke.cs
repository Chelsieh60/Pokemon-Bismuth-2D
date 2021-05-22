﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePoke : MonoBehaviour
{
    
    [SerializeField] bool playerPoke;

    public PokemonsMoreStats pokemon { get; set; }

    public void SetUp(PokemonsMoreStats pokemons)
    {
       pokemon = pokemon;
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
