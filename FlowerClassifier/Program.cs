using System;
using Microsoft.Win32;   // WPF діалог для вибору файлів

class Program
{
    [STAThread]   // обов'язково для OpenFileDialog
    static void Main(string[] args)
    {
        var dialog = new OpenFileDialog();
        dialog.Title = "Вибери фото квітки";
        dialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";

        bool? result = dialog.ShowDialog();

        if (result == true)
        {
            string selectedFile = dialog.FileName;

            Console.WriteLine("Вибране фото:");
            Console.WriteLine(selectedFile);

            // --------- ТУТ ПОТОМУ ПІДКЛЮЧИШ СВІЙ КЛАСИФІКАТОР ---------
            // var prediction = FlowerModel.Predict(selectedFile);
            // Console.WriteLine("Це квітка: " + prediction);
        }
        else
        {
            Console.WriteLine("Файл не вибрано.");
        }

        Console.WriteLine("\nНатисни Enter, щоб вийти.");
        Console.ReadLine();
    }
}