using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SideJob : JobInterface
{
    public int AgentLimits;
    public List<Agent> agents;

    public Tile tile;
    public float JobTime;

    public Job job;

    public Action<SideJob> OnJobStart;
    public Action<SideJob> OnJobEnd;

    public SideJob(int AgentLimits, Tile tile, float JobTime, Job job, Action<SideJob> OnJobStart, Action<SideJob> OnJobEnd)
    {
        agents = new List<Agent>();

        this.OnJobStart = OnJobStart;
        this.OnJobEnd = OnJobEnd;

        this.AgentLimits = AgentLimits;
        this.tile = tile;
        this.job = job;
        this.JobTime = JobTime;
    }

    public void OnAgentJoin(Agent agent)
    {
        agents.Add(agent);
        OnJobStart(this);
    }

    public void OnAgentRepeat()
    {

    }

    public void OnAgentExit()
    {
        OnJobEnd(this);
    }

    public void DoWork(float Do)
    {
        JobTime -= Do;
        if (JobTime <= 0)
        {
            OnAgentExit();
        }
    }

    public Vector2 getjobPosition()
    {
        return new Vector2(tile.X, tile.Y);
    }
}
