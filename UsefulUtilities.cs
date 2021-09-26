using System;


namespace Program_Ksiegowy
{
    public static class UsefulUtilities
    {
        public static string AskUser(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
        public static int AskUser_int(string question)
            => Convert.ToInt32(AskUser(question));
    }
}