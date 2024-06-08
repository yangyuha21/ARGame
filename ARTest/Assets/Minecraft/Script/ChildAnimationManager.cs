using UnityEngine;
using System.Collections;

public class ChildAnimationManager : MonoBehaviour
{
    // 设置动画触发器的名称  
    public string animationTriggerName = "StartAnimation";
    // 设置缩放持续帧数  
    public int scaleDurationFrames = 30;

    private float scaleDurationInSeconds;

    void Start()
    {
        // 根据帧率计算缩放持续时间（假设每帧大约0.033秒，即30FPS）  
        scaleDurationInSeconds = scaleDurationFrames * (1f / 30f);

        // 遍历所有子对象  
        foreach (Transform child in transform)
        {
            // 假设所有子对象都是Cube，并且都有Animator和Transform组件  
            Animator animator = child.GetComponent<Animator>();
            // if (animator == null)
            // {
            //     animator = child.AddComponent<Animator>();
            // }
            if (animator != null)
            {
                // 触发动画  
                animator.SetBool(animationTriggerName, true);
            }

            // 开始缩放动画  
            StartCoroutine(ScaleCube(child, scaleDurationInSeconds));
        }
    }

    IEnumerator ScaleCube(Transform cube, float duration)
    {
        // 开始时缩放到0（如果需要的话）  
        Vector3 startScale = cube.localScale;
        if (startScale == Vector3.one)
        {
            cube.localScale = Vector3.zero;
        }

        // 缩放动画  
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float lerpT = elapsedTime / duration;
            cube.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerpT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保缩放完成  
        cube.localScale = Vector3.one;
    }
}