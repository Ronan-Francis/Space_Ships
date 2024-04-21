using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoyStick : MonoBehaviour
{
    public RectTransform joystick;
    public RectTransform joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = joystickBG.anchoredPosition;
        joystickRadius = joystickBG.sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.parent as RectTransform, Input.mousePosition, null, out localPoint);
        joystick.anchoredPosition = localPoint;
        joystickBG.anchoredPosition = localPoint;
        joystickTouchPos = localPoint;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.parent as RectTransform, pointerEventData.position, null, out localPoint);
        Vector2 dragPos = localPoint;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
        if(joystickDist < joystickRadius)
        {
            joystick.anchoredPosition = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.anchoredPosition = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.anchoredPosition = joystickOriginalPos;
        joystickBG.anchoredPosition = joystickOriginalPos;
    }
}
