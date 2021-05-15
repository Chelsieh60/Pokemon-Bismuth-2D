using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, ActionChoice, MoveChoice, EnemyMove, Busy}
public class BattleController : MonoBehaviour
{
    [SerializeField] BattlePoke playerPoke;
    [SerializeField] BattlePoke enemyPoke;
    [SerializeField] Battle playerStats;
    [SerializeField] Battle enemyStats;
    [SerializeField] BattleText battlesText;
    int currentACtion;
    int currentMove;

    BattleState state;
    private void Start()
    {
       StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerPoke.SetUp();
        playerStats.SetData(playerPoke.pokemon);
        enemyPoke.SetUp();
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

        yield return battlesText.TypeText($"{playerPoke.pokemon._base.Names} used {move.Base.Name}!");
        
        yield return new WaitForSeconds(1);

        yield return enemyStats.HpGauge();

        bool isFainted = playerPoke.pokemon.TakDmg(move, enemyPoke.pokemon);

        if (isFainted)
        {
            yield return battlesText.TypeText($"{enemyPoke.pokemon._base.Names} fainted!");
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

        yield return battlesText.TypeText($"{enemyPoke.pokemon._base.Names} used {move.Base.Name}!");
        
        yield return new WaitForSeconds(1);

        yield return playerStats.HpGauge();

        bool isFainted = enemyPoke.pokemon.TakDmg(move, playerPoke.pokemon);

        if (isFainted)
        {
            yield return battlesText.TypeText($"{playerPoke.pokemon._base.Names} fainted!");
        }
        else
        {
            ActionChoice();
        }
    }
}
