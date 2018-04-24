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

    SceneController sc;
    private IUserInterface userInt;
    private IQueryStatus queryInt;

    private void Awake() {
        sc = Singleton<SceneController>.Instance;
    }

    void Start () {
        bullet = GameObject.Instantiate(bullet) as GameObject;
        userInt = sc as IUserInterface;
        queryInt = sc as IQueryStatus;
        sc.setPhysicsorTransform(0);
    }

    void OnGUI() {
        if (GUI.Button(new Rect(20, 100, 180, 30),"点击飞碟可以使用物理运动")) {
            sc.setPhysicsorTransform(1);
        }
        if (GUI.Button(new Rect(20, 140, 180, 30),"点击飞碟可以使用运动学变换")) {
            sc.setPhysicsorTransform(0);
        }
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
                // 击中飞碟设置为不活跃，自动回收  
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
            // 如果回合更新，主提示显示新回合  
            if (round != queryInt.getRound()) {
                round = queryInt.getRound();
                queryInt.setTotalDisk();
                sc.setPhysicsorTransform(0);
                mainText.text = "Round " + round.ToString() + " begins!";
                if (round == 2)
                    mainText.text += "\nnice! go 2 round!";
            }
        }
    }
}
