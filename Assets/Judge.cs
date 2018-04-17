using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class Judge : MonoBehaviour {
    public int pointToWin = 30;

    private SceneController scene;

    private void Awake() {
        scene = SceneController.getInstance();
        scene.setJudge(this);
    }

    void Start () {
        scene.nextRound();
	}
	
    public void scoreADisk() {
        int updatepoint = scene.getPoint() + scene.getGameModel().gethitpoint();
        scene.setPoint(updatepoint);
        if (scene.getRound() == 1 && scene.getPoint() > pointToWin) {
            scene.nextRound();
        }
    }

    public void failADisk() {
        scene.setPoint(scene.getPoint() - scene.getGameModel().gethitpoint());
    }
}
