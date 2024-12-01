using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCodeGenerator : MonoBehaviour
{
    private const int TEXTURE_SIZE = 256;

    [SerializeField] private RawImage rawImageReceiver;
    private Texture2D encodedTexture;
    void Start()
    {
        encodedTexture = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE);
    }

    private Color32[] Encode(string textToEncode, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textToEncode);
    }

    public void GenerateQR(string textToEncode)
    {
        if (string.IsNullOrEmpty(textToEncode))
            return;

        Color32[] convertPixelToTexture = Encode(textToEncode, encodedTexture.width, encodedTexture.height);
        encodedTexture.SetPixels32(convertPixelToTexture);
        encodedTexture.Apply();

        rawImageReceiver.texture = encodedTexture;

    }
}
