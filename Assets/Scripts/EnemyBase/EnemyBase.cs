//using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : PoolableObjectBase, IDamageable
{
    [Header("Properties")]
    [SerializeField] protected EnemyInfos enemyInfos;
    [SerializeField] protected Transform model;
    [SerializeField] protected PoolObjectType[] dropTypes;
    [SerializeField] protected AudioClip clip;
    //[SerializeField] [EnumFlags] private DropType dropType;

    [Header("Stats")]
    [SerializeField] private int level = 1;
    private float maxHealth;
    private float speed;
    protected float range;
    protected float attackDamage;
    protected float directionMultiplier = 1f;
    private float currentHealth;

    [Header("Animation")]
    [SerializeField] protected SimpleAnimancer animancer;
    [SerializeField] protected AnimationClip idleAnim;
    [SerializeField] protected AnimationClip runAnim;
    [SerializeField] protected NavMeshAgent agent;

    protected bool canMove = true;
    protected bool isRunning = false;

    protected ObjectPooler pooler;
    protected VibrationManager vibration;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        vibration = VibrationManager.Instance;
    }

    public override void Init()
    {
        ActionManager.AiUpdater += MoveTowardsPlayer;
        ActionManager.GameEnd += OnGameEnd;

        agent.isStopped = false;
        agent.updateRotation = false;
        canMove = true;
        SetProperties();
    }

    public void DeInit()
    {
        ActionManager.AiUpdater -= MoveTowardsPlayer;
        ActionManager.GameEnd -= OnGameEnd;

        if (agent.navMeshOwner) agent.isStopped = true;
        if (agent.navMeshOwner) canMove = false;
        gameObject.SetActive(false);
    }

    protected abstract void MoveTowardsPlayer(Vector3 player);

    protected abstract void CheckDirection();

    private void OnGameEnd(bool playerWin)
    {
        StopAllCoroutines();
        agent.isStopped = true;
        canMove = false;
        animancer.PlayAnimation(idleAnim);
    }

    private void SetProperties()
    {
        EnemyInfos.CharacterPref currentLevel = enemyInfos.GetCharacterPrefs[level - 1];

        maxHealth = currentLevel.maxHealth;
        speed = currentLevel.speed;
        range = currentLevel.range;
        attackDamage = currentLevel.attackDamge;

        agent.speed = speed;
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth -= damage;

        SlideText hitText = pooler.GetPooledText();
        hitText.gameObject.SetActive(true);
        hitText.SetTheText("", (int)damage, Color.red, transform.position);
        vibration.SoftVibration();

        var particle = pooler.GetPooledObjectWithType(PoolObjectType.BloodParticle);
        particle.gameObject.SetActive(true);
        particle.transform.position = transform.position;
        particle.Init();

        ActionManager.PlaySound?.Invoke(clip);
        if (currentHealth <= 0)
        {
            var drop = pooler.GetPooledObjectWithType(dropTypes[Random.Range(0, dropTypes.Length)]);
            drop.gameObject.SetActive(true);
            drop.transform.position = transform.position;
            drop.Init();
            DeInit();
        }
    }

    private void OnFear(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StopAllCoroutines();
        StartCoroutine(FearCo(duration));
    }

    private IEnumerator FearCo(float duration)
    {
        canMove = false;
        agent.isStopped = false;
        directionMultiplier = -1;
        isRunning = true;
        yield return CoroutineManager.GetTime(duration, 30f);
        directionMultiplier = 1;
        canMove = true;
    }
}