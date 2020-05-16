using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Rock, Tree, Cactus, Null
}

public class Player_DetectItem : MonoBehaviour
{
    [SerializeField] private float scanDistance = 1.5f;
    [SerializeField] private float scanRadius = 0.7f;
    [SerializeField] private LayerMask scanLayer;//for resources and items
    public GameObject interactPrompt;

    private ResourceType _resourceType;
    private ItemType _itemType;
    private Collider _collider;

    public ResourceType resourceType => _resourceType;
    public ItemType itemType => _itemType;
    public Collider collider => _collider;
    public   Resource_Master master;

    private GameManager_Master gameManagerMaster;

    private void Start()
    {
        _resourceType = ResourceType.Null;
        interactPrompt.SetActive(false);
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

        Scan();
        HandleResult();
    }

    private void Scan()
    {
//        Debug.Log("Found something");

        Collider[] hit = Physics.OverlapSphere(transform.position, scanRadius, scanLayer);
        if (hit.Length > 0)
        {
            Vector3 toCollider = hit[0].transform.position - transform.position;
            float dot = Vector3.Dot(toCollider, transform.forward);

//            Debug.Log("Found something");

            if (dot > 0.5f)
            {
                _collider = hit[0];
                return;
            }
        }

        _collider = null;

        /*RaycastHit hit;
        if (Physics.SphereCast(transform.position, scanRadius, transform.forward, out hit, scanDistance, scanLayer))
        {
            //Debug.Log("Hit something");
            _collider = hit.collider;
        }
        else
        {
            _collider = null;
        }*/
    }

    public  void HandleResult()
    {
        if (_collider == null)
        {
            _resourceType = ResourceType.Null;
            _itemType = ItemType.Null;
            interactPrompt.SetActive(false);
//            Debug.Log("Looking at nothing");
        }
        else
        {
             master = _collider.transform.root.GetComponent<Resource_Master>();//For this to work item / resource should be a root object in the scene

            if (master)
            {
                _resourceType = master.data.type;
          //    Debug.Log("Looking at " + resourceType);
            }
            else
            {
                _resourceType = ResourceType.Null;
            }

            Item_Master item = _collider.transform.root.GetComponent<Item_Master>();

            if (item)
            {
                _itemType = item.item.itemType;
            //    Debug.Log("Looking at " + _resourceType);
            }
            else
            {
                _itemType = ItemType.Null;
            }

            interactPrompt.SetActive(true);
            interactPrompt.transform.position = Camera.main.WorldToScreenPoint(_collider.transform.root.position);
        }
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.DrawWireSphere(transform.position, scanRadius);
        Gizmos.DrawWireSphere(transform.position + transform.forward * scanDistance, scanRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * scanDistance);*/
        //Gizmos.DrawWireSphere(transform.position + transform.forward * scanDistance, scanRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
