using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
	public int health = 3;
	public event Action<Player> onPlayerDeath;

	void collidedWithEnemy(Enemy enemy) {
		enemy.Attack(this);
		var gameControllerScript = FindObjectOfType<GameController>();
		gameControllerScript.GetComponent<GameController>().updateHealthPoints();
		if (health <= 0) {
			if(onPlayerDeath != null) {
			onPlayerDeath(this);
			gameControllerScript.button.SetActive(true);
			}
		}
	}

	void OnCollisionEnter (Collision col) {
		Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
			if(enemy) {
			collidedWithEnemy(enemy);
			}
	}
}
