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
    public Slider bgmSlider;
    public Slider sfxSlider;
    public GameObject settingPanel;

    [SerializeField]
    private PlayerController playerController;
    void Start()
    {
        settingBtn.onClick.AddListener(OnSettingBtnClick);
        exitBtn.onClick.AddListener(OnExitBtnClick);
        continueBtn.onClick.AddListener(OnContinueBtnClick);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", AudioController.instance.bgmSource.volume);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", AudioController.instance.sfxSource.volume);
        bgmSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void OnSettingBtnClick()
    {
        settingPanel.SetActive(true);
        playerController.StopAllCoroutine();
    }

    public void OnExitBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnContinueBtnClick()
    {
        settingPanel.SetActive(false);
        playerController.StartTimer(playerController.GetTimeNow());
    }
    void OnBGMSliderValueChanged(float value)
    {
        AudioController.instance.bgmSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }
    void OnSFXSliderValueChanged(float value)
    {
        AudioController.instance.sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}