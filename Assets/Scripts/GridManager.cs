using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{

    [SerializeField] GameObject[] prefab;
    [SerializeField] GameObject startPrefab;
    [SerializeField] int squareGridDimension = 8;


    public List<Ground> l_ground = new List<Ground>();


    GameObject newSlab;
    private int xMax;
    private int zMax;

    private void Start()
    {
        xMax = squareGridDimension;
        zMax = squareGridDimension;
        GenerateSlabList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NewGridLine();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            NewGridRow();
    }

    public void GenerateSlabList()
    {

        for (int x = 0; x < squareGridDimension; x++)
        {
            for (int z = 0; z < squareGridDimension; z++)
            {
                
                if (x == 0 && z == 0)
                {
                    newSlab = Instantiate(startPrefab, transform);
                }


                else
                {
                var randomSlab = prefab[Random.Range(0, prefab.Length)];
                newSlab = Instantiate(randomSlab, transform);
                l_ground.Add(newSlab.GetComponent<Ground>());
                }

                newSlab.transform.position = new Vector3(x, 0, z);
                newSlab.name = x + " , " + z;

            }
        }
    }

    private void NewGridRow()
    {
        for (int x = 0; x < xMax; x++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
            newSlab.transform.position = new Vector3(x, 0, zMax);
            newSlab.name = x + " , " + zMax;
        }
        zMax++;
    }

    private void NewGridLine()
    {
        for (int z = 0; z < zMax; z++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
            newSlab.transform.position = new Vector3(xMax, 0, z);
            newSlab.name = xMax + " , " + z;
        }
        xMax++;
    }
}
