using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDestroyable
{
    public List<PowerUp> PowerUps = new List<PowerUp>();

    public int ScoreValue = 100;

    public int Hits = 1;
    public void OnHit()
    {
        Hits--;
        if (Hits <= 0)
            OnDestroy();
    }
    public void OnDestroy()
    {
        gameObject.SetActive(false);

        //random chance at powerup
        if (Random.Range(0, 100) < 10)
        {
        //    PoolManager.Instance.GetObjectFromPool(PoolManager.Instance.ePoolItem.PowerUp);
        }

        // update score
        PlayerController.Instance.UpdateScore(ScoreValue);
    }

    void SpawnPowerUp()
    {
        GameObject newPowerup = new GameObject("Powerup");
        newPowerup.transform.position = transform.position;
        PowerUp powerUp = newPowerup.AddComponent<PowerUp>();
        powerUp.PowerUpType = (PowerUp.ePowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUp.ePowerUpType)).Length);
    }
}
