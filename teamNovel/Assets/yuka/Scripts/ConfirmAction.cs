using UnityEngine;
//ƒpƒlƒ‹•\Ž¦

public class ConfirmAction : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    void Start()
    {
        confirmPanel.SetActive(false);
    }

    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
    }
}
