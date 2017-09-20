using System;

namespace Johnny.Library.Cryptography
{
    /// <summary>
    /// Provides methods for generating random texts.
    /// </summary>
    public static class RandomText
    {
        /// <summary>
        /// Generates a length-indicated letter random text.
        /// </summary>
        public static string Generate(int length)
        {
            // Generate random text
            string s = "";
            //char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            //except capital i and l
            char[] chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            int index;
            for (int i = 0; i < length; i++)
            {
                index = RNG.Next(chars.Length - 1);
                s += chars[index].ToString();
            }
            return s;
        }

        /// <summary>
        /// Generates a letter random text within indicated scope.
        /// </summary>
        public static string Generate(int iFrom, int iTo)
        {
            // Generate random text
            string s = "";
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            int index;
            int lenght = RNG.Next(iFrom, iTo);            
            for (int i = 0; i < lenght; i++)
            {
                index = RNG.Next(chars.Length - 1);
                s += chars[index].ToString();
            }
            return s;
        }
    }
}