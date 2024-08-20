using UnityEngine;

public class LevelOneManger : MonoBehaviour
{
    [SerializeField] GameObject BoyPlayer;
    [SerializeField] GameObject GirlPlayer;
    private void Start()
    {
        if(PlayerSelector.selectedPlayer == 0)
        {
            GirlPlayer.SetActive(true);
        }
        else
        {
            BoyPlayer.SetActive(true);
        }
    }
}
