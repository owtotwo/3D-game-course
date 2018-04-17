using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame{
    public class DiskFactory : System.Object {
        private static DiskFactory _instance;
        private static List<GameObject> diskList;
        public GameObject diskTemplate;
        private static int totaldisk=0;

        public static DiskFactory getInstance() {
            if (_instance == null) {
                _instance = new DiskFactory();
                diskList = new List<GameObject>();
            }
            return _instance;
        }

        public static int getTotalDisk() {
            return totaldisk;
        }

        public static void setTotalDisk(int newTotalDisk) {
            totaldisk = newTotalDisk;
        }

        public int getDisk() {
            totaldisk++;
            for (int i = 0; i < diskList.Count; i++) {
                if (!diskList[i].activeInHierarchy) {
                    Debug.Log("recycle create");
                    return i;
                }
            }
            Debug.Log("add disk: total disk " + diskList.Count);
            diskList.Add(GameObject.Instantiate(diskTemplate) as GameObject);
            return diskList.Count - 1;
        }

        public GameObject getDiskObject(int id) {
            if (id>=0 && id < diskList.Count) {
                return diskList[id];
            }
            return null;
        }

        public void free(int id) {
            if (id > -1 && id < diskList.Count) {
                diskList[id].transform.localScale = diskTemplate.transform.localScale;
                diskList[id].SetActive(false);
            }
        }
    }
}

public class DiskFactoryBC : MonoBehaviour {
    public GameObject disk;

    private void Awake() {
        DiskFactory.getInstance().diskTemplate = disk;
    }
}