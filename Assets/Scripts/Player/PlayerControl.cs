using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Actor
{
    public static PlayerControl instance;
    [HideInInspector] public Transform trans;

    CharacterController m_CharacterController;
    Vector3 m_MoveDir = Vector3.zero;
    [SerializeField] float speed = 2f;
    [SerializeField] float m_StickToGroundForce;
    [SerializeField] float m_GravityMultiplier;
    protected override void Awake()
    {
        instance = this;
        trans = transform;
        m_CharacterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        
        base.Start();
    }

    protected override void Update()
    {
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * OnDragJoystick.inputDragTarget.y + transform.right * OnDragJoystick.inputDragTarget.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;

        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
    }
}
