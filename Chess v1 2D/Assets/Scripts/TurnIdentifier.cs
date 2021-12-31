using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIdentifier : MonoBehaviour
{
    Text turn;
    string checkMessage;
    string fullMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn = GetComponent<Text>();
        //Debug.Log(GameBoard.whiteCheck);
        //Debug.Log("check");
        //Debug.Log(GameBoard.blackCheck);
        if (GameBoard.isItStalemate)
        {
            if (GameBoard.isItCheckmate)
            { turn.text = "CHECKMATE"; }
            else
            { turn.text = "STALEMATE"; }
        }
        else
        {
            if (GameBoard.whiteCheck == false && GameBoard.blackCheck == false)
            { checkMessage = ""; }
            else
            { checkMessage = " CHECK!"; }

            if (GameBoard.isItWhitesTurn)
            {
                fullMessage = "White's turn" + checkMessage;
            }
            else
            {
                fullMessage = "Black's turn" + checkMessage;
            }
            turn.text = fullMessage;
        }
    }
}
