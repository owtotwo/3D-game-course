# Tank with AI

`Unity3d课程作业九`

这次工程量蛮大的，耗时很久。

主要是怎么设计解耦AI模块这部分很麻烦。

在代码文件中的[AI/](Assets.Script/AI/)中，实现了对玩家控制坦克的信息获取接口，Seen、Think、Act三者逐一执行（Seen是Sence的一种），然后EntityTankAi继承了它并做出特化。

而AI的组件分为巡逻、调查、追赶、战斗四个行动。

``` C#
this._ai = new GenericAi(new IAiComponent[] {
    new CombatComponent(...),
    new PursueComponent(...),
    new InvestigateComponent(...),
    new WanderComponent(...), 
}, ...);
```

本来想实现一个被子弹打到有动能效果那种（击退，击翻）和炸起整辆坦克什么的，但想想好像坦克的重量不允许它这样……一开始还写了个履带不着地就不能摩擦地面继续跑之类的，后来也废弃掉了。

由于个人操作不太好，这里主要展示AI的效果，所以玩家是无敌的（其实把if去掉就可以玩家有生命值了），而AI只有100hp这样。

还有顺手加多了个摄像头，变成第一视角，这样射击比较容易（按Left-Shift切换视角）。

最后，整个效果如下：

![TankWithAI](C:\Users\80642\Desktop\3D-game-course\TankWithAI.gif)
