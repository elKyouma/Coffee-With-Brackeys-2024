using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pullAxis { X, Y, Z }

public class Book : OutlineInteractable
{
    [SerializeField]
    private BookshelfPuzzle bookshelfPuzzle;
    [SerializeField]
    private float pullDistance = 0.1f;
    [SerializeField]
    private float animationTime = 0.3f;
    public bool isPulled = false;
    [SerializeField]
    private pullAxis pullAxis;
    public int id;
    void Start()
    {
        bookshelfPuzzle = FindObjectOfType<BookshelfPuzzle>();
    }

    public override void Interact()
    {
        PullBook();
        bookshelfPuzzle.UpdateBooks();
    }

    public void PullBook()
    {
        Vector3 pullVector = Vector3.zero;
        // Calculate the pull vector based on the pull axis
        switch (pullAxis)
        {
            case pullAxis.X:
                pullVector = new Vector3(pullDistance, 0, 0);
                break;
            case pullAxis.Y:
                pullVector = new Vector3(0, pullDistance, 0);
                break;
            case pullAxis.Z:
                pullVector = new Vector3(0, 0, pullDistance);
                break;
        }
        // Calculate the new position based on the pull state
        Vector3 targetPosition = transform.position + (isPulled ? pullVector : -pullVector);
        // Animate the book to the new position in world space
        LeanTween.move(gameObject, targetPosition, animationTime).setEase(LeanTweenType.easeOutCubic);

        // Toggle the pulled state
        isPulled = !isPulled;
    }
}
