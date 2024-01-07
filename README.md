# Mots_mysteres

A console-based word mystery game implemented in C#.

This project serves as my final term project for the first session of my software development program.

## How to Play

To play, enter the name of the grid file when prompted. For example: `grille1.txt`

## Mandatory Requirements

1. Create a console application (.NET Core) in C#.
2. Use the provided format of the `grille1.txt` file to build your algorithms.
3. Declare and use a 2D array for managing grid characters. The array dimensions are determined by the content of the text file and should accommodate any dimensions (e.g., 10 X 10, 12 X 12, 20 X 20, etc.).
4. Load the list of words to find into a generic `List<string>`.
5. Organize your logic into methods and/or functions to optimize based on the following criteria:
   - Avoid duplicating code lines.
   - Group together lines of code that represent a specific functionality (e.g., `LoadFile`, `DisplayWords`, etc.).

## How It Works

1. On application startup, prompt the user for the name of the file containing the grid. Validate its existence.
2. Display the grid, the list of words to find, and wait for the user to input either a word number (to validate) or the hidden word answer (to validate).
3. If the user inputs a valid word number, prompt for the coordinates of the first and last letters of the word.
4. Words can be found in the grid in all directions:
- Horizontal
  ```
  Tester:
  Abandonner
  Entend
  ```
- Vertical
  ```
  Tester:
  Chiquer
  Plant
  ```
- Diagonal 1
  ```
  Tester:
  Nage
  Avais
  ```
- Diagonal 2
  ```
  Tester:
  Baie
  Lac
  ```

5. At any time, when prompted to "Enter a word number," the user can input a word they believe is the answer. Display "Bravo!" if it's correct; otherwise, display "ERREUR – Pas le bon mot!".
