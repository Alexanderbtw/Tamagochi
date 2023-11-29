using UnityEngine;
using static TamagochiConstants;


public class Tamagochi : MonoBehaviour
{
    [SerializeField]
    private GameObject body;

    public static float Satiety { get; set; } = 100f;
    public static float Hygiene { get; set; } = 100f;
    public static float Programming { get; set; } = 100f;
    public static float Cheerfulness { get; set; } = 100f;

    private static bool IsSleeping { get; set; } = false;

    private static GameObject mainLight;

    void Start()
    {
        mainLight = GameObject.FindGameObjectWithTag("MainLight");
    }

    void Update()
    {
        Satiety -= SATIETY_DIMINISH_KOEF * Time.deltaTime;
        Hygiene -= HYGIENE_DIMINISH_KOEF * Time.deltaTime;
        Programming -= PROGRAMMING_DIMINISH_KOEF * Time.deltaTime;
        if (!IsSleeping)
            Cheerfulness -= CHEERFULNESS_DIMINISH_KOEF * Time.deltaTime;
        else
            Cheerfulness += CHEERFULNESS_ADD_KOEF * Time.deltaTime;
    }

    public static void Feed()
    {
        if (!IsSleeping)
        {
            Satiety += SATIETY_ADD;
            Satiety = Satiety > 100 ? 100 : Satiety;
        }
    }

    public static void Program()
    {
        if (!IsSleeping)
        {
            Programming += PROGRAMMING_ADD;
            Programming = Programming > 100 ? 100 : Programming;
        }
    }

    public static void Wash()
    {
        if (!IsSleeping)
        {
            Hygiene += HYGIENE_ADD;
            Hygiene = Hygiene > 100 ? 100 : Hygiene;
        }
    }

    public bool ToggleSleep()
    {
        IsSleeping = !IsSleeping;
        body.SetActive(!IsSleeping);
        return IsSleeping;
    }
}
