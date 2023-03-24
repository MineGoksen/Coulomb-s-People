using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public GameObject image_tip;
    public TextMeshProUGUI tip;
    string t = "tip";

    private void Start()
    {
        // Hide the pop-up when the scene starts
        image_tip.SetActive(false);
        tip.text = t;

    }

    public void ShowPopUp()
    {
        // Show the pop-up when the button is clicked
        image_tip.SetActive(true);


    }

    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_tip.SetActive(false);
    }
}
