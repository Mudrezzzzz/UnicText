using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCountApp
{
    class Program
    {
        /// <summary>
        /// точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {
            RunProgram();
        }
            /// <summary>
            /// Запускает программу
            /// </summary>
            static void RunProgram()
        {   while (true)
            {
                Console.WriteLine("Введите exit, для завершения программы");
                Console.Write("Введите путь, без кавычек к текстовому файлу: ");
                string inputFilePath = DeleteQuotes(Console.ReadLine());

                if (inputFilePath == "exit")
                { return; }

                try
                {
                    string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "Результат.txt");
                    WriteWordCountsToFile(CountWords(inputFilePath), outputFilePath);
                    Console.WriteLine("Результаты успешно записаны в файл: " + outputFilePath);

                    Console.WriteLine("Нажмите любую клавишу для завершения...");
                    Console.ReadKey();
                    return;
                }
                catch (FileNotFoundException e)
                { Console.WriteLine("Файл не найден"); }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Удаление кавычек
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string DeleteQuotes(string str)
        {
         if (str.StartsWith("\"") &&  str.EndsWith("\""))
            {
                return str.Trim('"');
            }
         return str;
        }

        /// <summary>
        /// Метод для подсчета количества употреблений каждого слова в тексте
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static Dictionary<string, int> CountWords(string filePath)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            string text = File.ReadAllText(filePath);
            string[] words = Regex.Split(text, @"\W+");
                      
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    string lowerCaseWord = word.ToLower();
                    if (wordCounts.ContainsKey(lowerCaseWord))
                    {
                        wordCounts[lowerCaseWord]++;
                    }
                    else
                    {
                        wordCounts.Add(lowerCaseWord, 1);
                    }
                }
            }

            
            return wordCounts.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Метод для записи результатов в файл
        /// </summary>
        /// <param name="wordCounts"></param>
        /// <param name="filePath"></param>
        static void WriteWordCountsToFile(Dictionary<string, int> wordCounts, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
                foreach (var pair in wordCounts)
                {
                    writer.WriteLine($"{pair.Key,-20}{pair.Value}");
                }
            }
        }
    }
}
