using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь, без кавычек к текстовому файлу:");
            string inputFilePath = Console.ReadLine();

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            try
            {
                Dictionary<string, int> wordCounts = CountWords(inputFilePath);

                string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "Результат.txt");
                WriteWordCountsToFile(wordCounts, outputFilePath);

                Console.WriteLine("Результаты успешно записаны в файл: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.ToString()); // Вывод сообщения об ошибке
            }

            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }

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
