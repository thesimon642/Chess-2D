using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string thisScene;

    void OnMouseDown()
    {
        
        GameBoard.isItWhitesTurn = true;
           GameBoard.gameBoardState = new float[8, 8]
        {
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f}
        };
        GameBoard.isItWhitesTurn = true;
        GameBoard.pieceToMove = -1f;
        GameBoard.squareToTake = new Vector2(-1f, -1f);
        GameBoard.currentPossibleMoves = new List<Vector2>();
        GameBoard.promotionChoiceWhite = "Queen";
        GameBoard.promotionChoiceBlack = "Queen";
        GameBoard.canWhiteCastleShort = true;
        GameBoard.canWhiteCastleLong = true;
        GameBoard.canBlackCastleShort = true;
        GameBoard.canBlackCastleLong = true;
        GameBoard.castlingAboutToCommence = false;

        GameBoard.numberOfPiecesWhite = 0;
        GameBoard.numberOfPiecesBlack = 0;
        GameBoard.moveCount = 0;
        GameBoard.numberOfPiecesUpdated = 0;
        GameBoard.isItStalemate = false;
        GameBoard.isItCheckmate = false;
        GameBoard.atLeastOneMoveMade = false;
        GameBoard.allPossibleMovesWhite = new float[8, 8]

        {
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f}
        };
        GameBoard.allPossibleMovesBlack = new float[8, 8]
        {
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f},
            {0f,0f,0f,0f,0f,0f,0f,0f}
        };
        GameBoard.pawnThatJustDoubleMoved = -1;
    SceneManager.LoadScene(thisScene);
    }

}
