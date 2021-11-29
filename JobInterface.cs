using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface JobInterface 
{
    void OnAgentJoin(Agent agent);
    void OnAgentRepeat();
    void OnAgentExit();
    void DoWork(float Do);
    Vector2 getjobPosition();
}
