using UnityEngine;
using System.Collections;

public class DragRigidbody2D : MonoBehaviour
{
    public float distance = 0.2f;
    public float dampingRatio = 1;
    public float frequency = 1.8f;
    public float linearDrag = 1.0f;
    public float angularDrag = 5.0f;
    public Camera mainCamera;
    private SpringJoint2D springJoint;

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        if (!springJoint)
        {
            GameObject go = new GameObject("Rigidbody2D Dragger");
            //Rigidbody2D body = go.AddComponent("Rigidbody2D") as Rigidbody2D;
            //springJoint = go.AddComponent("SpringJoint2D") as SpringJoint2D;

           // body.isKinematic = true;
        }
    }

    // Update
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        //Camera mainCamera = FindCamera();

        int mask = (1 << 8);
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);

        // I have proxy collider objects (empty gameobjects with a 2D Collider) as a child of a 3D rigidbody - simulating collisions between 2D and 3D objects
        // I therefore set any 'touchable' object to layer 8 and use the layerMask above for all touchable items

        if (hit.rigidbody != null && hit.rigidbody.isKinematic == true)
        {
            return;
        }

        if (hit.rigidbody != null && hit.rigidbody.isKinematic == false)
        {

            springJoint.transform.position = hit.point;

            springJoint.dampingRatio = dampingRatio;
            springJoint.frequency = frequency;
            springJoint.distance = distance;

            springJoint.connectedBody = hit.rigidbody;


            // maybe check if the 'fraction' is normalised. See http://docs.unity3d.com/Documentation/ScriptReference/RaycastHit2D-fraction.html
            StartCoroutine("DragObject", hit.fraction);



        } // end of hit true condition

    } // end of update


    IEnumerator DragObject(float distance)
    {

        float oldDrag = springJoint.connectedBody.drag;
        float oldAngularDrag = springJoint.connectedBody.angularDrag;

        springJoint.connectedBody.drag = linearDrag;
        springJoint.connectedBody.angularDrag = angularDrag;

        //Camera mainCamera = FindCamera();

        while (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            springJoint.transform.position = ray.GetPoint(distance);
            yield return null;
        }



        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }

    }

    Camera FindCamera()
    {
                return GetComponent<Camera>();
    }
}