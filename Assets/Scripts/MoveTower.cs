using UnityEngine.EventSystems;
using UnityEngine;

public class MoveTower : MonoBehaviour
{
    [Header("tempo para se clicar e mudar de lugar")]
    [SerializeField] private float timeToSetMove = 1.0f;
    [Header("layer das posições")]
    [SerializeField] private LayerMask layer;
    private float timeToSetMoveAux;
    private bool getTower = false, setTower = false, clickOnTower = false;
    private EnableTowerPoints enableTower;

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
       
    }
    private void OnMouseUp()
    {
        clickOnTower = false;
    }
}
