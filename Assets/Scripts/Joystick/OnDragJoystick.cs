using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEditor;
public class OnDragJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static OnDragJoystick instance;
    private void Awake()
    {
        instance = this;
    }
    public RectTransform rect_JS;
    public RectTransform rect_Knod;
    //[HideInInspector]
    //public Bounds AnchoredAreaBounds;
    [HideInInspector]
    public Vector2 MinScreenAreaBounds;
    [HideInInspector]
    public Vector2 MaxScreenAreaBounds;
    public static Vector2 inputDragTarget;
    private Vector3 canvasInitialPoint;
    private Vector2 CanvasSize;
    private Vector2 ScreenToAnchorPositionConversionConstant;
    private Vector2 ScreenPixels;
    Vector3 knobUnAnchoredPositionOnCanvas;
    private Image knobBackground;
    public Image knobImage;
    private float ScreenUnitsToWorldUnitsConversionConstant;
    private bool isDragging = true;
    private float CanvasScale;
    private bool bStart;
    private bool isDragByKeyBoard;
    Canvas canvas;
    public JoystickArea joystickArea;
    public RectTransform container;
    private Vector3 ogrinPos;
    public float distanceKnod;
    private bool bOut;
    private int touchIndex = -1;
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        knobBackground = rect_JS.gameObject.GetComponent<Image>();
        if (canvas == null)
        {
            Debug.LogError("canvas not found, put this object as children of an canvas.");
        }
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        ScreenPixels = new Vector2(Screen.width, Screen.height);
        CanvasSize = CanvasRect.sizeDelta;
        CanvasScale = canvas.transform.lossyScale.x;
        ScreenToAnchorPositionConversionConstant = new Vector2(CanvasSize.x / ScreenPixels.x, CanvasSize.y / ScreenPixels.y);
        ScreenUnitsToWorldUnitsConversionConstant = ScreenToAnchorPositionConversionConstant.y * CanvasScale;
        canvasInitialPoint = canvas.transform.position + (new Vector3(-CanvasSize.x * canvas.transform.lossyScale.x * 0.5f, -CanvasSize.y * canvas.transform.lossyScale.y * 0.5f));
        knobUnAnchoredPositionOnCanvas = (rect_JS.position - canvasInitialPoint) / canvas.transform.lossyScale.y;
        knobBackground.CrossFadeAlpha(0f, 7f, true);
        knobImage.CrossFadeAlpha(0f, 7f, true);
    }

    private void Update()
    {
        InputJoystick.SetAxisMobile("Horizontal", inputDragTarget.x);
        InputJoystick.SetAxisMobile("Vertical", inputDragTarget.y);
        MinScreenAreaBounds = (knobUnAnchoredPositionOnCanvas + joystickArea.AnchoredAreaBounds.min) / ScreenToAnchorPositionConversionConstant.y;
        MaxScreenAreaBounds = (knobUnAnchoredPositionOnCanvas + joystickArea.AnchoredAreaBounds.max) / ScreenToAnchorPositionConversionConstant.y;

        if (isDragging)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (!isDragByKeyBoard)
                {
                    knobBackground.CrossFadeAlpha(1f, 0.01f, true);
                    knobImage.CrossFadeAlpha(1f, 0.01f, true);
                    isDragByKeyBoard = true;
                }
                inputDragTarget = new Vector2(Input.GetAxis("Horizontal") * 2, Input.GetAxis("Vertical") * 2);
                inputDragTarget = inputDragTarget.magnitude > 1.0f ? inputDragTarget.normalized : inputDragTarget;
                rect_Knod.anchoredPosition = new Vector2(inputDragTarget.x * (rect_JS.sizeDelta.x / 2), inputDragTarget.y * (rect_JS.sizeDelta.y / 2));
            }
            else
            {
                if (isDragByKeyBoard)
                {
                    EndMoveJoyStick();
                    isDragByKeyBoard = false;
                    knobBackground.CrossFadeAlpha(0f, 0.5f, true);
                    knobImage.CrossFadeAlpha(0f, 0.5f, true);
                }
            }


        }


    }
    public void MoveJoystickWhenDragOut(PointerEventData eventData)
    {
        Vector2 pos_2 = Vector2.zero;
        Vector2 posMouse = touchIndex == -1 ? eventData.position : Input.GetTouch(touchIndex).position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, posMouse, eventData.pressEventCamera, out pos_2);
        Vector3 newPos = new Vector3(pos_2.x, pos_2.y, 0);
        float distance = Vector3.Distance(newPos, ogrinPos);
        float dis_Move = distance < distanceKnod ? 0 : distance - distanceKnod;
        Vector3 dir = newPos - ogrinPos;
        dir.Normalize();
        Vector3 needPos = ogrinPos + dir * dis_Move;
        rect_JS.anchoredPosition = needPos;
        ogrinPos = rect_JS.anchoredPosition;
    }
    bool CheckBoxArea(Vector3 posDrag)
    {
        var pos = posDrag;
        if (pos.x > MinScreenAreaBounds.x && pos.x < MaxScreenAreaBounds.x && pos.y > MinScreenAreaBounds.y && pos.y < MaxScreenAreaBounds.y)
            return true;
        else
            return false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckBoxArea(eventData.position) && isDragging && !isDragByKeyBoard)
        {
            isDragging = false;
            if (!bStart)
            {
                rect_JS.position = canvasInitialPoint + new Vector3(eventData.position.x, eventData.position.y, 0) * ScreenUnitsToWorldUnitsConversionConstant;
                ogrinPos = rect_JS.anchoredPosition;
            }

            bStart = true;

            knobBackground.CrossFadeAlpha(1f, 0.01f, true);
            knobImage.CrossFadeAlpha(1f, 0.01f, true);

            if (Input.touchCount > 0)
            {
                if (!OnRotateCamera.isRotateCamera)
                {
                    touchIndex = 0;
                }
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Vector2 pos;
            Vector2 posMouse = touchIndex == -1 ? eventData.position : Input.GetTouch(touchIndex).position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect_JS, posMouse, eventData.pressEventCamera, out pos);
            pos.x = (pos.x / rect_JS.sizeDelta.x);
            pos.y = (pos.y / rect_JS.sizeDelta.y);
            inputDragTarget = new Vector2(pos.x * 2, pos.y * 2);
            inputDragTarget = inputDragTarget.magnitude > 1.0f ? inputDragTarget.normalized : inputDragTarget;
            rect_Knod.anchoredPosition = new Vector2(inputDragTarget.x * (rect_JS.sizeDelta.x / 2), inputDragTarget.y * (rect_JS.sizeDelta.y / 2));

            //inputDragTarget = new Vector2(pos.x * 2.5f, pos.y * 2.5f);
            //if (inputDragTarget.magnitude > 3f)
            //{
            //    inputDragTarget = inputDragTarget.normalized;
            //}
            //else
            //    rect_Knod.anchoredPosition = new Vector2(inputDragTarget.x * (rect_JS.sizeDelta.x / 2), inputDragTarget.y * (rect_JS.sizeDelta.y / 2));
            //MoveJoystickWhenDragOut(eventData);
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging)
            EndMoveJoyStick();

    }
    public void EndMoveJoyStick()
    {
        isDragging = true;
        bStart = false;
        knobBackground.CrossFadeAlpha(0f, 0.5f, true);
        knobImage.CrossFadeAlpha(0f, 0.5f, true);
        inputDragTarget = Vector2.zero;
        rect_Knod.anchoredPosition = Vector2.zero;
        touchIndex = -1;
    }
    
    public void OnEndDrag()
    {
        InputJoystick.SetAxisMobile("Horizontal", 0);
        InputJoystick.SetAxisMobile("Vertical", 0);
        isDragging = true;
        bStart = false;
        knobBackground.CrossFadeAlpha(0f, 0.5f, true);
        knobImage.CrossFadeAlpha(0f, 0.5f, true);
        inputDragTarget = Vector2.zero;
        rect_Knod.anchoredPosition = Vector2.zero;
        
    }
}
