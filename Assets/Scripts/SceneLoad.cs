using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject tutorPanel;
    public GameObject settingPanel;
    public GameObject creditPanel;

    //public GameObject pausePanel;
    public static SceneLoad Instance { get; private set; }

    private int currentSceneIndex;
    private int sceneToContinue;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (MenuPanel == null || tutorPanel == null || creditPanel == null)
        {
            return;
        }
       
        /*
        if(Tutor1 == null && Tutor2 == null && Tutor3 == null)
        {
            return;
        }
        */


    }

    private void Start()
    {

        
        if (AudioManager.Instance.x == true)
        {
            if (SceneManager.GetActiveScene().name == ("MainMenu"))

            {
                AudioManager.Instance.PlayMusic("MainMenu");
            }

            AudioManager.Instance.x = false;
        }
        
    }
    public void NewGame()
    {
        
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        
        //AudioManager.Instance.x = true;

    }

    public void TutorPanel()
    {
        if (MenuPanel == null || tutorPanel == null || creditPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(false);
            creditPanel.SetActive(false);
            tutorPanel.SetActive(true);
        }
    }

    public void SettingPanel()
    {
        if (MenuPanel == null || tutorPanel == null || creditPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(true);
            creditPanel.SetActive(false);
            tutorPanel.SetActive(false);
        }
    }
   
    public void CreditPanel()
    {
        if (MenuPanel == null || tutorPanel == null || creditPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(false);
            creditPanel.SetActive(true);
            tutorPanel.SetActive(false);
        }
    }

    public void Back()
    {
        if (MenuPanel == null || tutorPanel == null || creditPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(true);
            settingPanel.SetActive(false);
            creditPanel.SetActive(false);
            tutorPanel.SetActive(false);
        }
    }

    public void BackMainMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        //AudioManager.Instance.x = true;
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != 0)
            SceneManager.LoadScene(sceneToContinue);
        else
            return;
        Time.timeScale = 1f;

        //AudioManager.Instance.x = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {


    }


}
