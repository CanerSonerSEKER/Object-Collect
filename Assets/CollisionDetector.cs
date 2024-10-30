using Components;
using Events;
using UnityEngine;
using WorldObjects;

public class CollisionDetector : EventListenerMono
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private int _scoreMulti;
    
    private void OnWoodCollision(Wood colWood)
    {
        Debug.LogWarning($"ColWood: {colWood}");
        CollisionEvents.Score?.Invoke(_scoreMulti);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Wood colWood))
        {
            CollisionEvents.WoodCollision?.Invoke(colWood);
            colWood.OnPickable();
        }
    }

    protected override void RegisterEvents()
    {
        CollisionEvents.WoodCollision += OnWoodCollision;
    }

    protected override void UnRegisterEvents()
    {
        CollisionEvents.WoodCollision -= OnWoodCollision;
    }
}

