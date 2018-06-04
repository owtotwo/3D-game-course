using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;

public class MyBag : MonoBehaviour {
    private Game_Scene_Manager gsm;
    private Image bag_image;
    public int mouse_type = 0;
    public Sprite hair;
    public Sprite weapon;
    public Sprite foot;
    public Sprite UISprite;
    public Color weapon_color;
    public Color UISprite_color;

    void Awake() {
        gsm = Game_Scene_Manager.GetInstance();
        bag_image = GetComponent<Image>();
    }

    public void On_equip_Button() {
        int MouseType = gsm.GetMouse().GetMouseType();
        //if (bag_image.sprite != UISprite && (MouseType == 0 || MouseType == mouse_type)) {
        //如果当前背包有装备，并且随鼠标移动的UI Image没有装备，拿起装备
        if (bag_image.sprite != UISprite && MouseType == 0) {
            Debug.Log("mousetype " + MouseType);
            bag_image.sprite = UISprite;
            bag_image.color = UISprite_color;
            gsm.GetMouse().SetMouseType(mouse_type);
            mouse_type = 0;
        }
        //else {
        //如果当前背包是空的，且UI Image有装备，放下装备
        else if (mouse_type == 0) {
            if (MouseType == 1) bag_image.sprite = hair;
            else if (MouseType == 2) bag_image.sprite = weapon;
            else if (MouseType == 3) bag_image.sprite = foot;
            mouse_type = MouseType;
            bag_image.color = weapon_color;
            gsm.GetMouse().SetMouseType(0);
        }
    }
}