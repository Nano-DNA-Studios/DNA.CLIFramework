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
        public struct ProgressBarStyle
        {
            public ConsoleColor FillColor;
            public ConsoleColor EmptyColor;
            public string HorizontalSeparator;
            public string VerticalSeparator;
            public string CrossSeparator;
            public char ProgressCharacter;
            public char ProgressBackgroundCharacter;
        }

        public static ProgressBarStyle DefaultStyle = new ProgressBarStyle
        {
            FillColor = ConsoleColor.Green,
            EmptyColor = ConsoleColor.Gray,
            HorizontalSeparator = "-",
            VerticalSeparator = "|",
            CrossSeparator = "+",
            ProgressCharacter = '#',
            ProgressBackgroundCharacter = '.'
        };

        public static ProgressBarStyle BlueVsBlue = new ProgressBarStyle
        {
            FillColor = ConsoleColor.Blue,
            EmptyColor = ConsoleColor.Red,
            HorizontalSeparator = "-",
            VerticalSeparator = "|",
            CrossSeparator = "+",
            ProgressCharacter = '#',
            ProgressBackgroundCharacter = '#'
        };

        /// <summary>
        /// The Style of the Progress Bar
        /// </summary>
        public ProgressBarStyle Style { get; set; }

        /// <summary>
        /// The Title of the Progress Bar
        /// </summary>
        public string Title { get; private set; }

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
        /// Default Empty Constructor for the Progress Bar
        /// </summary>
        public ProgressBar()
        {
            Width = 100;
            Height = 1;
            MaxValue = 100;
            MinValue = 0;
            Value = MinValue;
            Title = "";
            Style = DefaultStyle;

            UseBorder = false;
        }

        /// <summary>
        /// Default Constructor for the Progress Bar
        /// </summary>
        /// <param name="width"> The Width of the Progress Bar in the Console Window </param>
        /// <param name="height"> The Height of the Progress Bar in the Console Window </param>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Minimum Value of the Progress Bar </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Width or Height are invalid </exception>
        /// <exception cref="Exception"> Thrown if the Minimum Value is Larger than the Max Value </exception>
        public ProgressBar(float maxValue = 100, float minValue = 0, string title = "", int width = 100, int height = 1)
        {
            Width = width;
            Height = height;
            MaxValue = maxValue;
            MinValue = minValue;
            Value = minValue;
            Style = DefaultStyle;

            if (Width < 0 || Width > Console.WindowWidth)
                throw new ArgumentOutOfRangeException("Width must be greater than 0 and Less than Console Window Width.");

            if (Height < 0)
                throw new ArgumentOutOfRangeException("Height must be greater than 0.");

            if (MinValue > MaxValue)
                throw new Exception("The Maximum Value must be Larger than the Minimum Value.");

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
            Style = DefaultStyle;

            if (MinValue > MaxValue)
                throw new Exception("The Maximum Value must be Larger than the Minimum Value.");

            UseBorder = false;
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
            Console.WriteLine(Style.CrossSeparator + new string(char.Parse(Style.HorizontalSeparator), width - 2) + Style.CrossSeparator);
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
        /// Prints the Title of the Table to the Console
        /// </summary>
        /// <param name="tableWidth"> The Table Width in Characters </param>
        private void PrintTitle()
        {
            //int width = UseBorder ? Width - 2 : Width;

            if (!UseBorder)
            {
                Console.WriteLine(CenterTitle(Width - 1, Title));
                return;
            }

            PrintLine(Width);
            Console.WriteLine(Style.VerticalSeparator + CenterTitle(Width - 1, Title) + Style.VerticalSeparator);
        }

        /// <summary>
        /// Prints the Progress Bar to the Console
        /// </summary>
        public void PrintProgressBar()
        {
            int width = UseBorder ? Width - 2 : Width;
            int progressWidth = (int)(width * Progress);
            int halfHeight = Height / 2;

            if (!string.IsNullOrEmpty(Title))
                PrintTitle();

            if (UseBorder)
                PrintLine(Width);

            for (int i = 0; i < Height; i++)
            {
                if (UseBorder)
                    Console.Write(Style.VerticalSeparator);

                Console.ForegroundColor = Style.FillColor;
                Console.Write(new string(Style.ProgressCharacter, progressWidth));

                Console.ForegroundColor = Style.EmptyColor;
                Console.Write(new string(Style.ProgressBackgroundCharacter, width - progressWidth));

                Console.ResetColor();
                if (UseBorder)
                    Console.Write(Style.VerticalSeparator);

                if (i == halfHeight)
                    Console.Write($"  |  {ProgressPercentage}%");

                Console.WriteLine();
            }

            if (UseBorder)
                PrintLine(Width);
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Progress Value
        /// </summary>
        /// <param name="progress"> The Progress Value </param>
        /// <param name="title"> The Title of the Progress Bar </param>
        /// <param name="style"> The Style of the Progress Bar </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Progress Value is invalid in any way Less than 0, More than 100 </exception>
        public static void PrintProgressBar(float progress, ProgressBarStyle style, string title = "")
        {
            ProgressBar progressBar = new ProgressBar();

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

            progressBar.Title = title;
            progressBar.Style = style;

            progressBar.PrintProgressBar();
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Progress Value
        /// </summary>
        /// <param name="progress"> The Progress Value </param>
        /// <param name="title"> The Title of the Progress Bar </param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown if the Progress Value is invalid in any way Less than 0, More than 100 </exception>
        public static void PrintProgressBar(float progress, string title = "")
        {
            ProgressBar progressBar = new ProgressBar();

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

            progressBar.Title = title;

            progressBar.PrintProgressBar();
        }

        /// <summary>
        /// Prints a Progress Bar to the Console given the Value, Max Value, and Min Value
        /// </summary>
        /// <param name="value"> The Value of the </param>
        /// <param name="maxValue"> The Max Value of the Progress Bar </param>
        /// <param name="minValue"> The Minimum Value of the Progress Bar </param>
        /// 
        public static void PrintProgressBar(float value, ProgressBarStyle style, string title = "", float maxValue = 100, float minValue = 0,bool useBorder = false)
        {
            PrintProgressBar(value, style, title, maxValue, minValue, 100, 1, useBorder);
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
        public static void PrintProgressBar(float value, ProgressBarStyle style, string title, float maxValue, float minValue, int width, int height, bool useBorder = false)
        {
            ProgressBar progressBar = new ProgressBar(maxValue, minValue, title, width, height);
            progressBar.Value = value;
            progressBar.Title = title;
            progressBar.Style = style;
            progressBar.UseBorder = useBorder;
            progressBar.PrintProgressBar();
        }

    }
}
