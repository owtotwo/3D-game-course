using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class RoundManager : MonoBehaviour {

    SceneController sc;

    private void Awake() {
        sc = Singleton<SceneController>.Instance;
        sc.setRoundManager(this);
    }

    public void loadRoundData(int round) {
        switch (round) {
            case 1:   //第一关
                sc.getGameModel().setting(1);
                break;
            case 2:   //第二关
                sc.getGameModel().setting(2);
                break;
        }
    }
}
