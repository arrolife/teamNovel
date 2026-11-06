using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text valueText;

    void Start()
    {
        // 初期値を表示
        UpdateText(slider.value);

        // Sliderの変更を監視
        slider.onValueChanged.AddListener(UpdateText);
    }

    // Sliderの値をテキストに反映
    public void UpdateText(float value)
    {
        int displayValue = Mathf.RoundToInt(value * 10);

        valueText.text = displayValue.ToString();
    }

    void OnDestroy()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(UpdateText);
    }
}