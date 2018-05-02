using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public abstract class AIBase : PlayerNumber {
    [SerializeField, Header("歩き移動速度")]
    protected float walkSpeed = 1.5f;
    [SerializeField, Header("走り移動速度")]
    protected float runSpeed = 3.7f;
    [SerializeField, Header("obj探索範囲")]
    protected float searchDistance;


    protected NavMeshAgent navMeshAgent;
    protected GameObject[] targetMobs;
    protected GameObject target;
    protected int num;
    protected string targetTag;
    protected string scriptName;



    protected virtual void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}

	protected virtual void Update ()
    {
        InSight();
	}

    protected abstract void OnTriggerEnter(Collider col);

    protected void InSight()
    {
        SearchObj(targetTag, out targetMobs);
        if (targetMobs.Any())
        {
            num = 0;
            while (targetMobs.Length > num)
            {
                if (RayCheck())
                {
                    target = targetMobs[num];
                    return;
                }
                else
                {
                    num++;
                }
            }
        }
    }

    bool RayCheck()//視界に入っているか
    {
        if (targetMobs[num] != null)
        {
            Vector3 dir = targetMobs[num].transform.position - transform.position;

            if (ViewingAngle(dir))
            {
                Ray ray = new Ray(transform.position, dir);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == targetTag)
                    {
                        Debug.Log("Insight");
                        return true;
                    }


                    return false;
                }
                return false;
            }
            return false;
        }
        else
        {

        }
        return false;
    }

    bool ViewingAngle(Vector3 dir)//正面にいるか確認
    {
        dir.Normalize();
        float dot = Vector3.Dot(dir, transform.forward);
        float rad = Mathf.Acos(dot);
        if (rad < 0.85)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SearchObj(string tag, out GameObject[] objs)//範囲内のタグのobjを近い順に取得する
    {
        if (GameObject.FindGameObjectWithTag(tag))
        {
            objs = GameObject.FindGameObjectsWithTag(tag).
            Where(e => Vector3.Distance(transform.position, e.transform.position) < searchDistance).
            Where(e => e.GetComponent<PlayerNumber>().PlayerNum != playerNum).
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
        }
        else
        {
            objs = null;
        }
    }
}
