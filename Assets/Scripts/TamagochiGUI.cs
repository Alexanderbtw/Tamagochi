using UnityEngine;
using UnityEngine.UI;
using static TamagochiConstants;

public class TamagochiGUI : MonoBehaviour
{
    public Slider SatietySlider;
    public Slider HygieneSlider;
    public Slider ProgrammingSlider;
    public Slider CheerfulnessSlider;

    void Update()
    {
        CheerfulnessSlider.value = (int)Tamagochi.Cheerfulness;
        SatietySlider.value = (int)Tamagochi.Satiety;
        HygieneSlider.value = (int)Tamagochi.Hygiene;
        ProgrammingSlider.value = (int)Tamagochi.Programming;
    }
}
