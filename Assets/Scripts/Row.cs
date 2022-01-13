using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    private float timeInterval;

    public bool rowStopped;

    void Start()
    {
        rowStopped = true;
    }

    public void StartRotating()
    {
        rowStopped = false;
        StartCoroutine("Rotate");
    }

    public string GetNameSlotByCoordY(float coordY)
    {
        /*for(int i = 0; i < transform.childCount; i++) {
             Transform childPosition = transform.GetChild(i).GetComponent<Transform>();


             if(childPosition.position.y == coordY)
             {
                return transform.GetChild(i).name;
             }

         }*/

        foreach (Transform child in transform)
        {
            if(child.position.y == coordY)
            {
                return child.name;
            }
        }

            return "";
    }

    private IEnumerator Rotate()
    {
        timeInterval = 0.05f;
        int countRoll = Random.Range(100, 200);


        for (int c_r = 0; c_r < countRoll; c_r++)
        {

            /*for (int i = 0; i < transform.childCount; i++)
            {
                Transform childPosition = transform.GetChild(i).GetComponent<Transform>();

                if (childPosition.position.y <= -6f)
                    childPosition.position = new Vector2(transform.position.x, 6f);

                childPosition.position = new Vector2(transform.position.x, childPosition.position.y - 2f);
            }*/

            Vector3 slotPosition;
            foreach (Transform child in transform)
            {
                slotPosition = child.position;

                if (slotPosition.y <= -6f)
                {
                    slotPosition.y = 6f;
                    child.position = slotPosition;
                }

                child.Translate(Vector3.up * -2f);
            }

            yield return new WaitForSeconds(timeInterval);
        }

        rowStopped = true;
    }

}
