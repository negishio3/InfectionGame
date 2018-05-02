using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class MobTest : AIBase
{
    public enum MobPattern
    {
        WAIT,
        RUNDOMWAIK,
        RUN
    }
    [SerializeField]
    private MobPattern mobPattern;

    [SerializeField]
    private int walkDistance;
    [SerializeField]
    private int runDistance;

    private Vector3 Mypos;
    private Vector3 nextPos;
    private float RandomRunPosrange = 25;
    private float RandomWalkPosRange = 15;
    private float playerDistance;//プレイヤーと次の移動場所の距離
    private float nextPosDistance;//次の場所までの距離
    private float dis;//プレイヤーが近ければ0それ以外５
    private int numberOfZombi;//変身するゾンビの番号


    protected override void Start()
    {
        base.Start();
        targetTag = "Player";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (mobPattern)
        {
            case MobPattern.WAIT:
                Wait();
                break;
            case MobPattern.RUNDOMWAIK:
                RundomWalk();
                break;
            case MobPattern.RUN:
                Run();
                break;
        }

        if (Vector3.Distance(nextPos, transform.position) < 4 || Mypos == nextPos || nextPos == Vector3.zero)
        {
            switch (mobPattern)
            {
                case MobPattern.WAIT:
                    break;
                case MobPattern.RUNDOMWAIK:
                    MoveRandom(RandomWalkPosRange);
                    break;
                case MobPattern.RUN:
                    MoveRandom(RandomRunPosrange);
                    break;
            }
        }

        if (targetMobs.Any())
        {
            if (Vector3.Distance(targetMobs[0].transform.position, transform.position) < 5)
            {
                dis = 0;
            }
            else { dis = 5; }
        }
        Mypos = transform.position;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        throw new System.NotImplementedException();
    }


    void Wait()
    {
        if (targetMobs.Any())
        {
            if (Vector3.Distance(targetMobs[0].transform.position, transform.position) < walkDistance)
            {
                mobPattern = MobPattern.RUNDOMWAIK;
                MoveRandom(RandomWalkPosRange);
                navMeshAgent.speed = walkSpeed;
                return;
            }
        }
    }

    void RundomWalk()
    {
        if (targetMobs.Any())
        {
            if (target)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < runDistance)
                {
                    mobPattern = MobPattern.RUN;
                    MoveRandom(RandomRunPosrange);
                    navMeshAgent.speed = runSpeed;
                    return;
                }
            }
            //視界に入ってなくても逃げ始める
            else if (Vector3.Distance(targetMobs[0].transform.position, transform.position) < runDistance / 2)
            {
                target = targetMobs[0];
                mobPattern = MobPattern.RUN;
                MoveRandom(RandomRunPosrange);
                navMeshAgent.speed = runSpeed;
                return;
            }
        }
    }

    void Run()
        {
        if (targetMobs.Any())
        {
            if ((Vector3.Distance(targetMobs[0].transform.position, transform.position) > runDistance))
            {
                target = null;
                mobPattern = MobPattern.RUNDOMWAIK;
                MoveRandom(RandomWalkPosRange);
                navMeshAgent.speed = walkSpeed;
                return;
            }
        }
        else
        {
            target = null;
            mobPattern = MobPattern.RUNDOMWAIK;
            MoveRandom(RandomWalkPosRange);
            navMeshAgent.speed = walkSpeed;
            return;
        }
    }

    bool GetRandomPosition(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    void MoveRandom(float range)
    {
        if(GetRandomPosition(transform.position,range,out nextPos))
        {
            playerDistance = Vector3.Distance(targetMobs[0].transform.position, nextPos);
            nextPosDistance = Vector3.Distance(transform.position, nextPos);

            //プレイヤーに近い位置ならやり直す
            if ((mobPattern==MobPattern.RUN&&playerDistance - dis < nextPosDistance )||
            (mobPattern == MobPattern.RUNDOMWAIK && playerDistance < runDistance+2))
            {
                MoveRandom(range);
            }
            else
            {
                navMeshAgent.SetDestination(nextPos);
            }
        }
    }

}

