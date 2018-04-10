using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using My.Action;

namespace My.Scene
{
    public enum Status { sshore, eshore, startend, endstart, leftoff, rightoff, WIN, LOSE }
    //各个状态意思
    //sshore：船在开始岸
    //eshore：船在结束岸
    //startend：船从开始岸往结束岸移动
    //endstart：船从结束岸往开始岸移动
    //win：游戏胜利
    //lose：游戏失败
    public interface ISceneController
    {
        void LoadResources();
        void Pause();
        void Resume();
        Status getStatus();
    }
    
    public class Devil
    {
        public static Vector3 leftFirstPosition = new Vector3(-8, 1, 0);
        public static Vector3 rightFirstPosition = new Vector3(10, 1, 0);
        public static Vector3 spaceWithTwo = new Vector3(-1, 0, 0);
    }

    public class Priest
    {
        public static Vector3 leftFirstPosition = new Vector3(-5, 1, 0);
        public static Vector3 rightFirstPosition = new Vector3(7, 1, 0);
        public static Vector3 spaceWithTwo = new Vector3(-1, 0, 0);
    }

    public class Shore
    {
        public static Vector3 position = new Vector3(0, 0, 0);
        public static Vector3 leftShoreside = new Vector3(-4, 1, 0);
        public static Vector3 rightShoreside = new Vector3(4, 1, 0);
    }

    public class Boat
    {
        public static Vector3 leftPosition = new Vector3(-3, 0.25f, 0);
        public static Vector3 rightPosition = new Vector3(3, 0.25f, 0);
        public static Vector3 leftSeat = new Vector3(-0.5f, 0.75f, 0);
        public static Vector3 rightSeat = new Vector3(0.5f, 0.75f, 0);
        public static float Speed = 13f;
    }

    public class FirstScene : MonoBehaviour, ISceneController
    {
        public SSActionMassager actionMassager;

        public Stack<GameObject> leftPriests = new Stack<GameObject>();
        public Stack<GameObject> rightPriests = new Stack<GameObject>();
        public Stack<GameObject> leftDevils = new Stack<GameObject>();
        public Stack<GameObject> rightDevils = new Stack<GameObject>();

        GameObject priest;
        GameObject devil;

        public GameObject boat;
        // 船和船上的乘客

        public Status nowStatus;

        //--------------------------我是分割线--------------------
        //以下为monobehaviour

        void Start()
        {
            nowStatus = Status.sshore;
            Director.Director director = Director.Director.getInstance();
            director.setScene(this);
        }

        //--------------------我是分割线-------------------------
        //以下是场景构造

        public void LoadResources()
        {
            for (int i = 0; i < 3; i++)
            {
                priest = Instantiate<GameObject>(Resources.Load<GameObject>("Priest"),
                    Priest.leftFirstPosition + i * Priest.spaceWithTwo, Quaternion.identity);
                // priest.GetComponent<Renderer>().material.color = Color.blue;
                leftPriests.Push(priest);  // 实例化三个牧师

                devil = Instantiate<GameObject>(Resources.Load<GameObject>("Devil"),
                    Devil.leftFirstPosition + i * Devil.spaceWithTwo, Quaternion.identity);
                // devil.GetComponent<Renderer>().material.color = Color.red;
                leftDevils.Push(devil); // 实例化三个恶魔
            }

            Instantiate(Resources.Load("Shore"), Shore.position, Quaternion.identity);
            // 实例化左岸和右岸边

            boat = Instantiate<GameObject>(Resources.Load<GameObject>("Boat"),
                    Boat.leftPosition, Quaternion.identity);
            // boat.GetComponent<Renderer>().material.color = Color.black;  // 实例化船
        }

        public void Pause() // 游戏暂停
        {

        }

        public void Resume() // 游戏继续
        {

        }

        public Status getStatus()
        {
            return nowStatus;
        }        
    }
}
