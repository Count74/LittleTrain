using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {
    private Vector3 resetCamera;
    private Vector3 origin;
    private Vector3 diference;
    private bool isDrag = false;

    public BoxCollider2D boxCollider;


    public float outerLeft = 0f;
    public float outerRight = 30f;
    public float maxCameraZoom = 10;

    Camera camera;
    Bounds areaBounds;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        resetCamera = Camera.main.transform.position;
        boxCollider = this.GetComponentInParent<BoxCollider2D>();
        areaBounds = boxCollider.bounds;
        boxCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {


       
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.IsCameraLock())  // перетаскиваем блок
            return;

        if (GameManager.Instance.IsCameraFree())
            FreeCamera();
        else
            CameraFollowTrain();
        
    }

    private void CameraFollowTrain()
    {
        camera.orthographicSize = 5.0f;
        // рамки для камеры,
        float vertExtent = camera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;
        
        
        Vector3 trainPos = GameManager.Instance.getLoco().transform.position;

        camera.transform.position = new Vector3(
                    Mathf.Clamp(trainPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                    Mathf.Clamp(trainPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                    camera.transform.position.z);
    }

    private void FreeCamera()
    {
        float vertExtent = camera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera.orthographicSize < maxCameraZoom)
            {
                camera.orthographicSize = camera.orthographicSize + 0.5f;
                camera.transform.position = new Vector3(
                    Mathf.Clamp(camera.transform.position.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                    Mathf.Clamp(camera.transform.position.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                    camera.transform.position.z);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (camera.orthographicSize > 1)
                camera.orthographicSize = camera.orthographicSize - 0.5f;
        }


        if (Input.GetMouseButton(0))
        {
            diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (isDrag == false)
            {
                isDrag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            isDrag = false;
        }
        if (isDrag == true)
        {
            Vector3 newPos = origin - diference;
            Camera.main.transform.position = new Vector3(
                Mathf.Clamp(newPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                Mathf.Clamp(newPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                newPos.z);
        }
    }

  
}

