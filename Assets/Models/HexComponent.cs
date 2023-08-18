using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour
{
    public Hex Hex;
    public HexMap hexMap;

    public void UpdatePosition()
    {
        this.transform.position = Hex.PositionFromCamera(
            Camera.main.transform.position,
            hexMap.mapHeight,
            hexMap.mapWidth
        );
    }
}
