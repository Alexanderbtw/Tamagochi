using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    // Tamagochi 
    public int Level = 0;
    public long QuitTime = DateTime.UtcNow.ToFileTimeUtc();
    public long LastLevelUpTime = DateTime.UtcNow.ToFileTimeUtc();
    public float Cheerfulness = 100f;
    public float Satiety = 100f;
    public float Programming = 100f;
    public float Hygiene = 100f;
    public AgeState State = AgeState.Child;
    public bool IsSleeping = false;

    // PositionController
    public RoomPosition CurrentPosition = RoomPosition.Programming;
}
