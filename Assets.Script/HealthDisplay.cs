using UnityEngine;

public class HealthDisplay : MonoBehaviour {

	private TextMesh _showhealth;
	private GameObject _cam;
	
	void Start () {
		_showhealth = gameObject.GetComponent<TextMesh>();
		_cam = GameObject.Find("Main Camera") as GameObject;
	}
	
	void Update () {
		transform.rotation = _cam.transform.rotation;
	}

	public void UpdateHealth(int health){
		_showhealth.text = health.ToString ();
		_showhealth.color = Color.Lerp(Color.red, Color.green, health/100f);
	}
}
