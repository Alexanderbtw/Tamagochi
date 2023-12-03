using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class PositionController : MonoBehaviour
{
    private static Dictionary<RoomPosition, int> position_to_index = new()
    {
        { RoomPosition.Programming, 0 },
        { RoomPosition.Kitchen, 1 },
        { RoomPosition.Bathroom, 2 },
        { RoomPosition.Bedroom, 3 },
    };

    [SerializeField]
    private CinemachineVirtualCamera[] cameras;
    [SerializeField]
    private Transform[] positionPoints;

    public Tamagochi tamagochi;

    [HideInInspector]
    public RoomPosition CurrentPosition { get; private set; }
    
    public void SwitchPosition(RoomPosition position)
    {
        if (CurrentPosition == position) return;

        int index = position_to_index[position];
        int cur_index = position_to_index[CurrentPosition];

        cameras[cur_index].Priority = 0;
        cameras[index].Priority = 1;
        CurrentPosition = position;

        tamagochi.CurrBody.transform.position = positionPoints[index].position;
    }
}

public enum RoomPosition
{
    Programming,
    Kitchen,
    Bathroom,
    Bedroom
}