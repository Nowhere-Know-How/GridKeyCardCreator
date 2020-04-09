using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class KeyCard
{
    public int id;
    public CardColor firstToMove;
    public List<CardColor> data;

    public int redCount = 0;
    public int blueCount = 0;
    public int blackCount = 0;
    public int size = 25;

    public KeyCard(int id, int size = 25)
    {
        this.id = id;
        this.size = size;

        data = new List<CardColor>(size);
        for (int i = 0; i < size; i++)
        {
            data.Add(CardColor.Brown);
        }
    }

    public int GetCount(CardColor color)
    {
        UpdateCounts();
        if (color == CardColor.Red)
            return redCount;
        else if (color == CardColor.Blue)
            return blueCount;
        else if (color == CardColor.Black)
            return blackCount;
        else if (color == CardColor.Brown)
            return size - redCount - blueCount - blackCount;
        else
            return -1;
    }

    public void UpdateCounts()
    {
        redCount = 0;
        blueCount = 0;
        blackCount = 0;
        for (int i = 0; i < size; i++)
        {
            if (data[i] == CardColor.Red)
            {
                redCount += 1;
            }
            else if (data[i] == CardColor.Blue)
            {
                blueCount += 1;
            }
            else if (data[i] == CardColor.Black)
            {
                blackCount += 1;
            }
        }
    }
    public void SetFirstToMove(CardColor color)
    {
        firstToMove = color;
    }

    public CardColor GetFirstToMove()
    {
        return firstToMove;
    }

    public void SetMap(int index, CardColor color)
    {
        data[index] = color;
    }


    public CardColor GetMap(int index)
    {
        return data[index];
    }


    public string ToString()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }


}
