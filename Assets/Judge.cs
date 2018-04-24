using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class Judge : MonoBehaviour {
    public int pointToWin = 30;

    SceneController sc;

    private void Awake() {
        sc = Singleton<SceneController>.Instance;
        sc.setJudge(this);
    }

    void Start () {
        sc.nextRound(); // 默认开始第一关  
    }
    // 击中飞碟得分  
    public void scoreADisk() {
        int updatepoint = sc.getPoint() + sc.getGameModel().gethitpoint();
        sc.setPoint(updatepoint);
        if (sc.getRound() == 1 && sc.getPoint() > pointToWin) {
            sc.nextRound();
        }
    }
    // 掉落飞碟失分 
    public void failADisk() {
        sc.setPoint(sc.getPoint() - sc.getGameModel().gethitpoint());
    }
}
