using System;
[Flags]
[Serializable]
public enum PlatformType
{
    None = 0,
    TurnLeft = 1 << 1,
    TurnRight = 1 << 2,
    GoesUp = 1 << 3,
    GoesDown = 1 << 4,
    SpecialPlatform = 1 << 5,
    Straight = 1 << 6,
    NonStaticLanesDistance = 1 << 7,
    SpiralLeft = 1 << 8,
    SpiralRight = 1 << 9,
    NonStaticLanesNumber = 1 << 10,
    All = int.MaxValue,
}