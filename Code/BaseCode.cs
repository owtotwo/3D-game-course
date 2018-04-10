﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Director;

public class BaseCode : MonoBehaviour {

    public string GameName;
    public string GameRule;
    // Use this for initialization
    void Start () {
        Director director = Director.getInstance();
        director.setBasecode(this);
        GameName = "Priests and Devils";
        GameRule = "Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many ways. Keep all priests alive! Good luck!\nSphere -- Priest\nCube -- Devil\nCast -- Boat";
    }
}
