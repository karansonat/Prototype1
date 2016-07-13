using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoSingleton<UIController>
{
    public GameObject MainMenu;
    public GameObject LevelSelection;
    public GameObject GameMenu;
    public GameObject LevelWon;
    public GameObject LevelLost;

    private GameObject _tapToStart;
    private GameObject _starsGo;

	// Use this for initialization
	void Start () {
	    _starsGo = GameMenu.transform.FindChild("Stars").gameObject;
	    _tapToStart = GameMenu.transform.FindChild("txtTapToStart").gameObject;
	}

    public void SwitchToMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelection.SetActive(false);
        GameMenu.SetActive(false);
        LevelWon.SetActive(false);
        LevelLost.SetActive(false);
    }

    public void SwitchToLevelSelection()
    {
        MainMenu.SetActive(false);
        LevelSelection.SetActive(true);
        GameMenu.SetActive(false);
        LevelWon.SetActive(false);
        LevelLost.SetActive(false);

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
        LevelWon.SetActive(false);
        LevelLost.SetActive(false);
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

    public void ShowEndGameScreen(GameController.EndGameCondition condition)
    {
        GameMenu.SetActive(false);
        switch (condition)
        {
            case GameController.EndGameCondition.Win:
                var starCount = PlayerController.Instance.collectedStarCount;
                var starsObj = LevelWon.transform.FindChild("Stars").gameObject;
                switch (starCount)
                {
                    case 1:
                        starsObj.transform.FindChild("1Star").gameObject.SetActive(true);
                        starsObj.transform.FindChild("2Star").gameObject.SetActive(false);
                        starsObj.transform.FindChild("3Star").gameObject.SetActive(false);
                        break;
                    case 2:
                        starsObj.transform.FindChild("1Star").gameObject.SetActive(true);
                        starsObj.transform.FindChild("2Star").gameObject.SetActive(true);
                        starsObj.transform.FindChild("3Star").gameObject.SetActive(false);
                        break;
                    case 3:
                        starsObj.transform.FindChild("1Star").gameObject.SetActive(true);
                        starsObj.transform.FindChild("2Star").gameObject.SetActive(true);
                        starsObj.transform.FindChild("3Star").gameObject.SetActive(true);
                        break;
                    default:
                        starsObj.transform.FindChild("1Star").gameObject.SetActive(false);
                        starsObj.transform.FindChild("2Star").gameObject.SetActive(false);
                        starsObj.transform.FindChild("3Star").gameObject.SetActive(false);
                        break;
                }
                LevelWon.gameObject.SetActive(true);
                LevelWon.GetComponent<AudioSource>().Play();
                break;
            case GameController.EndGameCondition.Lose:
                LevelLost.gameObject.SetActive(true);
                LevelLost.GetComponent<AudioSource>().Play();
                break;
        }
    }


}
