using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public bool active; //is this pool object active or not?

    [HideInInspector]
    public PoolObjectItem.ePoolItem poolKey;

    // Disables a pool object.
    public void DisablePoolObject()
    {
        this.active = false;
        this.gameObject.SetActive(false);
    }

    // Enables a pool object.
    public void ActivatePoolObject()
    {
        this.active = true;
        this.gameObject.SetActive(true);
    }
}