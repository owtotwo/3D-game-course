﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

namespace Com.Mygame
{
    public interface IUserInterface {
        void emitDisk();
    }

    public interface IQueryStatus {
        bool isCounting();
        int getRound();
        int getPoint();
        int getEmitTime();
        void setTotalDisk();
        int checkgameover();
    }

    public interface IJudgeEvent {
        void nextRound();
        void setPoint(int point);
    }

    public class SceneController : System.Object, IUserInterface, IQueryStatus , IJudgeEvent{
        private static SceneController _instance;
        private SceneControllerBC _baseCode;
        private GameModel _gameModel;
        private Judge _judge;

        private int _point;
        private int _round;

        public static SceneController getInstance()
        {
            if (_instance == null) {
                _instance = new SceneController();
            }
            return _instance;                
        }
        public int checkgameover() { return _gameModel.checkgame(); }
        public void setTotalDisk() { _gameModel.setDiskNum(); }
        public void setGameModel(GameModel obj) { _gameModel = obj; }
        internal GameModel getGameModel() { return _gameModel; }

        public void setJudge(Judge obj) { _judge = obj; }
        internal Judge getJudge() { return _judge; }
        
        public void setSceneControllerBC(SceneControllerBC obj) { _baseCode = obj; }
        internal SceneControllerBC getSceneControllerBC() { return _baseCode; }

        public void emitDisk() { _gameModel.prepareToEmitDisk(); }
        public bool isCounting() { return _gameModel.isCounting(); }
        public int getRound() { return _round; }
        public int getPoint() { return _point; }
        public int getEmitTime() { return (int)_gameModel.timeToEmit + 1; }

        public void setPoint(int point) { _point = point; }
        public void nextRound() { _point = 0; _baseCode.loadRoundData(++_round); }
    }
}