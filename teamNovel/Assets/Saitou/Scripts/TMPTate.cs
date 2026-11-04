using UnityEngine;
using TMPro;

public class Vertical : MonoBehaviour
{
    private TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }
}