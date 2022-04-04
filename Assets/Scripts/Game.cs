using UnityEngine;

public class Game : MonoBehaviour
{
    int frameCount = 0;
    float startTime = 0;
    void Awake()
    {
        Application.targetFrameRate = 120;
        G.LoadStatistics();
    }
    // Start is called before the first frame update
    void Start()
    {
        //#if UNITY_EDITOR
        //QualitySettings.vSyncCount = 0;
        ////Application.targetFrameRate = 45;

        //#endif
        startTime = Time.time;

    }

    //private void OnEnable()
    //{
    //    G.LoadStatistics();
    //}

    //private void OnDisable()
    //{
    //    G.SaveStatistics();
    //}


    //private void OnDestroy()
    //{
    //    G.SaveStatistics();
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (G.isEnabledFreezing) G.Unfreeze();
            else G.Freeze();
        }

        frameCount++;
        if (Time.time - startTime >= 1f)
        {
            G.fps = frameCount;
            frameCount = 0;
            startTime = Time.time;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
