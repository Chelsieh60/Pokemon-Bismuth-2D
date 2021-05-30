using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject TextBox;
    [SerializeField] Text Texting;

    Textman textman;
    int currentline = 0;
    bool isTyping;
    

    public event Action OnShowText;
    public event Action OnCloseText;

    public static TextManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator ShowText(Textman textman)
    {
        yield return new WaitForEndOfFrame();
        this.textman = textman;
        OnShowText?.Invoke();
        TextBox.SetActive(true);
        StartCoroutine(TypeText(textman.lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentline;
            if (currentline < textman.lines.Count)
            {
                StartCoroutine(TypeText(textman.lines[currentline]));
            }
            else
            {
                currentline = 0;
                TextBox.SetActive(false);
                OnCloseText.Invoke();
            }
        }
    }
    public IEnumerator TypeText(string enterText)
    {
        isTyping = true;
        Texting.text = "";
        foreach (var letter in enterText.ToCharArray())
        {
            Texting.text += letter;
            yield return new WaitForSeconds(1 / 5);
        }
        isTyping = false;
    }
}
