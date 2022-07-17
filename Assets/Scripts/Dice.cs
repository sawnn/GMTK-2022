using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    int nb = 1;

    public int life = 50;
    public int heat = 50;

    int steps = 0;

    List<int> horizontalFaces = new List<int> { 1, 5, 6, 2 };

    List<int> verticalFaces = new List<int> { 1, 3, 6, 4 };

    public int speed = 300;
    bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
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
            int last = verticalFaces[verticalFaces.Count - 1];
            verticalFaces.Remove(last);
            verticalFaces.Insert(0, last);
            horizontalFaces[0] = verticalFaces[0];
            horizontalFaces[2] = verticalFaces[2];
            nb = horizontalFaces[0];
            StartCoroutine(Roll(Vector3.back));
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
    }

    IEnumerator CountDiceSteps(int maxSteps)
    {
        steps = 0;
        while (steps < maxSteps)
        {
            Debug.Log(steps);
            yield return null;
        }

        Unblind(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
       // if (isMoving)
            Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Fire"))
        {
            Fire();
        }

        if (collision.gameObject.CompareTag("HealFire"))
        {
            HealFire();
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            Damage();
        }

        if (collision.gameObject.CompareTag("HealDamage"))
        {
            HealDamage();
        }

        if (collision.gameObject.CompareTag("Blind") && collision.gameObject.GetComponent<Ground>().isBlank == false)
        {
            Blind();
        }

        if (collision.gameObject.CompareTag("Unblind"))
        {
            Unblind(true);
        }
    }


    void Fire()
    {
        heat -= nb;
    }

    void HealFire()
    {
        heat += nb;
    }

    void Damage()
    {
        life -= nb;
    }

    void HealDamage()
    {
        life += nb;
    }

    void Blind()
    {
        foreach (var ground in GridManager.Instance.l_ground)
        {
            ground.HideColor();
        }
        StartCoroutine(CountDiceSteps(nb));
    }

    void Unblind(bool bonus)
    {
        StopCoroutine("CountDiceSteps");
        foreach (var ground in GridManager.Instance.l_ground)
        {
            if (bonus && ground.gameObject.CompareTag("Blind"))
            {
                ground.ChangeGreyInWhite();
            }
            else
            {
                ground.RevealColor();
            }
        }
        if (bonus)
        {
            StartCoroutine(CountDiceSteps(nb));
            Debug.Log(nb);
        }
 
    }

}
