using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelector : MonoBehaviour
{
    [SerializeField] List<PlayerSelector> playerSelectors;
    [SerializeField] List<PlayerSelector> modSelectors;
    [SerializeField] GameObject StartBtn;
    string sceneName;
    private void Start()
    {
        StartBtn.SetActive(false);
        StartBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
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
            foreach (PlayerSelector modSelector in modSelectors)
            {
                if (modSelector.isSelected == true && PlayerSelector.selectedMod != modSelector.playerNumber)
                {
                    PlayerSelector.selectedMod = modSelector.playerNumber;
                    modSelector.SelectMe();
                    foreach (PlayerSelector temp in modSelectors)
                    {
                        if (temp.playerNumber != PlayerSelector.selectedMod)
                        {
                            var tempColor = temp.GetComponent<Image>().color;
                            tempColor.a = 0;
                            temp.GetComponent<Image>().color = tempColor;
                        }
                    }
                }
            }
            if(PlayerSelector.selectedMod == 2)
            {
                sceneName = "Level1.1";
            }
            if (PlayerSelector.selectedMod == 3)
            {
                sceneName = "Level1.2";
            }
            if (PlayerSelector.selectedMod != -1 && PlayerSelector.selectedPlayer != -1) StartBtn.SetActive(true);
        }
    }
}
