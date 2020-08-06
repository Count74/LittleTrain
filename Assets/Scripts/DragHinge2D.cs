using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHinge2D : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler{
    Rigidbody2D rb2D;
    HingeJoint2D hj2d;
    GameObject dragPrefab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject jointTrans = Instantiate(dragPrefab, mousePosition, transform.rotation) as GameObject;
        hj2d = jointTrans.GetComponent<HingeJoint2D>();
        hj2d.connectedAnchor = transform.InverseTransformPoint(mousePosition);
        hj2d.connectedBody = rb2D;
    }

    public void OnDrag(PointerEventData eventData)
    {
        hj2d.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Destroy(hj2d.gameObject);
    }

    private void Start()
    {
        dragPrefab = Resources.Load("DragRigitbody2DPrefab") as GameObject;
        Debug.Log(dragPrefab == null);
        rb2D = GetComponent<Rigidbody2D>();
    }



}
