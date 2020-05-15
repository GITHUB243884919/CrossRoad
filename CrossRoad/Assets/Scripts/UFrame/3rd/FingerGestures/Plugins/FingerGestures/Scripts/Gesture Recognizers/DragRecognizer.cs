using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UFrame.Logger;

public class DragGesture : ContinuousGesture
{
    Vector2 deltaMove = Vector2.zero;
    
    internal Vector2 LastPos = Vector2.zero;
    internal Vector2 LastDelta = Vector2.zero;

    /// <summary>
    /// Distance dragged since last frame
    /// </summary>
    public Vector2 DeltaMove
    {
        get { return deltaMove; }
        internal set { deltaMove = value; }
    }

    /// <summary>
    /// Total distance dragged since beginning of the drag gesture
    /// </summary>
    public Vector2 TotalMove
    {
        get { return Position - StartPosition; }
    }
}

/// <summary>
/// Drag Gesture Recognizer
///   A full finger press > move > release sequence
/// </summary>
[AddComponentMenu( "FingerGestures/Gestures/Drag Recognizer" )]
public class DragRecognizer : ContinuousGestureRecognizer<DragGesture>
{
    /// <summary>
    /// How far the finger is allowed to move from its <see cref="AveragedGestureRecognizer.StartPosition">initial position</see> without making the gesture fail
    /// <seealso cref="DistanceUnit"/>
    /// </summary>
    public float MoveTolerance = 0.25f; // (in cm by default - see DistanceUnit)

    /// <summary>
    /// Applies for multi-finger drag gestures only:
    /// Check if the gesture should fail when the fingers do not move in the same direction
    /// </summary>
    public bool ApplySameDirectionConstraint = false;
    
    public override string GetDefaultEventMessageName()
    {
        return "OnDrag";
    }

    protected override GameObject GetDefaultSelectionForSendMessage( DragGesture gesture )
    {
        return gesture.StartSelection;
    }

    /// <summary>
    /// 鼠标点击的位置
    /// </summary>
    static Vector2 mousePosition = new Vector2();
    static List<RaycastResult> results = new List<RaycastResult>();
    ///// <summary>
    ///// 返回值为true => 有UI   false => 无UI
    ///// </summary>
    ///// <returns></returns>
    //public static bool IsPointerOverUIObject()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    mousePosition.x = Input.mousePosition.x;
    //    mousePosition.y = Input.mousePosition.y;
    //    eventDataCurrentPosition.position = mousePosition;
    //    results.Clear();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

    //    return results.Count > 0;
    //}
    protected override bool CanBegin( DragGesture gesture, FingerGestures.IFingerList touches )
    {
        if( !base.CanBegin( gesture, touches ) )
            return false;
        ////若是在Ui层上滑动，false
        //if (IsPointerOverUIObject())
        //    return false;
        // must have moved beyond move tolerance threshold
        if ( touches.GetAverageDistanceFromStart() < ToPixels( MoveTolerance ) )
            return false;
        
        // all touches must be moving
        if( !touches.AllMoving() )
            return false;

        // if multiple touches, make sure they're all going in roughly the same direction
        if( RequiredFingerCount >= 2 && ApplySameDirectionConstraint && !touches.MovingInSameDirection( 0.35f ) )
            return false;
        //判断allPages是否有isActive为true
        foreach (var item in PageMgr.allPages.Values)
        {
            if (item.name == "UIParkPage" || item.name == "UIEntryPage" || item.name == "UIZooPage"|| item.name == "UILoading"|| item.name == "UIMapPage")
            {
                if (item.isActive() == true)
                {
                    return false;
                }
            }

        }
        return true;
    }

    protected override void OnBegin( DragGesture gesture, FingerGestures.IFingerList touches )
    {
        gesture.Position = touches.GetAveragePosition();
        gesture.StartPosition = touches.GetAverageStartPosition();
        gesture.DeltaMove = gesture.Position - gesture.StartPosition;
        gesture.LastDelta = Vector2.zero;
        gesture.LastPos = gesture.Position;
    }

    protected override GestureRecognitionState OnRecognize( DragGesture gesture, FingerGestures.IFingerList touches )
    {
        if( touches.Count != RequiredFingerCount )
        {
            // fingers were lifted off
            if( touches.Count < RequiredFingerCount )
                return GestureRecognitionState.Ended;

            return GestureRecognitionState.Failed;
        }

        if( RequiredFingerCount >= 2 && ApplySameDirectionConstraint && touches.AllMoving() && !touches.MovingInSameDirection( 0.35f ) )
            return GestureRecognitionState.Failed;

        gesture.Position = touches.GetAveragePosition();
        gesture.LastDelta = gesture.DeltaMove;
        gesture.DeltaMove = gesture.Position - gesture.LastPos;

        // if we are currently moving, or we were still moving last frame (allows listeners to detect when the finger is stationary when MoveDelta = 0)...
        if( gesture.DeltaMove.sqrMagnitude > 0 || gesture.LastDelta.sqrMagnitude > 0 )
            gesture.LastPos = gesture.Position;

        RaiseEvent( gesture );
        return GestureRecognitionState.InProgress;
    }
    protected void OnTap(TapGesture gesture)
    {
        //if (IsPointerOverUIObject()) return;//如果射线检测到UI上直接返回
        //UIInteractive.GetInstance().OnTapUIGB();         //否则直接调用具体的是否Ui显示
    }

   
}


