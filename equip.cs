using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;

public class equip : MonoBehaviour {

    private Game_Scene_Manager gsm;
    private Image equip_image;
    public int mouse_type;
    public Sprite weapon;
    public Sprite UISprite;
    public Color weapon_color;
    public Color UISprite_color;



    void Awake() {
        equip_image = GetComponent<Image>();
    }

    public void On_equip_Button() {
        //获得当前跟随鼠标移动的UI Image的精灵类型
        int MouseType = gsm.GetMouse().GetMouseType();
        //如果此时装备栏有装备，并且UI Image可以拿起装备，拿装备
        //if (equip_image.sprite == weapon && (MouseType == 0 || MouseType == mouse_type)) {
        if (equip_image.sprite == weapon && MouseType == 0) {
            equip_image.sprite = UISprite;
            equip_image.color = UISprite_color;
            gsm.GetMouse().SetMouseType(mouse_type);
        }
        //如果UI I mage此时拿着对应的装备，则放下装备
        //else {
        else if (equip_image.sprite == UISprite) {
            if (MouseType == mouse_type) {
                equip_image.sprite = weapon;
                equip_image.color = weapon_color;
                gsm.GetMouse().SetMouseType(0);
            }
        }
    }

    // Use this for initialization
    void Start() {
        gsm = Game_Scene_Manager.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        if (mouse_type == 1 && gsm.GetHair() == 1) {
            gsm.SetHair(0);
            equip_image.sprite = weapon;
            equip_image.color = weapon_color;
        }
        else if (mouse_type == 2 && gsm.GetWeapon() == 1) {
            gsm.SetWeapon(0);
            equip_image.sprite = weapon;
            equip_image.color = weapon_color;
        }
        else if (mouse_type == 3 && gsm.GetFoot() == 1) {
            gsm.SetFoot(0);
            equip_image.sprite = weapon;
            equip_image.color = weapon_color;
        }
    }
}