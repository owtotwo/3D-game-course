using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDiskController : MonoBehaviour {

    private Color diskColor;
    private Vector3 emitPosition;
    private Vector3 emitDirection;
    private float emitSpeedx;
    private float emitSpeedy;
    private float emitSpeedz;
    private float rdomnum;
    private float x;
    private float y;
    private float z;
    private float posx;

    // Use this for initialization
    void Start () {
        //设置飞碟的各种属性，速度，起始位置，方向等
        rdomnum = Random.Range(0.0f, 3.0f);
        if (rdomnum < 1.0f) diskColor = Color.yellow;
        else if (rdomnum < 2.0f) diskColor = Color.black;
        else diskColor = Color.green;
        this.GetComponent<Renderer>().material.color = diskColor;

        posx = Random.Range(-2.0f, 2.0f);
        emitPosition = new Vector3(posx, 1, 0);
        this.transform.position = emitPosition;

        x = Random.Range(-20.0f, 20.0f);
        Debug.Log("x: " + x);
        emitSpeedx = Random.Range(-8.0f, 8f);
        emitSpeedy = Random.Range(5.8f, 8.5f);
        emitSpeedz = Random.Range(1.8f, 2.5f);
        Debug.Log("speedx: " + emitSpeedx + "speedy: " + emitSpeedy);
        emitDirection = new Vector3(emitSpeedx, emitSpeedy, emitSpeedz);
        //this.gameObject.AddComponent<Rigidbody>();
        this.GetComponent<Rigidbody>().AddForce(emitDirection, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
