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
    private int diskScale;    // 飞碟大小  
    private int hitpoint;
    private int emitNumber;   // 发射数量  
    private bool emitEnable;
    private int gameover=0;
    private int physicsortransform;
    DiskFactory df;

    SceneController sc;

    private void Awake() {
        sc = Singleton<SceneController>.Instance;
        sc.setGameModel(this);
        df = Singleton<DiskFactory>.Instance;
    }

    private void Start()
    {
        setting(1);
    }
    // 初始化设置
    public void setting(int num) {
        emitNumber = num;
    }
    // 准备下一次发射  
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
    // 发射飞碟  
    void emitDisks()
    {
        physicsortransform = sc.getPhysicsorTransform();
        if (DiskFactory.getTotalDisk() < 5) {
            for (int i = 0; i < emitNumber; i++) {
                diskIds.Add(df.getDisk());
                disks.Add(df.getDiskObject(diskIds[i]));
                if (physicsortransform == 0) {
                    disks[i].AddComponent<DiskController>();
                }
                else {
                    disks[i].AddComponent<PhysicsDiskController>();
                    disks[i].AddComponent<Rigidbody>();
                }
                disks[i].SetActive(true);
            }
        }
        else {
            gameover = 1;
        }
    }
    // 回收飞碟  
    void freeADisk(int i)
    {
        df.free(diskIds[i]);
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
                emitDisks(); // 发射飞碟  
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
            if (disks[i].transform.position.y > 5 || disks[i].transform.position.y < 0) {
                if (disks[i].GetComponent<Renderer>().material.color == Color.black) {
                    Debug.Log("hit to free,color is : black");
                    hitpoint = 20;
                }
                Debug.Log("too high free or too low free"); // 飞碟飞太高不在场景中  
                sc.getJudge().failADisk();   // 失分  
                if (physicsortransform == 0) {
                    Destroy(disks[i].GetComponent<DiskController>());
                }
                else {
                    Destroy(disks[i].GetComponent<PhysicsDiskController>());
                    Destroy(disks[i].GetComponent<Rigidbody>());
                }
                freeADisk(i);
            } else
            if (!disks[i].activeInHierarchy)
            {
                //  击中飞碟
                if (disks[i].GetComponent<Renderer>().material.color == Color.black) {
                    Debug.Log("hit to free,color is : black");
                    hitpoint = 20;
                }
                //得分
                sc.getJudge().scoreADisk();
                if (physicsortransform == 0) {
                    Destroy(disks[i].GetComponent<DiskController>());
                } else {
                    Destroy(disks[i].GetComponent<PhysicsDiskController>());
                    Destroy(disks[i].GetComponent<Rigidbody>());
                }
                freeADisk(i);
            }
        }
	}
}
