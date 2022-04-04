using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player instance;


    private SpriteRenderer spriteRendererMan;

    public Sprite[] spriteArray;

    private float speedTimestamp;
    private float speedDelay = 3;
    public int state = 0;
    private float stateTimestamp;
    private bool fail = false;


    public AudioClip scissorsAudioClip;
    public AudioClip chponkAudioClip;
    public AudioClip amogusAudioClip;

    public AudioSource audioSource;

    public float DelayState => Mathf.Pow(0.9f, 10+G.speed);

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spriteRendererMan = transform.Find("Man").GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = scissorsAudioClip;
    }

    bool chponk = false;
    bool amongus = false;

    public void Init()
    {
        chponk = false;
        amongus = false;
        fail  = false;
        state = 0;
        speedTimestamp = Time.time;
        stateTimestamp = Time.time;
        spriteRendererMan.sprite = spriteArray[state];
        audioSource.clip = scissorsAudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (fail && Time.time - stateTimestamp >= 0.1)
        {
            if (state<spriteArray.Length-1)
            {
                state+=1;
                spriteRendererMan.sprite = spriteArray[state];
                stateTimestamp = Time.time;

                if (!chponk)
                {
                    chponk = true;
                    audioSource.clip = chponkAudioClip;
                    audioSource.PlayScheduled(0);
                }

            }
            else
            {
                if (!amongus)
                {
                    amongus = true;
                    audioSource.clip = amogusAudioClip;
                    audioSource.PlayScheduled(0);
                }
            }

        }

        if (chponk && Time.time - stateTimestamp >= 3)
        {
            if (state>=spriteArray.Length-1)
            {
                MenuKilled.instance.Show();
                G.Freeze();
                Init();
            }
        }


        if (G.isEnabledFreezing) return;

        if (!fail && Time.time - speedTimestamp>=speedDelay)
        {
            speedTimestamp = Time.time;
            G.speed+=1;
            Debug.Log($"Set speed {G.speed} DelayState {DelayState}");
        }

        if (!fail && Time.time - stateTimestamp >= DelayState)
        {
            stateTimestamp = Time.time;
            state+=1;
            if (state>=16)
            {
                fail = true;
                G.Stop();
                G.Freeze();
            }
            spriteRendererMan.sprite = spriteArray[state];

        }



        //var targetRotation = Quaternion.LookRotation(G.closePositionCat - G.playerEyesPosition);
        //float speedRotate = 1000f;

        ////eyes.rotation = Quaternion.Slerp(eyes.rotation, targetRotation, speedRotate * Time.deltaTime);


        //var tmpAngle = Vector2.Angle(Vector2.right, G.closePositionCat - eyel.position);//угол между вектором от объекта к мыше и осью х
        //var angle = eyel.position.y < G.closePositionCat.y ? tmpAngle : -tmpAngle;

        //angle = Mathf.MoveTowardsAngle(eyel.eulerAngles.z, angle, speedRotate * Time.deltaTime);


        ////eyel.eulerAngles = eyer.eulerAngles = new Vector3(0f, 0f, angle);//поворот глаза
        //eyel.eulerAngles = eyer.eulerAngles = new Vector3(0f, 0f, angle);//поворот глаза
        //Quaternion eyelpupilLocalRotation = eyelpupil.localRotation;
        //eyelpupilLocalRotation.eulerAngles = new Vector3(0f, 0f, -angle);//обратный поворот зрачка
        //eyelpupil.localRotation = eyerpupil.localRotation = eyelpupilLocalRotation;




        //print($"close cat position ({G.closePositionCat.x},{G.closePositionCat.y}) angle {angle}");

        //if (Input.anyKey)
        //{
        //    if (G.hp > 0)
        //    {
        //        animator.SetBool("isAttack", true);
        //    }
        //}
        //else
        //{
        //    animator.SetBool("isAttack", false);
        //}

    }

    public void onClick()
    {
        if (G.isEnabledFreezing) return;
        if (state>0) state--;
        spriteRendererMan.sprite = spriteArray[state];
        stateTimestamp = Time.time;
        audioSource.PlayScheduled(0);
    }

}
