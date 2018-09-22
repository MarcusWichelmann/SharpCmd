using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd
{
    public class Command
    {
        private const char PartDelimiter = ' ';
        private const char StringMarker = '"';
        private const char EscapeMarker = '\\';

        public string RawString { get; }

        public string[] CommandParts { get; }

        public Command(string rawString)
        {
            RawString = rawString ?? throw new ArgumentNullException(nameof(rawString));
            CommandParts = ParseParts();
        }

        private string[] ParseParts()
        {
            var parts = new List<string>();
            string currentPart = string.Empty;

            bool nextCharEscaped = false;
            bool isInString = false;

            for (int i = 0; i < RawString.Length; i++)
            {
                char currentChar = RawString[i];

                if (nextCharEscaped)
                {
                    currentPart += currentChar;
                    nextCharEscaped = false;
                }
                else if (!isInString)
                {
                    switch (currentChar)
                    {
                        case PartDelimiter:
                            if (!string.IsNullOrEmpty(currentPart))
                            {
                                parts.Add(currentPart);
                                currentPart = null;
                            }
                            break;
                        case EscapeMarker:
                            nextCharEscaped = true;
                            break;
                        case StringMarker:
                            isInString = true;
                            break;
                        default:
                            currentPart += currentChar;
                            break;
                    }
                }
                else if (isInString)
                {
                    switch (currentChar)
                    {
                        case EscapeMarker:
                            nextCharEscaped = true;
                            break;
                        case StringMarker:
                            isInString = false;
                            break;
                        default:
                            currentPart += currentChar;
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(currentPart))
                parts.Add(currentPart);

            return parts.ToArray();
        }
    }
}
