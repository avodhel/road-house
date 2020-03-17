using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data", menuName = "Scriptable Objects/Car Data", order = 1)]
public class CarData : ScriptableObject
{
    public int Id;
    public CarModel carModel;
    public int price;
    public bool unlocked;
    public bool selected;
}