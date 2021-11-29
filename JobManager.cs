using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager instance;
    public List<Job> HaulingJobs;
    public List<Job> ConstructionJobs;

    private void Start()
    {
        instance = this;

        HaulingJobs = new List<Job>();
        ConstructionJobs = new List<Job>();
    }

    public void BuildingJob(Tile tile, StructureScriptableObjects structure)
    {
        if (tile.Structure.RequiredInventory != null)
        {
            Job job = new Job(3, tile, 0,
                //start
                (TheJob) =>
                {
                    BuildingMaterial Required = tile.Structure.RequiredInventory.GetRequiredMaterial();
                    for (int i = 0; i < TheJob.agents.Count; i++)
                    {
                        if (TheJob.agents[i].inventory.AsType(Required.materialType) == false)
                        {
                            GoToStockable(TheJob, Required, TheJob.agents[i]);
                            TheJob.agents[i].GetPosition = true;
                        }
                    }
                },

                //middle
                (TheJob) =>
                {

                    BuildingMaterial Required = tile.Structure.RequiredInventory.GetRequiredMaterial();
                    for (int i = 0; i < TheJob.agents.Count; i++)
                    {
                        if (TheJob.agents[i].inventory.AsType(Required.materialType) == false)
                        {
                            GoToStockable(TheJob, Required, TheJob.agents[i]);
                            TheJob.agents[i].GetPosition = true;
                        }
                    }
                },

                //end
                (TheJob) =>
                {
                    BuildingMaterial Required = tile.Structure.RequiredInventory.GetRequiredMaterial();
                    for (int i = 0; i < TheJob.agents.Count; i++)
                    {
                        if (Vector2.Distance(TheJob.agents[i].transform.position, new Vector2(tile.X, tile.Y)) <= 0.001)
                        {
                            tile.Structure.RequiredInventory.AddToInventory(TheJob.agents[i].inventory.GetMaterialOfType(Required.materialType), Required.amount);
                        }
                    }

                    if (tile.Structure.RequiredInventory.RequirementDone() == true)
                    {
                        for (int i = 0; i < TheJob.agents.Count; i++)
                        {
                            TheJob.agents[i].CurrentJob = null;
                        }
                        TheJob.agents.Clear();
                        HaulingJobs.Remove(TheJob);

                        Job construction = new Job(1, tile, structure.JobTime, (nullJob) => { }, null,
                            (TheJob2) =>
                            {
                                LandManager.instance.land.StructureIsDone(tile.Structure);
                                for (int i = 0; i < TheJob2.agents.Count; i++)
                                {
                                    TheJob2.agents[i].CurrentJob = null;
                                }
                                TheJob2.agents.Clear();
                                ConstructionJobs.Remove(TheJob2);
                            }
                        );
                        ConstructionJobs.Add(construction);
                    }
                    else
                    {
                        TheJob.OnAgentRepeat();
                        TheJob.JobTime = 0;
                    }
                }
            );
            HaulingJobs.Add(job);
        }
        else
        {
            Job construction = new Job(1, tile, structure.JobTime, (nullJob) => { }, null,
                           (TheJob2) =>
                           {
                               LandManager.instance.land.StructureIsDone(tile.Structure);
                               for (int i = 0; i < TheJob2.agents.Count; i++)
                               {
                                   TheJob2.agents[i].CurrentJob = null;
                               }
                               TheJob2.agents.Clear();
                               ConstructionJobs.Remove(TheJob2);
                           }
                       );
            ConstructionJobs.Add(construction);
        }
    }

    public void FindJob(Agent agent)
    {
        List<Job> Jobs = GetListOfJob(agent.Skill);
        if (Jobs.Count != 0)
        {
            for (int i = 0; i < Jobs.Count; i++)
            {
                if(Jobs[i].agents.Count < Jobs[i].AgentLimits)
                {
                    Jobs[i].OnAgentJoin(agent);
                    break;
                }
            }
        }
    }

    public List<Job> GetListOfJob(SkillType type)
    {

        if (type == SkillType.Constructor && ConstructionJobs.Count != 0)
        {
            return ConstructionJobs;
        }

        return HaulingJobs;
    }

    void GoToStockable(Job TheJob, BuildingMaterial Required, Agent agent)
    {
        StockPIleTest test = LandManager.instance.land.GetStockpile(Required.materialType);

        if (test != null)
        {
                SideJob job2 = new SideJob(1, LandManager.instance.land.Tiles[(int)test.transform.position.x, (int)test.transform.position.y], 0, TheJob,

                (StockpileJobStart) =>
                {
                    for (int d = 0; d < StockpileJobStart.agents.Count; d++)
                    {
                        StockpileJobStart.agents[d].CurrentJob = StockpileJobStart;
                    }
                },

                (StockpileJobEnd) =>
                {
                    agent.inventory.AddToInventory(test.inventory.GetMaterialOfType(Required.materialType), Required.amount);

                    for (int d = 0; d < StockpileJobEnd.agents.Count; d++)
                    {
                        StockpileJobEnd.agents[d].CurrentJob = StockpileJobEnd.job;
                        StockpileJobEnd.agents[d].GetPosition = true;
                    }

                    StockpileJobEnd.agents.Clear();
                    TheJob.SideJobs.Remove(StockpileJobEnd);
                }
            );
            
            job2.OnAgentJoin(agent);

            TheJob.SideJobs.Add(job2);
        }
    }


}
