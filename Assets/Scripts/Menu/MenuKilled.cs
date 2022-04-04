using UnityEngine;
using UnityEngine.UI;

public class MenuKilled : MonoBehaviour
{
    public static MenuKilled instance;
    public bool show { get => gameObject.activeSelf; }

    void Start()
    {
        instance = this;

        transform.Find("Panel/RestartBtn").GetComponent<Button>().onClick.AddListener(OnRestart);
        transform.Find("TopCenter/MenuBtn").GetComponent<Button>().onClick.AddListener(OnMenu);

        //translate
        Translate();


        Hide();
    }

    void Translate()
    {
        transform.Find("TopCenter/MenuBtn").GetComponentInChildren<Text>().text = U.t("MENU");
        transform.Find("Panel/RestartBtn").GetComponentInChildren<Text>().text = U.t("RESTART");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Translate();
    }

    private void OnMenu()
    {
        Menu.instance.Show();
        Hide();
    }


    private void OnRestart()
    {
        Hide();
        MenuStartLevel.instance.StartLevel();
    }

    public void Hide() => gameObject.SetActive(false);
}
