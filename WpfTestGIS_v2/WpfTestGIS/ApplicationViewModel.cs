using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestGIS
{
    class ApplicationViewModel
    {
        public static List<MyPoint> pointsList = new List<MyPoint>();
        public static List<MyLine> lineList = new List<MyLine>();
        public static List<MyPolygon> polygonList = new List<MyPolygon>();

        public void ReadFile(string path)
        {
            pointsList.Clear(); lineList.Clear(); polygonList.Clear();  //очищаю коллекции, чтобы не было объектов из других файлов

            MainWindow.lineState = null;        //обнуляем перемен, отвечающую строку состояния 

            try
            {
                using (StreamReader fileStream = new StreamReader(path, System.Text.Encoding.UTF8)) //начинаем считывать текстовый файл
                {
                    int countIteration = 0;
                    int countError = 0;
                    string line;
                    while ((line = fileStream.ReadLine()) != null)  //считываем построчно файл, пока он не закончится
                    {
                        countIteration++;
                        try
                        {
                            string[] splitLine = line.Split(' ');   //получаем массив координат
                            if (splitLine.Length == 2)
                            {
                                pointsList.Add(new MyPoint(Convert.ToDouble(splitLine[0]), Convert.ToDouble(splitLine[1])));
                            }
                            else if (splitLine.Length == 4)
                            {
                                lineList.Add(new MyLine(Convert.ToDouble(splitLine[0]), Convert.ToDouble(splitLine[1]),
                                                        Convert.ToDouble(splitLine[2]), Convert.ToDouble(splitLine[3])));
                            }
                            else if (splitLine.Length == 6)
                            {
                                polygonList.Add(new MyPolygon(Convert.ToDouble(splitLine[0]), Convert.ToDouble(splitLine[1]),
                                                              Convert.ToDouble(splitLine[2]), Convert.ToDouble(splitLine[3]),
                                                              Convert.ToDouble(splitLine[4]), Convert.ToDouble(splitLine[5])));
                            }
                            else
                            {
                                countError++;
                            }
                        }
                        catch
                        {
                            countError++;
                        }
                    }
                    if (countError == 0)
                    {
                        MainWindow.lineState = "Данные прочитаны полностью";
                    }
                    else
                    {
                        MainWindow.lineState = "Данные прочитаны частично";
                    }

                }
            }
            catch
            {
                MainWindow.lineState = "Файл не прочитан";
            }
            if (pointsList.Count == 0 && lineList.Count == 0 && polygonList.Count == 0)
            {
                MainWindow.lineState = "Файл не прочитан";
            }
        }

    }
}
