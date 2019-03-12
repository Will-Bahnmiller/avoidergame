using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteColorController : MonoBehaviour
{
    public int playerNumber = 1;
    public SpriteRenderer playerSprite;
    public Image playerImage;


    void Start()
    {
        if (playerNumber == 1)
        {
            Vector3 playerOneColorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_one_color);
            Color playerOneColor = new Color(playerOneColorVec.x, playerOneColorVec.y, playerOneColorVec.z);

            if (playerSprite != null)
            {
                playerSprite.color = playerOneColor;
            }
            if (playerImage != null)
            {
                playerImage.color = playerOneColor;
            }
        }
        else if (playerNumber == 2)
        {
            Vector3 playerTwoColorVec = GlobalVariableManager.GetVectorVariable(VECTORVAR.player_two_color);
            Color playerTwoColor = new Color(playerTwoColorVec.x, playerTwoColorVec.y, playerTwoColorVec.z);

            if (playerSprite != null)
            {
                playerSprite.color = playerTwoColor;
            }
            if (playerImage != null)
            {
                playerImage.color = playerTwoColor;
            }
        }
    }
}
