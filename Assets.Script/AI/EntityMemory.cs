using UnityEngine;
using System.Collections.Generic;
using System;

public class EntityMemory {
	// should be private but too lazy to declare this class iterable
	public Dictionary<object, Encounter> Encounters;

	public EntityMemory() {
		this.Encounters = new Dictionary<object, Encounter> ();
	}

	public void ObservedEntity(object entity, Vector3 lastLocation) {
		Encounter encounter = new Encounter ();
		encounter.LastEncounterTime = DateTime.Now;
		encounter.LastLocation = lastLocation;

		if(Encounters.ContainsKey(entity)) {
			Encounters.Remove(entity);
		}
		Encounters.Add(entity, encounter);
	}

	public void ForgetEntity(object entity) {
		Encounters.Remove (entity);
	}

	public Encounter GetLastEncounter(object entity) {
		return (Encounter)Encounters[entity];
	}

	public class Encounter {
		public DateTime LastEncounterTime;
		public Vector3 LastLocation;
	}
}