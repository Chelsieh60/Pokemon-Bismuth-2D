using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] List<PokemonsMoreStats> Wildpokes;

    public PokemonsMoreStats GetWildPokes()
    {
        var Wildpoke = Wildpokes[Random.Range(0, Wildpokes.Count)];
        Wildpoke.Init();
        return Wildpoke;
            }
}
