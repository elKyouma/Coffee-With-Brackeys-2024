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
    private bool hasMoved = false; // Track if the bookshelf has moved

    [SerializeField] private SoundSO bookSlideSound;
    [SerializeField] private AudioSource fireplaceAudioSource;

    void Start()
    {
        solution = new HashSet<int>(solutionArray);
        books = new List<Book>(FindObjectsOfType<Book>());
        SortBooks();

        UpdateBooks();
    }
    void SortBooks()
    {
        // by id (Cube.1, Cube.2, Cube.3, etc.)
        foreach (Book book in books)
        {
            book.id = int.Parse(book.name.Split('.')[1]);
        }
        books.Sort((x, y) => x.id.CompareTo(y.id));
    }

    public void UpdateBooks()
    {
        pulledBooks.Clear();
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].isPulled)
            {
                pulledBooks.Add(i);
            }
        }
        CheckPuzzleState();
    }

    private void CheckPuzzleState()
    {
        if (ValidatePuzzle() && !hasMoved)
        {
            AnimateBookshelfDown();
        }
        else if (!ValidatePuzzle() && hasMoved)
        {
            MoveBookshelfBack();
        }
        // When the puzzle is not solved and hasn't been moved, we do nothing here.
    }

    private void AnimateBookshelfDown()
    {
        hasMoved = true; // Mark as moved
        fireplaceAudioSource.mute = false;
        LeanTween.moveY(objectToMove, objectToMove.transform.position.y - moveDistance, animationTime)
            .setEaseOutCubic()
            .setDelay(animationDelay)
            .setOnComplete(() =>
            {
                // Optional: Actions after moving down, if necessary.
                fireplaceAudioSource.mute = true;
            });
        
    }

    private void MoveBookshelfBack()
    {
        LeanTween.moveY(objectToMove, objectToMove.transform.position.y + moveDistance, animationTime)
            .setEaseOutCubic()
            .setDelay(animationDelay)
            .setOnComplete(() =>
            {
                hasMoved = false; // Reset move flag after moving back
            });
        
    }

    private bool ValidatePuzzle()
    {
        return pulledBooks.SetEquals(solution);
    }

    public SoundSO getBookSlideSound() => bookSlideSound;
}
