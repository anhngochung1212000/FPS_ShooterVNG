using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEditor;
public class OnRotateCamera : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 ogrinPos;
    private Vector2 ogrinPosMouse;
    public static Vector3 deltaMouse;
    public static bool isRotateCamera = false;
    public static bool isStartRotateCamera = false;
    public static int indexTouch = -1;
    private void Start()
    {


    }

    private void Update()
    {

        if (isRotateCamera)
        {
            if (indexTouch != -1)
            {
                Touch touchRotateCamera;
                if (indexTouch > Input.touches.Length - 1)
                {
                    touchRotateCamera = Input.GetTouch(Input.touches.Length - 1);
                }
                else
                {
                    touchRotateCamera = Input.GetTouch(indexTouch);

                }
                deltaMouse = touchRotateCamera.position - ogrinPosMouse;
                ogrinPosMouse = touchRotateCamera.position;
            }
            else
            {
                deltaMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - ogrinPosMouse;
                ogrinPosMouse = Input.mousePosition;
            }


        }


    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        isRotateCamera = true;
        isStartRotateCamera = false;
        if (Input.touchCount > 0)
        {
            if (OnDragJoystick.inputDragTarget != Vector2.zero)
            {

                indexTouch = 1;

            }
            else
            {
                indexTouch = 0;
            }
        }
        //else
        // indexTouch = -1;

        ogrinPosMouse = indexTouch == -1 ? eventData.position : Input.GetTouch(indexTouch).position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isRotateCamera)
        {
            //Debug.Log(indexTouch);
            //if (indexTouch == -1)
            //{

            //    deltaMouse = eventData.position - ogrinPosMouse;
            //    ogrinPosMouse = eventData.position;
            //}



        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isRotateCamera)
            EndRotateCamera();
    }

    public void EndRotateCamera()
    {
        isStartRotateCamera = true;
        isRotateCamera = false;
        indexTouch = -1;
        deltaMouse = Vector3.zero;
    }

}
