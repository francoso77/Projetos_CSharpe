using System;
using System.IO;
using System.Collections.Generic;


namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
                      
            string sourceFile = @"c:\temp\myfolder\file1.csv";
            string lines = "Frank, Alves, 1977";
            string[] palavras;

            Console.WriteLine(lines.Substring(0,2));
            palavras = lines.Split(',');
            foreach (string palavra in palavras) { Console.WriteLine(palavra); }

            try
            {
                using (StreamReader sr = File.OpenText(sourceFile))
                {
                    /*while (!sr.EndOfStream)
                    {
                        lines. sr.ReadLine();
                        Console.WriteLine(line);
                    }*/
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred!");
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Operação finalizada!");
            }

        }
    }
}