
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPooler : ObjectPooler
{

    public static CoinPooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }

}