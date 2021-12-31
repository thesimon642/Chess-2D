using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squares : MonoBehaviour
{

    public Vector2 currentSquare;
    public SpriteRenderer spriteRenderer;
    public Sprite notGlowing;
    public Sprite glowing;
    private bool clickable;

    // Start is called before the first frame update
    void Start()
    {
        clickable = false;
        spriteRenderer.sprite = notGlowing;
        transform.position = new Vector2(currentSquare.y - 3.5f, -1 * (currentSquare.x - 3.5f));
    }
    void OnMouseDown()

    {
        if (clickable)
        {
            GameBoard.squareToTake = currentSquare;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameBoard.currentPossibleMoves.Contains(currentSquare))
        {
            spriteRenderer.sprite = glowing;
            clickable = true;
        }
        else
        {
            spriteRenderer.sprite = notGlowing;
            clickable = false;
        }
    }
}
