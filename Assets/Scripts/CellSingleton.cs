using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellSingleton : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    private GameObject playerController;
    private PlayerController playerControllerScript;

    private Sprite OSprite;
    private Sprite XSprite;
    void Start()
    {
        OSprite = Resources.Load<Sprite>("sprites/OassetWhite");
        XSprite = Resources.Load<Sprite>("sprites/XassetWhite");
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnBtnClick);
        playerControllerScript = playerController.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBtnClick()
    {

        GetBtnPosition();
    }

    void GetBtnPosition()
    {
        int name = int.Parse(this.gameObject.name);
        int r = 0;
        int c = 0;
        if (name < 9)
        {
            r = 0;
            c = name % 10;
            ChangeBtnOnClick(r,c);
            return;
        }
        r = name / 10;
        c = name % 10;
        ChangeBtnOnClick(r,c);

    }

    public void ChangSprite(int player)
    {
        Image imageBtn = this.gameObject.GetComponent<Image>();
        if (player == 1)
        {
            imageBtn.sprite = OSprite;
            imageBtn.type = Image.Type.Simple;
            imageBtn.preserveAspect = true;
            return;
        }
        imageBtn.sprite = XSprite;
        imageBtn.type = Image.Type.Simple;
        imageBtn.preserveAspect = true;
    }

    public void ResetAllChanged()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.interactable = true;
        Image img = this.gameObject.GetComponent<Image>();
        img.sprite = null;
        img.type = Image.Type.Simple;
    }

    public void ChangeBtnOnClick(int r, int c)
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        ChangSprite(playerControllerScript.PlayerNow());
        playerControllerScript.CellClick(r, c);
        if (playerControllerScript.PlayerWin(r,c) == 1 || playerControllerScript.PlayerWin(r,c) == -1)
        {
            return;
        }
        playerControllerScript.ChangePlayer();

    }

    public void ChangeBtnAiPick(int r, int c)
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        ChangSprite(-1);
        return;
    }   
}
