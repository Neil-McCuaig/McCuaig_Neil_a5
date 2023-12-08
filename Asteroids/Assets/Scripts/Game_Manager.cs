using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;

    public float respawnTime = 3.0f;
    public float respawnInvulnerability = 3.0f;

    public int lives = 3;
    public int score = 0;

    public Text scoreText;
    public Text lifeText;
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        //small, medium, and large asteroids
        if (asteroid.size < 0.75f)
        {
            score += 100;
        }else if (asteroid.size < 1.25f)
        {
            score += 50;
        }
        else
        {
            this.score += 25;
        }
        //This prints the score
        scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        //This updates the life counter
        lifeText.text = lives.ToString();

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerability);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        //This resets your score and lives
        this.lives = 3;
        this.score = 0;
        Invoke(nameof(Respawn), this.respawnTime);

        //These visually reset your score and lives
        scoreText.text = score.ToString();
        lifeText.text = lives.ToString();
    }
}
