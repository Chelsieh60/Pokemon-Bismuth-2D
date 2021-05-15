using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    //name of pokemon
    [SerializeField] string names;

    //description of pokemon
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Types type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;
    public string Name
    {
        get { return names; }
    }
    public string Description
    {
        get { return description; }
    }
    public Types Type
    {
        get { return type; }
    }
    public int Power
    {
        get { return power; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }
    public int Pp
    {
        get { return pp; }
    }
}
