using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TouchRotation : MonoBehaviour
{
    private float rotationRateX = 0.5f;
    private float rotationRateY = 0.1f;

    Vector2 prevMousePos = Vector2.zero;
    bool dragging = false;

    private void DoRotation(float delX, float delY)
    {
        transform.Rotate(delY * rotationRateY,
                                -delX * rotationRateX, 0, Space.Self);
    }
    
    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("Touch phase Moved");
               DoRotation(touch.deltaPosition.x, touch.deltaPosition.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Touch phase Ended");
            }
        }
#else
        if (Input.GetMouseButtonDown(0)){
            prevMousePos = Input.mousePosition;
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
        if (dragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 delta = mousePos - prevMousePos;
            prevMousePos = mousePos;
            DoRotation(delta.x, delta.y);
        }
#endif

        /*if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            float scale = (deltaMagnitudeDiff * 0.05f);
            transform.localScale += Vector3.one * scale;
        }*/
    }
}
