using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TamagochiConstants;

public class GameController : MonoBehaviour
{
    public PositionController PosController;
    private GameObject[] lightsSources;
    public Tamagochi TamagochiScript;

    public void Awake()
    {
        lightsSources = GameObject.FindGameObjectsWithTag("MainLight");
    }

    public void Program()
    {
        if (PositionController.CurrentPosition == RoomPosition.Programming)
        {
            Tamagochi.Program();
        }
        else
        {
            PosController.SwitchPosition(RoomPosition.Programming);
        }
    }

    public void Feed()
    {
        if (PositionController.CurrentPosition == RoomPosition.Kitchen)
        {
            Tamagochi.Feed();
        }
        else
        {
            PosController.SwitchPosition(RoomPosition.Kitchen);
        }
    }

    public void Wash()
    {
        if (PositionController.CurrentPosition == RoomPosition.Bathroom)
        {
            Tamagochi.Wash();
        }
        else
        {
            PosController.SwitchPosition(RoomPosition.Bathroom);
        }
    }

    public void Sleep()
    {
        if (PositionController.CurrentPosition == RoomPosition.Bedroom)
        {
            bool isSleeping = TamagochiScript.ToggleSleep();

            foreach (var l in lightsSources)
                l.SetActive(!isSleeping);
        }
        else
        {
            PosController.SwitchPosition(RoomPosition.Bedroom);
        }
    }
}
