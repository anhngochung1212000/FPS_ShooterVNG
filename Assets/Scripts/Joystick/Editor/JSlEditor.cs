using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(JoystickArea))]
public class JSEditor : Editor
{
    void OnSceneGUI()
    {
        JoystickArea dragJoystick = target as JoystickArea;
        if (dragJoystick == null)
            return;
        var _OldAnchoredAreaBounds = dragJoystick.AnchoredAreaBounds;
        Canvas canvas = dragJoystick.GetComponentInParent<Canvas>();
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        RectTransform KnobRect = dragJoystick.GetComponent<RectTransform>();
        if (dragJoystick.AnchoredAreaBounds.size == Vector3.zero)
        {
            dragJoystick.AnchoredAreaBounds.size = new Vector3(200f, 200f, 0f);
            dragJoystick.AnchoredAreaBounds.center = new Vector3(50f, 80f, 0f);
        }
        Handles.color = Color.magenta;
        Bounds convertedBounds = new Bounds(dragJoystick.AnchoredAreaBounds.center * CanvasRect.lossyScale.x, dragJoystick.AnchoredAreaBounds.size * CanvasRect.lossyScale.x); //Converte
        TriggerHandler.Box(ref convertedBounds, KnobRect.transform.position, "Knob Area");
        dragJoystick.AnchoredAreaBounds = new Bounds(convertedBounds.center / CanvasRect.lossyScale.x, convertedBounds.size / CanvasRect.lossyScale.x);
        if (_OldAnchoredAreaBounds != dragJoystick.AnchoredAreaBounds )
            Undo.RecordObject(target, "Knob Handle");
    }

}
