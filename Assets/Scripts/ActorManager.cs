using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ActorManager : MonoBehaviour
{
    private Dictionary<int, Team> _teamActors = new Dictionary<int, Team>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public Team GetTeam(int id)
    {
        return _teamActors[id];
    }
    public Team[] GetMaskTeams(int id)
    {
        var allTeam = _teamActors.ToArray();
        return allTeam
            .Where(x => x.Key != id)
            .Select(x=>x.Value)
            .ToArray();
    }

    public void AddActor(Actor actor,int id)
    {
        GetOrAddTeam(id,out var team);
        actor.Manager = this;
        team.AddActor(actor);
    }
    private void GetOrAddTeam(int id,out Team team)
    {
        if (_teamActors.ContainsKey(id))
        {
            team = _teamActors[id];
            return;
        }
        else
        {
            var newTeam = new Team();
            team = newTeam;
            _teamActors.Add(id, newTeam);
            return;
        }
    }
    public class Team
    {
        private Actor[] _actors = new Actor[0];
        public Actor[] Actors => _actors;
        public void AddActor(Actor actor)
        {
            var list = _actors.ToList();
            list.Add(actor);
            _actors = list.ToArray();
        }
    }
}
public class Actor
{
    public ActorManager Manager;
}