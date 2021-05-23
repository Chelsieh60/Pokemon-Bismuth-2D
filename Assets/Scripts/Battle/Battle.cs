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

    public void SetData(PokemonsMoreStats pokemonsdata)
    {
        _pokemon = pokemonsdata;
        nameText.text = pokemonsdata._base.Names;
        levelText.text = "Lvl " + pokemonsdata.level;
        hpbar.SetHp((float) pokemonsdata.Hp / pokemonsdata.MaxHealth);
    }
    public IEnumerator HpGauge()
    {
       yield return hpbar.SetHpSmooth((float)_pokemon.Hp / _pokemon.MaxHealth);
    }
}
