using UnityEngine;
using UnityEngine.UI;

public class MenuStartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuStartLevel instance;

    [SerializeField]
    float timeout = 2;
    float _timeout = 0;
    bool show = false;

    Text levelText;


    void Start()
    {
        instance = this;
        levelText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void StartLevel()
    {

        G.speed = 1;
        levelText.text = $"{U.t("ATTEMPT ")} {G.attempt}";
        _timeout = timeout;

        show = true;
        Show();
        G.Freeze();

    }


    void FixedUpdate()
    {
        if (!show) return;
        _timeout -= Time.fixedDeltaTime;
        if (_timeout < 0)
        {
            show = false;
            Hide();
            G.Unfreeze();
            G.Start();
        }
    }
}
