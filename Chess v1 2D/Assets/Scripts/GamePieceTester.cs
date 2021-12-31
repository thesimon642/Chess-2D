using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceTester : MonoBehaviour
{
    //piece variables
    public float pieceNumber;
    public Vector2 currentSquare;
    private bool isWhite;
    public string pieceType;




    void Start()
    {



        isWhite = pieceNumber % 2 == 1f;
        //MovePiece();
        //put in piece number to current square
        GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] = pieceNumber;


        //List<Vector2> options = new List<Vector2>();



        //options = PossibleMoves();

        //DisplayListOfVectors(options);

    }

    void Update()
    {
        if (GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] != pieceNumber)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector2(currentSquare.y - 3.5f, -1 * (currentSquare.x - 3.5f));
    }
    private void MovePiece(Vector2 checkSquare)
    {
        List<Vector2> options = new List<Vector2>();
        if (options.Contains(checkSquare) && isWhite == GameBoard.isItWhitesTurn)
        {
            GameBoard.gameBoardState[(int)checkSquare.x, (int)checkSquare.y] = pieceNumber;
            GameBoard.gameBoardState[(int)currentSquare.x, (int)currentSquare.y] = 0f;

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


        }
        checkSquare = new Vector2(currentSquare.x - 1f, currentSquare.y);





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
}
