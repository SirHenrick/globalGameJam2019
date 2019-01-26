using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    float ppu = 16; // pixels per unit (your tile size)

    private void LateUpdate()
    {
        Vector3 position = transform.localPosition;

        position.x = (Mathf.Round(transform.parent.position.x * ppu) / ppu) - transform.parent.position.x;
        position.y = (Mathf.Round(transform.parent.position.y * ppu) / ppu) - transform.parent.position.y;

        transform.localPosition = position;
    }
}