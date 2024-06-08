using UnityEngine;

public class MeshVisibilityManager : MonoBehaviour
{
    public MeshRenderer[] meshes; // 12个Mesh的Renderer组件  
    public MeshRenderer specialMesh; // 特殊的Mesh的Renderer组件

    public AudioSource eyePlaceSource; // 在Inspector中设置AudioSource
    public AudioClip[] eyePlaceSounds; // 存储音效的数组
    public AudioClip openPortalSound;

    private int visibleCount = 0;
    bool isChecked = false;

    void Start()
    {
        firstCheck();
    }

    void Update()
    {
        if (!isChecked)
        {
            firstCheck();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            print("hit ray");
            if (Physics.Raycast(ray, out hit))
            {
                print("hit");
                PlaceEyes(hit);
            }
        }
        if (Input.touchCount > 0)
        {
            // 获取第一个触摸点（假设只处理单点触摸）  
            Touch touch = Input.GetTouch(0);

            // 检查触摸是否开始（即用户刚刚按下屏幕）  
            if (touch.phase == TouchPhase.Began)
            {
                // 将触摸的屏幕位置转换为世界空间中的射线  
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // 尝试投射射线并检测碰撞  
                if (Physics.Raycast(ray, out hit))
                {
                    PlaceEyes(hit);
                }
            }
        }
    }

    public void firstCheck()
    {
        // 检查 Camera.main 是否为 null  
        if (Camera.main == null)
        {
            Debug.LogError("No main camera found in the scene!");
            return;
        }

        // 检查 meshes 数组是否已初始化  
        if (meshes == null || meshes.Length == 0)
        {
            Debug.LogError("meshes array is not set or is empty!");
            return;
        }

        // 初始化时，假设所有Mesh都是不可见的（或者根据您的需求进行设置）  
        foreach (var mesh in meshes)
        {
            if (mesh == null)
            {
                Debug.LogError("One of the meshes in the array is null!");
            }
            else
            {
                mesh.enabled = Random.value <= 0.7f;
            }
        }
        // specialMesh.enabled = false;
        UpdateSpecialMeshVisibility();
        Debug.LogFormat("初始化成功");
        isChecked = true;
    }

    public void PlaceEyes(RaycastHit hit)
    {
        // 检查点击的对象是否是我们的Mesh之一  
        foreach (var mesh in meshes)
        {
            if (mesh.gameObject == hit.collider.gameObject)
            {
                // 切换被点击Mesh的可见性  
                mesh.enabled = !mesh.enabled;
                UpdateSpecialMeshVisibility();
                if (mesh.enabled)
                {
                    int randomIndex = Random.Range(0, eyePlaceSounds.Length);
                    AudioClip randomSound = eyePlaceSounds[randomIndex];
                    // 播放随机选择的音效  
                    if (eyePlaceSource != null && !specialMesh.enabled)
                    {
                        eyePlaceSource.PlayOneShot(randomSound); // 播放随机音效  
                    }
                }
                break;
            }
        }
    }

    public void UpdateSpecialMeshVisibility()
    {
        visibleCount = 0;
        foreach (var mesh in meshes)
        {
            if (mesh.enabled)
            {
                visibleCount++;
            }
        }
        specialMesh.enabled = (visibleCount == meshes.Length); // 如果所有Mesh都可见，则特殊Mesh也可见
        if (specialMesh.enabled)
        {
            eyePlaceSource.PlayOneShot(openPortalSound);
        }
    }
}