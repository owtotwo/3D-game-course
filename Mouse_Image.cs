using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Manager;
using UnityEngine.UI;

public class Mouse_Image : MonoBehaviour {

    private Game_Scene_Manager gsm;
    private Image mouse_image;
    private int mouse_type = 0;
    public Sprite none;
    public Sprite hair;
    public Sprite weapon;
    public Sprite foot;
    public Color None;
    public Color NotNone;
    public Camera cam;

    void Awake() {
        gsm = Game_Scene_Manager.GetInstance();
        gsm.SetMouse(this);
        mouse_image = GetComponent<Image>();
    }

    public int GetMouseType() {
        return mouse_type;
    }

    public void SetMouseType(int Mouse_type) {
        mouse_type = Mouse_type;
        //Debug.Log("mousetype:" + mouse_type);
    }

    void Update() {
        if (mouse_type == 0) {
            mouse_image.sprite = none;
            mouse_image.color = None;
        }
        else {
            mouse_image.color = NotNone;
            if (mouse_type == 1) mouse_image.sprite = hair;
            else if (mouse_type == 2) mouse_image.sprite = weapon;
            else if (mouse_type == 3) mouse_image.sprite = foot;
        }
        transform.position = new Vector3(Input.mousePosition.x - 425 - 200, Input.mousePosition.y - 165 - 80, 0);
    }
}