using UnityEngine;

public class EnableTowerPoints : MonoBehaviour
{
    [Header("pontos para colocar ou mover as torres")]
    [SerializeField] private GameObject[] points;
    [Header("Layer de obstaculos")]
    [SerializeField] private LayerMask layer;
    public bool enablePositions = false;

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
                        points[i].SetActive(false);
                }
                else
                {
                    points[i].SetActive(true);
                }
            }
        }
    }
}
