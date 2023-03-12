using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RandomNum : MonoBehaviour
{
    public TMP_Text[] bingoTable;
    public Text rightNumShow;
    private int showIndex = 0;
    public int[] randomNum;
    public int[] rightNum;
 
    void Start()
    {
        System.Random random = new System.Random();
        int[] numbers = new int[50];

        for (int i = 0; i < numbers.Length; i++)
        {
            int num;
            do
            {
                num = random.Next(1, 51);
            } while (Array.IndexOf(numbers, num) != -1);
            numbers[i] = num;
        }

        for (int i = 0; i < bingoTable.Length; i++)
        {
            bingoTable[i].text = "" + numbers[i];
            randomNum[i] = numbers[i];
        }

        System.Random randomR = new System.Random();
        int[] numbersR = new int[100];

        for (int i = 0; i < numbersR.Length; i++)
        {
            int num;
            do
            {
                num = randomR.Next(1, 101);
            } while (Array.IndexOf(numbersR, num) != -1);
            numbersR[i] = num;
        }

        for (int i = 0; i < numbersR.Length; i++)
        {
            rightNum[i] = numbersR[i];
        }
    }

    public void randomRightNumber()
    {
        rightNumShow.text = "" + rightNum[showIndex];
        showIndex++;
    }
}
