using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject TextBox;
    [SerializeField] Text Texting;

    public void ShowText(Textman textman)
    {
        TextBox.SetActive(true);
        StartCoroutine(TypeText(textman.lines[0]));
    }
    public IEnumerator TypeText(string enterText)
    {
        Texting.text = "";
        foreach (var letter in enterText.ToCharArray())
        {
            Texting.text += letter;
            yield return new WaitForSeconds(1 / 5);
        }
    }
}
