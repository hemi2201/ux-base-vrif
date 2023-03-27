using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameManager
{
    public class FiveMinuteBlitz : MonoBehaviour
    {
        public Transform minRotationPoint1;
        public Transform minRotationPoint2;
        public bool player1Turn;
        public bool allowCount = true;
        private int player1ButtonCount = 0;
        public bool player2Turn;
        private float degreePerSecond = 0.1f;
        private float remainingDegrees1 = -30;
        private float remainingDegrees2 = -30;
        public GameObject winCanvas;
        public GameObject instructionCanvas;
        public AudioSource tickingClock;
        public AudioSource alarm;
        public AudioClip alarmClip;
        public VirtualWorldManager vwm;
        public GameObject player1Active;
        public GameObject player2Active;

        // Start is called before the first frame update
        void Start()
        {
            player1Turn = false;
            player2Turn = false;

            ResetGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (player1Turn && remainingDegrees1 < 0)
            {
                remainingDegrees1 += degreePerSecond * Time.deltaTime;
                Debug.Log(remainingDegrees1);
                minRotationPoint1.Rotate(0, 0, degreePerSecond * Time.deltaTime);
            }
            else if (player2Turn && remainingDegrees2 < 0)
            {
                remainingDegrees2 += degreePerSecond * Time.deltaTime;
                Debug.Log(remainingDegrees2);
                minRotationPoint2.Rotate(0, 0, degreePerSecond * Time.deltaTime);
            }
            else if (remainingDegrees1 >= 0 && allowCount)
            {
                player1Turn = false;
                player2Turn = false;
                alarm.Play();
                Debug.Log("Player 2 Won!");
                winCanvas.SetActive(true);
                tickingClock.Stop();
                allowCount = false;
                player1Active.SetActive(false);
                player2Active.SetActive(false);

            } else if (remainingDegrees2 >= 0)
            {
                player1Turn = false;
                player2Turn = false;
                Debug.Log("Player 1 Won!");
                winCanvas.SetActive(true);
                tickingClock.Stop();
                allowCount = false;
                player1Active.SetActive(false);
                player2Active.SetActive(false);
            }
        }
        public void Player2Turn()
        {

            if (player1Turn && player1ButtonCount > 0)
            {
                player1Turn = false;
                player1Active.SetActive(false);
                player2Turn = true;
                player2Active.SetActive(true);
            }

            if (player1ButtonCount == 0)
            {
                StartBlitz();
                player1Active.SetActive(true);
            }
            
        }
        public void Player1Turn()
        {
            if (player2Turn)
            {
                player2Turn = false;
                player2Active.SetActive(false);
                player1Turn = true;
                player1Active.SetActive(true);
            }
            
        }

        public void StartBlitz()
        {
            player1Turn = true;
            player1ButtonCount++;
            instructionCanvas.SetActive(false);
            tickingClock.Play();
        }
        
        public void ResetGame()
        {
            minRotationPoint1.localEulerAngles = new Vector3 (0,0,0);
            minRotationPoint2.localEulerAngles = new Vector3 (0,0,0);

            remainingDegrees1 = -30f;
            remainingDegrees1 = -30f;

            minRotationPoint1.Rotate(0, 0, remainingDegrees1);
            minRotationPoint2.Rotate(0, 0, remainingDegrees2);

            allowCount = true;
            instructionCanvas.SetActive(true);
            player1ButtonCount = 0;

            player1Active.SetActive(false);
            player2Active.SetActive(false);
        }

    }
}
