
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPooler : ObjectPooler
{

    public static PathPooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }

}
