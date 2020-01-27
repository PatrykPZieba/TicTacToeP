using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    static class AI
    {
        private static readonly string O_SYMBOL = "O";
        private static readonly string X_SYMBOL = "X";
        private static readonly int size = 4;

        delegate bool DelegateAIorEnemy(Button button);

        /// <summary>
        /// Główna metoda klasy AI
        /// </summary>
        /// <param name = "listOfButtons"> Lista przycisków </param>
        /// <param name = "bestMoves"> Lista najlepszych ruchów </param>
        public static void MoveAI(List<Button> listOfButtons, List<Button> bestMoves)
        {
            bool isAIWinBool = false;
            bool isHumanWinBool = false;

            DelegateAIorEnemy DelegateAI = isAI;
            DelegateAIorEnemy DelegateEnemy = isEnemy;

            if (isWin(listOfButtons, isAI))
                isAIWinBool = true;

            else if (isWin(listOfButtons, isEnemy))
                isHumanWinBool = true;

            else if ((isAIWinBool == false) && (isHumanWinBool == false))
            {
                foreach (var button in bestMoves) // w przeciwnym razie wybierz najlepszy dostępny ruch
                {
                    if (button.IsEnabled == true)
                    {
                        button.Content = O_SYMBOL;
                        button.IsEnabled = false;
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// Ruch komputera
        /// </summary>
        /// <param name = "button"> Przycisk, na którym zmieniamy wartość </param>
        private static void PerformMove(Button button)
        {
            button.Content = O_SYMBOL;
            button.IsEnabled = false;
        }


        /// <summary>
        ///Sprawdź, czy gracz lub komputer wygra w następnej turze
        /// </summary>
        private static bool isWin(List<Button> listOfButtons, DelegateAIorEnemy DelegateAIorEnemy)
        {
            bool returnValue = false;

            if (isHorizontalWin(listOfButtons, DelegateAIorEnemy))
                returnValue = true;

            else if (isVerticalWin(listOfButtons, DelegateAIorEnemy) && returnValue == false)
                returnValue = true;

            else if (isDiagonalWin(listOfButtons, DelegateAIorEnemy) && returnValue == false)
                returnValue = true;

            return returnValue;
        }





        /// <summary>
        /// Sprawdzanie, czy komputer lub gracz może wygrać grę poziomo w następnej turze
        /// </summary>
        private static bool isHorizontalWin(List<Button> listOfButtons, DelegateAIorEnemy DelegateAIorEnemy)
        {
            bool returnValue = false;
            for (int i = 0; i < size; i++)
            {
                Button firstButton = listOfButtons.ElementAt(i * size);
                Button secondButton = listOfButtons.ElementAt((i * size) + 1);
                Button thirdButton = listOfButtons.ElementAt((i * size) + 2);
                Button fouthButton = listOfButtons.ElementAt((i * size) + 3);

                isWinOnThreeSides(firstButton, secondButton, thirdButton, fouthButton, DelegateAIorEnemy, ref returnValue);
            }
            return returnValue;
        }


        /// <summary>
        /// Sprawdzanie, czy komputer lub gracz może wygrać grę pionowo w następnej turze
        /// </summary>
        private static bool isVerticalWin(List<Button> listOfButtons, DelegateAIorEnemy DelegateAIorEnemy)
        {
            bool returnValue = false;

            for (int i = 0; i < size; i++)
            {
                Button firstButton = listOfButtons.ElementAt(i);
                Button secondButton = listOfButtons.ElementAt(i + (1 * 4));
                Button thirdButton = listOfButtons.ElementAt(i + (2 * 4));
                Button fouthButton = listOfButtons.ElementAt(i + (3 * 4));

                isWinOnThreeSides(firstButton, secondButton, thirdButton, fouthButton, DelegateAIorEnemy, ref returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Sprawdź, czy komputer lub gracz może wygrać grę po przekątnej w następnej turze
        /// </summary>
        private static bool isDiagonalWin(List<Button> listOfButtons, DelegateAIorEnemy DelegateAIorEnemy)
        {

            bool returnValue = false;

            Button firstTopLeft = listOfButtons.ElementAt(0); // A1
            Button secondTopLeft = listOfButtons.ElementAt(5); // B2
            Button firstTopRight = listOfButtons.ElementAt(3); // A4
            Button secondTopRight = listOfButtons.ElementAt(6); // B3
            Button firstBottomLeft = listOfButtons.ElementAt(12); // D1
            Button secondBottomLeft = listOfButtons.ElementAt(9); // C2
            Button firstBottomRight = listOfButtons.ElementAt(10); // C3
            Button secondBottomRight = listOfButtons.ElementAt(15); // D4

            if (isWinOnThreeSides(firstTopLeft, secondTopLeft, firstBottomRight, secondBottomRight, DelegateAIorEnemy, ref returnValue))
                returnValue = true;

            else if (isWinOnThreeSides(firstBottomLeft, secondBottomLeft, firstTopRight, secondTopRight, DelegateAIorEnemy, ref returnValue))
                returnValue = true;

            return returnValue;
        }






        /// <summary>
        /// Sprawdzenie czy to komputer
        /// </summary>
        private static bool isAI(Button button)
        {
            bool isAIBool = false;

            if (button.Content.Equals(O_SYMBOL))
                isAIBool = true;

            return isAIBool;
        }

        /// <summary>
        /// Sprawdzenie czy to gracz
        /// </summary>
        private static bool isEnemy(Button button)
        {
            bool isEnemyBool = false;

            if (button.Content.Equals(X_SYMBOL))
                isEnemyBool = true;

            return isEnemyBool;
        }

        /// <summary>
        /// Ogólna metoda określania zwycięstwa
        /// </summary>
        private static bool isWinOnThreeSides(Button firstButton, Button secondButton, Button thirdButton, Button fouthButton, DelegateAIorEnemy DelegateAIorEnemy, ref bool returnValue)
        {

            if (inRow(firstButton, secondButton, thirdButton, fouthButton, DelegateAIorEnemy))
                returnValue = true;

            else if (inRow(firstButton, secondButton, fouthButton, thirdButton, DelegateAIorEnemy))
                returnValue = true;

            else if (inRow(firstButton, thirdButton, fouthButton, secondButton, DelegateAIorEnemy))
                returnValue = true;

            else if (inRow(secondButton, thirdButton, fouthButton, firstButton, DelegateAIorEnemy))
                returnValue = true;

            return returnValue;
        }

        private static bool inRow(Button first, Button second, Button third, Button fouth, DelegateAIorEnemy DelegateAIorEnemy)
        {
            bool returnValue = false;
            if (DelegateAIorEnemy(first) && DelegateAIorEnemy(second) && DelegateAIorEnemy(third))
            {
                if (fouth.IsEnabled == true)
                {
                    PerformMove(fouth);
                    returnValue = true;
                }
            }
            return returnValue;
        }
    }
}