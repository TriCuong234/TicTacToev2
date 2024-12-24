using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject PvPbtn;

    [SerializeField]
    private GameObject PvEbtn;
    [SerializeField]
    private GameObject Exitbtn;

    public GameObject choseMode;
    void Start()
    {
        PvPbtn.GetComponent<Button>().onClick.AddListener(OnClickPvP);
        PvEbtn.GetComponent<Button>().onClick.AddListener(OnClickPvE);
        Exitbtn.GetComponent<Button>().onClick.AddListener(OnClickExit);
    }

    // Update is called once per frame

    void OnClickPvP()
    {
        PlayerPrefs.SetInt("PvP", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }
    void OnClickPvE()
    {
        choseMode.SetActive(true);
    }
    void OnClickExit()
    {
        Application.Quit();
        print("Dang Quitting");
    }
}
