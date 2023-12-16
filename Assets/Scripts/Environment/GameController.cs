using UnityEngine;

public class GameController : MonoBehaviour
{
    private PositionController positionController;
    private GameObject[] lightsSources;
    private Tamagochi tamagochi;
    public static bool AllDisabled;

    public void Initialize(GameObject[] lights, PositionController posController, Tamagochi _tamagochi)
    {
        lightsSources = lights;
        positionController = posController;
        tamagochi = _tamagochi;

        if (tamagochi.IsSleeping)
        {
            foreach (var l in lightsSources)
                l.SetActive(false);
        }
    }

    public void Program()
    {
        if (tamagochi.IsDie || AllDisabled) return;

        if (positionController.CurrentPosition == RoomPosition.Programming)
        {
            StartCoroutine(tamagochi.Program());
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Programming);
        }
    }

    public void Feed()
    {
        if (tamagochi.IsDie || AllDisabled) return;

        if (positionController.CurrentPosition != RoomPosition.Kitchen)
        {
            positionController.SwitchPosition(RoomPosition.Kitchen);
        }
    }

    public void Wash()
    {
        if (tamagochi.IsDie || AllDisabled) return;

        if (positionController.CurrentPosition == RoomPosition.Bathroom)
        {
            positionController.SwitchPosition(RoomPosition.Shower);
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Bathroom);
        }
    }

    public void Sleep()
    {
        if (tamagochi.IsDie || AllDisabled) return;

        if (positionController.CurrentPosition == RoomPosition.Bedroom)
        {
            tamagochi.ToggleSleep();

            foreach (var l in lightsSources)
                l.SetActive(!tamagochi.IsSleeping);
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Bedroom);
        }
    }
}
