using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float VerticalSpeed; 
	public float RotationalSpeed; 
	public GameObject NormalProjectile; 
	public GameObject SpecialProjectile; 
	public int SpecialProjectileAmmo; 
	public bool IsTouchingGround;
	
	private Vector3 _rotationVector;
	private Quaternion _rotationQuaternion;
	private bool _thirdPersonMode;
	
	void Start () {
		_thirdPersonMode = true;
	}
	
	
	void Update () {
		if(IsTouchingGround){
			if (Input.GetAxis ("Vertical") > 0) {
				
				GetComponent<Rigidbody>().velocity = (transform.forward * VerticalSpeed);
			} else if (Input.GetAxis ("Vertical") < 0) {
				GetComponent<Rigidbody>().velocity = (transform.forward * (-1 * VerticalSpeed));
			} else {
				GetComponent<Rigidbody>().velocity = (transform.forward * 0f);
			}
		}
		
		if (Input.GetAxis ("Horizontal") > 0) {
			_rotationVector = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + RotationalSpeed), transform.eulerAngles.z);
			_rotationQuaternion = Quaternion.Euler(_rotationVector);
			transform.localRotation = (_rotationQuaternion);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			_rotationVector = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y - RotationalSpeed), transform.eulerAngles.z);
			_rotationQuaternion = Quaternion.Euler(_rotationVector);
			transform.localRotation = (_rotationQuaternion);
		} else {
			Vector3 rotationVector = new Vector3(0f,0f,0f);
			transform.Rotate(rotationVector);
		}
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot (NormalProjectile, 1000f);
		} else if (Input.GetKeyDown (KeyCode.Tab) && SpecialProjectileAmmo > 0) {
			Shoot(SpecialProjectile, 1000f);
			SpecialProjectileAmmo--;
		}
		
		if (Input.GetMouseButtonDown (0)) {
			Shoot (NormalProjectile, 1000f);		
		} else if (Input.GetMouseButtonDown (1) && SpecialProjectileAmmo > 0) {
			Shoot (SpecialProjectile, 1000f);
			SpecialProjectileAmmo--;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			_thirdPersonMode = !_thirdPersonMode;
			GameObject.Find("TankPlayer/Turret/Sphere/FPSCameraLocation/BarrelCamera").GetComponent<Camera>().gameObject.SetActive(!_thirdPersonMode);
			GameObject.Find("TankPlayer/MainCamera").GetComponent<Camera>().gameObject.SetActive(_thirdPersonMode);
		}

		Vector3 rotationVectorTemp = transform.rotation.eulerAngles;
	    rotationVectorTemp.x = rotationVectorTemp.z = 0;
		transform.rotation = Quaternion.Euler(rotationVectorTemp);
	}
	
	private void Shoot(GameObject projectileType, float power){
		Transform barrel = gameObject.transform.GetChild(3).GetChild(0).GetChild(0);
		GameObject projectile = Instantiate (projectileType, barrel.position + barrel.up.normalized * -2f, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody>().AddForce ((-barrel.up).normalized * power);
	}

	public void TouchingGround(bool correct){
		IsTouchingGround = correct;
	}
}
