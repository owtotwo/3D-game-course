using UnityEngine;

public interface IEntityInterface {
	void SetEntityLocation(Vector3 location, float speed);
	Vector3 GetEntityLocation();
	float GetEntityRotation();
	Vector3 GetPlayerLocation();
	Vector3 GetEntityForward();
	void SetEntityRotation(Vector3 location);
	void SetEntityRotation(float rotation);
	Transform GetEntityTransform ();
	int GetEntityHealth();
}
