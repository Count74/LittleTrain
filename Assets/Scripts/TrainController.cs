using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {
    ParticleSystem smoke;
    WheelJoint2D[] wheels;
    AudioSource audioSource;
    bool isStarted = false;
    GameObject loco;

    public AudioClip tutu;
    public AudioClip chuchu;
    public AudioClip fail;


    // Use this for initialization
    void Awake () {
        smoke = GetComponentInChildren<ParticleSystem>();
        wheels = GetComponentsInChildren<WheelJoint2D>();
        loco = GameObject.FindGameObjectWithTag("Loco");
        audioSource = GetComponent<AudioSource>();
        StopSmoke();
    }
	
    public void StartEngine(){
        if (isStarted)
            return;
        isStarted = true;

        foreach (WheelJoint2D wheel in wheels){
            if (wheel != null)
                wheel.useMotor = true;
        }
        audioSource.PlayOneShot(tutu);
        audioSource.Play();
        GameManager.Instance.CameraToTrain();
    }


    internal void Stop()
    {
        smoke.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(fail);
        
        foreach (WheelJoint2D wheel in wheels)
        {
            if (wheel != null)
                wheel.useMotor = false;
        }
    }

    public void StartSmoke()
    {
        smoke.Play();
    }

    public void StopSmoke()
    {
        smoke.Stop();
    }

	// Update is called once per frame
	void Update () {
		
	}

    internal GameObject getLoco()
    {
        return loco ;
    }
}
