using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Magazine magazine;
    private PlayerInput controls;
    private bool shotInput;

    private void Awake()
    {
        controls = new PlayerInput();

        controls.Shoot.Shoot.started += ctx => QueueShot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        magazine.Start();
    }

    private void Update()
    {
        magazine.Update();
        AttemptShot();
        Debug.Log(magazine.currentMagazine);
    }
    
    public void QueueShot()
    {
        shotInput = true;
    }

    public void AttemptShot()
    {
        if (!shotInput)
        {
            return;
        }
        shotInput = false;
        if (magazine.CanShoot())
        {
            magazine.Shoot();
        }
    }
}
