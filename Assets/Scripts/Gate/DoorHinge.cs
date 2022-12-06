using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    //Key for this door
    [SerializeField] private Key.KeyType keyType;

    private HingeJoint2D hingeJoint2D;
    private JointAngleLimits2D openDoorLimits;
    private JointAngleLimits2D closedDoorLimits;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    private void Awake()
    {
        hingeJoint2D = transform.Find("Hinge").GetComponent<HingeJoint2D>();
        openDoorLimits = hingeJoint2D.limits;
        closedDoorLimits = new JointAngleLimits2D { min = 0f, max = 0f };
        ClosedDoor();
    }

    public void OpenDoor()
    {
        Debug.Log("I'm open!");
        hingeJoint2D.limits = openDoorLimits;
    }

    public void ClosedDoor()
    {
        hingeJoint2D.limits = closedDoorLimits;
    }

}
