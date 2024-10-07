using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversalBoolBehaviour : StateMachineBehaviour
{
    [SerializeField] string boolName;
    [SerializeField] TraversalMode traversalMode;
    [SerializeField] bool newBool;
    int boolID;
    private void Awake()
    {
        boolID = Animator.StringToHash(boolName);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (traversalMode == TraversalMode.Enter) animator.SetBool(boolID, newBool);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (traversalMode == TraversalMode.Exit) animator.SetBool(boolID, newBool);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
[System.Serializable]
enum TraversalMode
{
    Enter,
    Exit
}
