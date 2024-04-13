using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA_CLI_Framework
{
    /// <summary>
    /// Represents a Progress Bar that can be displayed in the Console
    /// </summary>
    public class ProgressBar
    {
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
        /// The Character to use for the Progress of the Progress Bar
        /// </summary>
        private char _progressCharacter = '#'; //'█'

        /// <summary>
        /// The Character to use for the Background of the Progress Bar
        /// </summary>
        private char _progressBackgroundCharacter = '.'; // '░'

        /// <summary>
        /// The Color of the Fill of the Progress Bar
        /// </summary>
        public ConsoleColor FillColor { get; set; }

        /// <summary>
        /// The Color of the Empty Space of the Progress Bar
        /// </summary>
        public ConsoleColor EmptyColor { get; set; }

        /// <summary>
        /// The Width of the Progress Bar
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The Height of the Progress Bar
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Boolean Flag to see if the Progress Bar should have a Border
        /// </summary>
        public bool UseBorder { get; set; }

        /// <summary>
        /// The Progress or Completion Percentage of the Progress Bar
        /// </summary>
        public float Progress
        {
            get => GetProgress();
            set => Value = GetValueFromProgress(value);
        }

        /// <summary>
        /// The Progress as a Percentage Value
        /// </summary>
        public float ProgressPercentage
        {
            get => Progress * 100;
            set => Value = GetValueFromProgressPercentage(value);
        }

        /// <summary>
        /// The Max Value of the Progress Bar
        /// </summary>
        public float MaxValue { get; private set; }

        /// <summary>
        /// The Min Value of the Progress Bar
        /// </summary>
        public float MinValue { get; private set; }

        /// <summary>
        /// The Current Value of the Progress Bar
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Default Constructor for the Progress Bar
        /// </summary>
        /// <param name="width"> The Width of the Progress Bar in the Console Window </param>
        /// <param name="height"> The Height of the Progress Bar in the Console Window </param>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Minimum Value of the Progress Bar </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Width or Height are invalid </exception>
        /// <exception cref="Exception"> Thrown if the Minimum Value is Larger than the Max Value </exception>
        public ProgressBar(int width, int height = 1, float maxValue = 100, float minValue = 0)
        {
            Width = width;
            Height = height;
            MaxValue = maxValue;
            MinValue = minValue;
            Value = minValue;

            if (Width < 0 || Width > Console.WindowWidth)
                throw new ArgumentOutOfRangeException("Width must be greater than 0 and Less than Console Window Width.");

            if (Height < 0)
                throw new ArgumentOutOfRangeException("Height must be greater than 0.");

            if (MinValue > MaxValue)
                throw new Exception("The Maximum Value must be Larger than the Minimum Value.");

            SetDefaultSeparators();
            SetDefaultColors();

            UseBorder = false;
        }

        /// <summary>
        /// Default Constructor for the Progress Bar
        /// </summary>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Min Value of the Progress Bar </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the  </exception>
        /// <exception cref="Exception"></exception>
        public ProgressBar(float maxValue = 100, float minValue = 0)
        {
            Width = 100;
            Height = 1;
            MaxValue = maxValue;
            MinValue = minValue;
            Value = minValue;


            if (MinValue > MaxValue)
                throw new Exception("The Maximum Value must be Larger than the Minimum Value.");

            SetDefaultSeparators();
            SetDefaultColors();

            UseBorder = false;
        }

        /// <summary>
        /// Sets the Default Colors for the Style of the Progress Bar
        /// </summary>
        private void SetDefaultColors()
        {
            FillColor = ConsoleColor.Green;
            EmptyColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Sets the Default Separators for the Style of the Progress Bar
        /// </summary>
        private void SetDefaultSeparators()
        {
            _horizontalSeparator = "-";
            _verticalSeparator = "|";
            _crossSeparator = "+";
        }

        /// <summary>
        /// Sets the Progress of the Progress Bar by Updating the Value based on the Progress Percentage
        /// </summary>
        /// <param name="progress"> The Progress Value between 0 and 1 </param>
        public void SetProgress(float progress)
        {
            Value = GetValueFromProgress(progress);
        }

        /// <summary>
        /// Sets the Progress of the Progress Bar by Updating the Value based on the Progress Percentage
        /// </summary>
        /// <param name="progressPercent"> The Progress Value Percentage between 0 and 100 </param>
        /// <returns> The Value of the Progress Bar based off the new Progress Value Percentage </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Progress Percentage Provided is not within 0% and 100% </exception>
        private float GetValueFromProgressPercentage(float progressPercent)
        {
            if (progressPercent < 0 && progressPercent > 100)
                throw new ArgumentOutOfRangeException("Progress must be between 0 and 1");

            return MinValue + (MaxValue - MinValue) * (progressPercent / 100);
        }

        /// <summary>
        /// Sets the Progress of the Progress Bar by Updating the Value based on the Progress Value
        /// </summary>
        /// <param name="progress"> The Progress Value between 0 and 1 </param>
        /// <returns> The Value of the Progress Bar based off the new Progress Value </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Progress Value Provided is not within 0 and 1 </exception>
        private float GetValueFromProgress(float progress)
        {
            if (progress < 0 && progress > 1)
                throw new ArgumentOutOfRangeException("Progress must be between 0 and 1");

            return MinValue + (MaxValue - MinValue) * progress;
        }

        /// <summary>
        /// Boolean Check to see if the Value is within the Bounds of the Progress Bar
        /// </summary>
        /// <param name="value"> The Value to Check if it is Within Bounds </param>
        /// <returns> True if the Value is Within Bounds, False if it isn't </returns>
        private bool IsWithinBounds(float value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        /// <summary>
        /// Returns the Progress of the Progress Bar
        /// </summary>
        /// <returns> The Progress of the Progress Bar between 0 and 1 </returns>
        private float GetProgress()
        {
            return (Value - MinValue) / (MaxValue - MinValue);
        }

        /// <summary>
        /// Prints the Border Line to the Console
        /// </summary>
        /// <param name="width"></param>
        private void PrintLine(int width)
        {
            Console.WriteLine(_crossSeparator + new string(char.Parse(_horizontalSeparator), width - 2) + _crossSeparator);
        }

        /// <summary>
        /// Prints the Progress Bar Portion to the Console
        /// </summary>
        /// <param name="width"></param>
        private void PrintProgress(int width)
        {
            int progressWidth = (int)(width * Progress);
            int halfHeight = Height / 2;

            for (int i = 0; i < Height; i++)
            {
                Console.ForegroundColor = FillColor;
                Console.Write(new string(_progressCharacter, progressWidth));

                Console.ForegroundColor = EmptyColor;
                Console.Write(new string(_progressBackgroundCharacter, width - progressWidth));

                if (i == halfHeight)
                {
                    Console.ResetColor();
                    Console.Write($"  |  {ProgressPercentage}%");
                }


                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the Progress Bar to the Console
        /// </summary>
        public void PrintProgressBar()
        {
            if (UseBorder)
            {
                PrintLine(Width);
                int widthWithoutBorder = Width - 2;
                Console.ResetColor();
                Console.Write(_verticalSeparator);

                PrintProgress(widthWithoutBorder);

                Console.ResetColor();
                Console.WriteLine(_verticalSeparator);

                PrintLine(Width);
            }
            else
                PrintProgress(Width);
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Progress Value
        /// </summary>
        /// <param name="progress"> The Progress Value </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Progress Value is invalid in any way Less than 0, More than 100 </exception>
        public static void PrintProgressBar(float progress)
        {
            ProgressBar progressBar = new ProgressBar(100, 1, 100, 0);

            if (progress < 0)
                throw new ArgumentOutOfRangeException("Progress must be more than 0.");

            if (progress > 1)
            {
                if (progress > 100)
                    throw new ArgumentOutOfRangeException("Progress must be less than 100.");

                progressBar.ProgressPercentage = progress;
            }
            else
                progressBar.Progress = progress;

            progressBar.PrintProgressBar();
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Value, Max Value, and Min Value
        /// </summary>
        /// <param name="value"> The Value of the </param>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Minimum Value of the Progress Bar </param>
        public static void PrintProgressBar(float value, float maxValue = 100, float minValue = 0)
        {
            PrintProgressBar(value, maxValue, minValue, 100, 1, false);
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Progress Value, Max Value, Min Value, Width, Height, and Border
        /// </summary>
        /// <param name="value"> Value of the Progress Bar </param>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Min Value of the Progress Bar </param>
        /// <param name="width"> The Width of the Progress Bar </param>
        /// <param name="height"> The Height of the Progress Bar </param>
        /// <param name="useBorder"></param>
        public static void PrintProgressBar(float value, float maxValue, float minValue, int width, int height, bool useBorder = false)
        {
            ProgressBar progressBar = new ProgressBar(width, height, maxValue, minValue);
            progressBar.Value = value;
            progressBar.UseBorder = useBorder;
            progressBar.PrintProgressBar();
        }

    }
}
