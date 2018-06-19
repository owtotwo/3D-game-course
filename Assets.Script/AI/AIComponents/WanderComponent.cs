using UnityEngine;

public class WanderComponent : IAiComponent {

	private Rect _territory;
	private Vector3 _target;
	private float _speed;

	public WanderComponent(Rect territory, float speed) {
		this._territory = territory;
		this._speed = speed;
		PickTarget ();
	}

	public void Sense() {
		return;
	}

	public void Think(IEntityInterface npcInterface) {
		if(GenericAi.Distance(npcInterface.GetEntityLocation(), _target) <= 2.0f) {
			PickTarget();
		}
		return;
	}

	private void PickTarget() {
		Vector3 newTarget = new Vector3();
		do {
			newTarget.x = Random.Range (_territory.x, _territory.x + _territory.width);
			newTarget.z = Random.Range (_territory.y, _territory.y + _territory.height);
		} while (GenericAi.Distance(_target, newTarget) < 5);
		_target = newTarget;
	}

	public bool Act(IEntityInterface npcInterface) {
		npcInterface.SetEntityLocation (_target, this._speed);
		return true;
	}
}
