using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Job : JobInterface
{
    public int AgentLimits;
    public List<Agent> agents;

    public Tile tile;

    public float JobTime;

    public List<SideJob> SideJobs;

    public Action<Job> OnJobStart;
    public Action<Job> OnJobRepeat;
    public Action<Job> OnJobEnd;

    public Job(int AgentLimits, Tile tile, float JobTime, Action<Job> OnJobStart, Action<Job> OnJobRepeat, Action<Job> OnJobEnd)
    {
        agents = new List<Agent>();
        SideJobs = new List<SideJob>();

        this.OnJobStart = OnJobStart;
        this.OnJobRepeat = OnJobRepeat;
        this.OnJobEnd = OnJobEnd;

        this.AgentLimits = AgentLimits;
        this.tile = tile;
        this.JobTime = JobTime;
    }

    public void OnAgentJoin(Agent agent)
    {
        agents.Add(agent);
        agent.CurrentJob = this;
        OnJobStart(this);
    }

    public void OnAgentRepeat()
    {
        OnJobRepeat(this);
    }

    public void OnAgentExit()
    {
        OnJobEnd(this);
    }

    public void DoWork(float Do)
    {
        JobTime -= Do;
        if(JobTime <= 0)
        {
            OnAgentExit();
        }
    }

    public Vector2 getjobPosition()
    {
        return new Vector2(tile.X, tile.Y);
    }
}