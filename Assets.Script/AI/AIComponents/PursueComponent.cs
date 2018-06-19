using UnityEngine;
using System;

public class PursueComponent : IAiComponent {
	private static bool _playerLastPosKnown;
	private static Vector3 _playerLastKnownLocation;
	private DateTime _playerLastEncounterTime;
	private float _viewingDistance;
	private float _viewingAngle;
	private float _maxAttackDistance;
	private float _pursueSpeed;
	private float _wanderAndPatrolSpeed;

	public PursueComponent(float viewingDistance, float viewingAngle, float maxAttackDistance,
	                       float pursueSpeed, float wanderAndPatrolSpeed) {
		_playerLastPosKnown = false;
		this._viewingDistance = viewingDistance;
		this._viewingAngle = viewingAngle;
		this._maxAttackDistance = maxAttackDistance;
		this._pursueSpeed = pursueSpeed;
		this._wanderAndPatrolSpeed = wanderAndPatrolSpeed;
	}
	
	public void Think(IEntityInterface npcInterface) {
		Vector3 npcLocation = npcInterface.GetEntityLocation ();
		Vector3 playerLocation = npcInterface.GetPlayerLocation ();
		float npcRotation = npcInterface.GetEntityRotation ();

		if(GenericAi.EntitySeen(npcLocation, npcRotation, playerLocation, 
			                       this._viewingAngle, this._viewingDistance)) {
			_playerLastPosKnown = true;
			_playerLastKnownLocation = playerLocation;
			this._playerLastEncounterTime = DateTime.Now;
		} else if(DateTime.Now.Subtract(_playerLastEncounterTime).TotalSeconds > 10) {
			_playerLastPosKnown = false;
		}
	}



	public bool Act(IEntityInterface npcInterface) {
		Vector3 npcLocation = npcInterface.GetEntityLocation ();
		Vector3 playerLocation = npcInterface.GetPlayerLocation ();
		float npcRotation = npcInterface.GetEntityRotation ();

		if (GenericAi.EntitySeen(npcLocation, npcRotation, playerLocation, 
		                        this._viewingAngle, this._viewingDistance)) {
			if(GenericAi.Distance(playerLocation, npcLocation) <= this._maxAttackDistance) {
				return false;
			} else {
				npcInterface.SetEntityLocation(playerLocation, _pursueSpeed);
				return true;
			}
		} else if(_playerLastPosKnown) {
			if(GenericAi.Distance(npcLocation, _playerLastKnownLocation) > 1) {
				npcInterface.SetEntityLocation(_playerLastKnownLocation, _wanderAndPatrolSpeed);
				return true;
			} else {
				npcInterface.SetEntityRotation (1);
				return true;
			}
		}
		return false;
	}
}
