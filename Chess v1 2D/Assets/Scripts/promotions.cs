using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class promotions : MonoBehaviour
{
    public string pieceType;
    public bool colourIsWhite;
    public SpriteRenderer spriteRenderer;
    private Sprite notGlowing;
    private Sprite glowing;

    public Sprite notGlowingRook;
    public Sprite glowingRook;
    public Sprite notGlowingKnight;
    public Sprite glowingKnight;
    public Sprite notGlowingBishop;
    public Sprite glowingBishop;
    public Sprite notGlowingQueen;
    public Sprite glowingQueen;



    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colourIsWhite)
        {
            if (GameBoard.promotionChoiceWhite == pieceType)
            {
                spriteRenderer.sprite = glowing;
            }
            else
            {
                spriteRenderer.sprite = notGlowing;
            }
        }
        else
        {
            if (GameBoard.promotionChoiceBlack == pieceType)
            {
                spriteRenderer.sprite = glowing;
            }
            else
            {
                spriteRenderer.sprite = notGlowing;
            }
        }
    }

    private void OnMouseDown()
    {
        if (colourIsWhite)
        {
            GameBoard.promotionChoiceWhite = pieceType;
        }
        else
        {
            GameBoard.promotionChoiceBlack = pieceType;
        }
    }
}
