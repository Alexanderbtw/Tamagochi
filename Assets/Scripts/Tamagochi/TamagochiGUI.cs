using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TamagochiConstants;

public class TamagochiGUI : MonoBehaviour
{
    public Slider SatietySlider;
    public Slider HygieneSlider;
    public Slider ProgrammingSlider;
    public Slider CheerfulnessSlider;
    public TextMeshProUGUI LevelText;
    public GameObject DiePanel;

    private Tamagochi tamagochi;

    private void Start()
    {
        tamagochi = GetComponent<Tamagochi>();
    }

    void Update()
    {
        CheerfulnessSlider.value = (int)tamagochi.Cheerfulness;
        SatietySlider.value = (int)tamagochi.Satiety;
        HygieneSlider.value = (int)tamagochi.Hygiene;
        ProgrammingSlider.value = (int)tamagochi.Programming;
        LevelText.text = $"Level: {tamagochi.Level}";

        DiePanel.SetActive(tamagochi.IsDie);
    }
}
