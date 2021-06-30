using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTower : MonoBehaviour
{
    public delegate void DragDelegate(DragTower dragObject);
    //public DragDelegate dragEndedCallback;
    //public DragDelegate dragBeginCallback;
    bool isDragged = false;
    Vector3 mouseDragStartPosition;
    Vector3 spriteDragStartPosition;


    private void Awake()
    {
        spriteDragStartPosition = transform.position;
    }

    public void OnMouseDown()
    {
        isDragged = true;
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
        transform.localPosition -= new Vector3(0, 0, 1);
        this.gameObject.GetComponent<CheckMergeController>().enabled = true;
        //dragBeginCallback(this);
    }
    private void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 localPos = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
            transform.localPosition = new Vector3(localPos.x, localPos.y, 1);
            Debug.DrawRay(this.transform.localPosition, new Vector3(0, 0, -1));
        }
    }
    private void OnMouseUp()
    {
        isDragged = false;
        this.gameObject.GetComponent<CheckMergeController>().enabled = false;
        //dragEndedCallback(this);
    }

    public void ResetPosition()
    {
        transform.position = spriteDragStartPosition;
    }
}