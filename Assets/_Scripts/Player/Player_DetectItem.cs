using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Rock, Tree, Null
}

public class Player_DetectItem : MonoBehaviour
{
    [SerializeField] private float scanDistance;
    [SerializeField] private float scanRadius;
    [SerializeField] private LayerMask scanLayer;//for resources and items
    
    private ResourceType _type;
    private ItemType _itemType;
    private Collider _collider;

    public ResourceType type => _type;
    public ItemType itemType => _itemType;
    public Collider collider => _collider;
    
    private void Start()
    {
        _type = ResourceType.Null;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, scanRadius, transform.forward, out hit, scanDistance, scanLayer))
        {
            Debug.Log("Hit something");
            _collider = hit.collider;
            
            Resource_Master master = hit.collider.transform.root.GetComponent<Resource_Master>();

            if (master)
            {
                _type = master.data.type;
                Debug.Log("Looking at " + _type);
                return;
            }

            Item_Master item = hit.collider.transform.root.GetComponent<Item_Master>();    
            
            if (item)
            {
                _itemType = item.item.itemType;
                Debug.Log("Looking at " + _type);
                return;
            }
        }
        
        _type = ResourceType.Null;
        _itemType = ItemType.Null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius);
        Gizmos.DrawWireSphere(transform.position + transform.forward * scanDistance, scanRadius);
    }
}
