using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame{
    public class DiskFactory : Singleton<DiskFactory> {
        /*private static DiskFactory _instance;*/
        private static List<GameObject> diskList;// 飞碟队列  
        public GameObject diskTemplate;   // 预设对象  
        private static int totaldisk=0;
        //public GameObject disk;
        /*
        public static DiskFactory getInstance() {
            if (_instance == null) {
                _instance = new DiskFactory();
                diskList = new List<GameObject>();
            }
            return _instance;
        }*/
        private void Awake() {
            //this.diskTemplate = disk;
            diskList = new List<GameObject>();
        }

        public static int getTotalDisk() {
            return totaldisk;
        }

        public static void setTotalDisk(int newTotalDisk) {
            totaldisk = newTotalDisk;
        }
        // 获取可用飞碟id  
        public int getDisk() {
            totaldisk++;
            for (int i = 0; i < diskList.Count; i++) {
                if (!diskList[i].activeInHierarchy) {
                    Debug.Log("recycle create");
                    return i;  // 飞碟空闲  
                }
            }
            Debug.Log("add disk: total disk " + diskList.Count);
            // 无空闲飞碟，则实例新的飞碟预设 
            diskList.Add(GameObject.Instantiate(diskTemplate) as GameObject);
            return diskList.Count - 1;
        }
        // 获取飞碟对象  
        public GameObject getDiskObject(int id) {
            if (id>=0 && id < diskList.Count) {
                return diskList[id];
            }
            return null;
        }
        // 回收飞碟  
        public void free(int id) {
            if (id > -1 && id < diskList.Count) {
                // 重置飞碟大小  
                diskList[id].transform.localScale = diskTemplate.transform.localScale;
                diskList[id].SetActive(false);
            }
        }
    }
}

/*
public class DiskFactoryBC : MonoBehaviour {
    public GameObject disk;

    private void Awake() {
            DiskFactory.getInstance().diskTemplate = disk;
    }
}*/
