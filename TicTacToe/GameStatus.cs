using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Status gry
    /// </summary>
    class GameStatus
    {
        public bool gameOver { get; private set; }
        public bool isTie { get; private set; }
        public string winner { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public GameStatus()
        {
            gameOver = false;
            isTie = false;
            winner = null;
        }
        public GameStatus(bool gameOver, string winner, bool isTie)
        {
            this.gameOver = gameOver;
            this.winner = winner;
            this.isTie = isTie;
        }


        /// <summary>
        /// Czy wszystkie przyciski mają tę samą wartość?
        /// </summary>
        /// <returns>Zwraca wartość Prawda, jeśli wszystkie przyciski mają tę samą wartość</returns>
        public static bool isEquals(Button A, Button B, Button C, Button D, ref bool gameOver)
        {
            bool returnValue = false;

            if (A.Content.Equals(B.Content) && A.Content.Equals(C.Content) && A.Content.Equals(D.Content)
                && B.Content.Equals(C.Content) && B.Content.Equals(D.Content)
                && C.Content.Equals(D.Content) && !A.Content.Equals(""))
            {
                gameOver = true;
                returnValue = true;
            }
            return returnValue;
        }

    }
}
