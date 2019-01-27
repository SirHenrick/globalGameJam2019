using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWave : MonoBehaviour
{
    Vector2 initialPosition;
    float angle = 0;

    public float waveSpeed = 5;
    public float waveStrength = 0.08f;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        angle += waveSpeed * Time.deltaTime;
        if (angle > 360) angle -= 360;
        transform.localPosition = new Vector2(initialPosition.x, initialPosition.y + Mathf.Sin(angle) * waveStrength);
    }
}
