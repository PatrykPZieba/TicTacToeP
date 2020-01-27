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
    /// <summary>
    /// Logika interakcji dla MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int size = 4;

        private List<Button> listOfButtons;
        private List<Button> bestMoves;

        public MainWindow()
        {
            InitializeComponent();

            // Lista przycisków (pól)
            listOfButtons = new List<Button>() {
                A1, A2, A3, A4,
                B1, B2, B3, B4,
                C1, C2, C3, C4,
                D1, D2, D3, D4 };

            // Lista przycisków (pól) od najlepszego do najgorszego
            bestMoves = new List<Button>() {
                B2, C3, B3, C2,
                A1, D4, D1, A4,
                A2, A3, B1, C1,
                D2, D3, B4, C4 };
        }

        /// <summary>
        /// Kliknij na pole (przycisk)
        /// </summary>
        private void ClickOnButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "X";
            button.IsEnabled = false;

            AI.MoveAI(listOfButtons, bestMoves);

            if (CheckWinner())
            {
                foreach (var element in listOfButtons)
                {
                    element.IsEnabled = true;
                    element.Content = "";
                }
            }
        }

        /// <summary>
        /// Sprawdź, czy jest zwycięzca
        /// </summary>
        /// <returns>True = jest zwycięzca. False = brak zwycięzcy</returns>
        private bool CheckWinner()
        {
            bool returnValue = false;

            // Dwuwymiarowa tablica przycisków (pól)
            Button[,] arrayOfButtons = new Button[size, size]
            {
                {A1, A2, A3, A4},
                {B1, B2, B3, B4},
                {C1, C2, C3, C4},
                {D1, D2, D3, D4},
            };

            GameStatus status = checkHorizontalAndVertical(arrayOfButtons); // sprawdź wiersze i kolumny
            if (isOver(status)) returnValue = true;

            status = checkDialog(); // sprawdź 2 przekątne
            if (isOver(status)) returnValue = true;

            if (checkForTie() && returnValue == false) // sprawdź remis
            {
                MessageBox.Show("Game ended in Tie");
                returnValue = true;
            }
            return returnValue;
        }


        /// <summary>
        /// Sprawdź wiersze i kolumny
        /// </summary>
        /// <param name = "arrayOfButtons"> Dwuwymiarowa tablica przycisków </param>
        /// <returns> Aktualizowanie statusu gry </returns>
        private GameStatus checkHorizontalAndVertical(Button[,] arrayOfButtons)
        {
            bool gameOver = false;
            string winner = null;

            for (int i = 0; i < size; i++)
            {
                if (GameStatus.isEquals(arrayOfButtons[i, 0], arrayOfButtons[i, 1], arrayOfButtons[i, 2], arrayOfButtons[i, 3], ref gameOver))
                    winner = Convert.ToString(arrayOfButtons[i, 0].Content);

                if (GameStatus.isEquals(arrayOfButtons[0, i], arrayOfButtons[1, i], arrayOfButtons[2, i], arrayOfButtons[3, i], ref gameOver))
                    winner = Convert.ToString(arrayOfButtons[0, i].Content);
            }
            return new GameStatus(gameOver, winner, false);
        }


        /// <summary>
        /// Sprawdzanie 2 przekątnych
        /// </summary>
        /// <returns> Aktualizowanie statusu gry </returns>
        private GameStatus checkDialog()
        {
            bool gameOver = false;
            string winner = null;

            if (GameStatus.isEquals(A1, B2, C3, D4, ref gameOver))
                winner = Convert.ToString(A1.Content);

            else if (GameStatus.isEquals(A4, B3, C2, D1, ref gameOver))
                winner = Convert.ToString(A4.Content);

            return new GameStatus(gameOver, winner, false);
        }


        /// <summary>
        /// Sprawdź remis
        /// </summary>
        /// <returns>True = remis</returns>
        private bool checkForTie()
        {
            bool tie = true;
            foreach (var button in listOfButtons)
            {
                if (button.IsEnabled == true)
                    tie = false;
            }
            return tie;
        }

        /// <summary>
        /// Czy nastąpił koniec gry
        /// </summary>
        /// <param name = "status"> Status gry </param>
        /// <powrót> Prawda = jest zwycięzca. Fałsz = brak zwycięzcy </returns>
        private bool isOver(GameStatus status)
        {
            bool returnValue = false;
            if (status.gameOver)
            {
                MessageBox.Show((status.winner == "O") ? ("A.I Wins") : ("Player Wins"));
                returnValue = true;
            }
            return returnValue;
        }


        /// <summary>
        /// Przycisk do restartu gry
        /// </summary>
        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            foreach (var button in listOfButtons)
            {
                button.IsEnabled = true;
                button.Content = "";
            }
        }


    }
}
