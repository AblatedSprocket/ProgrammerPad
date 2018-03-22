using System;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace ProgrammerPad.Utilities
{
    static class TextFormatter
    {
        public static string FormatJSON(string text)
        {
            if (text.StartsWith("{"))
            {

                StringBuilder jsonBuilder = new StringBuilder();
                int tabCount = 0;
                char previous = char.MinValue;
                foreach (char current in text)
                {
                    if (previous == '{' && current == '}')
                    {
                        jsonBuilder.Append(current);
                    }
                    else if (previous == '[' && current == ']')
                    {
                        jsonBuilder.Append(current);
                    }
                    else if (previous == '{' || previous == '[')
                    {
                        tabCount++;
                        jsonBuilder.Append("\n");
                        jsonBuilder.Append(new string(' ', tabCount));
                        jsonBuilder.Append(current);
                    }
                    else if (previous == ',')
                    {
                        //if (previous == '\"' || previous == '}' || previous == ']' || Regex.Match(previous.ToString(), "[0-9]").Success)
                        //{
                        jsonBuilder.Append("\n");
                        jsonBuilder.Append(new string(' ', tabCount));
                        jsonBuilder.Append(current);
                        //}
                    }
                    else if (current == '}')
                    {
                        tabCount--;
                        jsonBuilder.Append("\n");
                        jsonBuilder.Append(new string(' ', tabCount));
                        jsonBuilder.Append(current);
                    }
                    else if (current == ']')
                    {
                        tabCount--;
                        jsonBuilder.Append("\n");
                        jsonBuilder.Append(new string(' ', tabCount));
                        jsonBuilder.Append(current);

                    }
                    else
                    {
                        jsonBuilder.Append(current);
                    }
                    previous = current;
                }
                return jsonBuilder.ToString();
            }
            else
            {
                MessageBox.Show("Text is invalid CSS.", "Format Error", MessageBoxButton.OK);
                return text;
            }
        }
        public static string FormatCSS(string text)
        {
            if (text.StartsWith("-"))
            {
                StringBuilder cssBuilder = new StringBuilder();
                int tabCount = 0;
                char previous = char.MinValue;
                foreach (char current in text)
                {
                    if (previous == '{' && current == '}')
                    {
                        cssBuilder.Append(current);
                    }
                    else if (previous == '{')
                    {
                        tabCount++;
                        cssBuilder.Append("\n");
                        cssBuilder.Append(new string(' ', tabCount));
                        cssBuilder.Append(current);
                    }
                    else if (previous == ',')
                    {
                        cssBuilder.Append("\n");
                        cssBuilder.Append(new string(' ', tabCount));
                        cssBuilder.Append(current);
                    }
                    else if (current == '}')
                    {
                        tabCount--;
                        cssBuilder.Append("\n");
                        cssBuilder.Append(new string(' ', tabCount));
                        cssBuilder.Append(current);
                    }
                    else
                    {
                        cssBuilder.Append(current);
                    }
                    previous = current;
                }
                return cssBuilder.ToString();
            }
            else
            {
                MessageBox.Show("Text is invalid CSS.", "Format Error", MessageBoxButton.OK);
                return text;
            }
        }
        public static string FormatXML(string text)
        {
            try
            {
                XDocument document = XDocument.Parse(text);
                return document.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Text is invalid XML.", "Format Error", MessageBoxButton.OK);
                return text;
            }
        }
    }
}
