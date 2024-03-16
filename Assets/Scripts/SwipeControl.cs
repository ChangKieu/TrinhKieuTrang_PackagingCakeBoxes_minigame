using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class SwipeControl : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private GameController game;

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        game.swipe=swipe;
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}