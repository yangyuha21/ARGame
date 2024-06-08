using UnityEngine;

public class TapToTrigger : MonoBehaviour
{
    // 用于检测触摸输入的层掩码  
    public LayerMask tapLayerMask;

    // Update is called once per frame  
    void Update()
    {
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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tapLayerMask))
                {
                    // 如果射线击中了某个物体，并且该物体是我们想要检测的Mesh  
                    // （即它有一个Collider组件，并且它的层在tapLayerMask中）  
                    // 那么调用func函数  
                    func();
                }
            }
        }
    }

    // 这是你想要在点击Mesh时触发的函数  
    void func()
    {
        Debug.Log("Mesh was tapped!");
        // 在这里添加你的逻辑代码  
    }
}