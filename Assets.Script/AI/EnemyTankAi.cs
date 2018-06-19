using UnityEngine;

public class EnemyTankAi : MonoBehaviour {

	public Rect Bounds;
	public float WanderAndPatrolSpeed = 1.0f;
	public float PursuitSpeed = 2.0f;
	public GameObject Player;
	public GameObject Bullet;
	public float Firepower;
	public float ViewingAngle = 90;
	public float ViewingDistance = 25;
	public int ReloadTimeMillis = 1000;
	public float MaxAttackDistance = 15;
	
	private GenericAi _ai;
	private TankController _npcInterface;

	void Start() {
		this._npcInterface = new TankController (transform, Player.transform);

		AiResources resources = new AiResources (100, 50, 10, 1);

		this._ai = new GenericAi(new IAiComponent[] {
			new CombatComponent(resources, ViewingAngle, ViewingDistance, WanderAndPatrolSpeed, 
								Bullet, Firepower, ReloadTimeMillis, MaxAttackDistance),
			new PursueComponent(ViewingDistance, ViewingAngle, 
			                    MaxAttackDistance, PursuitSpeed, WanderAndPatrolSpeed),
			new InvestigateComponent(PursuitSpeed),
			new WanderComponent(Bounds, WanderAndPatrolSpeed), 
		}, resources, this._npcInterface);
	}
	
	void Update() {
		this._ai.Think ();
		this._ai.Act ();
	}

	public AiResources GetAiStats() {
		return _ai.GetAiStats ();
	}
}
