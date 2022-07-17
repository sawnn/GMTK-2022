using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{

    int nb = 1;

    public int life = 15;
    public int heat = 0;
    public int maxHeat = 15;


    int steps = 0;
    public int score = 0;

    List<int> horizontalFaces = new List<int> { 1, 5, 6, 2 };

    List<int> verticalFaces = new List<int> { 1, 3, 6, 4 };

    public int speed = 300;
    bool isMoving = false;


    //UI Manon
    public FireBar fireBar;
    public HealthBar healthBar;

    //pas très propre, il vaut mieux utiliser des listes ou tableaux mais la flemme on a pas le time
    public Image currentTile;
    public Sprite red;
    public Sprite blue;
    public Sprite purple;
    public Sprite green;
    public Sprite gray;
    public Sprite white;
    public Sprite gold;
    public Sprite start;

    public Image currentDiceFace;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;

    //GameOver Manon
    public GameOver gameOver;

    void Start()
    {
        fireBar.SetMinFire();
        healthBar.SetMaxHealth(life);
        currentTile.GetComponent<Image>().sprite = start;
    }

    void Update()
    {
        if (isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            score++;
            int last = horizontalFaces[horizontalFaces.Count - 1];
            horizontalFaces.Remove(last);
            horizontalFaces.Insert(0, last);
            verticalFaces[0] = horizontalFaces[0];
            verticalFaces[2] = horizontalFaces[2];
            nb = horizontalFaces[0];
            StartCoroutine(Roll(Vector3.right));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            score++;
            int first = horizontalFaces[0];
            horizontalFaces.RemoveAt(0);
            horizontalFaces.Add(first);
            verticalFaces[0] = horizontalFaces[0];
            verticalFaces[2] = horizontalFaces[2];
            nb = horizontalFaces[0];
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            score++;
            int first = verticalFaces[0];
            verticalFaces.RemoveAt(0);
            verticalFaces.Add(first);
            horizontalFaces[0] = verticalFaces[0];
            horizontalFaces[2] = verticalFaces[2];
            nb = horizontalFaces[0];
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            score++;
            int last = verticalFaces[verticalFaces.Count - 1];
            verticalFaces.Remove(last);
            verticalFaces.Insert(0, last);
            horizontalFaces[0] = verticalFaces[0];
            horizontalFaces[2] = verticalFaces[2];
            nb = horizontalFaces[0];
            StartCoroutine(Roll(Vector3.back));
        }

        switch (nb)
        {
            case 1:
                currentDiceFace.GetComponent<Image>().sprite = one;
                break;
            case 2:
                currentDiceFace.GetComponent<Image>().sprite = two;
                break;
            case 3:
                currentDiceFace.GetComponent<Image>().sprite = three;
                break;
            case 4:
                currentDiceFace.GetComponent<Image>().sprite = four;
                break;
            case 5:
                currentDiceFace.GetComponent<Image>().sprite = five;
                break;
            case 6:
                currentDiceFace.GetComponent<Image>().sprite = six;
                break;
        }
   
    }

    

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;
        steps++;
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }
        isMoving = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator CountDiceSteps(int maxSteps)
    {
        steps = 0;
        while (steps < maxSteps)
        {
            yield return null;
        }

        Unblind(false);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        gameOver.GameOverScreen();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Fire"))
        {
            Fire();
            currentTile.GetComponent<Image>().sprite = red; //ajout Manon
        }

        if (collision.gameObject.CompareTag("HealFire"))
        {
            HealFire();
            currentTile.GetComponent<Image>().sprite = blue; //ajout Manon
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            Damage();
            currentTile.GetComponent<Image>().sprite = purple; //ajout Manon
        }

        if (collision.gameObject.CompareTag("HealDamage"))
        {
            HealDamage();
            currentTile.GetComponent<Image>().sprite = green; //ajout Manon
        }

        if (collision.gameObject.CompareTag("Blind") && collision.gameObject.GetComponent<Ground>().isBlank == false)
        {
            Blind();
            currentTile.GetComponent<Image>().sprite = gray; //ajout Manon
        }

        if (collision.gameObject.CompareTag("Unblind"))
        {
            Unblind(true);
            currentTile.GetComponent<Image>().sprite = gold; //ajout Manon
        }

        if (collision.gameObject.GetComponent<Ground>().isBlank == true) //ajout Manon
        {
            currentTile.GetComponent<Image>().sprite = white;
        }
    }


    void Fire()
    {
        heat = Mathf.Min(heat + nb, maxHeat); //ajout Manon
        fireBar.SetFire(heat);
        if (heat == maxHeat)
        {
            gameOver.GameOverScreen();
        }
    }

    void HealFire()
    {
        heat = Mathf.Max(heat - nb, 0); //ajout Manon
        fireBar.SetFire(heat);
    }

    void Damage()
    {
        life = Mathf.Max(life - nb, 0); //ajout Manon
        healthBar.SetHealth(life);

        if (life == 0)
        {
            gameOver.GameOverScreen();
        }
    }

    void HealDamage()
    {
        life = Mathf.Min(life + nb, 15); //ajout Manon
        healthBar.SetHealth(life);
    }

  void Blind()
    {
        foreach (var ground in GridManager.Instance.l_ground)
        {
            foreach (var item in ground)
            {
                if (item != null)
                {
                    item.HideColor();
                }
            }
            
        }
        StartCoroutine(CountDiceSteps(nb));
    }

    void Unblind(bool bonus)
    {
        StopCoroutine("CountDiceSteps");
        foreach (var ground in GridManager.Instance.l_ground)
        {
            foreach (var item in ground)
            {
                if (item != null)
                {
                    if (bonus && item.gameObject.CompareTag("Blind"))
                    {
                        item.ChangeGreyInWhite();
                    }
                    else
                    {
                        item.RevealColor();
                    }
                }
            }
           
        }
        if (bonus)
        {
            StartCoroutine(CountDiceSteps(nb));
        }
 
    }

}
