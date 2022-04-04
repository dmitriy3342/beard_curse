using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatistics : MonoBehaviour
{
    public static MenuStatistics instance;

    public GameObject statisticsItem;
    public GameObject menuItem;

    Transform content;

    void Start()
    {
        instance = this;
        content = transform.Find("Scroll View/Viewport/Content");

        gameObject.SetActive(false);
    }


    private void InitItemView(GameObject go, Dictionary<string, long> statistics)
    {
        go.transform.Find("Attempt").GetComponent<Text>().text = $"{statistics["attempt"]}";
        go.transform.Find("ResultText").GetComponent<Text>().text = $"{U.t("RESULT")}";
        go.transform.Find("ResultValue").GetComponent<Text>().text = $"{U.ms2time(statistics["result"])}";
        go.transform.Find("SpeadValue").GetComponent<Text>().text = $"SPEED: {statistics["speed"]}";
    }

    public void Show()
    {
        GameObject go = Instantiate(menuItem, content);
        go.GetComponentInChildren<Button>().onClick.AddListener(OnMenu);

        int[] keys = new int[G.statisticsDict.Keys.Count];
        G.statisticsDict.Keys.CopyTo(keys, 0);
        Array.Sort(keys);
        Array.Reverse(keys);

        foreach (var key in keys)
        {
            go = Instantiate(statisticsItem, content);
            InitItemView(go, G.statisticsDict[key]);
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }


    public void OnMenu()
    {
        Menu.instance.Show();
        Hide();
    }

    void Update()
    {

    }
}
