using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public float MinimumY = -40F;
    public float MaximumY = 40F;
    public float smoothTime = 5f;
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private PlayerControl player;
    private Camera m_Camera;
    public float horizontal;
    public float vertical;
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        player = GetComponentInParent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        vertical = player.trans.localEulerAngles.x;
        horizontal = player.trans.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        LookRotation();
    }

    public void LookRotation()
    {
        Vector3 move = OnRotateCamera.deltaMouse;
        horizontal = Mathf.Lerp(horizontal, horizontal + move.x * XSensitivity, Time.deltaTime * smoothTime);
        // horizontal = Mathf.Clamp(horizontal, MinimumX, MaximumX);
        vertical = Mathf.Lerp(vertical, vertical + move.y * YSensitivity, Time.deltaTime * smoothTime);
        vertical = Mathf.Clamp(vertical, MinimumY, MaximumY);
        player.trans.localRotation = Quaternion.Euler(-vertical, horizontal, 0);
    }
}
