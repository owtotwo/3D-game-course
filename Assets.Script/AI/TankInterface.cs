using UnityEngine;

public class TankController : IEntityInterface {
	private Transform _transform;
	private Transform _playerTransform;
	
	public TankController(Transform transform, Transform playerTransform) {
		this._transform = transform;
		this._playerTransform = playerTransform;
	}
	
	public void SetEntityLocation(Vector3 location, float speed) {   
		location.y = _transform.position.y;
		this._transform.LookAt (2 * this._transform.position - location);
		this._transform.GetComponent<Rigidbody>().velocity = (this._transform.forward * -1 * speed);
	}
	
	public Vector3 GetEntityLocation() {
		return this._transform.position;
	}

	public Transform GetEntityTransform(){
		return this._transform;
	}

	public float GetEntityRotation() {
		return this._transform.rotation.eulerAngles.y;
	}
	
	public Vector3 GetPlayerLocation() {
		return _playerTransform.position;
	}

	public Vector3 GetEntityForward() {
		return this._transform.forward.normalized;
	}

	public void SetEntityRotation(Vector3 location) {
		this._transform.LookAt (2 * this._transform.position - location);
	} 

	public void SetEntityRotation(float rotation) {
		this._transform.Rotate(Vector3.up * rotation);
	}

	public int GetEntityHealth(){
		return GetEntityTransform().GetComponent<Tank>().GetHealth();
	}
}