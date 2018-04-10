using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Action;
using My.Scene;
using UnityEngine.SceneManagement;

namespace My.FirstAction
{
    public interface IUserAction
    {
        void boatLeftToRight();
        void boatRightToLeft();
        void leftDevilOnBoat();
        void rightDevilOnBoat();
        void leftPriestOnBoat();
        void rightPriestOnBoat();
        void passOffBoatToLeft();
        void passOffBoatToRight();
        void restart();
    }

    public class FirstAction : SSActionMassager, ISSActionCallback, IUserAction
    {
        private FirstScene firstScene;
        private CCMoveToAction boatMovetoRight, boatMovetoLeft;
        private CCSequenceAction leftPassOnBoat, leftPassOffBoat
            , rightPassOnBoat, rightPassOffBoat;

        private float boatSpeed = 15f;
        private float passSpeed = 10f;

        GameObject[] boatPass = new GameObject[2];
        string[] boatPassName = new string[2];

        // Use this for initialization
        new void Start()
        {
            firstScene = Director.Director.getInstance().currentSceneController as FirstScene;
            firstScene.actionMassager = this;
            // 初始化actionmassager
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (firstScene.nowStatus == Status.eshore || firstScene.nowStatus == Status.sshore)
                checkEnding();
        }

        public void SSEventAction(SSAction source, SSActionEventType events = SSActionEventType.COMPLETED,
                    int intParam = 0, string strParam = null, Object objParam = null)
        {
            if (source == boatMovetoLeft || source == leftPassOffBoat
                || source == leftPassOnBoat) firstScene.nowStatus = Status.sshore;
            // 如果动作是左岸执行的动作，结束后将状态设置为左岸
            else firstScene.nowStatus = Status.eshore;
            // 如果是其他动作，设置成右岸
        }

        public void boatLeftToRight() // 如果船在左边并且船上有人，船开向右边
        {
            if (firstScene.nowStatus == Status.sshore && boatSize() != 0)
            {
                boatMovetoRight = CCMoveToAction.GetSSAction(Boat.rightPosition, boatSpeed);
                this.runAction(firstScene.boat, boatMovetoRight, this);
                firstScene.nowStatus = Status.startend;
            }
        }

        public void boatRightToLeft() // 如果船在右边并且船上有人，船开向左边
        {
            if (firstScene.nowStatus == Status.eshore && boatSize() != 0)
            {
                boatMovetoLeft = CCMoveToAction.GetSSAction(Boat.leftPosition, boatSpeed);
                this.runAction(firstScene.boat, boatMovetoLeft, this);
                firstScene.nowStatus = Status.endstart;
            }   
        }

        public void leftDevilOnBoat() // 如果船在左岸边并且人没满，恶魔上船
        {
            if (boatSize() != 2 && firstScene.leftDevils.Count != 0)
            // 船上有空位并且有恶魔
            {
                firstScene.nowStatus = Status.leftoff;
                int pos = (boatPass[0] == null ? 0 : 1); // 看要插入的是左还是右
                boatPass[pos] = firstScene.leftDevils.Pop(); // 左岸恶魔移动到船上

                boatPassName[pos] = "devil"; // 设置姓名
                boatPass[pos].transform.parent = firstScene.boat.transform; // 设置关系

                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.leftShoreside, passSpeed);
                CCMoveToAction moveB = CCMoveToAction.GetSSAction(Boat.leftPosition + (pos == 0 ? Boat.leftSeat : Boat.rightSeat), passSpeed);
                // 设置两个动作
                // todo：
                leftPassOnBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], leftPassOnBoat, this);
                // 执行动作
            }
        }

        public void rightDevilOnBoat() // 如果船在左岸边并且人没满，恶魔上船
        {
            if (boatSize() != 2 && firstScene.rightDevils.Count != 0)
            // 同上
            {
                firstScene.nowStatus = Status.rightoff;
                int pos = (boatPass[0] == null ? 0 : 1); // 看要插入的是左还是右
                boatPass[pos] = firstScene.rightDevils.Pop(); // 右岸恶魔移动到船上

                boatPassName[pos] = "devil"; // 设置姓名
                boatPass[pos].transform.parent = firstScene.boat.transform; // 设置关系

                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.rightShoreside, passSpeed);
                CCMoveToAction moveB = CCMoveToAction.GetSSAction(Boat.rightPosition + (pos == 0 ? Boat.leftSeat : Boat.rightSeat), passSpeed);
                rightPassOnBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], rightPassOnBoat, this);
            }
        }

        public void leftPriestOnBoat() // 同上
        {
            if (boatSize() != 2 && firstScene.leftPriests.Count != 0)
            // 船上有空位并且有牧师
            {
                firstScene.nowStatus = Status.leftoff;
                int pos = (boatPass[0] == null ? 0 : 1); // 看要插入的是左还是右
                boatPass[pos] = firstScene.leftPriests.Pop(); // 左岸恶魔移动到船上

                boatPassName[pos] = "priest"; // 设置姓名
                boatPass[pos].transform.parent = firstScene.boat.transform; // 设置关系

                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.leftShoreside, passSpeed);
                CCMoveToAction moveB = CCMoveToAction.GetSSAction(Boat.leftPosition + (pos == 0 ? Boat.leftSeat : Boat.rightSeat), passSpeed);
                leftPassOnBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], leftPassOnBoat, this);
            }
        }

        public void rightPriestOnBoat() // 同上
        {
            if (boatSize() != 2 && firstScene.rightPriests.Count != 0)
            // 船上有空位并且有牧师
            {
                firstScene.nowStatus = Status.rightoff;
                int pos = (boatPass[0] == null ? 0 : 1); // 看要插入的是左还是右
                boatPass[pos] = firstScene.rightPriests.Pop(); // 左岸恶魔移动到船上

                boatPassName[pos] = "priest"; // 设置姓名
                boatPass[pos].transform.parent = firstScene.boat.transform; // 设置关系

                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.rightShoreside, passSpeed);
                CCMoveToAction moveB = CCMoveToAction.GetSSAction(Boat.rightPosition + (pos == 0 ? Boat.leftSeat : Boat.rightSeat), passSpeed);
                rightPassOnBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], rightPassOnBoat, this);
            }
        }

        public void passOffBoatToLeft() // 如果船在左岸边并且船不空
        {
            if (boatSize() != 0)
            {
                firstScene.nowStatus = Status.leftoff;
                int pos = boatSize() - 1;
                boatPass[pos].transform.parent = null;
                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.leftShoreside, passSpeed);
                CCMoveToAction moveB;
                if (boatPassName[pos] == "devil")
                {
                    moveB = CCMoveToAction.GetSSAction(Devil.leftFirstPosition + Devil.spaceWithTwo * firstScene.leftDevils.Count, passSpeed);
                    // 左边恶魔的动作
                    firstScene.leftDevils.Push(boatPass[pos]); // 船上恶魔移动到左岸上
                }
                else
                {
                    moveB = CCMoveToAction.GetSSAction(Priest.leftFirstPosition + Priest.spaceWithTwo * firstScene.leftPriests.Count, passSpeed);
                    // 左岸牧师的动作
                    firstScene.leftPriests.Push(boatPass[pos]); // 船上牧师移动到左岸上
                }
                leftPassOffBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], leftPassOffBoat, this);
                // 进行动作
                boatPass[pos] = null; // 清除船上物体
            }
        }

        public void passOffBoatToRight() // 同上
        {
            if (boatSize() != 0)
            {
                firstScene.nowStatus = Status.rightoff;
                int pos = boatSize() - 1;
                boatPass[pos].transform.parent = null;
                CCMoveToAction moveA = CCMoveToAction.GetSSAction(Shore.rightShoreside, passSpeed);
                CCMoveToAction moveB;
                if (boatPassName[pos] == "devil")
                {
                    moveB = CCMoveToAction.GetSSAction(Devil.rightFirstPosition + Devil.spaceWithTwo * firstScene.rightDevils.Count, passSpeed);
                    // 右边恶魔的动作
                    firstScene.rightDevils.Push(boatPass[pos]); // 船上恶魔移动到右岸上
                }
                else
                {
                    moveB = CCMoveToAction.GetSSAction(Priest.rightFirstPosition + Priest.spaceWithTwo * firstScene.rightPriests.Count, passSpeed);
                    // 右岸牧师的动作
                    firstScene.rightPriests.Push(boatPass[pos]); // 船上牧师移动到右岸上
                }
                rightPassOffBoat = CCSequenceAction.GetSSAction(new List<SSAction> { moveA, moveB });
                this.runAction(boatPass[pos], rightPassOffBoat, this);
                // 进行动作
                boatPass[pos] = null; // 清除船上物体
            }
        }
        
        public void restart()
        {
            SceneManager.LoadScene("PriestsAndDevils");
            firstScene.nowStatus = Status.sshore;
        }

        // 辅助函数
        private int boatSize()
        {
            int sum = 0;
            if (boatPass[0] != null) sum++;
            if (boatPass[1] != null) sum++;
            return sum;
        }

        public void checkEnding()
        {
            if (firstScene.rightDevils.Count == 3 && firstScene.rightPriests.Count == 3)  //判断胜利
            {
                firstScene.nowStatus = Status.WIN;
                return;
            }

            int bpri = 0, bdev = 0;
            int ld = 0, rd = 0, lp = 0, rp = 0;
            for (int i = 0; i < 2; i++)  // 记录船上牧师与魔鬼的数量
            {
                if (boatPass[i] != null)
                    if (boatPassName[i] == "devil") bdev++;
                    else bpri++;
            }

            ld = firstScene.leftDevils.Count;
            lp = firstScene.leftPriests.Count;
            rd = firstScene.rightDevils.Count;
            rp = firstScene.rightPriests.Count;

            if (firstScene.nowStatus == Status.sshore) // 船在左边的牧师恶魔数量更新
            {
                ld += bdev;
                lp += bpri;
            }
            if (firstScene.nowStatus == Status.eshore) // 船在右边的牧师恶魔数量更新
            {
                rd += bdev;
                rp += bpri;
            }
            if (lp != 0 && ld > lp) firstScene.nowStatus = Status.LOSE;
            if (rp != 0 && rd > rp) firstScene.nowStatus = Status.LOSE;
        }
    }

}
