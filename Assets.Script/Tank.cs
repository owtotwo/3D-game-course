using System;
using UnityEngine;

public class Tank : MonoBehaviour {

	private HealthDisplay _showhealth;

	void Start () {
		_showhealth = gameObject.transform.GetComponentsInChildren<HealthDisplay> ()[0];
	}
	
	void Update () {
		if (gameObject.tag != "Player") {
		    if (gameObject.GetComponent<EnemyTankAi>() == null)
		    {
                throw new Exception("Null!!!!!!!!!!!!!!!!");
		    } else
			_showhealth.UpdateHealth(gameObject.GetComponent<EnemyTankAi>().GetAiStats().GetHealthPoints());
		}
	}

	public void Hurt (float amount){
		EnemyTankAi ai = gameObject.GetComponent<EnemyTankAi> ();
		if(ai != null) {
			ai.GetAiStats ().Damage (amount);
		    if (ai.GetAiStats().GetHealthPoints() == 0)
		    {
		        Die();
		    }
		}
	}

	public int GetHealth(){
		EnemyTankAi ai = gameObject.GetComponent<EnemyTankAi> ();
		return ai.GetAiStats ().GetHealthPoints ();
	}

	private void Die(){
		Destroy (gameObject);
	}
}
