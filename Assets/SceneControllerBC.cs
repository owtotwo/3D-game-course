using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class SceneControllerBC : MonoBehaviour {

    private void Awake() {
        SceneController.getInstance().setSceneControllerBC(this);
    }

    public void loadRoundData(int round) {
        switch (round) {
            case 1:
                SceneController.getInstance().getGameModel().setting(1);
                break;
            case 2:
                SceneController.getInstance().getGameModel().setting(2);
                break;
        }
    }
}
