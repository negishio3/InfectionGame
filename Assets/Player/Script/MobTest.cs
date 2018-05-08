using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class MobTest : MonoBehaviour
{
    public enum MobPattern
    {
        WAIT,
        RUNDOMWAIK,
        RUN
    }
    [SerializeField]
    private MobPattern mobPattern;

    [SerializeField, Header("歩き移動速度")]
    private float walkSpeed=1.5f;
    [SerializeField, Header("走り移動速度")]
    private float runSpeed=3.7f;
    [SerializeField, Header("行動開始距離")]
    private int walkDistance;
    [SerializeField]
    private int runDistance;

    private NavMeshAgent navMeshAgent;
    private GameObject[] nearPlayer;
    private Vector3 pos;
    private Vector3 randomPos;
    private float rad;
    private float RandomRunPosrange = 25;
    private float RandomWalkPosRange = 15;
    private Vector3 Mypos;
    [SerializeField]
    private Vector3 nextPos;
    private float playerDistance;//プレイヤーと次の移動場所の距離
    private float nextPosDistance;//次の場所までの距離
    private float dis;//プレイヤーが近ければ0それ以外５


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSearch();
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

        if (Vector3.Distance(nextPos,transform.position)<4||Mypos==nextPos||nextPos==Vector3.zero)
        {
            switch (mobPattern)
            {
                case MobPattern.WAIT:
                    Debug.Log("wait");
                    break;
                case MobPattern.RUNDOMWAIK:
                    MoveRandom(RandomWalkPosRange);
                    break;
                case MobPattern.RUN:
                    Debug.Log("Run");
                    MoveRandom(RandomRunPosrange);
                    break;
            }
        }
        if (Vector3.Distance(nearPlayer[0].transform.position, transform.position) < 5)
        {
            dis = 0;
        }
        else { dis = 5; }
        Mypos = transform.position;
    }

    void PlayerSearch()//Playerタグのobjを近い順に取得する
    {
        if (GameObject.FindGameObjectWithTag("Red"))
        {
            nearPlayer = GameObject.FindGameObjectsWithTag("Red").
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
        }
    }


    void Wait()
    {
        if (Vector3.Distance(nearPlayer[0].transform.position, transform.position) < walkDistance)
        {
            mobPattern = MobPattern.RUNDOMWAIK;
            MoveRandom(RandomWalkPosRange);
            navMeshAgent.speed = walkSpeed;
            return;
        }
    }

    void RundomWalk()
    {
        if (Vector3.Distance(nearPlayer[0].transform.position, transform.position) < runDistance)
        {
            mobPattern = MobPattern.RUN;
            MoveRandom(RandomRunPosrange);
            navMeshAgent.speed = runSpeed;
            return;
        }
    }

    void Run()
    {
        if ((Vector3.Distance(nearPlayer[0].transform.position, transform.position) > runDistance))
        {
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
            playerDistance = Vector3.Distance(nearPlayer[0].transform.position, nextPos);
            nextPosDistance = Vector3.Distance(transform.position, nextPos);

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

