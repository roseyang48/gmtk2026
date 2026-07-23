using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public enum AnimationTriggerType {Hit, Damaged, Charge}
    public StatBlock statBlock;
    protected float currHP;
    [HideInInspector]
    public Unit targetUnit;
    [HideInInspector]
    public CombatHandler.Team unitTeam;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer hpSprite;
    public Gradient hpGradient;
    public UnitStateMachine StateMachine;
    public IdleState IdleState;
    public FleeState FleeState;
    public AttackState AttackState;
    public ChaseState ChaseState;
    protected MaterialPropertyBlock mpb;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NavMeshObstacle obstacle;
    private bool hasFled;
    [SerializeField] private SpriteRenderer hatSprite;

    void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = statBlock.moveSpeed;
        agent.angularSpeed = statBlock.rotationSpeed;
        obstacle.enabled = false;
        mpb = new MaterialPropertyBlock();
        StateMachine = new UnitStateMachine();
        IdleState = new IdleState(this, StateMachine);
        FleeState = new FleeState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
        AttackState = new AttackState(this, StateMachine);
    }
    void Start()
    {
        currHP = statBlock.maxHP;
        hpSprite.GetPropertyBlock(mpb);
        mpb.SetFloat("_Arc2", 360 * (1 - currHP / statBlock.maxHP));
        hpSprite.SetPropertyBlock(mpb);
        hpSprite.color = hpGradient.Evaluate(1 - currHP / statBlock.maxHP);
        StateMachine.Initialize(IdleState);
    }
    public void Initialize(Color hatColor, CombatHandler.Team team)
    {
        unitTeam = team;
        hatSprite.color = hatColor;
    }
    public virtual void Update()
    {
        if(currHP/statBlock.maxHP <= statBlock.fleeThreshhold && StateMachine.currState != FleeState && !hasFled)
        {
            hasFled = true;
            if(Random.Range(0f,1f) <= statBlock.fleeChance)
            {
                StateMachine.ChangeState(FleeState);
            }
        }
        StateMachine.currState.Update();
    }
    void FixedUpdate()
    {
        StateMachine.currState.FixedUpdate();
    }
    public virtual void Attack()
    {
        animator.Play("Attack");
    }
    public virtual void OnHitEnemy(Unit target)
    {
        target.ChangeHP(-statBlock.attackDamage);
    }
    public virtual void SetTarget(Transform target)
    {
        if(target == null)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.back);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, statBlock.rotationSpeed));
            if(agent.isActiveAndEnabled)
            {
                agent.SetDestination(target.position);
            }
        }
    }
    public virtual void SetTarget(Vector2 target)
    {
        if(target == null)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(target - (Vector2)transform.position, Vector3.back);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, statBlock.rotationSpeed));
            if(agent.isActiveAndEnabled)
            {
                agent.SetDestination(target);
            }
        }
    }
    public virtual void ChangeHP(float value)
    {
        currHP += value;
        if(value < 0)
        {
            animator.Play("Damaged");
        }
        if(currHP <= 0)
        {
            Die();
        }
        mpb.SetFloat("_Arc2", 360 * (1 - currHP / statBlock.maxHP));
        hpSprite.SetPropertyBlock(mpb);
        hpSprite.color = hpGradient.Evaluate(1 - currHP / statBlock.maxHP);
    }
    private void AnimationTriggerEvent(AnimationTriggerType type)
    {
        StateMachine.currState.AnimationTriggerEvent(type);
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void SetObstacle(bool setObstacle)
    {
        if(setObstacle)
        {
            agent.enabled = false;
            obstacle.enabled = true;
        }
        else
        {
            obstacle.enabled = false;
            StartCoroutine("EnableAgent");
        }
    }

    private IEnumerator EnableAgent()
    {
        yield return new WaitUntil(()=> obstacle.enabled == false);
        agent.enabled = true;
    }
    
    public bool UpdateTarget()
    {
        float minDistance = float.PositiveInfinity;
        GameObject[] targetArray = {};
        if(unitTeam == CombatHandler.Team.Enemy)
        {
            targetArray = GameObject.FindGameObjectsWithTag("PlayerUnit");
        }
        else if(unitTeam == CombatHandler.Team.Player)
        {
            targetArray = GameObject.FindGameObjectsWithTag("EnemyUnit");
        }
        if(targetArray.Length != 0)
        {
            foreach(GameObject obj in targetArray)
            {
                if((obj.transform.position - transform.position).magnitude < minDistance)
                {
                    minDistance = (obj.transform.position - transform.position).magnitude;
                    targetUnit = obj.GetComponent<Unit>();
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
