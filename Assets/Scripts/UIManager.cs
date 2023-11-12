using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public int objCount;

    [HideInInspector]
    public bool isPaused;

    [SerializeField]
    private GameObject objective;

    [SerializeField]
    public Image healthImage;

    [SerializeField]
    private GameObject panelWin;

    public GameObject panelLose;

    public GameObject pausePanel;

    public GameObject settingPanel;

    public GameObject tutorPanel;
    private bool isTutor;

    public GameObject newTutorPanel;


    public GameObject specialAtkPanel;
    public GameObject specialAtkBtn;
    

    // Start is called before the first frame update
    [Header("MusicBtn")]
    public GameObject musicBtnOn;
    public GameObject musicBtnOff;

    [Header("SFXBtn")]
    public GameObject SFXBtnOn;
    public GameObject SFXBtnOff;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isTutor = true;
    }
    private void Start()
    {
        if(panelWin != null)
        {
            panelWin.SetActive(false);
        }
        if (panelLose != null)
        {
            panelLose.SetActive(false);
        }
        
    }

    

    // Update is called once per frame
    private void Update()
    {
        if (newTutorPanel != null)
        {

            if (newTutorPanel.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else if (isPaused == false && GameManager.Instance.isGameOver == false)
            {
                Time.timeScale = 1f;
            }

        }
        objCount = GameManager.Instance.Objective;

        if(objective != null)
        {
            switch (objCount)
            {
                case 0:
                    objective.transform.GetChild(0).gameObject.SetActive(false);
                    objective.transform.GetChild(1).gameObject.SetActive(false);
                    objective.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 1:
                    objective.transform.GetChild(0).gameObject.SetActive(true);
                    objective.transform.GetChild(1).gameObject.SetActive(false);
                    objective.transform.GetChild(2).gameObject.SetActive(false);
                    if (isTutor)
                    {
                        tutorPanel.SetActive(true);
                        isPaused = true;
                        Time.timeScale = 0f;
                        isTutor = false;
                    }
                    break;
                case 2:
                    objective.transform.GetChild(0).gameObject.SetActive(true);
                    objective.transform.GetChild(1).gameObject.SetActive(true);
                    objective.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 3:
                    objective.transform.GetChild(0).gameObject.SetActive(true);
                    objective.transform.GetChild(1).gameObject.SetActive(true);
                    objective.transform.GetChild(2).gameObject.SetActive(true);
                    panelWin.SetActive(true);
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }

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

        if(GameManager.Instance.isWin == true && panelWin != null)
        {
            panelWin.SetActive(true);
        }

        if (GameManager.Instance.isGameOver == true && panelLose != null)
        {
            //panelLose.SetActive(true);
        }

    }

    public void OpenSkill()
    {
        specialAtkBtn.SetActive(true);
        specialAtkPanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void UI_Health(float value, float max)
    {
        healthImage.fillAmount = GameManager.Instance.GetPercent(value, max);

    }

    public void CloseTutorPanel()
    {
        tutorPanel.SetActive(false);
        specialAtkPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void musicBtn()
    {
       
        AudioManager.Instance.ToogleMusic();

    }

    public void SFXBtn()
    {
       
        AudioManager.Instance.ToogleSFX();

    }

    public void SettingPanel()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(true);
    }


    public void RestartToChekcpoint()
    {
        panelLose.SetActive(false);
        GameManager.Instance.RestartToChekcpoint();
    }

    public void Pause()
    {
        if (GameManager.Instance.isGameOver == true)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            settingPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }
    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void OpenTutor()
    {
        newTutorPanel.SetActive(true);
    }
}
