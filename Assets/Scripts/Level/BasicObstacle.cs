/*using UnityEngine;
public class BasicObstacle : Obstacle
{
    protected override void OnHumanTriggerEnter(Collider human)
    {
        EntityStats stats = human.gameObject.GetComponent<EntityStats>();
        if (stats != null)
            stats.Health--;
    }
    protected override void OnMonsterTriggerEnter(Collider monster)
    {
        EntityStats stats = monster.gameObject.GetComponent<EntityStats>();
        if (stats != null)
            stats.Health--;
        this.gameObject.SetActive(false);
    }
    protected override void OnObstacleDestroyerTriggerEnter(Collider monster)
    {
        this.gameObject.SetActive(false);
    }
    protected override void OnObstacleDestroyerTriggerExit(Collider monster)
    {

    }
    protected override void OnHumanTriggerExit(Collider human)
    {

    }
    protected override void OnMonsterTriggerExit(Collider monster)
    {

    }
}*/