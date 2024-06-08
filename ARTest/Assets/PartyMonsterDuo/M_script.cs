using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_script : MonoBehaviour
{
    Animator animator;
    // AudioSource music;
    bool ifDance = false;
    // Start is called before the first frame update
    private float touchTime;
    private bool newTouch = false;
    // TouchEvent:
    // 假设双击的时间阈值为0.5秒  
    float doubleTapThreshold = 0.5f; 
    // 初始化上一次点击的时间为-1（表示没有点击）
    float touchEndTime = 0; 
    // 标记是否正在等待第二次触摸以检测双击  
    private bool isWaitingForDoubleTap = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        // music = GetComponent<AudioSource>();
    }

    void Update()
    {
        // TouchEvent
        Touch();
        // KeysEvent
        InputKey();
    }
    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ifDance = !ifDance;
            animator.SetBool("B_Dance",ifDance);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetTrigger("T_Jump");
        }
    }
    // 检测双击的协程  

    public void Touch()
    {  
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)  
        {   
            // print(Input.touchCount);
            isWaitingForDoubleTap = true;
            // 等待双击
            if(isWaitingForDoubleTap && Time.time - touchEndTime < doubleTapThreshold )
            {
                OnDoubleTap();
                isWaitingForDoubleTap = false;
            }
            // if(isWaitingForDoubleTap && Time.time - touchEndTime >= doubleTapThreshold )
            // {
            //     OnSingleTap();
            //     isWaitingForDoubleTap = false;
            // }
            touchEndTime = Time.time;
        }  
    }      
  
    private void OnSingleTap()  
    {  
        Debug.Log("Single Tap Detected");  
        // 处理单击事件 
        animator.SetTrigger("T_Jump");
    }  
  
    private void OnDoubleTap()  
    {  
        Debug.Log("Double Tap Detected");  
        // 处理双击事件  
        ifDance = !ifDance;  
        animator.SetBool("B_Dance", ifDance);  
    } 

    public void startAnim()
    {
        animator.SetTrigger("T_Start");
        animator.ResetTrigger("T_Reverse");
        // music.Play();
    }
    public void renewAnim()
    {
        // music.Stop();
        animator.SetTrigger("T_Reverse");
        animator.ResetTrigger("T_Start");
    }
    public void dance()
    {
        
    }
    public void jump()
    {
        
    }
}
