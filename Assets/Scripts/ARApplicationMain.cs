using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ARApplicationMain : MonoBehaviour
{
    public enum AppMode {AR, Cupon, QR};
    private AppMode currentMode;

    [SerializeField] private GameObject[] ARObjects;
    [SerializeField] private Camera UICam;

    [SerializeField] private ARApplicationUI applicationUI;
    private CuponManager cuponManager;

    private QRCodeGenerator qrGenerator;

    private void Start()
    {
        qrGenerator = GetComponent<QRCodeGenerator>();
        cuponManager = GetComponent<CuponManager>();

        cuponManager.OnExchange += GenerateQR;
        SetMode(AppMode.AR);
    }

    public void SetMode(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.AR:
                applicationUI.ShowARView();
                SetComponentsActive(ARObjects, true);

                UICam.enabled = false;
                currentMode = mode;
                break;

            case AppMode.Cupon:
                applicationUI.ShowCuponView();
                SetComponentsActive(ARObjects, false);

                UICam.enabled = true;
                currentMode = mode;
                break;

            case AppMode.QR:
                if (currentMode != AppMode.Cupon)
                    break;

                applicationUI.ShowQRView();
                SetComponentsActive(ARObjects, false);

                UICam.enabled = true;
                currentMode = mode;
                break;
        }
    }

    private void SetComponentsActive(GameObject[] objects, bool isActive)
    {
        foreach (var obj in objects)
            obj.SetActive(isActive);
    }

    private void GenerateQR(string text)
    {
        SetMode(AppMode.QR);
        qrGenerator.GenerateQR(text);
    }

    public void SwitchToCupon() => SetMode(AppMode.Cupon);
    public void SwitchToCam() => SetMode(AppMode.AR);

}
