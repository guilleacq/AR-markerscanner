using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARApplicationUI : MonoBehaviour
{
    [SerializeField] private UIView arView;
    [SerializeField] private UIView cuponView;
    [SerializeField] private UIView qrView;

    private UIView currentView;
    private void Awake()
    {
        arView.gameObject.SetActive(false);
        cuponView.gameObject.SetActive(false);
        qrView.gameObject.SetActive(false);
    }
    public void ShowARView() => ShowView(arView);
    public void ShowCuponView() => ShowView(cuponView);
    public void ShowQRView() => ShowView(qrView);

    private void ShowView(UIView view)
    {
        if (currentView == view)
            return;

        currentView?.Hide();
        view.Show();

        currentView = view;
    }
}
