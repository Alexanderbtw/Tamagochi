using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;
using System;

public class PositionController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private Transform[] positionPoints;
    private Tamagochi tamagochi;
    [HideInInspector] public RoomPosition CurrentPosition { get; private set; }

    public void Initialize(Tamagochi _tamagochi)
    {
        tamagochi = _tamagochi;
    }
    
    public void SwitchPosition(RoomPosition position)
    {
        if (CurrentPosition == position) return;

        int index = Convert.ToInt32(position);
        int cur_index = Convert.ToInt32(CurrentPosition);

        cameras[cur_index].Priority = 0;
        cameras[index].Priority = 1;
        CurrentPosition = position;

        tamagochi.CurrBody.transform.SetPositionAndRotation(positionPoints[index].position, positionPoints[index].rotation); 
    }
}

public enum RoomPosition
{
    Programming,
    Kitchen,
    Bathroom,
    Bedroom,
    Shower,
    Bed
}