using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 _direction;
    private Vector3 _forward, _right;
    private float _aimTime;
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private EquipmentManager _equipmentManager;
    private Animator _animator;
    private bool _aiming = false;
    private NavMeshAgent _navMeshAgent;
    // Animator parameters
    private readonly int RunHash = Animator.StringToHash("Run");
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int StateTimeHash = Animator.StringToHash("StateTime");
    private readonly int DrawBowHash = Animator.StringToHash("Draw");

    public CharacterStatsBase characterStats;
    public HealthIndicator healthIndicator;

    public bool PlayerReachedMapBoundaries { get; set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Attack"].started += context => DrawBow();
        _playerInput.actions["Attack"].canceled += context => Attack();

        //_playerInput.actions["Aim"].started += context => Aim();
    }

    // Start is called before the first frame update
    void Start()
    {
        _forward = Camera.main.transform.forward;
        _forward.y = 0f;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(0, 90, 0) * _forward;

        _controller = GetComponent<CharacterController>();
        _equipmentManager = GetComponent<EquipmentManager>();
        _animator = GetComponent<Animator>();

        //healthIndicator.SetMaxHealth(characterStats.maxHealth);
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(StateTimeHash, Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
    }

    private Vector3 ConvertFromCartesianToIsometric(float z, float x)
    {
        Vector3 upMovement = _forward * z;
        Vector3 rightMovement = _right * x;

        return Vector3.Normalize(rightMovement + upMovement);
    }

    private void Move()
    {
        if (enabled)
        {
            var input = _playerInput.actions["Move"].ReadValue<Vector2>();
            if (input.y != 0 || input.x != 0)
            {
                _direction = ConvertFromCartesianToIsometric(input.y, input.x);
                _direction *= characterStats.movementSpeed;

                transform.forward = _direction;
                _navMeshAgent.Move(_direction * Time.deltaTime);
                _animator.SetBool(RunHash, true);
                FindObjectOfType<AudioManager>().PlaySound("footstep");

                //animator.ResetTrigger("Hit");
                if (_direction != Vector3.zero /*&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Dive Forward")*/)
                {
                }
            }
            else
            {
                _animator.SetBool(RunHash, false);
                FindObjectOfType<AudioManager>().StopSound("footstep");
            }
        }
    }

    private void Aim()
    {
        var input = _playerInput.actions["Aim"].ReadValue<Vector2>();
        if (input.y != 0 || input.x != 0)
        {
            //if (input.y > 1 || input.x > 1)
            //{
            //Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //transform.LookAt(position);
            //var normalizedInput = Vector3.Normalize(input);
            //_direction = ConvertFromCartesianToIsometric(pos.y, pos.x);
            //_direction *= characterStats.movementSpeed;
            //Vector3.Normalize(_direction);
            //transform.forward = pos;
            //}
            //else
            //{
            _direction = ConvertFromCartesianToIsometric(input.y, input.x);
            //_direction *= characterStats.movementSpeed;
            transform.forward = _direction;
            //}
        }
    }

    private void Attack()
    {
        var weapon = _equipmentManager.GetMainHandWeapon();

        if (weapon.WeaponType == WeaponType.Bow)
        {
            if (_aimTime > 1f)
            {
                _aimTime = 0;
                ShootArrow();
            }

            _animator.SetBool(DrawBowHash, false);
        }
        else
        {
            _animator.SetTrigger(AttackHash);
            Debug.Log("Attack Triggered");
            //StartCoroutine(FireAttack());
        }
    }

    //public GameObject fireSlash;

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    _pressed = true;
    //    if (_isBowEquiped)
    //    {
    //        _aimTime += Time.time;
    //    }
    //    else
    //    {
    //        _aimTime = 0;
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    _pressed = false;
    //}

    private void DrawBow()
    {
        _aiming = true;
        var weapon = _equipmentManager.GetMainHandWeapon();

        if (weapon.WeaponType == WeaponType.Bow)
        {
            _aimTime += Time.time;
            _animator.SetBool(DrawBowHash, true);
        }
    }

    private void ShootArrow()
    {
        var weapon = _equipmentManager.GetMainHandWeapon();
        var projectilePrefab = Instantiate(weapon.projectilePrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        var projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.damage = weapon.Damage;
        projectile.range = weapon.Range;
        projectile.targetMask = _equipmentManager.targetLayer;
        Rigidbody rb = projectilePrefab.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 500f);
        _aiming = false;
    }

    //private IEnumerator FireAttack()
    //{
    //    var transform = fireSlash.GetComponent<Transform>();
    //    transform.rotation = transform.rotation * player.transform.rotation;
    //    fireSlash.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    fireSlash.SetActive(false);
    //}
}
