using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static MenuPause instance;
    public bool show { get => gameObject.activeSelf; }

    void Start()
    {
        instance = this;

        transform.Find("Panel/ResumeBtn").GetComponent<Button>().onClick.AddListener(OnResume);
        transform.Find("TopCenter/MenuBtn").GetComponent<Button>().onClick.AddListener(OnMenu);

        Translate();


        Hide();
    }

    void Translate()
    {
        transform.Find("Panel/ResumeBtn").GetComponentInChildren<Text>().text = U.t("RESUME");
        transform.Find("TopCenter/MenuBtn").GetComponentInChildren<Text>().text = U.t("MENU");
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

    public void Hide() => gameObject.SetActive(false);


    private void OnResume()
    {
        Hide();
        G.Unfreeze();
        G.Resume();
    }

}
