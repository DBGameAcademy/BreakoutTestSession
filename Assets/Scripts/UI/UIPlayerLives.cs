using System.Collections.Generic;
using UnityEngine;

public class UIPlayerLives : MonoBehaviour
{
    public List<GameObject> LivesMarker = new List<GameObject>();

    public void UpdateLives(int _livesRemaining)
    {
        for (int i = 0; i < LivesMarker.Count; i++)
        {
            LivesMarker[i].SetActive(i < _livesRemaining);
        }
    }
}