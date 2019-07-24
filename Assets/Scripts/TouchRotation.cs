using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotation : MonoBehaviour
{
    private float rotationRateX = 0.5f;
    private float rotationRateY = 0.1f;

    [SerializeField]
    GameObject sun;

    public void Reset()
    {
        transform.localScale = Vector3.one;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Update()
    {
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
                transform.Rotate(touch.deltaPosition.y * rotationRateY,
                                 -touch.deltaPosition.x * rotationRateX, 0, Space.Self);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Touch phase Ended");
            }
        }

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
