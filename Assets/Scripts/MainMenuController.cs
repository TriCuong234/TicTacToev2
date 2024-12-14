using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        PvPbtn.GetComponent<Button>().onClick.AddListener(OnClickPvP);
        PvEbtn.GetComponent<Button>().onClick.AddListener(OnClickPvE);
        Exitbtn.GetComponent<Button>().onClick.AddListener(OnClickExit);
    }

    // Update is called once per frame

    void OnClickPvP()
    {
        print("hehe");
    }
    void OnClickPvE()
    {
        print("hehe");
    }
    void OnClickExit()
    {
        Application.Quit();
        print("Dang Quitting");
    }
}
