using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChessPieces : MonoBehaviour
{
    [SerializeField] private Transform[] chessPieces;
    [SerializeField] private Transform[] spawnPos;
    public GameObject winCanvas;
    public FiveMinuteBlitz fiveMinBlitz;

    private int numOfPieces = 32;

    public void resetGame()
    {
        for (int i = 0; i < numOfPieces; i++)
        {
            chessPieces[i].position = spawnPos[i].position;
            chessPieces[i].rotation = spawnPos[i].localRotation;
        }

        if (winCanvas.activeInHierarchy)
        {
            winCanvas.SetActive(false);
            fiveMinBlitz.ResetGame();
        }
    }
}
