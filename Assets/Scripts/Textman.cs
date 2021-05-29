using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Textman
{
    [SerializeField] List<string> Lines;

    public List<string> lines
    {
        get { return Lines; }
    }
}
