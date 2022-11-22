using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Canvas : MonoBehaviour
{
    public TextMeshProUGUI[] textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.lives<=0)
        {
            textDisplay[0].text = "Game Over !"; //show game over when lives become 0
        }
        textDisplay[1].text = "Score : " + PlayerController.score; //showws player score
        textDisplay[2].text = "Lives : " + PlayerController.lives; //shpws player lives
        if(PlayerController.nopower)
        {
            textDisplay[3].text = "Power : None"; // shows that no power is assigned
        }
        else if(PlayerController.mines)
        {
            textDisplay[3].text = "Power : Mine"; // shows mines are assigned
            
        }
        else if(PlayerController.sticky)
        {
            textDisplay[3].text = "Power : Sticky Bombs"; //shows sticky bombs are assigned
        }
        else if(PlayerController.multi)
        {
            textDisplay[3].text = "Power : Multi Bombs"; // shows multiple bombs are assigned
        }
    }
}
