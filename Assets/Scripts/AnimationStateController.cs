using System;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    private int _isChosen;

    private int _isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _isChosen = Animator.StringToHash("isChosen");
        _isMoving = Animator.StringToHash("isMoving");
    }

    public void ToggleMoving()
    {
        animator.SetBool(_isMoving, !animator.GetBool(_isMoving));
    }
    
    public void ToggleChosen()
    {
        animator.SetBool(_isChosen, !animator.GetBool(_isChosen));
    }
}
