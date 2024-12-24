using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Button settingBtn;
    public Button exitBtn;
    public Button continueBtn;

    public GameObject settingPanel;
    void Start()
    {
        settingBtn.onClick.AddListener(OnSettingBtnClick);
        exitBtn.onClick.AddListener(OnExitBtnClick);
        continueBtn.onClick.AddListener(OnContinueBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingBtnClick()
    {
        settingPanel.SetActive(true);
    }

    public void OnExitBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnContinueBtnClick()
    {
        settingPanel.SetActive(false);
    }
}
