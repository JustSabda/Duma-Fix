using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject selectLevelPanel;
    public GameObject settingPanel;
    public GameObject creditPanel;

    //public GameObject pausePanel;
    public static SceneLoad Instance { get; private set; }

    private int currentSceneIndex;
    private int sceneToContinue;


    [Header("MusicBtn")]
    public GameObject musicBtnOn;
    public GameObject musicBtnOff;

    [Header("SFXBtn")]
    public GameObject SFXBtnOn;
    public GameObject SFXBtnOff;


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

        //if (MenuPanel == null || selectLevelPanel == null || creditPanel == null || settingPanel == null)
        //{
        //    return;
        //}
       
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            if (!AudioManager.Instance.musicSource.mute)
            {
                musicBtnOn.SetActive(true);
                musicBtnOff.SetActive(false);
            }
            else
            {
                musicBtnOn.SetActive(false);
                musicBtnOff.SetActive(true);
            }

            if (!AudioManager.Instance.sfxSource.mute)
            {
                SFXBtnOn.SetActive(true);
                SFXBtnOff.SetActive(false);
            }
            else
            {
                SFXBtnOn.SetActive(false);
                SFXBtnOff.SetActive(true);
            }
        }
    }

    public void musicBtn()
    {

        AudioManager.Instance.ToogleMusic();

    }

    public void SFXBtn()
    {

        AudioManager.Instance.ToogleSFX();

    }
    public void NewGame()
    {
        
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        
        //AudioManager.Instance.x = true;

    }

    public void SelectLevelPanel()
    {
        if (MenuPanel == null || selectLevelPanel == null || creditPanel == null || settingPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(false);
            creditPanel.SetActive(false);
            selectLevelPanel.SetActive(true);
        }
    }

    public void SettingPanel()
    {
        if (MenuPanel == null || selectLevelPanel == null || creditPanel == null || settingPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(true);
            creditPanel.SetActive(false);
            selectLevelPanel.SetActive(false);
        }
    }
   
    public void CreditPanel()
    {
        if (MenuPanel == null || selectLevelPanel == null || creditPanel == null || settingPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(false);
            settingPanel.SetActive(false);
            creditPanel.SetActive(true);
            selectLevelPanel.SetActive(false);
        }
    }

    public void Back()
    {
        if (MenuPanel == null || selectLevelPanel == null || creditPanel == null || settingPanel == null)
        {
            return;
        }
        else
        {
            MenuPanel.SetActive(true);
            settingPanel.SetActive(false);
            creditPanel.SetActive(false);
            selectLevelPanel.SetActive(false);
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


}
