using UnityEngine;

public class GameController : MonoBehaviour
{
    private PositionController positionController;
    private GameObject[] lightsSources;
    private Tamagochi tamagochi;

    public void Initialize(GameObject[] lights, PositionController posController, Tamagochi _tamagochi)
    {
        lightsSources = lights;
        positionController = posController;
        tamagochi = _tamagochi;

        if (tamagochi.isSleeping)
        {
            foreach (var l in lightsSources)
                l.SetActive(false);
        }
    }

    public void Program()
    {
        if (tamagochi.IsDie) return;

        if (positionController.CurrentPosition == RoomPosition.Programming)
        {
            tamagochi.Program();
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Programming);
        }
    }

    public void Feed()
    {
        if (tamagochi.IsDie) return;

        if (positionController.CurrentPosition != RoomPosition.Kitchen)
        {
            positionController.SwitchPosition(RoomPosition.Kitchen);
        }
    }

    public void Wash()
    {
        if (tamagochi.IsDie) return;

        if (positionController.CurrentPosition == RoomPosition.Bathroom)
        {
            tamagochi.Wash();
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Bathroom);
        }
    }

    public void Sleep()
    {
        if (tamagochi.IsDie) return;

        if (positionController.CurrentPosition == RoomPosition.Bedroom)
        {
            bool isSleeping = tamagochi.ToggleSleep();

            foreach (var l in lightsSources)
                l.SetActive(!isSleeping);
        }
        else
        {
            positionController.SwitchPosition(RoomPosition.Bedroom);
        }
    }
}
