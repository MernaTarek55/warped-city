using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelector : MonoBehaviour
{
    [SerializeField] List<PlayerSelector> playerSelectors;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (PlayerSelector playerSelector in playerSelectors)
            {
                if (playerSelector.isSelected == true && PlayerSelector.selectedPlayer != playerSelector.playerNumber)
                {
                    PlayerSelector.selectedPlayer = playerSelector.playerNumber;
                    playerSelector.SelectMe();
                    foreach (PlayerSelector temp in playerSelectors)
                    {
                        if(temp.playerNumber != PlayerSelector.selectedPlayer)
                        {
                            var tempColor = temp.GetComponent<Image>().color;
                            tempColor.a = 0;
                            temp.GetComponent<Image>().color = tempColor;
                        }
                    }
                }
            }
        }
    }
}
