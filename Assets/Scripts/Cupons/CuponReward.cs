using System;
using UnityEngine;

public class CuponReward : MonoBehaviour
{
    [SerializeField] private CuponData rewardData;

    public event Action<CuponData> OnRewardScanned;
    private ImageModel imageModel;
    private void Start()
    {
        imageModel = GetComponent<ImageModel>();

        if (imageModel != null)
            imageModel.OnFirstTracked += AddCupon;
    }
    private void OnDestroy()
    {
        imageModel.OnFirstTracked -= AddCupon;
    }

    private void AddCupon()
    {
        OnRewardScanned?.Invoke(rewardData);
    }
}
