using UnityEngine;

public class GameController : MonoBehaviour, IDataPersistence
{
    private PositionController posController;
    private GameObject[] lightsSources;
    public Tamagochi tamagochi;

    public void Awake()
    {
        lightsSources = GameObject.FindGameObjectsWithTag("MainLight");
        posController = GetComponent<PositionController>();
    }

    public void Program()
    {
        if (tamagochi.IsDie) return;

        if (posController.CurrentPosition == RoomPosition.Programming)
        {
            tamagochi.Program();
        }
        else
        {
            posController.SwitchPosition(RoomPosition.Programming);
        }
    }

    public void Feed()
    {
        if (tamagochi.IsDie) return;

        if (posController.CurrentPosition == RoomPosition.Kitchen)
        {
            tamagochi.Feed();
        }
        else
        {
            posController.SwitchPosition(RoomPosition.Kitchen);
        }
    }

    public void Wash()
    {
        if (tamagochi.IsDie) return;

        if (posController.CurrentPosition == RoomPosition.Bathroom)
        {
            tamagochi.Wash();
        }
        else
        {
            posController.SwitchPosition(RoomPosition.Bathroom);
        }
    }

    public void Sleep()
    {
        if (tamagochi.IsDie) return;

        if (posController.CurrentPosition == RoomPosition.Bedroom)
        {
            bool isSleeping = tamagochi.ToggleSleep();

            foreach (var l in lightsSources)
                l.SetActive(!isSleeping);
        }
        else
        {
            posController.SwitchPosition(RoomPosition.Bedroom);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.IsSleeping)
        {
            foreach (var l in lightsSources)
                l.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) { }
}
