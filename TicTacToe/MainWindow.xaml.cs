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
    /// Logika interakcji dla klasy MainWindow.xaml
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

    }
    }
}
