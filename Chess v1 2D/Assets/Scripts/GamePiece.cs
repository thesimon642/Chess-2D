using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GamePiece : MonoBehaviour
{
    //piece variables
    public float pieceNumber;
    public Vector2 currentSquare;
    private bool isWhite;
    public string pieceType;


    public SpriteRenderer spriteRenderer;
    private Sprite notGlowing;
    private Sprite glowing;


    private bool shouldBeGlowing = false;
    public Sprite notGlowingPawn;
    public Sprite glowingPawn;
    public Sprite notGlowingRook;
    public Sprite glowingRook;
    public Sprite notGlowingKnight;
    public Sprite glowingKnight;
    public Sprite notGlowingBishop;
    public Sprite glowingBishop;
    public Sprite notGlowingKing;
    public Sprite glowingKing;
    public Sprite notGlowingQueen;
    public Sprite glowingQueen;
    public Sprite glowingKingRed;

    private int moveCount;


    void Start()
    {
        shouldBeGlowing = false;
        moveCount = 0;

        isWhite = pieceNumber % 2 == 1f;
        if (isWhite)
        { GameBoard.numberOfPiecesWhite += 1; }
        else
        { GameBoard.numberOfPiecesBlack += 1; }
        //MovePiece();
        //put in piece number to current square
        GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] = pieceNumber;


        //List<Vector2> options = new List<Vector2>();



        //options = PossibleMoves();

        //DisplayListOfVectors(options);

    }



    void OnMouseDown()

    {


        float pieceToMove = GameBoard.pieceToMove;
        Vector2 squareToTake = GameBoard.squareToTake;

        if (pieceToMove == -1f)
        {
            //if nothing has been clicked select this as a piece to be moved
            if (isWhite == GameBoard.isItWhitesTurn)
            {
                pieceToMove = pieceNumber;
            }
            //play light up animation, change global variable to options so board can light up
        }
        else if (pieceToMove == pieceNumber)
        {
            //if clicked after selected: unselect
            if (isWhite == GameBoard.isItWhitesTurn)
            {
                pieceToMove = -1f;
                GameBoard.currentPossibleMoves = new List<Vector2>();
            }
            //remove light up animation
        }
        else if (pieceNumber % 2 == pieceToMove % 2)
        {

            //if same colour then swap active piece
            if (isWhite == GameBoard.isItWhitesTurn)
            {
                pieceToMove = pieceNumber;
            }
            //light up new piece and remove old one
        }
        else
        {
            //if moved into this piece, record its poition
            squareToTake = currentSquare;
            //remove  light up on this piece
        }


        GameBoard.pieceToMove = pieceToMove;
        GameBoard.squareToTake = squareToTake;

    }

    void Update()
    {
        //Debug.Log("start");
        //BoardDisplayer(GameBoard.allPossibleMovesBlack);
        switch (pieceType)
        {
            case "Rook":
                glowing = glowingRook;
                notGlowing = notGlowingRook;
                break;
            case "Bishop":
                glowing = glowingBishop;
                notGlowing = notGlowingBishop;
                break;
            case "Queen":
                glowing = glowingQueen;
                notGlowing = notGlowingQueen;
                break;
            case "Knight":
                glowing = glowingKnight;
                notGlowing = notGlowingKnight;
                break;
            case "King":
                glowing = glowingKing;
                notGlowing = notGlowingKing;
                break;
            case "Pawn":
                glowing = glowingPawn;
                notGlowing = notGlowingPawn;
                break;
        }

        //updates exactly 1 time before checkmate check can occur


        if (GameBoard.currentPossibleMoves.Contains(currentSquare))
        {
            shouldBeGlowing = true;
        }
        if (shouldBeGlowing)
        {
            spriteRenderer.sprite = glowing;
        }
        else
        {
            spriteRenderer.sprite = notGlowing;
        }
        //if taken destroy it and remove from count
        if (GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] != pieceNumber)
        {
            if (isWhite)
            { GameBoard.numberOfPiecesWhite -= 1; }
            else
            { GameBoard.numberOfPiecesBlack -= 1; }
            Destroy(gameObject);
        }





        transform.position = new Vector2(currentSquare.y - 3.5f, -1 * (currentSquare.x - 3.5f));

        float pieceToMove = GameBoard.pieceToMove;
        Vector2 squareToTake = GameBoard.squareToTake;
        if (pieceToMove == pieceNumber)
        {
            if (isWhite == GameBoard.isItWhitesTurn)
            {
                shouldBeGlowing = true;
                GameBoard.currentPossibleMoves = PossibleMoves();
            }
            //Debug.Log("area1" + squareToTake);
            if (OutOfBounds(squareToTake) == false)
            {
                Vector2 oldSquare = currentSquare;


                //swap the turn so considered a valid move to castle (otherwise moving same colour twiuce is blocked)
                if (GameBoard.castlingAboutToCommence)
                {
                    GameBoard.isItWhitesTurn = !GameBoard.isItWhitesTurn;
                }
                MovePiece(squareToTake);
                //if it didn't actually move (invalid square)
                if (oldSquare == currentSquare)
                {
                    //remove the request of moving to that square
                    squareToTake = new Vector2(-1f, -1f);
                }
                else
                {
                    GameBoard.atLeastOneMoveMade = true;
                    //if the move was successful, remove the request to move and the request of where to move
                    pieceToMove = -1;
                    squareToTake = new Vector2(-1f, -1f);
                    GameBoard.currentPossibleMoves = new List<Vector2>();
                    //removes castling ability
                    //and castle if applicable
                    GameBoard.castlingAboutToCommence = false;
                    if (isWhite)
                    {
                        if (pieceType == "King")
                        {
                            GameBoard.canWhiteCastleLong = false;
                            GameBoard.canWhiteCastleShort = false;
                            //castle if the king moved 2
                            if (oldSquare == new Vector2(7, 4))
                            {
                                //castle short
                                if (currentSquare == new Vector2(7, 6))
                                {
                                    GameBoard.castlingAboutToCommence = true;
                                    pieceToMove = 31;
                                    squareToTake = new Vector2(7, 5);
                                }
                                //castle long
                                if (currentSquare == new Vector2(7, 2))
                                {
                                    GameBoard.castlingAboutToCommence = true;
                                    pieceToMove = 17;
                                    squareToTake = new Vector2(7, 3);
                                }

                            }
                        }
                        else if (pieceType == "Rook")
                        {
                            if (oldSquare == new Vector2(7, 0))
                            {
                                GameBoard.canWhiteCastleLong = false;
                            }
                            else if (oldSquare == new Vector2(7, 7))
                            {
                                GameBoard.canWhiteCastleShort = false;
                            }
                        }
                    }
                    else
                    {
                        {
                            if (pieceType == "King")
                            {
                                GameBoard.canBlackCastleLong = false;
                                GameBoard.canBlackCastleShort = false;
                                //castle if the king moved 2
                                if (oldSquare == new Vector2(0, 4))
                                {
                                    //castle short
                                    if (currentSquare == new Vector2(0, 6))
                                    {
                                        GameBoard.castlingAboutToCommence = true;
                                        pieceToMove = 32;
                                        squareToTake = new Vector2(0, 5);
                                    }
                                    //castle long
                                    if (currentSquare == new Vector2(0, 2))
                                    {
                                        GameBoard.castlingAboutToCommence = true;
                                        pieceToMove = 18;
                                        squareToTake = new Vector2(0, 3);
                                    }

                                }
                            }
                            else if (pieceType == "Rook")
                            {
                                if (oldSquare == new Vector2(0, 0))
                                {
                                    GameBoard.canBlackCastleLong = false;
                                }
                                else if (oldSquare == new Vector2(0, 7))
                                {
                                    GameBoard.canBlackCastleShort = false;
                                }
                            }
                        }
                    }



                    //promotes pawns
                    if (pieceType == "Pawn")
                    {

                        if (isWhite)
                        {
                            if (currentSquare.x == 0)
                            {
                                pieceType = GameBoard.promotionChoiceWhite;
                            }
                        }
                        else
                        {
                            if (currentSquare.x == 7)
                            {
                                pieceType = GameBoard.promotionChoiceBlack;

                            }
                        }
                    }

                    //reset the move count so another round of checking for a checkmate commences
                    GameBoard.moveCount += 1;
                    GameBoard.numberOfPiecesUpdated = 0;
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

                    //en passent stuff

                    //if an en passent happened do it
                    if (isWhite)
                    {
                        if (pieceType == "Pawn" && oldSquare.x == 3 && currentSquare.y == GameBoard.pawnThatJustDoubleMoved)
                        {//if a pawn on "row 3" took towards a pawn that just double moved, it took en passent
                            GameBoard.gameBoardState[3, (int)currentSquare.y] = 0;
                        }

                    }
                    else
                    {
                        if (pieceType == "Pawn" && oldSquare.x == 4 && currentSquare.y == GameBoard.pawnThatJustDoubleMoved)
                        {//if a pawn on "row 4" took towards a pawn that just double moved, it took en passent
                            GameBoard.gameBoardState[4, (int)currentSquare.y] = 0;
                        }
                    }


                    //if a pawn just moved 2 let the global variable know, otherwise let it know it didn't

                    if (isWhite)
                    {
                        if (pieceType == "Pawn" && oldSquare.x == 6 && currentSquare.x == 4)
                        {
                            GameBoard.pawnThatJustDoubleMoved = (int)currentSquare.y;
                        }
                        else
                        {
                            GameBoard.pawnThatJustDoubleMoved = -1;
                        }
                    }
                    else
                    {
                        if (pieceType == "Pawn" && oldSquare.x == 1 && currentSquare.x == 3)
                        {
                            GameBoard.pawnThatJustDoubleMoved = (int)currentSquare.y;
                        }
                        else
                        {
                            GameBoard.pawnThatJustDoubleMoved = -1;
                        }
                    }




                }

            }
        }
        else
        {
            shouldBeGlowing = false;
        }
        //make kings in check glow red
        if (pieceType == "King")
        {
            if (CheckChecker(currentSquare, GameBoard.gameBoardState, isWhite))
            {
                spriteRenderer.sprite = glowingKingRed;

                if (isWhite)
                { GameBoard.whiteCheck = true; }
                else
                { GameBoard.blackCheck = true; }
            }
            else
            {
                if (isWhite)
                { GameBoard.whiteCheck = false; }
                else
                { GameBoard.blackCheck = false; }
            }
        }

        GameBoard.pieceToMove = pieceToMove;
        GameBoard.squareToTake = squareToTake;
        if (pieceType == "King")
        {
            if (isWhite)
            { GameBoard.whiteKingPosition = currentSquare; }
            else
            { GameBoard.blackKingPosition = currentSquare; }
        }
        //bit after and stops pieces about to be destroyed form counting
        if (moveCount < GameBoard.moveCount && GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] == pieceNumber)
        {
            List<Vector2> options = PossibleMoves();
            

            foreach (Vector2 option in options)
            {
                if (isWhite)
                { GameBoard.allPossibleMovesWhite[(int)option.x, (int)option.y] += 1f; }
                else
                { GameBoard.allPossibleMovesBlack[(int)option.x, (int)option.y] += 1f; }
            }
            moveCount += 1;
            GameBoard.numberOfPiecesUpdated += 1;


        }

    }
    private void MovePiece(Vector2 checkSquare)
    {
        List<Vector2> options = new List<Vector2>();
        options = PossibleMoves();
        if (options.Contains(checkSquare) && isWhite == GameBoard.isItWhitesTurn)
        {
            //then a move can happen (if a valid move and the right turn)
            GameBoard.isItWhitesTurn = !GameBoard.isItWhitesTurn;
            GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] = pieceNumber;
            GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] = 0f;
            currentSquare = checkSquare;
        }
    }
    private List<Vector2> PossibleMoves()
    {

        List<Vector2> options = new List<Vector2>();


        switch (pieceType)
        {
            case "Rook":
                options = PossibleMovesRook();
                break;
            case "Bishop":
                options = PossibleMovesBishop();
                break;
            case "Queen":
                options = PossibleMovesRook();
                options.AddRange(PossibleMovesBishop());
                break;
            case "Knight":
                options = PossibleMovesKnight();
                break;
            case "King":
                options = PossibleMovesKing();
                break;
            case "Pawn":
                options = PossibleMovesPawn();
                break;
        }

        //removes checks as valid moves
        float[,] tempGameBoard = new float[8,8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                tempGameBoard[i, j] = GameBoard.gameBoardState[i, j];
            }
        }
      
        
        Vector2 myKingPosition;

        if (isWhite)
        {
            myKingPosition = GameBoard.whiteKingPosition;
        }
        else
        {
            myKingPosition = GameBoard.blackKingPosition;
        }


        List<Vector2> tempOptions = new List<Vector2>(options);
        foreach (Vector2 move in tempOptions)
        {
            if (pieceType == "King")
            { myKingPosition = move; }

            //for each possible move simulate it and if it would cause a check remove it
            tempGameBoard[(int)currentSquare.x, (int)currentSquare.y] = 0;
            tempGameBoard[(int)move.x, (int)move.y] = pieceNumber;
            if (CheckChecker(myKingPosition,tempGameBoard,isWhite))
            {
                options.RemoveAll(item => item.x == move.x && item.y == move.y);
            }
            //tempGameBoard = GameBoard.gameBoardState;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tempGameBoard[i, j] = GameBoard.gameBoardState[i, j];
                }
            }
        }


        return options;
    }

    private List<Vector2> PossibleMovesPawn()
    {
        List<Vector2> options = new List<Vector2>();
        Vector2 checkSquare;
        float checkSquareValue;
        if (isWhite)
        {
            //move up 1
            checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid move if space in front is empty
                if (GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == 0)
                {
                    options.Add(checkSquare);

                    //move 2 (only possible if move 1 is)
                    if (currentSquare.x == 6)
                    {
                        checkSquare = new Vector2(currentSquare.x - 2f, currentSquare.y);
                        if (GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == 0)
                        {
                            options.Add(checkSquare);
                        }


                    }
                }

                //taking left
                checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y - 1f);
                if (OutOfBounds(checkSquare) == false)
                {
                    //only valid if a black piece is on it (even piece number)
                    checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                    if (checkSquareValue != 0 && checkSquareValue % 2 != pieceNumber % 2)
                    {
                        options.Add(checkSquare);
                    }
                }
                //taking right
                checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y + 1f);
                if (OutOfBounds(checkSquare) == false)
                {
                    //only valid if a black piece is on it (even piece number)
                    checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                    if (checkSquareValue != 0 && checkSquareValue % 2 != pieceNumber % 2)
                    {
                        options.Add(checkSquare);
                    }
                }

            }
            //en passent
            if (currentSquare.x == 3 && GameBoard.pawnThatJustDoubleMoved != -1 && Math.Abs(currentSquare.y - GameBoard.pawnThatJustDoubleMoved) == 1)
            {//check that you're on the right row, a pawn has just double moved, and you're next to it
                options.Add(new Vector2(2,GameBoard.pawnThatJustDoubleMoved));
            }

        }
        else
        {
            //if black piece
            //move down 1
            checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid move if space in front is empty
                if (GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == 0)
                {
                    options.Add(checkSquare);

                    //move 2 (only possible if move 1 is)
                    if (currentSquare.x == 1)
                    {
                        checkSquare = new Vector2(currentSquare.x + 2f, currentSquare.y);
                        if (GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == 0)
                        {
                            options.Add(checkSquare);
                        }


                    }
                }

                //taking left
                checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y - 1f);
                if (OutOfBounds(checkSquare) == false)
                {
                    //only valid if a black piece is on it (even piece number)
                    checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                    if (checkSquareValue != 0 && checkSquareValue % 2 != pieceNumber % 2)
                    {
                        options.Add(checkSquare);
                    }
                }
                //taking right
                checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y + 1f);
                if (OutOfBounds(checkSquare) == false)
                {
                    //only valid if a black piece is on it (even piece number)
                    checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                    if (checkSquareValue != 0 && checkSquareValue % 2 != pieceNumber % 2)
                    {
                        options.Add(checkSquare);
                    }
                }

            }
            //en passent
            if (currentSquare.x == 4 && GameBoard.pawnThatJustDoubleMoved != -1 && Math.Abs(currentSquare.y - GameBoard.pawnThatJustDoubleMoved) == 1)
            {//check that you're on the right row, a pawn has just double moved, and you're next to it
                options.Add(new Vector2(5,GameBoard.pawnThatJustDoubleMoved));
            }

        }
        





        return options;
    }


    private List<Vector2> PossibleMovesKing()
    {
        Vector2 checkSquare;
        List<Vector2> options = new List<Vector2>();

        // up right
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y + 1f);
        options = AddIfValid(options, checkSquare);

        // up left
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y - 1f);
        options = AddIfValid(options, checkSquare);

        // down right
        checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y + 1f);
        options = AddIfValid(options, checkSquare);

        //down left
        checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y - 1f);
        options = AddIfValid(options, checkSquare);

        //up
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y);
        options = AddIfValid(options, checkSquare);

        // down
        checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y);
        options = AddIfValid(options, checkSquare);

        //left
        checkSquare = new Vector2(currentSquare.x, currentSquare.y - 1f);
        options = AddIfValid(options, checkSquare);

        // right
        checkSquare = new Vector2(currentSquare.x, currentSquare.y + 1f);
        options = AddIfValid(options, checkSquare);

        float[,] gameBoardState = GameBoard.gameBoardState;
        //adds casling as an options
        //remove castling if in or through check

        if (CheckChecker(currentSquare, GameBoard.gameBoardState, isWhite) == false)
        {//only add caslting if not in check
            if (isWhite)
            {
                if (GameBoard.canWhiteCastleShort && gameBoardState[7, 6] == 0f && gameBoardState[7, 5] == 0f)
                {
                    if (CheckChecker(new Vector2(7,5), GameBoard.gameBoardState, isWhite) == false)
                    {//removes castling though check
                        options.Add(new Vector2(7, 6));
                    }
                }
                if (GameBoard.canWhiteCastleLong && gameBoardState[7, 1] == 0f && gameBoardState[7, 2] == 0f && gameBoardState[7, 3] == 0f)
                {
                    if (CheckChecker(new Vector2(7, 3), GameBoard.gameBoardState, isWhite) == false)
                    {//removes castling though check
                        options.Add(new Vector2(7, 2));
                    }
                }
            }
            else
            {
                if (GameBoard.canBlackCastleShort && gameBoardState[0, 6] == 0f && gameBoardState[0, 5] == 0f)
                {
                    if (CheckChecker(new Vector2(0, 5), GameBoard.gameBoardState, isWhite) == false)
                    {//removes castling though check
                        options.Add(new Vector2(0, 6));
                    }
                }
                if (GameBoard.canBlackCastleLong && gameBoardState[0, 1] == 0f && gameBoardState[0, 2] == 0f && gameBoardState[0, 3] == 0f)
                {
                    if (CheckChecker(new Vector2(0, 3), GameBoard.gameBoardState, isWhite)==false)
                    {//removes castling though check
                        options.Add(new Vector2(0, 2));
                    }
                }
            }

        }
        return options;
    }
    private List<Vector2> PossibleMovesKnight()
    {
        Vector2 checkSquare;
        List<Vector2> options = new List<Vector2>();

        //2 up 1 right
        checkSquare = new Vector2(currentSquare.x - 2f, currentSquare.y + 1f);
        options = AddIfValid(options, checkSquare);

        //2 up 1 left
        checkSquare = new Vector2(currentSquare.x - 2f, currentSquare.y - 1f);
        options = AddIfValid(options, checkSquare);

        //2 down 1 right
        checkSquare = new Vector2(currentSquare.x + 2f, currentSquare.y + 1f);
        options = AddIfValid(options, checkSquare);

        //2 down 1 left
        checkSquare = new Vector2(currentSquare.x + 2f, currentSquare.y - 1f);
        options = AddIfValid(options, checkSquare);

        //2 left 1 up
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y - 2f);
        options = AddIfValid(options, checkSquare);

        //2 left 1 down
        checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y - 2f);
        options = AddIfValid(options, checkSquare);

        //2 right 1 up
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y + 2f);
        options = AddIfValid(options, checkSquare);

        //2 right 1 down
        checkSquare = new Vector2(currentSquare.x + 1f, currentSquare.y + 2f);
        options = AddIfValid(options, checkSquare);


        return options;
    }








    private List<Vector2> PossibleMovesBishop()
    {
        Vector2 checkSquare;
        float checkSquareValue;
        bool limitReached;




        List<Vector2> options = new List<Vector2>();

        //moving down right
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y + 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);


        //moving down left
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y - 1f);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);




        //moving up right
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y + 1f);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);





        //moving up left
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y - 1f);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
             //you've hit a wall but you can take it

                options.Add(checkSquare);
                limitReached = true;

            }
        } while (limitReached == false);



        return options;
    }

    private List<Vector2> PossibleMovesRook()
    {






        Vector2 checkSquare;
        float checkSquareValue;
        bool limitReached;
        List<Vector2> options = new List<Vector2>();

        if (GameBoard.castlingAboutToCommence)
        {
            //if we're about to castle this is about to happen automatically so it only matters if the valid option is a correct move so set them as as valid (irellavent of which rook)
            options.Add(new Vector2(7, 3));
            options.Add(new Vector2(7, 5));
            options.Add(new Vector2(0, 3));
            options.Add(new Vector2(0, 5));
        }




        //moving right
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x, checkSquare.y + 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);



            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);


        //moving left
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x, checkSquare.y - 1f);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);




        //moving down
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
                //you've hit a wall but you can take it
                options.Add(checkSquare);
                limitReached = true;
            }
        } while (limitReached == false);





        //moving up
        checkSquare = currentSquare;
        limitReached = false;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y);
            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];

            if (checkSquareValue == 0)
            {
                //if that square is empty move on and store it as an option
                options.Add(checkSquare);

            }
            else if (checkSquareValue % 2 == pieceNumber % 2)
            {//if the same oddness i.e. the piece is the same colour
                //you've hit a wall so stop
                limitReached = true;
            }
            else
            {//if different oddness i.e. the piece is different colour
             //you've hit a wall but you can take it

                options.Add(checkSquare);
                limitReached = true;

            }
        } while (limitReached == false);



        return options;
    }
    private bool OutOfBounds(Vector2 position)
    {
        //checks if a move is out of the 8x8 board
        bool isItOutOfBounds = true;
        if (position.x >= 0 && position.y >= 0 && position.x <= 7 && position.y <= 7)
        {
            isItOutOfBounds = false;
        }

        return isItOutOfBounds;
    }

    private void DisplayListOfVectors(List<Vector2> options)
    {
        string displayer;
        foreach (Vector2 vect in options)
        {
            displayer = vect.x.ToString() + " " + vect.y.ToString();
            Debug.Log(displayer);
        }
    }
    private List<Vector2> AddIfValid(List<Vector2> options, Vector2 checkSquare)
    {
        if (OutOfBounds(checkSquare))
        {
            return options;
        }
        float checkSquareValue = GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
        if (checkSquareValue == 0 || checkSquareValue % 2 != pieceNumber % 2)
        {
            options.Add(checkSquare);
        }
        return options;
    }



    private bool CheckChecker(Vector2 kingSquare, float[,] gameBoardState, bool kingIsWhite)
    {
        //bool kingIsWhite;

        //kingIsWhite = gameBoardState[(int)kingSquare.x, (int)kingSquare.y] % 2 == 1;
        //second part of that is true iff king is white
        bool runningChecker = false;
        float[,] copyTarget = new float[8, 8];

        Array.Copy(gameBoardState, copyTarget, 64);
        runningChecker = runningChecker || CheckCheckerBishop(kingIsWhite, kingSquare, copyTarget);
        Array.Copy(gameBoardState, copyTarget, 64);
        runningChecker = runningChecker || CheckCheckerKing(kingIsWhite, kingSquare, copyTarget);
        Array.Copy(gameBoardState, copyTarget, 64);
        runningChecker = runningChecker || CheckCheckerPawn(kingIsWhite, kingSquare, copyTarget);
        Array.Copy(gameBoardState, copyTarget, 64);
        runningChecker = runningChecker || CheckCheckerRook(kingIsWhite, kingSquare, copyTarget);
        Array.Copy(gameBoardState, copyTarget, 64);
        runningChecker = runningChecker || CheckCheckerKnight(kingIsWhite, kingSquare, copyTarget);

        return runningChecker;
    }

    private bool CheckCheckerPawn(bool kingIsWhite, Vector2 kingSquare, float[,] gameBoardState)
    {
        bool isCheck = false;
        Vector2 checkSquare;
        float checkSquareValue;
        if (kingIsWhite)
        {
            checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y - 1f);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid if a black piece is on it (even piece number)
                checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                if (GameBoard.blackPawns.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
            }
            //taking right
            checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y + 1f);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid if a black piece is on it (even piece number)
                checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                if (GameBoard.blackPawns.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
            }

        }

        else
        {
            checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y - 1f);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid if a black piece is on it (even piece number)
                checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                if (GameBoard.whitePawns.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
            }
            //taking right
            checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y + 1f);
            if (OutOfBounds(checkSquare) == false)
            {
                //only valid if a black piece is on it (even piece number)
                checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
                if (GameBoard.whitePawns.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
            }
        }

        return isCheck;

    }


    private bool CheckCheckerKing(bool kingIsWhite, Vector2 kingSquare, float[,] gameBoardState)
    {
        Vector2 checkSquare;
        bool isCheck = false;
        float enemyKing;
        if (kingIsWhite)
        {
            enemyKing = 26f;
        }
        else
        {
            enemyKing = 25f;
        }

        // up right
        checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y + 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        // up left
        checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y - 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        // down right
        checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y + 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        //down left
        checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y - 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        //up
        checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y);
        if (OutOfBounds(checkSquare) == false)
        {

            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        // down
        checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        //left
        checkSquare = new Vector2(kingSquare.x, kingSquare.y - 1f);
        if (OutOfBounds(checkSquare) == false)
        {

            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }
        // right
        checkSquare = new Vector2(kingSquare.x, kingSquare.y + 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (gameBoardState[(int)checkSquare.x, (int)checkSquare.y] == enemyKing)
            { isCheck = true; }
        }

        return isCheck;
    }
    private bool CheckCheckerKnight(bool kingIsWhite, Vector2 kingSquare, float[,] gameBoardState)
    {
        Vector2 checkSquare;
        bool isCheck = false;

        List<float> poolOfAttackers;
        if (kingIsWhite)
        { poolOfAttackers = new List<float>() { 20f, 30f }; }
        else
        { poolOfAttackers = new List<float>() { 19f, 29f }; }


        //2 up 1 right
        checkSquare = new Vector2(kingSquare.x - 2f, kingSquare.y + 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 up 1 left
        checkSquare = new Vector2(kingSquare.x - 2f, kingSquare.y - 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 down 1 right
        checkSquare = new Vector2(kingSquare.x + 2f, kingSquare.y + 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 down 1 left
        checkSquare = new Vector2(kingSquare.x + 2f, kingSquare.y - 1f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 left 1 up
        checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y - 2f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 left 1 down
        checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y - 2f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 right 1 up
        checkSquare = new Vector2(kingSquare.x - 1f, kingSquare.y + 2f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
        //2 right 1 down
        checkSquare = new Vector2(kingSquare.x + 1f, kingSquare.y + 2f);
        if (OutOfBounds(checkSquare) == false)
        {
            if (poolOfAttackers.Contains(gameBoardState[(int)checkSquare.x, (int)checkSquare.y]))
            { isCheck = true; }
        }
    

        return isCheck;
    }








    private bool CheckCheckerBishop(bool kingIsWhite, Vector2 kingSquare, float[,] gameBoardState)
    {
        Vector2 checkSquare;
        float checkSquareValue=-3;
        bool isCheck = false;

        //moving down right
        checkSquare = kingSquare;
        List<float> poolOfAttackers;
        if (kingIsWhite)
        { poolOfAttackers = new List<float>() { 22f, 28f, 24f }; }
        else
        { poolOfAttackers = new List<float>() { 21f, 27f, 23f }; }
            do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y + 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0f)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);



        //moving down left
        checkSquare = kingSquare;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y - 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0f)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);




        //moving up right
        checkSquare = kingSquare;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y + 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0f)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);


        //moving up left
        checkSquare = kingSquare;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y - 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0f)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);
        //if (kingSquare.x == 6f && kingSquare.y ==4f)
        //{
        //    if (kingSquare.x == 6f && kingSquare.y == 4f)
        //    { foreach (float item in poolOfAttackers) { Debug.Log(item); } }
        //    Debug.Log(checkSquareValue);
        //    Debug.Log(checkSquare);
        //    Debug.Log(isCheck);
        //    Debug.Log(kingIsWhite);
        //    Debug.Log(kingSquare);
        //    Debug.Log("test1");
        //    Debug.Log(gameBoardState);
        //    Debug.Log("test1");
        //}

        return isCheck;
    }

    private bool CheckCheckerRook(bool kingIsWhite, Vector2 kingSquare, float[,] gameBoardState)
    {
        Vector2 checkSquare;
        float checkSquareValue;
        bool isCheck = false;

        //moving down
        checkSquare = kingSquare;
        List<float> poolOfAttackers;
        if (kingIsWhite)
        { poolOfAttackers = new List<float>() { 18f, 32f, 24f }; }
        else
        { poolOfAttackers = new List<float>() { 17f, 31f, 23f }; }


        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x + 1f, checkSquare.y);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);



        //moving left
        checkSquare = kingSquare;
        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x, checkSquare.y - 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);




        //moving up
        checkSquare = kingSquare;

        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x - 1f, checkSquare.y);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);


        //moving right
        checkSquare = kingSquare;

        do
        {
            //check one square further away
            checkSquare = new Vector2(checkSquare.x, checkSquare.y + 1f);

            //keep in bounds
            if (OutOfBounds(checkSquare))
            {
                break;
            }
            checkSquareValue = gameBoardState[(int)checkSquare.x, (int)checkSquare.y];
            //if that square is empty move on
            if (checkSquareValue != 0)
            {//if it hits a piece, check if its a bishop

                if (poolOfAttackers.Contains(checkSquareValue))
                {
                    isCheck = true;
                }
                break;
            }
        } while (true);

        return isCheck;
    }
    private void BoardDisplayer(float[,] boardToDisplay)
    {
        string oneLine;
        for (int i = 0; i < 8; i++)
        {
            oneLine = "";
            for (int j = 0; j < 8; j++)
            {
                oneLine = oneLine +" "+boardToDisplay[i,j].ToString();
            }
            Debug.Log(oneLine);
        }
       
    }
}

