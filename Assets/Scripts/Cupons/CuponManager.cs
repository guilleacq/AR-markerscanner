using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuponManager : MonoBehaviour
{
    [SerializeField] private GameObject cuponObj;
    private CuponReward[] rewards;

    [SerializeField] private GameObject cuponContainer;
    private List<Cupon> cupons;

    public event Action<string> OnExchange;

    public CuponData TEMPDATA; //REMOVE THIS
    private void Start()
    {
        rewards = FindObjectsOfType<CuponReward>(true);
        cupons = new List<Cupon>();

        foreach (var reward in rewards)
        {
            reward.OnRewardScanned += RewardScanned;
        }
    }

    private void RewardScanned(CuponData data) => AddCupon(data);

    public void AddCupon(CuponData data)
    {
        Cupon cupon = Instantiate(cuponObj, cuponContainer.transform).GetComponent<Cupon>();
        if (cupon != null)
        {
            cupon.LoadData(data);
            cupons.Add(cupon);
            cupon.OnCuponExchanged += OnCuponExchanged;
        }
    }

    public void OnCuponExchanged(string text)
    {
        OnExchange.Invoke(text);
    }

    private void OnDisable()
    {
        if (rewards != null)
        {
            foreach (var reward in rewards)
                reward.OnRewardScanned -= RewardScanned;
        }

        if (cupons != null)
            foreach (var cupon in cupons)
                cupon.OnCuponExchanged -= OnCuponExchanged;
    }


    [ContextMenu("Add cupon")]
    public void ADDRANDOMCUPON() => AddCupon(TEMPDATA); //TEMP. REMOVE THIS PLS
}
