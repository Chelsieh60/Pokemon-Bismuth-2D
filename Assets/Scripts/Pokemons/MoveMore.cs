using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMore
{
   public MoveBase Base{ get; set; }
    public int Pp { get; set; }

    public MoveMore(MoveBase pBase)
    {
        Base = pBase;
        Pp = pBase.Pp;
    }
}
