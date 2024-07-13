using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InputInfo : MonoBehaviour
{
    [SerializeField] Player_Interaction interaction;
    [SerializeField] GameObject leftClickInfo;
    [SerializeField] GameObject rightClickInfo;
    [SerializeField] TMP_Text leftClickText;
    [SerializeField] TMP_Text rightClickText;



    private void Start()
    {
        leftClickInfo.SetActive(false);
        rightClickInfo.SetActive(false);
    }

    private void Update()
    {
        if (interaction.Puzzle1Piece != null)
        {
            leftClickInfo.SetActive(true);

            if (interaction.HoldingPuzzlePiece)
            {
                leftClickText.text = "[Relsease] Drop";
            }
            else
            {
                leftClickText.text = "[Hold] Pick up";
            }
        }

        else if (interaction.Puzzle2Piece != null)
        {
            leftClickInfo.SetActive(true);
            leftClickText.text = "[Click] Activate";
        }

        else
        {
            leftClickInfo.SetActive(false);
        }


        if (interaction.Puzzle1PieceSlot != null)
        {
            rightClickInfo.SetActive(true);
            rightClickText.text = "[Click] Play";
        }
        else
        {
            rightClickInfo.SetActive(false);
        }
    }
}
