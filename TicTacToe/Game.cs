﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    public class Game
    {
        public Sign order;
        private bool isStarted = false;
        short xScore = 0,
              oScore = 0;

        Grid grid;
        MainForm mainForm;

        readonly Random random = new Random();

        public Game(MainForm mainForm)
        {
            this.mainForm = mainForm;
            grid = new Grid(this);
        }

        public void Start()
        {
            isStarted = true;
            order = (random.Next(2) == 0) ? Sign.X : Sign.O;

            if (grid.Value.Count == 0 ||
                grid.Value.Count != Settings.gridSize)
            {
                SetGrid();
            }
        }

        public void CellClick()
        {
            if (IsWinCombination())
            {
                SetScore(order);
                mainForm.PauseGame();

                MessageBox.Show($"{order} is won.", "WIN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            ToggleTurnValue();
        }

        private bool IsWinCombination()
        {
            bool isWinCombination = true;

            // Rows checking.

            for (byte rows = 0; rows < Settings.gridSize; rows++)
            {
                isWinCombination = true;

                for (byte columns = 0; columns < Settings.gridSize - 1; columns++)
                {
                    if (grid.Value[columns][rows].Text != grid.Value[columns + 1][rows].Text ||
                        grid.Value[columns][rows].Text == String.Empty)
                    {
                        isWinCombination = false;
                        break;
                    }
                }

                if (isWinCombination)
                {
                    return isWinCombination;
                }
            }

            // Columns checking.

            for (byte rows = 0; rows < Settings.gridSize; rows++)
            {
                isWinCombination = true;

                for (byte columns = 0; columns < Settings.gridSize - 1; columns++)
                {
                    if (grid.Value[rows][columns].Text != grid.Value[rows][columns + 1].Text ||
                        grid.Value[rows][columns].Text == String.Empty)
                    {
                        isWinCombination = false;
                        break;
                    }
                }

                if (isWinCombination)
                {
                    return isWinCombination;
                }
            }

            // Diagonals checking.

            isWinCombination = true;
            for (byte rows = 0; rows < Settings.gridSize - 1; rows++)
            {
                if (grid.Value[rows][rows].Text != grid.Value[rows + 1][rows + 1].Text ||
                    grid.Value[rows][rows].Text == String.Empty)
                {
                    isWinCombination = false;
                    break;
                }
            }

            if (isWinCombination)
            {
                return isWinCombination;
            }

            isWinCombination = true;
            for (byte rows = 0; rows < Settings.gridSize - 1; rows++)
            {
                if (grid.Value[rows][Settings.gridSize - rows - 1].Text != grid.Value[rows + 1][Settings.gridSize - rows - 2].Text ||
                    grid.Value[rows][Settings.gridSize - rows - 1].Text == String.Empty)
                {
                    isWinCombination = false;
                    break;
                }
            }

            return isWinCombination;
        }

        private void ToggleTurnValue()
        {
            order = (order == Sign.X) ? Sign.O : Sign.X;
            mainForm.SetTurnValue(order);
        }

        public void SetScore(Sign winner)
        {
            if (winner == Sign.X)
            {
                xScore++;
            }
            else if (winner == Sign.O)
            {
                oScore++;
            }
        }

        public void Pause()
        {
            isStarted = false;
        }

        public void ResetValues()
        {
            isStarted = false;
            xScore = 0;
            oScore = 0;
            grid = null;
        }

        private void SetGrid()
        {
            grid.SetGrid(Settings.gridSize);
        }

        public List<List<Button>> GetGrid()
        {
            return grid.Value;
        }

        public void ToggleEnabledGrid(bool enabled)
        {
            grid.ToggleEnabled(enabled);
        }

        public void ClearGrid()
        {
            grid.Clear();
        }

        private void DeleteGrid()
        {
            grid.Delete();
        }
    }
}
