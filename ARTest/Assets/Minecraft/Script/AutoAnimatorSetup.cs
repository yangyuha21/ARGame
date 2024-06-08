using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class AutoAnimatorSetup : MonoBehaviour
{
    public UnityEditor.Animations.AnimatorController animatorController; // 引用动画控制器  
    public GameObject[] meshesToAnimate; // 需要添加动画的mesh所在的GameObject数组  
    public string triggerName = "Entrance"; // 动画触发器的名称  

    void Start()
    {
        // 确保动画控制器和mesh数组都被设置了  
        if (animatorController != null && meshesToAnimate != null && meshesToAnimate.Length > 0)
        {
            foreach (var mesh in meshesToAnimate)
            {
                // 检查mesh是否有Animator组件，如果没有则添加  
                Animator animator = mesh.GetComponent<Animator>();
                if (animator == null)
                {
                    animator = mesh.AddComponent<Animator>();
                }

                // 设置Animator的控制器  
                animator.runtimeAnimatorController = animatorController;

                // 触发动画（如果需要的话，可以在这里或者在稍后的某个时间点触发）  
                animator.SetBool(triggerName, true);

                // 注意：通常你会在Animator Controller中设置动画的自动重置，  
                // 或者在动画结束后通过监听器（Listener）来重置触发器  
            }
        }
    }
}