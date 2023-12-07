using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;

    public float respawnTime = 3.0f;
    public float respawnInvulnerability = 3.0f;

    public int lives = 3;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

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

    }
}
