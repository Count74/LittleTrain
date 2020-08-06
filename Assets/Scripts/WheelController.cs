using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {


    void OnJointBreak2D(Joint2D brokenJoint)
    {
        GameManager.Instance.TrainIsBroken();
        Debug.Log("Braking Force " + brokenJoint.reactionForce);
        Debug.Log("The broken joint exerted a reaction torque of " + brokenJoint.reactionTorque);
    }
}
