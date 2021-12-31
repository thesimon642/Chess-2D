using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static float[,] gameBoardState = new float[8, 8]
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
    public static bool isItWhitesTurn = true;
    public static float pieceToMove = -1f;
    public static Vector2 squareToTake = new Vector2(-1f, -1f);
    public static List<Vector2> currentPossibleMoves = new List<Vector2>();
    public static string promotionChoiceWhite = "Queen";
    public static string promotionChoiceBlack = "Queen";
    public static bool canWhiteCastleShort = true;
    public static bool canWhiteCastleLong = true;
    public static bool canBlackCastleShort = true;
    public static bool canBlackCastleLong = true;
    public static bool castlingAboutToCommence = false;
    public static List<float> whitePawns = new List<float>() { 1f, 3f, 5f, 7f, 9f, 11f, 13f, 15f };
    public static List<float> blackPawns = new List<float>() { 2f, 4f, 6f, 8f, 10f, 12f, 14f, 16f };
    public static List<float> whiteKnights = new List<float>() { 19f, 29f };
    public static List<float> blackKnights = new List<float>() { 20f, 30f };
    public static List<float> whiteRooksQueen = new List<float>() { 17f, 31f,23f };
    public static List<float> blackRooksQueen = new List<float>() { 18f, 32f, 24f };
    public static List<float> whiteBishopsQueen = new List<float>() { 21f, 27f, 23f };
    public static List<float> blackBishopsQueen = new List<float>() { 22f, 28f, 24f };
    public static Vector2 whiteKingPosition;
    public static Vector2 blackKingPosition;
    public static bool whiteCheck;
    public static bool blackCheck;

    public static int numberOfPiecesWhite=0;
    public static int numberOfPiecesBlack=0;
    public static int moveCount = 0;
    public static int numberOfPiecesUpdated=0;
    public static bool isItStalemate = false;
    public static bool isItCheckmate = false;
    public static bool atLeastOneMoveMade = false;

    public static int pawnThatJustDoubleMoved = -1;

    public static float[,] allPossibleMovesWhite = new float[8, 8]
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
    public static float[,] allPossibleMovesBlack = new float[8, 8]
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

    //public static GameBoard instance;
    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        //float[,] gameBoardState = new float[8, 8]
        //{
        //    {0,0,0,0,0,0,0,0},
        //    {0,0,0,0,0,0,0,0},
        //    {0,0,0,0,0,0,0,0},
        //    {0,0,0,0,0,0,0,0},
        //    {26,0,0,0,0,0,0,0},
        //    {0,0,0,0,0,0,0,0},
        //    {0,0,0,0,0,0,0,0},
        //    {17,0,0,0,0,9,0,0}
        //};

    }

    // Update is called once per frame
    void Update()
    {
       //turns false if any element isnt zero in the for loop
        bool loopChecker = false;
        //if first move has been made
        if (atLeastOneMoveMade)
        {
            //if every piece has had a chnace to add possible moves to their pool
            if (numberOfPiecesUpdated == numberOfPiecesWhite + numberOfPiecesBlack)
            {
                if (isItWhitesTurn)
                {

                    //if no possible moves for white after every piece has tried to find one
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (allPossibleMovesWhite[i, j] != 0f)
                            { loopChecker = true; }
                        }
                    }



                }
                else
                {
                    //if no possible moves for white after every piece has tried to find one

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (allPossibleMovesBlack[i, j] != 0f)
                            { loopChecker = true; }
                        }
                    }
                }
            }
            else 
            {
                loopChecker = true;
                isItStalemate = false;
            }

            if (loopChecker == false) { isItStalemate = true; }
            if (isItStalemate)
            {
                if (isItWhitesTurn)
                {

                    if (whiteCheck)
                    { isItCheckmate = true; }

                }
                else
                {
                    if (blackCheck)
                    { isItCheckmate = true; }
                }
            }
        }
    }
    private void BoardDisplayer(float[,] boardToDisplay)
    {
        string oneLine;
        for (int i = 0; i < 8; i++)
        {
            oneLine = "";
            for (int j = 0; j < 8; j++)
            {
                oneLine = oneLine + " " + boardToDisplay[i, j].ToString();
            }
            Debug.Log(oneLine);
        }

    }
}
