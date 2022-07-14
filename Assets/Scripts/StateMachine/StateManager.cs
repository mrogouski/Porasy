using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    private IBaseState _currentState;
    private IdleState _idleState;
    private WalkState _walkState;
    private PursuitState _pursuitState;
    private AttackState _attackState;
    private HitState _hitState;
    private DeadState _deadState;

    // Player Parameters
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public EquipmentManager equipmentManager;
    [HideInInspector] public AbilityHolder abilityHolder;
    public Renderer mainRenderer;

    // Animator parameters
    [HideInInspector] public int IdleHash = Animator.StringToHash("Idle");
    [HideInInspector] public int WalkHash = Animator.StringToHash("Walk");
    [HideInInspector] public int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    [HideInInspector] public int RunHash = Animator.StringToHash("Run");
    [HideInInspector] public int AlertHash = Animator.StringToHash("EnemySpotted");
    [HideInInspector] public int AttackHash = Animator.StringToHash("Attack");
    [HideInInspector] public int ShootHash = Animator.StringToHash("Shoot");
    [HideInInspector] public int InvokeHash = Animator.StringToHash("Invoke");
    [HideInInspector] public int HitHash = Animator.StringToHash("Hit");
    [HideInInspector] public int DeathHash = Animator.StringToHash("Death");

    [HideInInspector] public List<GameObject> _minions;

    // Enemy AI Parameters
    [HideInInspector] public Vector3 SpawnPoint { get; private set; }
    [HideInInspector] public Vector3 TargetPosition { get; private set; }
    [HideInInspector] public bool TargetDetected { get; private set; }
    [HideInInspector] public bool TargetOnAttackRange { get; private set; }

    public EnemyStatsBase CharacterStats;

    public IdleState IdleState
    {
        get
        {
            if (_idleState == null)
            {
                _idleState = new IdleState();
            }

            return _idleState;
        }
    }

    public WalkState WalkState
    {
        get
        {
            if (_walkState == null)
            {
                _walkState = new WalkState();
            }

            return _walkState;
        }
    }

    public PursuitState PursuitState
    {
        get
        {
            if (_pursuitState == null)
            {
                _pursuitState = new PursuitState();
            }

            return _pursuitState;
        }
    }

    public AttackState AttackState
    {
        get
        {
            if (_attackState == null)
            {
                _attackState = new AttackState();
            }

            return _attackState;
        }
    }

    public HitState HitState
    {
        get
        {
            if (_hitState == null)
            {
                _hitState = new HitState();
            }

            return _hitState;
        }
    }

    public DeadState DeadState
    {
        get
        {
            if (_deadState == null)
            {
                _deadState = new DeadState();
            }

            return _deadState;
        }
    }

    public bool GotHit { get; set; }

    public bool IsDead { get; set; }

    private void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void SwitchState(IBaseState state)
    {
        if (_currentState != null && _currentState != state)
        {
            _currentState.ExitState(this);
        }

        _currentState = state;
        _currentState.EnterState(this);
    }

    public void OnAlertTriggerStateChanged(bool entered, GameObject target)
    {
        TargetDetected = entered;
        TargetPosition = target.transform.position;
    }

    public void OnAttackTriggerStateChanged(bool inRange, GameObject target)
    {
        TargetOnAttackRange = inRange;
        TargetPosition = target.transform.position;
    }

    public void OnHit()
    {
        GotHit = true;
    }

    private void Initialize()
    {
        animator = gameObject.GetComponent<Animator>();
        audioManager = gameObject.GetComponent<AudioManager>();
        equipmentManager = gameObject.GetComponent<EquipmentManager>();
        //mainRenderer = gameObject.GetComponentInChildren<Renderer>();

        if (gameObject.TryGetComponent(out CharacterController characterController))
        {
            CharacterController = characterController;
        }

        if (gameObject.TryGetComponent(out NavMeshAgent navMeshAgent))
        {
            this.navMeshAgent = navMeshAgent;
        }

        if (gameObject.TryGetComponent(out AbilityHolder abilityHolder))
        {
            this.abilityHolder = abilityHolder;
        }

        SpawnPoint = transform.position;
        _currentState = IdleState;
        _currentState.EnterState(this);
    }

    public void ShootProjectile()
    {
        var weapon = equipmentManager.GetMainHandWeapon();
        var projectile = Instantiate(weapon.projectilePrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        projectile.GetComponent<Projectile>().damage = weapon.Damage;
        projectile.GetComponent<Projectile>().range = weapon.Range;
        projectile.GetComponent<Projectile>().targetMask = equipmentManager.targetLayer;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 500f);
    }
}
