using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cupon", menuName = "Cupon")]
public class CuponData : ScriptableObject
{
    public string title;
    [Tooltip("Number of discount in %")]
    public int discountAmount; 
    public string description;
}
