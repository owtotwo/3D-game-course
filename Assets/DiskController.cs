using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour {

    private Color diskColor;
    private Vector3 emitPosition;
    private Vector3 emitDirection;
    private float emitSpeedx;
    private float emitSpeedy;
    private float rdomnum;
    private float x;
    private float y;
    private float z;
    private float posx;

    void Start () {
        rdomnum = Random.Range(0.0f, 3.0f);
        if (rdomnum < 1.0f) diskColor = Color.yellow;
        else if (rdomnum < 2.0f) diskColor = Color.black;
        else diskColor = Color.green;
        this.GetComponent<Renderer>().material.color = diskColor;
        posx = Random.Range(-2.0f, 2.0f);
        emitPosition = new Vector3(posx, 1, 0);
        this.transform.position = emitPosition;
        x = Random.Range(-2.0f, 2.0f);
        Debug.Log("x: " + x);
        emitSpeedx = Random.Range(2.0f, 3f);
        emitSpeedy = Random.Range(1.80f, 2.5f);
        Debug.Log("speedx: " + emitSpeedx + "speedy: " + emitSpeedy);
	}
	

	void Update () {
        if (x < 0) this.transform.position += Vector3.left * Time.deltaTime * emitSpeedx;
        else this.transform.position += Vector3.right * Time.deltaTime * emitSpeedx;
        this.transform.position += Vector3.up * Time.deltaTime * emitSpeedy;
    }
}
