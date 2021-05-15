using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleText : MonoBehaviour
{
    [SerializeField] Text battleText;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    [SerializeField] GameObject moveText;
    [SerializeField] GameObject options;
    [SerializeField] GameObject details;

    [SerializeField] List<Text> optionMoves;
    [SerializeField] List<Text> actionMoves;

    [SerializeField] Color textColor;

    public void SetbattleText(string enterText)
    {
        battleText.text = enterText;
    }
    public IEnumerator TypeText(string enterText)
    {
        battleText.text = "";
        foreach (var letter in enterText.ToCharArray())
        {
            battleText.text += letter;
            yield return new WaitForSeconds(1/5);
        }
    }
    public void EnabledBattleText(bool enabled)
    {
        battleText.enabled = enabled;
    }
    public void EnabledOptionMoves(bool enabled)
    {
        options.SetActive(enabled);
    }
    public void EnabledMoveText(bool enabled)
    {
        moveText.SetActive(enabled);
        details.SetActive(enabled);
    }
    public void UpdateActionChoice(int selectedAction)
    {
        for (int i = 0; i < optionMoves.Count; ++i)
        {
            if (i == selectedAction && i == 0)
            {
                optionMoves[i].color = Color.red;
            }
            else if (i == selectedAction && i == 1)
            {
                optionMoves[i].color = Color.blue;
            }
            else
            {
                optionMoves[i].color = Color.black;
            }
        }
    }
    public void UpdateMoveChoice(int selectedMove, MoveMore move)
    {
        for (int i = 0; i < actionMoves.Count; ++i)
        {
            if (i == selectedMove)
            {
                actionMoves[i].color = Color.red;
            }
            else
            {
                actionMoves[i].color = Color.black;
            }
        }
        ppText.text = $"PP {move.Pp} / {move.Base.Pp}";
        typeText.text = move.Base.Type.ToString();
    }
    public void SetMoveNames(List<MoveMore> moves)
    {
        for (int i = 0; i < actionMoves.Count; ++i)
        {
            if (i < moves.Count)
            {
                actionMoves[i].text = moves[i].Base.Name;
            }
            else
            {
                actionMoves[i].text = "---";
            }
        }
    }
}

