using UnityEngine;

public class NoiseManager : MonoBehaviour
{
    [SerializeField]
    private NoiseLayer[] noiseLayers;

    public void NoiseOn()
    {
        foreach (var layer in noiseLayers)
        {
            layer.NoiseOn();
        }
    }

    public void NoiseOff()
    {
        foreach (var layer in noiseLayers)
        {
            layer.NoiseOff();
        }
    }
}