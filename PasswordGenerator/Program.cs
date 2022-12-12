namespace PasswordGenerator
{
    internal class Program
    {
        static readonly Random Random = new();
        static void Main(string[] args)
        {
            if (!IsValid(args))
            {
                ShowHelpText();
                return;
            }
            var length = Convert.ToInt32(args[0]);
            var options = args[1];

            var pattern = options.PadRight(length, 'l');
            var password = string.Empty;
            while (pattern.Length > 0)
            {
                var randomIndex = Random.Next(0, pattern.Length - 1);
                var category = pattern[randomIndex];

                pattern = pattern.Remove(randomIndex, 1);
                if (category == 'l') password += WriteRandomLowerCaseLetter();
                else if (category == 'L') password += WriteRandomUpperCaseLetter();
                else if (category == 'd') password += WriteRandomDigit();
                else password += WriteRandomSpecialCharacter();
            }
            Console.WriteLine(password);
        }
        private static char WriteRandomSpecialCharacter()
        {
            const string allSpecialCharacters = "!\"#¤%&/(){}[]";
            var index = Random.Next(0, allSpecialCharacters.Length - 1);
            return allSpecialCharacters[index];
        }

        private static int WriteRandomDigit()
        {
            return Random.Next(0, 9).ToString()[0];
        }
        private static char WriteRandomUpperCaseLetter()
        {
            return GetRandomLetter('A', 'Z');
        }

        private static char WriteRandomLowerCaseLetter()
        {
            return GetRandomLetter('a', 'z');
        }

        private static char GetRandomLetter(char min, char max)
        {
            return (char)Random.Next(min, max);
        }

        private static bool IsValid(string[] args)
        {
            if (args.Length != 2) return false;

            var lengthStr = args[0];
            var options = args[1];

            if (!IsValidPattern(options)) return false;
            return IsValidLength(lengthStr);
        }

        private static bool IsValidLength(string lengthStr)
        {
            foreach (var character in lengthStr)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidPattern(string options)
        {
            foreach (var character in options)
            {
                const string validCharacters = "lLds";
                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }

        private static void ShowHelpText()
        {
            Console.WriteLine("PasswordGenerator <length> <options>");
            Console.WriteLine("  Options:");
            Console.WriteLine("  - l = lower case letter");
            Console.WriteLine("  - L = upper case letter");
            Console.WriteLine("  - d = digit");
            Console.WriteLine("  - s = special character (!\"#¤%&/(){}[]");
            Console.WriteLine("Example: PasswordGenerator 14 lLssdd");
            Console.WriteLine("         Min. 1 lower case");
            Console.WriteLine("         Min. 1 upper case");
            Console.WriteLine("         Min. 2 special characters");
            Console.WriteLine("         Min. 2 digits");
        }
    }
}