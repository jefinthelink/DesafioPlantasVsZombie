using UnityEngine;

public class EnableTowerPoints : MonoBehaviour
{
    
    [SerializeField] private GameObject[] points;
    public bool enablePositions = false;
    [SerializeField] private LayerMask layer;

    private void Start()
    {
        DisablePositions();
    }
    public void enablePoints(bool value)
    {
        if (!value)
            DisablePositions();
           
        
        enablePositions = value;


    }
    private void DisablePositions()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        VerifyPositions();
    }

    private void VerifyPositions()
    {
        if (enablePositions)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Ray ray = new Ray(points[i].transform.position, transform.up);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1, layer))
                {
                    if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Tower"))
                    {
                       
                        points[i].SetActive(false);
                    }
                }
                else
                {
                    points[i].SetActive(true);
                }
            }
        }
    }
}
