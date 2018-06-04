using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;

public class GenerateEquipment : MonoBehaviour {

    private Game_Scene_Manager gsm;

    public void On_Press_GT() {
        gsm.GenAll();
    }

    void Awake() {
        gsm = Game_Scene_Manager.GetInstance();
    }
}