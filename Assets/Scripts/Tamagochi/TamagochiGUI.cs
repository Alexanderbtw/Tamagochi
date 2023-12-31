using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TamagochiGUI : MonoBehaviour
{
    public Slider SatietySlider;
    public Slider HygieneSlider;
    public Slider ProgrammingSlider;
    public Slider CheerfulnessSlider;
    public TextMeshProUGUI LevelText;
    public GameObject RestartButton;

    private Tamagochi tamagochi;

    public void Initialize(Tamagochi _tamagochi)
    {
        tamagochi = _tamagochi;
        tamagochi.StatsChanged += StatsDisplay;
    }

    private void StatsDisplay()
    {
        CheerfulnessSlider.value = (int)tamagochi.Cheerfulness;
        SatietySlider.value = (int)tamagochi.Satiety;
        HygieneSlider.value = (int)tamagochi.Hygiene;
        ProgrammingSlider.value = (int)tamagochi.Programming;

        LevelText.text = $"Level: {tamagochi.Level}";

        if (tamagochi.IsDie)
        {
            LevelText.color = Color.red;
            LevelText.text = $"Wasted";
            RestartButton.SetActive(true);
        }
    }
}
