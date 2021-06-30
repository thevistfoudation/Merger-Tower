using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMoveController : MoveController
{
    float height;

    Vector3 directionY;

    float speedY = 0;

    public float Height
    {
        set
        {
            height = value;
        }
    }

    public float SpeedY
    {
        get
        {
            return speedY;
        }
    }


    public void SetSpeedY(float time)
    {
        speedY = time * height;
    }
    public override void Move(Vector3 direction)
    {
        speedY -= height;
        directionY = Vector3.up * speedY;
        direction += directionY;
        base.Move(direction);
        transform.up = direction;
    }
}
