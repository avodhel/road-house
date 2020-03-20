
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarPooler : ObjectPooler
{

    public static AICarPooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }

}
