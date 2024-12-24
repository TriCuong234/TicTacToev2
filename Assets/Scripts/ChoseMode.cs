using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseMode : MonoBehaviour
{
    // Start is called before the first frame update
    public ScrollRect scrollRect;

    public RectTransform viewPort;

    public RectTransform content;

    public HorizontalLayoutGroup horizontalLayoutGroup;

    public RectTransform[] items;

    Vector2 oldVelocity;

    bool isUpdate;
    void Start()
    {
        items[0].GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[0]));
        items[1].GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[1]));
        items[2].GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[2]));
        isUpdate = false;
        oldVelocity = Vector2.zero;
        int ItemsToAdd = Mathf.CeilToInt(viewPort.rect.width / (items[0].rect.width + horizontalLayoutGroup.spacing));

        for (int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform newItem = Instantiate(items[i % items.Length], content);
            newItem.SetAsLastSibling();
            newItem.GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[i % items.Length]));
        }

        for (int i = 0; i < ItemsToAdd; i++)
        {
            int num = items.Length - i - 1;
            RectTransform newItem = Instantiate(items[num], content);
            newItem.SetAsFirstSibling();
            newItem.GetComponent<Button>().onClick.AddListener(() => OnItemClick(items[num]));
        }

        content.localPosition = new Vector3((0 - (items[0].rect.width + horizontalLayoutGroup.spacing) * ItemsToAdd), content.localPosition.y, content.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdate)
        {
            scrollRect.velocity = oldVelocity;
            isUpdate = false;
        }
        if (content.localPosition.x > 0)
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            content.localPosition -= new Vector3(items.Length * (items[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdate = true;
        }
        if (content.localPosition.x < (0 - (items[0].rect.width + horizontalLayoutGroup.spacing) * items.Length))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            content.localPosition += new Vector3(items.Length * (items[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdate = true;
        }
    }
    void AddOnClickEvent(RectTransform item)
    {
        Button btn = item.GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnItemClick(item));
        }
    }
    void OnItemClick(RectTransform item)
    {
        if (item.CompareTag("ez"))
        {
            PlayerPrefs.SetInt("PvP", 0);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Mode", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GamePlay");
        }
        if (item.CompareTag("med"))
        {
            PlayerPrefs.SetInt("PvP", 0);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Mode", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GamePlay");
        }
        if (item.CompareTag("hard"))
        {
            PlayerPrefs.SetInt("PvP", 0);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("Mode", 2);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GamePlay");
        }
    }
}