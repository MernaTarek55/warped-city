using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int playerNumber;
    public static int selectedPlayer;
    [HideInInspector] public bool isSelected;
    Image myImage;
    float alphaValue;
    bool increase;
    private void Start()
    {
        myImage = GetComponent<Image>();
        selectedPlayer = -1;
        increase = true;
        alphaValue = 0;
        var tempColor = myImage.color;
        tempColor.a = alphaValue;
        myImage.color = tempColor;
        isSelected = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
        if (selectedPlayer != playerNumber)
        {
            StartCoroutine(nameof(AnimateBackground));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        if (selectedPlayer != playerNumber)
        {
            alphaValue = 0;
            increase = true;
            var tempColor = myImage.color;
            tempColor.a = alphaValue;
            myImage.color = tempColor;
        }
        StopAllCoroutines();
    }
    private IEnumerator AnimateBackground()
    {
        var tempColor = myImage.color;
        while (true)
        {
            tempColor.a = alphaValue;
            myImage.color = tempColor;
            yield return new WaitForSeconds(0.025f);
            if (alphaValue > 0 && !increase)
            {
                alphaValue -= 0.1f;
            }
            else if (alphaValue < 1 && increase)
            {
                alphaValue += 0.1f;
            }
            if (alphaValue >= 1) increase = false;
            else if (alphaValue <= 0) increase = true;
        }
    }
    public void SelectMe()
    {
        alphaValue = 1;
        increase = false;
        var tempColor = myImage.color;
        tempColor.a = alphaValue;
        myImage.color = tempColor;
        StopAllCoroutines();
    }
}
