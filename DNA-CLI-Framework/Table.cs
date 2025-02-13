
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNA_CLI_Framework
{
    /// <summary>
    /// Represents a Data Table that can be printed to the Console
    /// </summary>
    public class Table
    {
        /// <summary>
        /// Rows of Data to Print Out
        /// </summary>
        private List<string[]> _rows = new List<string[]>();

        /// <summary>
        /// The Title of the Table
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Width of each Column in Characters
        /// </summary>
        private int[] _columnWidths;

        /// <summary>
        /// The Character to use for the Horizontal Separator
        /// </summary>
        private string _horizontalSeparator;

        /// <summary>
        /// The Character to use for the Vertical Separator
        /// </summary>
        private string _verticalSeparator;

        /// <summary>
        /// The Character to use for the Cross Separator
        /// </summary>
        private string _crossSeparator;

        /// <summary>
        /// Creates a new Table with the specified Separators
        /// </summary>
        /// <param name="horizontal"> Horizontal Seperator Character </param>
        /// <param name="vertical"> Vertical Seperator Character</param>
        /// <param name="cross"> Cross Seperator Character </param>
        public Table(char horizontal = '-', char vertical = '|', char cross = '+')
        {
            this._horizontalSeparator = horizontal.ToString();
            this._verticalSeparator = vertical.ToString();
            this._crossSeparator = cross.ToString();
        }

        /// <summary>
        /// Adds a Row to the Table, recalcutes the Column Widths
        /// </summary>
        /// <param name="columns"> The Data in each Column of the Row </param>
        public void AddRow(params string[] columns)
        {
            _rows.Add(columns);
            CalculateColumnWidths();
        }

        /// <summary>
        /// Sets the Title of the Table
        /// </summary>
        /// <param name="title"> The Title of the Table </param>
        public void SetTitle(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Calculates the Width of each Column based on the Data in the Rows
        /// </summary>
        private void CalculateColumnWidths()
        {
            if (_rows.Count == 0) return;

            int numColumns = _rows[0].Length;
            _columnWidths = new int[numColumns];

            foreach (var row in _rows)
            {
                for (int i = 0; i < numColumns; i++)
                {
                    if (row.Length > i)
                        _columnWidths[i] = Math.Max(_columnWidths[i], row[i].Length);
                }
            }
        }

        /// <summary>
        /// Centers the Title of the Table
        /// </summary>
        /// <param name="width"> The Width of the Table </param>
        /// <param name="title"> The Title of the Table </param>
        /// <returns> The String Title Centered based on the Width </returns>
        private string CenterTitle(int width, string title)
        {
            int padding = (width - title.Length) / 2;
            return title.PadLeft(title.Length + padding).PadRight(width - 1);
        }

        /// <summary>
        /// Calculates the Total Width of the Table
        /// </summary>
        /// <returns> The Width of the Table in Characters </returns>
        private int CalculateTableWidth()
        {
            return _columnWidths.Sum() + _columnWidths.Length * 3 + 1;
        }

        /// <summary>
        /// Prints the Title of the Table to the Console
        /// </summary>
        /// <param name="tableWidth"> The Table Width in Characters </param>
        private void PrintTitle(int tableWidth)
        {
            PrintLine(tableWidth);
            Console.WriteLine(_verticalSeparator + CenterTitle(tableWidth - 1, Title) + _verticalSeparator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableWidth"></param>
        private void PrintLine(int tableWidth)
        {
            Console.WriteLine(_crossSeparator + new string(char.Parse(_horizontalSeparator), tableWidth - 2) + _crossSeparator);
        }

        /// <summary>
        /// Prints the Table to the Console
        /// </summary>
        public void PrintTable()
        {
            if (_rows.Count == 0) return;

            int tableWidth = CalculateTableWidth();

            Console.WriteLine();

            if (!string.IsNullOrEmpty(Title))
                PrintTitle(tableWidth);

            PrintLine();
            foreach (var row in _rows)
            {
                PrintRow(row);
                PrintLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prints an Individual Line to the Console
        /// </summary>
        private void PrintLine()
        {
            Console.Write(_crossSeparator);
            for (int i = 0; i < _columnWidths.Length; i++)
            {
                Console.Write(new string(char.Parse(_horizontalSeparator), _columnWidths[i] + 2));
                Console.Write(_crossSeparator);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints an Individual Row to the Console
        /// </summary>
        /// <param name="columns"></param>
        private void PrintRow(string[] columns)
        {
            Console.Write(_verticalSeparator);
            for (int i = 0; i < columns.Length; i++)
            {
                string text = columns[i];
                text = text.PadRight(_columnWidths[i] - (_columnWidths[i] - text.Length) / 2).PadLeft(_columnWidths[i]);
                Console.Write($" {text} {_verticalSeparator}");
            }
            Console.WriteLine();
        }
    }
}
