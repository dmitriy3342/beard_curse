using UnityEngine;
using UnityEngine.UI;
public class UIStats : MonoBehaviour
{
    public static UIStats instance;
    private Text speedNumber;
    private Text frameCount;

    void Start()
    {
        instance = this;
        speedNumber = transform.Find("SpeedNumber").gameObject.GetComponent<Text>();
        frameCount = transform.Find("FrameCount").gameObject.GetComponent<Text>();
        transform.Find("TopCenter/PauseBtn").GetComponent<Button>().onClick.AddListener(OnPause);
        transform.Find("ClickBtn").GetComponent<Button>().onClick.AddListener(Player.instance.onClick);

        //translate
        transform.Find("TopCenter/PauseBtn").GetComponentInChildren<Text>().text = U.t("PAUSE");

    }
    public void OnPause()
    {
        MenuPause.instance.Show();
        G.Pause();
    }

    void Update()
    {



    }

    void OnGUI()
    {
        speedNumber.text = G.speed.ToString();
        frameCount.text = G.fps.ToString();
    }
}
