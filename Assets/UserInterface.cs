using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Mygame;

public class UserInterface : MonoBehaviour {
    public Text mainText;   // 显示主提示：倒计时、新回合  
    public Text scoreText;  // 显示得分   
    public Text roundText;  // 显示回合  

    private int round;  // 当前回合  

    public GameObject bullet;
    
    private IUserInterface userInt;
    private IQueryStatus queryInt;

    void Start () {
        bullet = GameObject.Instantiate(bullet) as GameObject;
        userInt = SceneController.getInstance() as IUserInterface;
        queryInt = SceneController.getInstance() as IQueryStatus;
    }
    
    void Update() {
        if (Input.GetKeyDown("space")) {
            userInt.emitDisk();    // 发射飞碟
        }

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 摄像机到鼠标射线
            bullet.transform.position = transform.position;    // 子弹从摄像机位置射出

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Finish") {
                    hit.collider.gameObject.SetActive(false);
            }
        }
        if (round == 1 && queryInt.checkgameover() == 1) {
            mainText.text = "you only have 5 chances\nyou didn't pass round 1";
        } else if (round == 2 && queryInt.checkgameover() == 1) {
            mainText.text = "you only have 5 chances\nyou finished the game!";
        }
        else {
            roundText.text = "Round: " + queryInt.getRound().ToString();
            scoreText.text = "Score: " + queryInt.getPoint().ToString();
            if (round != queryInt.getRound()) {
                round = queryInt.getRound();
                queryInt.setTotalDisk();
                mainText.text = "Round " + round.ToString() + " begins!";
                if (round == 2)
                    mainText.text += "\nnice! go 2 round!";
            }
        }
    }
}
