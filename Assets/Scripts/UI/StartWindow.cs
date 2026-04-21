using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button newGameButton;
    public Button optinsButton;
    public bool canContinue;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        newGameButton.onClick.AddListener(OnNewGame);
        optinsButton.onClick.AddListener(OnOptions);
    }

    public override void Open()
    {
        continueButton.gameObject.SetActive(canContinue);
        if (canContinue == false)
        {
            firstSelected = newGameButton.gameObject;
        }
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        windowManager.Open(1);
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame");
    }

    public void OnOptions()
    {
        windowManager.Open(3);
    }
}
