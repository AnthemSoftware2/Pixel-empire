using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillType
{
    Unskilled,
    Constructor,

}

public class Agent : MonoBehaviour
{
    public SkillType Skill;
    public CarryInventory inventory = new CarryInventory();
    public JobInterface CurrentJob;
    Vector3 jobPosition;
    public bool GetPosition;

    private void Update()
    {
        if(CurrentJob != null)
        {
            if(GetPosition == true)
            {
                jobPosition = new Vector3(CurrentJob.getjobPosition().x, CurrentJob.getjobPosition().y, -2);
                GetPosition = false;
            }
            float distance = Vector2.Distance(transform.position, jobPosition);
            transform.position = Vector3.MoveTowards(transform.position, jobPosition, 5 * Time.deltaTime);

            if(distance <= 0.001)
            {
                CurrentJob.DoWork(5 * Time.deltaTime);
            }
        }
        else
        {
            GetPosition = true;
            JobManager.instance.FindJob(this);
        }
    }
}

