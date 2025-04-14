using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SensorFOV : MonoBehaviour
{
    [Header("FOV Settings")]
    public float viewRadius = 5f; // 시야 거리
    [Range(0, 360)] public float viewAngle = 90f; // 시야각 (0도 ~ 360도)
    public int resolution = 10; // 시야각의 세밀함 (폴리곤의 세그먼트 수)

    [Header("Layer Masks")]
    public LayerMask obstacleMask; // 장애물 레이어

    [Header("Material Settings")]
    public Material fovMaterial; // FOV 폴리곤에 적용할 Material
    public Color normalColor = Color.green; // 평상시 
    public Color alertColor = Color.red; // 경고 색상 (적 발견 시)

    private MeshFilter viewMeshFilter;
    private MeshRenderer viewMeshRenderer;
    private Mesh viewMesh;


    private void Start()
    {
        // MeshFilter 및 MeshRenderer 초기화
        viewMeshFilter = GetComponent<MeshFilter>();
        viewMeshRenderer = GetComponent<MeshRenderer>();

        // Mesh 초기화
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        // Material 설정
        if (fovMaterial != null)
        {
            viewMeshRenderer.material = fovMaterial;
            // 평상시 색상으로 Material 초기화
            viewMeshRenderer.material.color = normalColor;
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        // 1. 각도를 기준으로 시야의 각 점을 계산
        List<Vector3> viewPoints = new List<Vector3>();
        float stepAngle = viewAngle / resolution;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngle * i;
            Vector3 p = ViewCast(angle);
            viewPoints.Add(p);
        }

        // 2. Mesh 생성
        int vertexCount = viewPoints.Count + 1; // 중심점 + 각 점
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero; // Mesh의 중심점
        for (int i = 0; i < viewPoints.Count; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < viewPoints.Count - 1)
            {
                // 삼각형 정의
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        // Mesh에 데이터 적용
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    
    Vector3 ViewCast(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, viewRadius, obstacleMask))
            return hit.point;

        return transform.position + direction * viewRadius;
    }



    public void AlertColor(bool isAlert)
    {
        if (viewMeshRenderer != null && fovMaterial != null)
        {
            viewMeshRenderer.material.color = isAlert ? alertColor : normalColor;
        }
    }

    public void ResetColor()
    {
        if (viewMeshRenderer != null && fovMaterial != null)
        {
            viewMeshRenderer.material.color = normalColor;
        }
    }

}