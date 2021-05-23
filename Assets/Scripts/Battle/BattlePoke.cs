using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePoke : MonoBehaviour
{
    
    [SerializeField] bool playerPoke;

    public PokemonsMoreStats pokemon { get; set; }

    public void SetUp(PokemonsMoreStats pokemonsimage)
    {
       pokemon = pokemonsimage;
        if (playerPoke)
        {
            GetComponent<Image>().sprite = pokemonsimage._base.Back;
        }
        else if (!playerPoke) 
        {
            GetComponent<Image>().sprite = pokemonsimage._base.Front;
        }
    }
}
