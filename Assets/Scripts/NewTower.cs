using UnityEngine;
using UnityEngine.EventSystems;

public class NewTower : MonoBehaviour
{
    [Header("torres")]
    [SerializeField] private Tower[] towers;
    [SerializeField] private LayerMask layer;
    private EnableTowerPoints enableTower;
    private int index;
    private bool newTower = false;

    private void Start()
    {
        SetValues();
    }
    private void SetValues()
    {
        enableTower = GameObject.FindGameObjectWithTag("Points").GetComponent<EnableTowerPoints>();
    }
    private void Update()
    {
        SetTower();
    }
    public void NewTowers(int id)
    {
       
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].id == id)
            {
                
                index = i;
                newTower = true;
                enableTower.enablePoints(true);
                return;
            }
        }
    }
    private void SetTower()
    {
        if (newTower)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000, layer))
                {
                   Instantiate(towers[index].gameObject, new Vector3(hit.transform.position.x, 1, hit.transform.position.z), towers[index].transform.rotation);
                    newTower = false;
                    enableTower.enablePoints(false);
                }
            }
        }
    }
}
