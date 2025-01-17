using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{

    [SerializeField] GameObject[] prefab;
    [SerializeField] GameObject startPrefab;
    [SerializeField] int squareGridDimension = 8;


    public List<List<Ground>> l_ground = new List<List<Ground>>();


    GameObject newSlab;
    private int xMax;
    private int zMax;

    int[] countPrefab = { 0, 0, 0, 0, 0, 0 };
    int[] maxPrefab = { 13, 13, 13, 8, 8, 9};
    string[] namePrefab = { };


    private void Start()
    {
        xMax = squareGridDimension;
        zMax = squareGridDimension;
        GenerateSlabList();
     
    }

    private void Update()
    {
      
    }

    public void StartRemove()
    {
        StartCoroutine(RemoveGround(0));
    }

    public void GenerateSlabList()
    {

        for (int x = 0; x < squareGridDimension; x++)
        {
            l_ground.Add(new List<Ground>());
            for (int z = 0; z < squareGridDimension; z++)
            {
                
                if (x == 0 && z == 0)
                {
                    newSlab = Instantiate(startPrefab, transform);
                }


                else
                {
                    int random = Random.Range(0, prefab.Length);
                    do 
                    {
                        random = Random.Range(0, prefab.Length);
                    } while (countPrefab[random] >=  maxPrefab[random]);
                    countPrefab[random]++;
                    var randomSlab = prefab[random];
                    newSlab = Instantiate(randomSlab, transform);
              
                }
                l_ground[x].Add(newSlab.GetComponent<Ground>());
                newSlab.transform.position = new Vector3(x, 0, z);
                newSlab.name = x + " , " + z;

            }
        }
    }

    private void NewGridRow(int nb)
    {
        for (int x = nb; x < xMax; x++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
            newSlab.transform.position = new Vector3(x, 0, zMax);
            newSlab.name = x + " , " + zMax;
            l_ground[x].Add(newSlab.GetComponent<Ground>());
        }
        zMax++;
    }

    private void NewGridLine(int nb)
    {
        l_ground.Add(new List<Ground>());
        for (int z = 0; z < zMax; z++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
            newSlab.transform.position = new Vector3(xMax, 0, z);
            newSlab.name = xMax + " , " + z;
            l_ground[xMax].Add(newSlab.GetComponent<Ground>());
        }
        xMax++;
    }


    IEnumerator RemoveGround(int nb)
    {
        yield return new WaitForSeconds(1f);
        //code here will execute after 5 seconds
        for (int i = 0; i < zMax; i++)
        {
           // Debug.Log(i + " " + (0 + nb));
            if (l_ground[0 + nb][i] != null)
            {
                Destroy(l_ground[0 + nb][i].gameObject);
                l_ground[0 + nb][i] = null;
            }

        }
  
    
        NewGridLine(nb + 1); 
       // NewGridRow(nb + 1);
        StartCoroutine(RemoveGround(nb + 1));
        
    }



   /*private void NewGridRow(int nb)
    {
        int i = 0;
        for (int x = nb; x < squareGridDimension; x++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
           // Debug.Log(x + " " + zMax);
            newSlab.transform.position = new Vector3(x, 0, zMax);
            newSlab.name = x + " , " + zMax;
            l_ground[i][7] = newSlab.GetComponent<Ground>();
            i++;
        }
        zMax++;
    }

    private void NewGridLine(int nb)
    {
        int i = 0;
        l_ground.Add(new List<Ground> { null, null, null, null, null, null, null, null });
        for (int z = nb; z < zMax; z++)
        {
            var randomSlab = prefab[Random.Range(0, prefab.Length)];
            newSlab = Instantiate(randomSlab, transform);
          //  Debug.Log(xMax + " " + z +  " " + (z - nb));
            newSlab.transform.position = new Vector3(xMax, 0, z);
            newSlab.name = xMax + " , " + z;
           // l_ground[xMax].Add(newSlab.GetComponent<Ground>());
            l_ground[7][i] = newSlab.GetComponent<Ground>();
            i++;
        }
        xMax++;
    }


    IEnumerator RemoveGround(int nb)
    {
        yield return new WaitForSeconds(2f);
        //code here will execute after 5 seconds
        for (int i = 0; i < squareGridDimension; i++)
        {

            if (l_ground[0][i] != null)
            {
                //  Debug.Log((0 + nb) + " " + i);
                Destroy(l_ground[0][i].gameObject);
                l_ground[0][i] = null;
            }

        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < squareGridDimension; i++)
        {
            Debug.Log(i + " " + (0 + nb) + "  " + squareGridDimension + nb);
            if (l_ground[i][0] != null)
            {
             //   Debug.Log("REAL " + i + " " + (0) + " xmax " + zMax);
                Destroy(l_ground[i][0].gameObject);
                l_ground[i][0] = null;
            }
        }
        yield return new WaitForSeconds(2f);
        NewGridRow(nb + 1);
        yield return new WaitForSeconds(2f);
        NewGridLine(nb + 1);
        StartCoroutine(RemoveGround(nb + 1));

    }*/
}
