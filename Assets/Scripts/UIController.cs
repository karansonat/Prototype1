using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoSingleton<UIController>
{
    public GameObject MainMenu;
    public GameObject LevelSelection;
    public GameObject GameMenu;

    private GameObject _tapToStart;
    private GameObject _starsGo;

	// Use this for initialization
	void Start () {
	    _starsGo = GameMenu.transform.FindChild("Stars").gameObject;
	    _tapToStart = GameMenu.transform.FindChild("txtTapToStart").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchToMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelection.SetActive(false);
        GameMenu.SetActive(false);
    }

    public void SwitchToLevelSelection()
    {
        MainMenu.SetActive(false);
        LevelSelection.SetActive(true);
        GameMenu.SetActive(false);

        if (GameController.Instance._activeLevel)
        {
            GameController.Instance.UnLoadActiveLevel();
        }
    }

    public void SwitchToGameMenu()
    {
        MainMenu.SetActive(false);
        LevelSelection.SetActive(false);
        GameMenu.SetActive(true);
    }

    public void SetStars(int starCount)
    {
        switch (starCount)
        {
            case 1:
                _starsGo.transform.FindChild("1Star").gameObject.SetActive(true);
                _starsGo.transform.FindChild("2Star").gameObject.SetActive(false);
                _starsGo.transform.FindChild("3Star").gameObject.SetActive(false);
                break;
            case 2:
                _starsGo.transform.FindChild("1Star").gameObject.SetActive(true);
                _starsGo.transform.FindChild("2Star").gameObject.SetActive(true);
                _starsGo.transform.FindChild("3Star").gameObject.SetActive(false);
                break;
            case 3:
                _starsGo.transform.FindChild("1Star").gameObject.SetActive(true);
                _starsGo.transform.FindChild("2Star").gameObject.SetActive(true);
                _starsGo.transform.FindChild("3Star").gameObject.SetActive(true);
                break;
            default:
                _starsGo.transform.FindChild("1Star").gameObject.SetActive(false);
                _starsGo.transform.FindChild("2Star").gameObject.SetActive(false);
                _starsGo.transform.FindChild("3Star").gameObject.SetActive(false);
                break;
        }
    }

    public void ShowTapToStartDialogue()
    {
        _tapToStart.SetActive(true);
    }

    public void HideTapToStartDialogue()
    {
        _tapToStart.SetActive(false);
    }
}
