using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Text looseCounter;

    [SerializeField] private Text winCounter;
    private int looseCount;
    private int winCount;

    void Start()
    {
        looseCounter.text = "0";
        winCounter.text = "0";

        ball.OnLoose += AddLooseCounter;
        ball.OnWin += AddWinCounter;
    }

    public void AddLooseCounter() =>
        looseCounter.text = (++looseCount).ToString();

    public void AddWinCounter() =>
        winCounter.text = (++winCount).ToString();
}