using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text distanceText;

    public static UI UIManager { get; private set; }

    private void Awake()
    {
        if (UIManager == null)
        {
            UIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
