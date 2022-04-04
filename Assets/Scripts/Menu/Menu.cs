using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    void Start()
    {
        instance = this;

        transform.Find("CenterPanel/PlayBtn").GetComponent<Button>().onClick.AddListener(OnPlay);
        transform.Find("CenterPanel/ProgressBtn").GetComponent<Button>().onClick.AddListener(OnProgressBtn);
        transform.Find("LeftBottomPanel/ExitBtn").GetComponent<Button>().onClick.AddListener(OnExitBtn);
        transform.Find("RightTopPanel/AboutBtn").GetComponent<Button>().onClick.AddListener(OnAboutBtn);
        transform.Find("RightTopPanel/SettingsBtn").GetComponent<Button>().onClick.AddListener(OnSettingsBtn);

        Translate();

        G.Freeze();
    }

    void Translate()
    {
        transform.Find("CenterPanel/PlayBtn").GetComponentInChildren<Text>().text = U.t("PLAY GAME");
        transform.Find("CenterPanel/ProgressBtn").GetComponentInChildren<Text>().text = U.t("PROGRESS");
        transform.Find("LeftBottomPanel/ExitBtn").GetComponentInChildren<Text>().text = U.t("EXIT");
        transform.Find("RightTopPanel/AboutBtn").GetComponentInChildren<Text>().text = U.t("ABOUT");
        transform.Find("RightTopPanel/SettingsBtn").GetComponentInChildren<Text>().text = U.t("LANG") == "RU" ? "ENGLISH" : "ÐÓÑÑÊÈÉ";
    }

    void OnPlay()
    {
        MenuStartLevel.instance.StartLevel();
        Hide();
        G.Unfreeze();
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    void OnProgressBtn()
    {
        MenuStatistics.instance.Show();
        Hide();
    }
    void OnExitBtn() => Application.Quit();
    void OnAboutBtn()
    {
        MenuAbout.instance.Show();
        Hide();
    }
    void OnSettingsBtn()
    {
        Debug.Log("OnSettingsBtn");

        if (U.t("LANG") == "RU")
        {
            U.setLang("en");
        }
        else
        {
            U.setLang("ru");
        }
        Translate();
    }

}
