using UnityEngine;
using System;

public class CombatComponent : IAiComponent {
	private EntityMemory _memory;
	private float _viewingAngleDegrees;
	private float _viewingDistance;
	private float _pursueSpeed;
	private AiResources _resources;
	private GameObject _bullet;
	private float _firepower;
	private DateTime _lastFireTime;
	private int _reloadMillis;
	private float _maxFireDistance;

	public CombatComponent (AiResources resources, float viewingAngleDegrees, float viewingDistance,
	                        float pursueSpeed, GameObject bullet, float firepower, int reloadMillis,
	                        float maxFireDistance) {
		this._resources = resources;
		this._memory = new EntityMemory ();
		this._viewingAngleDegrees = viewingAngleDegrees;
		this._viewingDistance = viewingDistance;
		this._pursueSpeed = pursueSpeed;
		this._bullet = bullet;
		this._firepower = firepower;
		this._lastFireTime = DateTime.Now;
		this._reloadMillis = reloadMillis;
		this._maxFireDistance = maxFireDistance;
	}

	public void Think(IEntityInterface npcInterface) {
		return;
	}

	private void Fire(IEntityInterface npcInterface) {
		if(DateTime.Now.Subtract(this._lastFireTime).TotalMilliseconds >= _reloadMillis) {

			npcInterface.SetEntityRotation(npcInterface.GetPlayerLocation());
			Transform barrel = npcInterface.GetEntityTransform().GetChild(2).GetChild(0);
			GameObject projectile = MonoBehaviour.Instantiate (_bullet, barrel.position + barrel.up.normalized * -2f, Quaternion.identity) as GameObject;
			projectile.GetComponent<Rigidbody>().AddForce ((-barrel.up).normalized * _firepower);
			this._lastFireTime = DateTime.Now;
		}
	}

	public bool Act(IEntityInterface npcInterface) {
		Vector3 playerLocation = npcInterface.GetPlayerLocation ();
		Vector3 npcLocation = npcInterface.GetEntityLocation();
		float npcRotation = npcInterface.GetEntityRotation ();

		if(GenericAi.EntitySeen(npcLocation, npcRotation, playerLocation, 
		              _viewingAngleDegrees, _viewingDistance)) {
			if(GenericAi.Distance(npcLocation, playerLocation) > _maxFireDistance) {
				npcInterface.SetEntityLocation(playerLocation, _pursueSpeed);
			} else {
				Fire (npcInterface);
			}
			return true;
		}

		return false;
	}
}