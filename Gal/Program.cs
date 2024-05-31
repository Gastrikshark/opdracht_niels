using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace Gal
{
    internal class Program
    {
        //Maak galgje, gebaseerd op een interne lijst met woorden.
        public static List<string> wordList = new List<string> { "school", "cookie", "light", "quaternium", "pilot", "Eredoseclitus" };
        //1 speler, 10 kansen.
        static string playerName;
        static int attempts = 10;
        static string guess;
        static string chosenWord;
        static bool gameWon;
        static List<HangmanLetter> guessedLetters = new List<HangmanLetter>();

        public struct HangmanLetter
        {
            public char letter;
            public bool isInWord;
        }
        //Letter raden,
        //niet in het woord, dan kans eraf,
        //wel in het woord, dan letter tonen.
        //Alle letters geraden, dan gewonnen.
        //Kansen op, dan game over.
        static void Main(string[] args)
        {
            Random rnd = new Random();
            chosenWord = wordList[rnd.Next(0, wordList.Count)];
            //player naam vragen
            Console.WriteLine("Wat is jou Naam :");
            playerName = Console.ReadLine();
            //welkom heten
            Console.WriteLine($"Welcome" +
                $" {playerName}");
            //spel spelen
            PlayGame();
        }

        static void PlayGame()
        {
            //zo lang het woord niet geraden is of de attempts niet 0 zijn
            //voer raad functie uit
            while (attempts > 0 && gameWon == false)
            {
                Guess();
            }
            
        }

        static void Guess()
        {
            Console.Clear();
            DisplayGame();
            //letter invoer uitlezen
            Console.WriteLine("Raad een letterr of het gehele woord.");
            guess = Console.ReadLine().ToLower();
            //controleren of het een nummer is
            foreach (char c in guess)
            {
                if (!char.IsLetter(c))
                {
                    //geen valide input
                    Console.WriteLine("je hebt iets ingevoerd dat geen letter is");
                    //opnieuw proberen
                    return;
                }
            }

            //controleren of letter in woord zit
            //letter opslaan als struct
            //controleren of het een letter of woord is
            if (guess.Length == 1)
            {
                //is letter
                //check of letter is al geraden
                foreach (HangmanLetter h in guessedLetters)
                {
                    if (h.letter == guess[0])
                    {
                        Console.WriteLine("Deze heb je all ingevuild");
                        return;
                    }
                }
                if (LetterInWord(guess[0], chosenWord))
                {
                    guessedLetters.Add(new HangmanLetter { letter = guess[0], isInWord = true });
                }
                else
                {
                    guessedLetters.Add(new HangmanLetter { letter = guess[0], isInWord = false });
                    attempts--;
                    return;
                }
            }
            else if (guess.Length > 1)
            {
                //is een woord
                if (string.Compare(guess, chosenWord) == 0)
                {
                    gameWon = true;
                    Console.WriteLine("You guessed correctly!");
                    return;
                }
            }
            else
            {
                //is invalide poging
                Console.WriteLine("Geen correcte invoer probeer het nog een keer");
                return;
            }
            if (WoordComplete())
            {
                gameWon = true;
                return;
            }

        }

        static void DisplayGame()
        {
            Console.WriteLine($"hallo {playerName} ");
            Console.WriteLine($"Je hebt nog {attempts} pogingen");
            foreach (char c in chosenWord)
            {
                char displayletter = '-';
                foreach (HangmanLetter h in guessedLetters)
                {
                    if (h.letter == c)
                    {
                        displayletter = h.letter;
                    }

                }
                Console.Write(displayletter);
            }
            Console.WriteLine();
            Console.WriteLine("guessed letters");
            foreach (HangmanLetter h in guessedLetters)
            {
                if (h.isInWord == false)
                {
                    Console.Write(h. letter + " ");
                }
            }
        }

        static bool WoordComplete()
        {
            int uniqueletters = chosenWord.Distinct().Count();
            foreach (HangmanLetter h in guessedLetters)
            {
                if (h.isInWord)
                {
                    uniqueletters--;
                }
            }
            if (uniqueletters == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static bool LetterInWord(char letter, string word)
        {
            foreach (char c in word)
            {
                if (c == letter)
                {
                    return true;
                }
            }
            return false;
        }
  

       
    }
}
