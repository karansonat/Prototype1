using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoSingleton<UIController>
{

    private GameObject _starsGo;

	// Use this for initialization
	void Start () {
	    _starsGo = GameObject.Find("Stars");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Retry()
    {
        SceneManager.LoadScene(Application.loadedLevelName);
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
        }
    }
}
