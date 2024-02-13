using UnityEngine;
using System.Collections.Generic;

public class BookshelfPuzzle : MonoBehaviour
{
    [SerializeField] private List<Book> books; // Assume this is manually assigned or found at runtime
    [SerializeField] private int[] solutionArray;
    private HashSet<int> pulledBooks = new HashSet<int>();
    private HashSet<int> solution = new HashSet<int>();
    [SerializeField] private GameObject objectToMove; // The actual bookshelf object to move
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float animationDelay = 0.5f;
    [SerializeField] private float moveDistance = 0.5f;

    void Start()
    {
        solution = new HashSet<int>(solutionArray);
        books = new List<Book>(FindObjectsOfType<Book>());
        UpdateBooks();
    }

    public void UpdateBooks()
    {
        pulledBooks.Clear();
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].isPulled)  // Adjusted to property access if you updated Book as per previous suggestions
            {
                pulledBooks.Add(i);
            }
        }
        if (ValidatePuzzle())
        {
            // Animate only the bookshelfObject
            AnimateBookshelf();
        }
    }

    private void AnimateBookshelf()
    {
        LeanTween.moveY(objectToMove, objectToMove.transform.position.y + moveDistance, animationTime)
                 .setEaseOutCubic()
                 .setDelay(animationDelay);
    }

    private bool ValidatePuzzle()
    {
        return pulledBooks.SetEquals(solution);
    }
}
