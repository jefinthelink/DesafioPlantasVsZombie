using UnityEngine.EventSystems;
using UnityEngine;

public class MoveTower : MonoBehaviour
{
    [SerializeField] private float timeToSetMove = 1.0f;
    [SerializeField] private LayerMask layer;
    private float timeToSetMoveAux;
    public bool getTower = false, setTower = false, clickOnTower = false;
    public EnableTowerPoints enableTower;

    private void Start()
    {
        SetValues();
    }
    private void Update()
    {
        GetTower();
        MovementTower();
    }
    private void SetValues()
    {
        timeToSetMoveAux = timeToSetMove;
        enableTower = GameObject.FindGameObjectWithTag("Points").GetComponent<EnableTowerPoints>();
        
    }
    private void GetTower()
    {
        if (clickOnTower)
        {
            timeToSetMove -= Time.deltaTime;
            if (timeToSetMove <= 0.0f)
            {
                timeToSetMove = timeToSetMoveAux;
                clickOnTower = false;
                getTower = true;
                enableTower.enablePoints(true);
            }
        }
    }

    private void MovementTower()
    {
        if (getTower)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000,layer))
                {
                    transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                    getTower = false;
                    enableTower.enablePoints(false);
                }
            }
        }
    }


    private void OnMouseDown()
    {
        clickOnTower = true;
        Debug.Log("clicando");
    }
    private void OnMouseUp()
    {
        clickOnTower = false;
    }
}
