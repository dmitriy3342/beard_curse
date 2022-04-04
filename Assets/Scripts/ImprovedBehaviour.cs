using UnityEngine;


public abstract class ImprovedBehaviour : MonoBehaviour
{


    public enum PAUSE_MODE
    {
        Inherit,
        Stop,
        Process
    }

    [SerializeField]
    private PAUSE_MODE pauseMode = PAUSE_MODE.Inherit;

    private PAUSE_MODE? _pauseMode;

    public PAUSE_MODE GetPauseMode()
    {
        if (_pauseMode != null)
        {
            return (PAUSE_MODE)_pauseMode;
        }

        if (pauseMode == PAUSE_MODE.Inherit)
        {
            try
            {
                _pauseMode = transform.parent.GetComponent<ImprovedBehaviour>().GetPauseMode();
            }
            catch (System.Exception)
            {
                _pauseMode = PAUSE_MODE.Stop;
            }
        }
        else
        {
            _pauseMode = pauseMode;
        }
        return (PAUSE_MODE)_pauseMode;
    }

    // Вынести заборозку сюда, чтобы она происходила один раз
    private bool isFrozen => GetPauseMode() == PAUSE_MODE.Process ? false : G.isEnabledFreezing;
    private bool _isUnFrozenState = false;

    void Start()
    {
        _isUnFrozenState = !isFrozen;
        ImprovedStart();
    }

    void Freeze()
    {
        try
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
        catch (System.Exception) { }
    }


    void UnFreeze()
    {
        try
        {
            GetComponent<Rigidbody2D>().simulated = true;
        }
        catch (System.Exception) { }

    }

    void Update()
    {
        if (_isUnFrozenState) ImprovedUpdate();
    }
    void FixedUpdate()
    {
        if (isFrozen && _isUnFrozenState)
        {
            Freeze();
            _isUnFrozenState = false;
            return;
        }

        if (!isFrozen && !_isUnFrozenState)
        {
            UnFreeze();
            _isUnFrozenState = true;
        }

        if (_isUnFrozenState) ImprovedFixedUpdate();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFrozen) return;
        ImprovedOnCollisionEnter2D(collision);
    }
    // Start is called before the first frame update
    protected virtual void ImprovedStart() { }
    // Update is called once per frame
    protected virtual void ImprovedUpdate() { }
    protected virtual void ImprovedFixedUpdate() { }
    protected virtual void ImprovedOnCollisionEnter2D(Collision2D collision) { }


}

