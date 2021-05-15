using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Hpbar hpbar;

    PokemonsMoreStats _pokemon;

    public void SetData(PokemonsMoreStats pokemons)
    {
        _pokemon = pokemons;
        nameText.text = pokemons._base.Names;
        levelText.text = "Lvl " + pokemons.level;
        hpbar.SetHp((float) pokemons.Hp / pokemons.MaxHealth);
    }
    public IEnumerator HpGauge()
    {
       yield return hpbar.SetHpSmooth((float)_pokemon.Hp / _pokemon.MaxHealth);
    }
}
