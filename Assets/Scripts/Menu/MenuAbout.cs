using UnityEngine;
using UnityEngine.UI;

public class MenuAbout : MonoBehaviour
{
    public static MenuAbout instance;

    void Start()
    {
        instance = this;
        Translate();
        Hide();
    }

    void Translate()
    {
        transform.Find("Scroll View/Viewport/Content/Text").GetComponent<Text>().text = U.t("ABOUT DESCRIPTION");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Translate();
    }

    public void Hide() => gameObject.SetActive(false);

    public void OnMenu()
    {
        Menu.instance.Show();
        Hide();
    }
}
