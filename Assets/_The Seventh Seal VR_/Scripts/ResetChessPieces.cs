using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChessPieces : MonoBehaviour
{
    [SerializeField] private Transform[] chessPieces;
    [SerializeField] private Transform[] spawnPos;

    private int numOfPieces = 32;

    public void resetGame()
    {
        for (int i = 0; i < numOfPieces; i++)
        {
            chessPieces[i].position = spawnPos[i].position;
            chessPieces[i].rotation = spawnPos[i].localRotation;
        }
    }
}
