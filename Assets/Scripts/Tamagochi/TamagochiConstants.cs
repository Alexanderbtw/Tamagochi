using System;

public static class TamagochiConstants
{
    public static readonly float PROGRAMMING_DIMINISH_PER_MINUTE = 0.18f;
    public static readonly float HYGIENE_DIMINISH_PER_MINUTE = 0.13f;
    public static readonly float SATIETY_DIMINISH_PER_MINUTE = 0.2f;
    public static readonly float CHEERFULNESS_DIMINISH_PER_MINUTE = 0.1f;

    public static readonly float PROGRAMMING_ADD = 10f;
    public static readonly float HYGIENE_ADD = 1f;
    public static readonly float SATIETY_ADD = 10f;
    public static readonly float CHEERFULNESS_ADD_PER_MINUTE = 0.2f;

    public static readonly TimeSpan TIME_TO_LEVELUP = TimeSpan.FromDays(1);
    public static readonly int TEENAGER_LEVEL_CUP = 20;
    public static readonly int ADULT_LEVEL_CUP = 40;
}

public enum AgeState
{
    Child,
    Teenager,
    Adult
}