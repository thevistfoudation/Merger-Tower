using UnityEngine;

[DisallowMultipleComponent]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    public float Speed
    {
        set
        {
            speed = value;
        }
        get
        {
            return speed;
        }
    }

    bool isStop = false;

    public bool Stop
    {
        set
        {
            isStop = value;
        }
    }

    public virtual void Move(Vector3 direction)
    {
        if (isStop) return;
        transform.position += direction * speed*Time.fixedDeltaTime;
    }

}
