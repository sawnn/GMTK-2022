using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{

    [SerializeField] GameObject[] prefab;
    [SerializeField] int gridWidth = 20;
    [SerializeField] int gridHeight = 20;
    [SerializeField] float tileSize = 1f;

    public List<Ground> l_ground = new List<Ground>();

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        float posX = 0;
        float posZ = 0;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                var randomTile = prefab[Random.Range(0, prefab.Length)];
                GameObject newTile = Instantiate(randomTile, transform);
                l_ground.Add(newTile.GetComponent<Ground>());


                posZ += tileSize;

                newTile.transform.position = new Vector3(posX, 0, posZ);
                newTile.name = x + " , " + z;
                
            }
            posX = (x * tileSize);
            posZ = 0;
        }
    }
}
