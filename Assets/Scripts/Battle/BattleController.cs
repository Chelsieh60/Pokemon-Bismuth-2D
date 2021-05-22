using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PokemonsMoreStats;

public enum BattleState { Start, ActionChoice, MoveChoice, EnemyMove, Busy}
public class BattleController : MonoBehaviour
{
    [SerializeField] BattlePoke playerPoke;
    [SerializeField] BattlePoke enemyPoke;
    [SerializeField] Battle playerStats;
    [SerializeField] Battle enemyStats;
    [SerializeField] BattleText battlesText;

    public event Action<bool> OnBattleOver;

    int currentACtion;
    int currentMove;

    BattleState state;

    Party playerParty;
    PokemonsMoreStats wildPokes;
    public void StartBattle(Party playerParty, PokemonsMoreStats wildPokes)
    {
        this.playerParty = playerParty;
        this.wildPokes = wildPokes;
       StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerPoke.SetUp(playerParty);
        playerStats.SetData(playerPoke.pokemon);
        enemyPoke.SetUp(wildPokes);
        enemyStats.SetData(enemyPoke.pokemon);

        battlesText.SetMoveNames(playerPoke.pokemon.Moves);

        yield return StartCoroutine(battlesText.TypeText($"A wild {enemyPoke.pokemon._base.Names} appeared"));
        yield return new WaitForSeconds(1f);

        ActionChoice();
    }

    public void ActionChoice()
    {
        state = BattleState.ActionChoice;
        StartCoroutine(battlesText.TypeText("What will you do?"));
        battlesText.EnabledOptionMoves(true);
    }
    public void MoveChoice()
    {
        state = BattleState.MoveChoice;
        battlesText.EnabledOptionMoves(false);
        battlesText.EnabledBattleText(false);
        battlesText.EnabledMoveText(true);
    }
    public void HandleUpdate()
    {
        if (state == BattleState.ActionChoice)
        {
            HandleActionChoice();
        }
        else if (state == BattleState.MoveChoice)
        {
            HandleMoveChoice();
        }
    }
    void HandleActionChoice()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentACtion < 1)
            {
                ++currentACtion;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentACtion > 0)
            {
                --currentACtion;
            }
        }

        battlesText.UpdateActionChoice(currentACtion);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentACtion == 0)
            {
                MoveChoice();
            }
            else if (currentACtion == 1)
            {

            }
        }
    }
    public void HandleMoveChoice()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerPoke.pokemon.Moves.Count - 1)
            {
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
            {
                --currentMove;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerPoke.pokemon.Moves.Count - 2)
            {
                currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
            {
                currentMove -= 2;
            }
        }

        battlesText.UpdateMoveChoice(currentMove, playerPoke.pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            battlesText.EnabledMoveText(false);
            battlesText.EnabledBattleText(true);
            StartCoroutine(PerformPlayerMove());
         
        }

    }
    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerPoke.pokemon.Moves[currentMove];
        var damageDets = playerPoke.pokemon.TakDmg(move, enemyPoke.pokemon);

        yield return battlesText.TypeText($"{playerPoke.pokemon._base.Names} used {move.Base.Name}!");
        
        yield return new WaitForSeconds(1);

        yield return enemyStats.HpGauge();

        yield return ShowDamageDets(damageDets);
        yield return new WaitForSeconds(1);

        if (damageDets.Fainted)
        {
            yield return battlesText.TypeText($"{enemyPoke.pokemon._base.Names} fainted!");
            yield return new WaitForSeconds(2);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(enemyMove());
        }
    }
    IEnumerator enemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyPoke.pokemon.GetRandMove();
        var damageDets = enemyPoke.pokemon.TakDmg(move, playerPoke.pokemon);

        yield return battlesText.TypeText($"{enemyPoke.pokemon._base.Names} used {move.Base.Name}!");
        
        yield return new WaitForSeconds(1);

        yield return playerStats.HpGauge();

        yield return ShowDamageDets(damageDets);
        yield return new WaitForSeconds(1);

        if (damageDets.Fainted)
        {
            yield return battlesText.TypeText($"{playerPoke.pokemon._base.Names} fainted!");
            yield return new WaitForSeconds(2);
            OnBattleOver(false);
        }
        else
        {
            ActionChoice();
        }
    }
    IEnumerator ShowDamageDets(DamageDets damageDets)
    {
        if (damageDets.Crit > 1)
        {
            yield return battlesText.TypeText("A critical hit!");
            Debug.Log("Critical");
        }
        if (damageDets.TypeEffect < 1)
        {
            yield return battlesText.TypeText("It's super effective!");
            Debug.Log("effective");
        }
        else if (damageDets.TypeEffect > 1)
        {
            yield return battlesText.TypeText("It's not very effective!");
            Debug.Log("not effective");
        }
    }
}
