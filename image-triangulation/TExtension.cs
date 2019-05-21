﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


// TODO
// * Передавать размер изображения для Striping
namespace image_triangulation
{
    /// <summary>
    /// Содержит функции для работы с .t файлами.
    /// </summary>
    static class TExtension
    {
        public static void Save(HashSet<Pixel> pivotPoints, string path)
        {
            List<Pixel> sortedPointsList = Striping(pivotPoints);

            // Запись
            using (BinaryWriter tmpFileWriter = new BinaryWriter(File.Create(path + "~")))
            {
                // Записываем первую точку
                Pixel previousPixel = sortedPointsList.First();
                tmpFileWriter.Write((short)previousPixel.X);
                tmpFileWriter.Write((short)previousPixel.Y);
                tmpFileWriter.Write(previousPixel.brightness);

                // Записываем остальные точки в дельта-координатах
                foreach (Pixel pixel in sortedPointsList.Skip(1))
                {
                    tmpFileWriter.Write((short)(pixel.X - previousPixel.X));
                    tmpFileWriter.Write((short)(pixel.Y - previousPixel.Y));
                    tmpFileWriter.Write(pixel.brightness);
                    previousPixel = pixel;
                }
            }

            // Компрессия
            using (FileStream tmpFileReader = new FileStream(path + "~", FileMode.Open))
            {
                using (FileStream outputFileWriter = new FileStream(path, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(outputFileWriter, CompressionMode.Compress))
                    {
                        tmpFileReader.CopyTo(compressionStream);
                    }
                }
            }
            File.Delete(path + "~");
        }

        public static void Open(HashSet<Pixel> pivotPoints, string path)
        {
            // Декомпрессия
            using (FileStream compressionFileReader = new FileStream(path, FileMode.Open))
            {
                using (FileStream tmpFileWriter = new FileStream(path + "~", FileMode.Create))
                {
                    using (GZipStream decompressionStream = new GZipStream(compressionFileReader, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(tmpFileWriter);
                    }
                }
            }

            // Чтение
            using (BinaryReader tmpFileReader = new BinaryReader(File.OpenRead(path + "~")))
            {
                // Читаем первую точку
                Pixel pixel = new Pixel(tmpFileReader.ReadInt16(), tmpFileReader.ReadInt16(), tmpFileReader.ReadByte());
                pivotPoints.Add(pixel);

                // Читаем остальные точки в дельта-координатах
                while (tmpFileReader.BaseStream.Position != tmpFileReader.BaseStream.Length)
                {
                    pixel = new Pixel(pixel.X + tmpFileReader.ReadInt16(), pixel.Y + tmpFileReader.ReadInt16(), tmpFileReader.ReadByte());
                    pivotPoints.Add(pixel);
                }
            }

            File.Delete(path + "~");
        }

        private static List<Pixel> Striping(HashSet<Pixel> pivotPoints)
        {
            const float STRIPING_FACTOR = 0.5F;

            List<List<Pixel>> stripes = new List<List<Pixel>>();
            int numberOfStripes = (int)Math.Sqrt((STRIPING_FACTOR * 512 * pivotPoints.Count) / 512);

            // Разбиваем исходный список на полосы
            for (int i = 0; i < numberOfStripes; ++i)
            {
                stripes.Add(new List<Pixel>());
            }
            foreach (Pixel p in pivotPoints)
            {
                stripes[(int)(p.X / (512.0 / numberOfStripes))].Add(p);
            }

            // Сортируем пиксели в полосках
            for (int i = 0; i < numberOfStripes; ++i)
            {
                stripes[i].Sort(new YComparer());
            }

            // Склеиваем полоски в один массив            
            List<Pixel> sortedPointsList = new List<Pixel>();
            for (int i = 0; i < numberOfStripes; ++i)
            {
                sortedPointsList.AddRange(stripes[i]);
            }

            return sortedPointsList;
        }
    }
}
