using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Object")]
public class AudioObject : ScriptableObject
{
    [Header("Audio Object")]
    public List<AudioClip> ClipList = new List<AudioClip>();
    [Range(0, 1)]
    public float Volume = 1.0f;
    public bool Looping = false;

    public bool HasClip { get { return ClipList.Count > 0; } }

    public AudioClip GetClip()
    {
        if (ClipList.Count > 1)
        {
            return ClipList[Random.Range(0, ClipList.Count)];
        }
        else if (ClipList.Count > 0)
        {
            return ClipList[0];
        }
        return null;
    }

    public AudioClip GetClipByIndex(int index)
    {
        if (ClipList.Count == 0)
            return null;

        if (index < 0)
            return ClipList[0];

        if (index >= ClipList.Count)
            return ClipList[ClipList.Count - 1];

        return ClipList[index];
    }
}