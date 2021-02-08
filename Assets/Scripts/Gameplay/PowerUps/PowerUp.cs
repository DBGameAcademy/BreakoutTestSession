using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectable
{
    // how fast the object falls down the screen
    float speed = 5f;

    // type of powerup
    public enum ePowerUpType
    {
        Wide,
        Thin,
        Gun,
        ExtraLife
    }
    public ePowerUpType PowerUpType;

    public GameObject WideSprite;
    public GameObject ThinSprite;
    public GameObject GunSprite;
    public GameObject ExtraLifeSprite;

    private void SetupPowerUp(ePowerUpType _type)
    {
        PowerUpType = _type;

        WideSprite.SetActive(false);
        ThinSprite.SetActive(false);
        GunSprite.SetActive(false);
        ExtraLifeSprite.SetActive(false);

        switch (PowerUpType)
        {
            case ePowerUpType.Wide: WideSprite.SetActive(true);
                break;

            case ePowerUpType.Thin: ThinSprite.SetActive(true);
                break;

            case ePowerUpType.Gun: GunSprite.SetActive(true);
                break;

            case ePowerUpType.ExtraLife: ExtraLifeSprite.SetActive(true);
                break;
        }
    }

    public void OnPickup()
    {
        PlayerController.Instance.CollectPowerUp(PowerUpType);
        PoolManager.Instance.ReturnObjectToPool(gameObject);
    }

    // Update is called once per frame
    public void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
