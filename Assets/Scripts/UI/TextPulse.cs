using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPulse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float pulse = ((Mathf.Sin(Time.time) / 4f) + 1);
        transform.localScale = new Vector3(pulse, pulse, pulse);
    }
}
