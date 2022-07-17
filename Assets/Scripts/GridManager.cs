using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{

    [SerializeField] GameObject[] prefab;
    [SerializeField] GameObject startPrefab;
    [SerializeField] int squareGridDimension = 8;


    public List<Ground> l_ground = new List<Ground>();

    private void Start()
    {
        GenerateSlabList();
        //CreateGrid();
    }

    private void GenerateSlabList()
    {



        for (int x = 0; x < squareGridDimension; x++)
        {
            for (int z = 0; z < squareGridDimension; z++)
            {
                GameObject newSlab;
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

    private void CreateGrid()
    {

    }

}
