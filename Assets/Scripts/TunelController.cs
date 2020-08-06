using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.CompareTag("Loco"))
        {
            collision.gameObject.GetComponentInParent<TrainController>().StopSmoke();
            GameManager.Instance.LevelComplete();
        }
    }
}
