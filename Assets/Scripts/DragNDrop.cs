using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour {
    Rigidbody2D rb2d;
    HingeJoint2D hj2d;
    GameObject dragPrefab;
    GameObject jointObj;

    Vector3 offset;
  
    public float rotationSpeed = 8;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dragPrefab = Resources.Load("HingeJoint2D") as GameObject;
    }
    private void OnMouseDown()
    {
        GameManager.Instance.SetLockCamera(true);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        offset = objPosition - transform.position;

        jointObj = Instantiate(dragPrefab, objPosition, transform.rotation) as GameObject;
        hj2d = jointObj.GetComponent<HingeJoint2D>();
        hj2d.connectedAnchor = transform.InverseTransformPoint(mousePosition);
        hj2d.connectedBody = rb2d;
    }

    private void OnMouseUp()
    {
        Destroy(hj2d.gameObject);
        GameManager.Instance.SetLockCamera(false);
    }


    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //float angle = -Mathf.Atan2(transform.position.z - objPosition.z, transform.position.x - objPosition.x) * Mathf.Rad2Deg;
        //transform.position = objPosition;
        Vector3 point = objPosition - transform.position - offset;
        //rb.velocity = ( objPosition - transform.position - offset) * 4;
        //rb.AddRelativeForce(point*20, ForceMode2D.Force);
        //rb.AddForceAtPosition(point.normalized * 50, transform.position );
        //transform.position = objPosition;
        jointObj.GetComponent<Rigidbody2D>().MovePosition(objPosition);
      
        //jointObj.GetComponent<Rigidbody2D>().velocity = (objPosition - transform.position) * 4;
        //rb2d.MovePosition(objPosition - offset);

        //rb.AddForceAtPosition(-point.normalized * 2, transform.position - offset);
        // body.AddForceAtPosition(direction.normalized, transform.position);
        //rb.rotation = 200 * Time.deltaTime;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);


        //Vector3 v3 = objPosition - transform.position;
        //Debug.Log(objPosition);
        //Debug.Log(transform.position);
        //Debug.Log(transform.localScale);
        //float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        //rb.angularVelocity
        //transform.eulerAngles = new Vector3(0, 0, angle );
        //transform.
    }
	
}
