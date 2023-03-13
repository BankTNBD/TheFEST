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
    private int[] bingoBoard = new int[25] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
        bool correctNum = false;
        int indexN = 0;
        rightNumShow.text = "" + rightNum[showIndex];
        for (int i = 0; i < bingoTable.Length; i++)
        {
            if (bingoTable[i].text == "" + rightNum[showIndex])
            {
                indexN = i;
                correctNum = true;
                break;
            }
        }
        if (correctNum)
        {
            bingoTable[indexN].color = new Color(1f, 0f, 0f);
            bingoBoard[indexN] = -1;
        }
        if (IsBingoPattern())
        {
            Debug.Log("Bingo");
        }
        showIndex++;
    }

    bool IsBingoPattern()
    {
        // Check for horizontal Bingo pattern
        for (int i = 0; i <= 20; i += 5)
        {
            if (bingoBoard[i] == -1 && bingoBoard[i + 1] == -1 && bingoBoard[i + 2] == -1 && bingoBoard[i + 3] == -1 && bingoBoard[i + 4] == -1)
            {
                return true;
            }
        }

        // Check for vertical Bingo pattern
        for (int i = 0; i <= 4; i++)
        {
            if (bingoBoard[i] == -1 && bingoBoard[i + 5] == -1 && bingoBoard[i + 10] == -1 && bingoBoard[i + 15] == -1 && bingoBoard[i + 20] == -1)
            {
                return true;
            }
        }

        // Check for diagonal Bingo pattern (top-left to bottom-right)
        if (bingoBoard[0] == -1 && bingoBoard[6] == -1 && bingoBoard[12] == -1 && bingoBoard[18] == -1 && bingoBoard[24] == -1)
        {
            return true;
        }

        // Check for diagonal Bingo pattern (top-right to bottom-left)
        if (bingoBoard[4] == -1 && bingoBoard[8] == -1 && bingoBoard[12] == -1 && bingoBoard[16] == -1 && bingoBoard[20] == -1)
        {
            return true;
        }

        return false; // No Bingo pattern was found
    }
}
