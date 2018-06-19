using UnityEngine;

public class StandardBullet : MonoBehaviour {

	private bool _active; 
	public float Damage;
	public float Decay;

	void Start () {
		_active = true;
		gameObject.GetComponent<TrailRenderer>().material.SetColor("_Color", Color.yellow);
	}
	
	void Update () {}

	void OnTriggerEnter(Collider target){
		if (target.GetComponent<Collider>().tag == "Tank" && _active) {
			Transform tank = target.transform.parent;
			while(tank.parent != null){
				tank = tank.parent;
			}
			tank.GetComponent<Tank>().Hurt(Damage);
			_active = false;
		}
		if (target.GetComponent<Collider>().tag == "Ground") {
			Damage -= Decay;
			if (Damage<0f){
				Damage = 0f;
			}
		}
	}
}
