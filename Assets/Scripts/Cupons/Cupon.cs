using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cupon : MonoBehaviour
{
    [SerializeField] private TMP_Text titleUI;
    [SerializeField] private TMP_Text descriptionUI;

    public event Action<string> OnCuponExchanged;

    private int discountAmount;
    
    public void LoadData(CuponData data)
    {
        titleUI.text = data.title;
        descriptionUI.text = data.description;
        discountAmount = data.discountAmount;
    }

    public void ExchangeButtonPressed() => OnCuponExchanged.Invoke(GenerateStringValues());

    private string GenerateStringValues() //Generates a string based on the discount values (to be transformed into a QR)
    {
        Debug.Log("Gets to Cupon");
        return "ASFJSAFSANFISANFSAFSNAFHJSBAFHSABFSHAJFBSAJHFBSAHJFB";
    }
}
