using UnityEngine;

public class AiResources {
	private int _healthPoints;
	private int _ammo;
	private int _armor;
	private int _armorDeteriorationRate;

	public AiResources (int healthPoints, int ammo, int armor, 
	                    int armorDeteriorationRate) {
		this._healthPoints = healthPoints;
		this._ammo = ammo;
		this._armor = armor;
		this._armorDeteriorationRate = armorDeteriorationRate;
	}

	public void SetHealthPoints(int healthPoints) {
		if(healthPoints > 100) {
			this._healthPoints = 100;
		} else if(healthPoints < 0) {
			this._healthPoints = 0;
		} else {
			this._healthPoints = healthPoints;
		}
	}

	public int GetHealthPoints() {
		return this._healthPoints;
	}

	public void SetAmmo(int ammo) {
		if(ammo > 100) {
			this._ammo = 100;
		} else if(ammo < 10) {
			this._ammo = 0;
		} else {
			this._ammo = _healthPoints;
		}
	}
	
	public int GetAmmo() {
		return this._ammo;
	}

	public void SetArmor(int armor) {
		if(armor > 10) {
			this._armor = 10;
		} else if(armor < 0) {
			this._armor = 0;
		} else {
			this._armor = armor;
		}
	}
	
	public int GetArmor() {
		return this._armor;
	}

	public void Damage(float amount) {
		this._healthPoints -= Mathf.CeilToInt(amount/_armor);
		this._armor -= _armorDeteriorationRate;

		if(this._healthPoints < 0) {
			this._healthPoints = 0;
		}

		if(this._armor < 1) {
			this._armor = 1;
		}
	}
}