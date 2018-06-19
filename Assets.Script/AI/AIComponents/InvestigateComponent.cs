using UnityEngine;

public class InvestigateComponent : IAiComponent {
	
	private Vector3 _target;
	private float _speed;
	private int _lasthealth;
	private int _currhealth;
	private float _timehit;
	
	public InvestigateComponent(float speed) {
		this._target = new Vector3(0,0,0);
		this._speed = speed;
		_timehit = -100f;
	}
	
	public void Think(IEntityInterface npcInterface) {
		if (npcInterface.GetEntityHealth () != _currhealth) {
			_currhealth = npcInterface.GetEntityHealth();
			_timehit = Time.timeSinceLevelLoad;
			_target = npcInterface.GetPlayerLocation();
		}
		return;
	}

	public bool Act(IEntityInterface npcInterface) {
		if (Time.timeSinceLevelLoad - _timehit < 10f) {
			npcInterface.SetEntityLocation (_target, this._speed);
			return true;
		}
		return false;
	}
}
