using UnityEngine;
using System.Collections;

public sealed class InputSystem : MonoBehaviour
{
    static Vector2 startPosition = Vector2.zero;
    static Vector2 TouchInput()
    {
        foreach (Touch currentTouch in Input.touches)
        {
            switch (currentTouch.phase)
            {
                case TouchPhase.Began:
                    startPosition = Camera.main.ScreenToWorldPoint(currentTouch.position);
                    break;
                case TouchPhase.Moved:
                    Vector2 touchToWorld = (Vector2)Camera.main.ScreenToWorldPoint(currentTouch.position);
                    return touchToWorld - startPosition;
            }
        }
        return startPosition;
    }

    public static float GetAxis(Axis axis)
    {
        Vector2 movedFor = TouchInput();
        if (axis == Axis.Horizontal)
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }

    public enum Axis { Horizontal, Vertical };

}