using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject TextBox;
    [SerializeField] Text Texting;

    public event Action OnShowText;
    public event Action OnCloseText;

    public static TextManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void ShowText(Textman textman)
    {
        OnShowText?.Invoke();
        TextBox.SetActive(true);
        StartCoroutine(TypeText(textman.lines[0]));
    }

    public void HandleUpdate()
    {

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
