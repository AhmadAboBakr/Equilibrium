using UnityEngine;
using System.Collections;

public enum Direction
{
    ClockWise = 0, AntiClockWise = 1
}
public struct CircularMotion
{
    public Direction direction;
    public float speed;
    public CircularMotion(Direction direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }
}
