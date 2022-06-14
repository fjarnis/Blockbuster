using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public EnemyProducer enemyProducer;
	public GameObject playerPrefab;
	public Text winText;
	public Text countText;
	public Text healthPoints;
	public Text youLose;
	public int enemyDeaths;
	public GameObject button;
	public AudioSource deathSound;

	void Start() {
		enemyDeaths = 0;
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.onPlayerDeath += onPlayerDeath;
		countText.text = GetKillCountText();
		updateHealthPoints();
	}

	public void updateHealthPoints() {
		healthPoints.text = $@"Health: {(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health)}";
	}

	void onPlayerDeath(Player player) {
		enemyProducer.SpawnEnemies(false);
		youLose.text = "You lose!";
		Destroy(player.gameObject);
	}

	void StopEnemies() {
		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemies)
		{
			Destroy(enemy);
		}
		var enemyProducer = GameObject.Find("EnemyProducer");
		Destroy(enemyProducer);
	}
	
	public void OnEnemyDeath() {
		enemyDeaths++;
		countText.text = GetKillCountText();
		
		if (enemyDeaths >= 10) {
			winText.text = "Filip WINS!";
			button.SetActive(true);
			StopEnemies();
		}
	}
	
	string GetKillCountText() {
		return "Kill count: " + enemyDeaths;
	}

	public void deathSoundFunction()
    {
		deathSound = GetComponent<AudioSource>();
		deathSound.Play(0);
    }
}