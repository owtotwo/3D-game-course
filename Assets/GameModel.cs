using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class GameModel : MonoBehaviour {
    public float countDown = 3f;    // 飞碟发射倒计时总时间  
    public float timeToEmit;        // 飞碟发射倒计时剩余时间  
    private bool counting;          // 正在倒计时  
    public bool isCounting() { return counting; }

    private List<GameObject> disks = new List<GameObject>();
    private List<int> diskIds = new List<int>();
    private int diskScale;
    private int hitpoint;
    private int emitNumber;
    private bool emitEnable;
    private int gameover=0;

    private SceneController scene;

    private void Awake()
    {
        scene = SceneController.getInstance();
        scene.setGameModel(this);
    }

    private void Start()
    {
        setting(1);
    }
    
    public void setting(int num) {
        emitNumber = num;
    }

    public void prepareToEmitDisk()
    {
        if (!counting)
        {
            timeToEmit = countDown;
            emitEnable = true;
        }
    }

    public int checkgame() {
        return gameover;
    }

    public void setDiskNum() {
        DiskFactory.setTotalDisk(0);
    }

    void emitDisks()
    {
        if (DiskFactory.getTotalDisk() < 5) {
            for (int i = 0; i < emitNumber; i++) {
                diskIds.Add(DiskFactory.getInstance().getDisk());
                disks.Add(DiskFactory.getInstance().getDiskObject(diskIds[i]));
                disks[i].AddComponent<DiskController>();
                disks[i].SetActive(true);
            }
        }
        else {
            gameover = 1;
        }
    }

    void freeADisk(int i)
    {
        DiskFactory.getInstance().free(diskIds[i]);
        disks.RemoveAt(i);
        diskIds.RemoveAt(i);
    }

    private void FixedUpdate()
    {
        if (timeToEmit > 0)
        {
            counting = true;
            timeToEmit -= Time.deltaTime;
        }
        else
        {
            counting = false;
            if (emitEnable)
            {
                emitDisks();
                emitEnable = false;
            }
        }
    }

    public int gethitpoint() {
        return hitpoint;
    }

    void Update () {
		for (int i = 0; i < disks.Count; i++)
        {
            hitpoint = 10;
            if (disks[i].transform.position.y > 5) {
                if (disks[i].GetComponent<Renderer>().material.color == Color.black) {
                    Debug.Log("hit to free,color is : black");
                    hitpoint = 20;
                }
                Debug.Log("too high free");
                scene.getJudge().failADisk();   // 失分  
                Destroy(disks[i].GetComponent<DiskController>());
                freeADisk(i);
            } else
            if (!disks[i].activeInHierarchy)
            {
                if (disks[i].GetComponent<Renderer>().material.color == Color.black) {
                    Debug.Log("hit to free,color is : black");
                    hitpoint = 20;
                }
                scene.getJudge().scoreADisk();
                Destroy(disks[i].GetComponent<DiskController>());
                freeADisk(i);
            }
        }
	}
}
