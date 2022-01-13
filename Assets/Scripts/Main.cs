using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Main : MonoBehaviour
{
    
    private const float topSlot = 4f;
    private const float secondSlot = 2f;
    private const float middleSlot = 0f;
    private const float penultSlot = -2f;
    private const float lastSlot = -4f;
    

    [SerializeField]
    private Button spin;

    [SerializeField]
    private InputField bet;

    [SerializeField]
    private InputField lines;

    [SerializeField]
    private Text winValue;

    private float[,] winCombinationList = {
        {topSlot, topSlot, topSlot, topSlot, topSlot},
        {secondSlot, secondSlot, secondSlot, secondSlot, secondSlot},
        {middleSlot, middleSlot, middleSlot, middleSlot, middleSlot},
        {penultSlot, penultSlot, penultSlot, penultSlot, penultSlot},
        {lastSlot, lastSlot, lastSlot, lastSlot, lastSlot},
        {topSlot, secondSlot, middleSlot, penultSlot, lastSlot},
        {lastSlot, penultSlot, middleSlot, secondSlot, topSlot},
        {lastSlot, penultSlot, middleSlot, penultSlot, lastSlot},
        {penultSlot, middleSlot, secondSlot, middleSlot, penultSlot},
        {topSlot, secondSlot, middleSlot, secondSlot, topSlot},
        {secondSlot, middleSlot, penultSlot, middleSlot, secondSlot},
        {penultSlot, lastSlot, lastSlot, penultSlot, penultSlot},
        {secondSlot, middleSlot, middleSlot, secondSlot, secondSlot},
        {secondSlot, topSlot, topSlot, secondSlot, secondSlot},
        {secondSlot, middleSlot, middleSlot, penultSlot, lastSlot},
        {penultSlot, middleSlot, secondSlot, secondSlot, topSlot},
        {lastSlot, penultSlot, middleSlot, middleSlot, secondSlot},
        {secondSlot, middleSlot, penultSlot, penultSlot, lastSlot},
        {topSlot, secondSlot, middleSlot, middleSlot, penultSlot},
        {penultSlot, lastSlot, penultSlot, lastSlot, penultSlot},
        {secondSlot, middleSlot, secondSlot, middleSlot, secondSlot},
        {middleSlot, secondSlot, middleSlot, secondSlot, middleSlot},
        {topSlot, secondSlot, lastSlot, secondSlot, topSlot},
        {lastSlot, penultSlot, topSlot, penultSlot, lastSlot},
        {penultSlot, middleSlot, penultSlot, middleSlot, penultSlot}
    };
    // 25

    [SerializeField]
    private Row[] rows;

    private float factor = 1.3f;

    private int prize;
    private bool rezultsChecked = true;
    private bool startGame = false;

    void Update()
    {
        if (startGame) {
            int countStoppedRows = 0;
            foreach (Row row in rows)
            {
                if (row.rowStopped)
                {
                    countStoppedRows++;
                }
            }
            if (countStoppedRows == rows.Length && !rezultsChecked)
            {
                CheckResults();
                winValue.text = "WIN: " + prize.ToString();
                winValue.gameObject.SetActive(true);
                spin.interactable = true;
                rezultsChecked = true;
                lines.enabled = true;
                bet.enabled = true;
                startGame = false;
            }
        }
    }

    public void StartRotating() {

        startGame = true;
        spin.interactable = false;
        rezultsChecked = false;
        winValue.gameObject.SetActive(false);
        lines.enabled = false;
        bet.enabled = false;

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].StartRotating();
        }
        
    }

    private void CheckResults() 
    {
        int countRows = winCombinationList.GetUpperBound(0) + 1;
        int countColumns = winCombinationList.Length / countRows;
        string now_slot = "";
        string prev_slot = "";
        int combination = 1;

        for(int i = 0; i < countRows; i++)
        {
            for(int j = 1; j < countColumns; j++)
            {
                /*now_slot = rows[j].GetNameSlotByCoordY(winCombinationList[i, j]);
                prev_slot = rows[j - 1].GetNameSlotByCoordY(winCombinationList[i, j - 1]);*/
                if(j == 1)
                foreach (Transform slot in rows[j].GetComponent<Transform>())
                {
                    Debug.Log(slot.tag);
                }

                if(prev_slot == now_slot)
                {
                    combination++;
                }
                else
                {
                    break;
                }
            }

            if(combination == 5) 
            {
                prize = Convert.ToInt32(bet.text) / Convert.ToInt32(lines.text);
                prize = Convert.ToInt32((float)prize * factor);
                break;
            }

            combination = 1;
        }

    }

}
