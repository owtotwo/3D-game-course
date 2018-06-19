using UnityEngine;

public class GenericAi {

	private IAiComponent[] _components = null;
	private IEntityInterface _npcInterface;
	private AiResources _resources;

	public GenericAi(IAiComponent[] components, AiResources resources, IEntityInterface npcInterface) {
		this._components = components;
		this._resources = resources;
		this._npcInterface = npcInterface;
	}

	public static bool EntitySeen(Vector3 observerPos, float observerRotation, Vector3 observeePos, 
	                              float viewingAngle, float viewingDistance) {
		Vector3 direction = (observeePos - observerPos).normalized;
		float npcAngle = Vector3.Angle (direction, 
		                                Quaternion.AngleAxis(observerRotation, Vector3.up) * new Vector3 (0, 0, -1));
		if(GenericAi.Distance(observerPos, observeePos) <= viewingDistance
		   && npcAngle <= (viewingAngle / 2)) {
			return true;
		}
		if(GenericAi.Distance(observerPos, observeePos) <= 7.0f) {
			return true;
		}
		return false;
	}

	public void Think() {
		for(int i = 0; i < this._components.Length; i++) {
			this._components[i].Think(_npcInterface);
		}
	}

	public void Act() {
		for(int i = 0; i < this._components.Length; i++) {
			if(this._components[i].Act(_npcInterface)) {
				return;
			}
		}
	}

	public static float Distance(Vector3 v1, Vector3 v2) {
		return Mathf.Sqrt (Mathf.Pow ((v1.x - v2.x), 2) + Mathf.Pow ((v1.z - v2.z), 2));
	}
	
	public AiResources GetAiStats() {
		return this._resources;
	}
}
