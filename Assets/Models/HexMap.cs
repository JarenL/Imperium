using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject HexPrefab;

    public Material[] HexMaterials;

    public readonly int mapHeight = 12500;
    public readonly int mapWidth = 25000;

    public void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Hex newHex = new Hex(x, y);
                Vector3 position = newHex.PositionFromCamera(
                    Camera.main.transform.position,
                    mapHeight,
                    mapWidth
                );

                GameObject hexGameObject = (GameObject) Instantiate(
                    HexPrefab,
                    position,
                    Quaternion.identity,
                    this.transform
                );
                hexGameObject.GetComponent<HexComponent>().Hex = newHex;
                hexGameObject.GetComponent<HexComponent>().hexMap = this;

                MeshRenderer meshRenderer = hexGameObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = HexMaterials[Random.Range(0, HexMaterials.Length)
                ];
            }
        }
        //StaticBatchingUtility.Combine(this.gameObject);
    }
}
