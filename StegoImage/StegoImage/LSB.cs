using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StegoImage
{
    public class LSB
    {
        public Bitmap bmp; // изображение
        public BitArray bits { get; set; } // Битовый массив сообщения
        public int numberOfSymbols;// кол-во сиволов
        public bool[] cRGB = new bool[3];
        public int bitOnC;
        public LSB(Bitmap image, string message, bool[] arr, int y) //передача данных в текущий класс (режим кодирования)
        {
            this.bmp = new Bitmap(image);
            numberOfSymbols = message.Length;
            arr.CopyTo(cRGB, 0);
            bitOnC = y;
            GetBitsOfMessage(message);// получение битового массива сообщения

        }
        public LSB(Bitmap image, bool[] arr, int y)//передача данных в текущий класс (режим декодирования)
        {
            this.bmp = new Bitmap(image);
            arr.CopyTo(cRGB, 0);
            bitOnC = y;
        }
        public void GetBitsOfMessage(string message) // получение битового массива сообщения
        {
            byte[] temp = Encoding.Default.GetBytes(message); //перевод в массив байтов
            bits = new BitArray(temp); // перевод в массив битов
        }
        public Bitmap insertTextToImage() // LSB -кодировние
        {
            int R, G, B;
            Bitmap bitmap = (Bitmap)bmp.Clone();//Клонируем изображение
            int bitCount = 0; //счетчики битов сообщения и пикселей изображения
            int allbit = 0;
            bool getAllNeedPixel = false; // булевая переменная, необходимая для выхода из внешнего цикла
            //С помощью циклов проходим по изображению (по пикселям)
            for (int i = 0; i < bmp.Height; i++)
            {
                if (getAllNeedPixel) break; //Если сообщение уже закодированно, то прекращаем проход по изображению
                for (int j = 0; j < bmp.Width; j++)
                {
                    if (allbit != bits.Count)
                    {
                        Color pixel = bmp.GetPixel(j, i); // получем текущий пиксель по координате
                        R = pixel.R;
                        G = pixel.G;
                        B = pixel.B;
                        if (cRGB[0])
                        {
                                for (int l = 0; l < bitOnC; l++)
                                {
                                if (bitCount == bits.Count)
                                {
                                    getAllNeedPixel = true;
                                    break;
                                }
                                    R = SetPixel(bits[bitCount], R, l);
                                    bitCount++; // увеличиваем счетчик - получаем бит следующий сообщения
                                    allbit++;
                                }                                
                        }
                        if (cRGB[1])
                        {
                                for (int l = 0; l < bitOnC; l++)
                                {
                                    if (bitCount == bits.Count)
                                    {
                                        getAllNeedPixel = true;
                                        break;
                                    }
                                    G = SetPixel(bits[bitCount], G, l);
                                    bitCount++; // увеличиваем счетчик - получаем бит следующий сообщения
                                    allbit++;
                                }

                        }
                        if (cRGB[2])
                        {
                                for (int l = 0; l < bitOnC; l++)
                                {
                                if (bitCount == bits.Count)
                                {
                                    getAllNeedPixel = true;
                                    break;
                                }
                                B = SetPixel(bits[bitCount], B, l);
                                    bitCount++; // увеличиваем счетчик - получаем бит следующий сообщения
                                    allbit++;
                                }
                        }
                        bitmap.SetPixel(j, i, Color.FromArgb(pixel.A, R, G, B));
                    }
                    else // выход из цикла
                    {
                        getAllNeedPixel = true;
                        break;
                    }
                }
            }
            return bitmap; // возвращаем результат
        }
        public string ExtractMessage(Bitmap bmpCry) // Декодировка всего изображения
        {
            string result = string.Empty;
            int bitCount = 0, bit; // счётчик и переменая для сохранение каждого бита отдельно (временная переменная)
            List<bool> messBits = new List<bool>();//Список булевых значений, здесь будут храниться значения битов
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);// получение пикселя
                    if (cRGB[0])
                        for (int l = 0; l < bitOnC; l++)
                        {
                            bit = GetBitfromPixel(pixel.R, l);// вызов метода для получение последнего бита
                            messBits.Add(Convert.ToBoolean(bit));//запоминаем бит
                            bitCount++;
                        }
                    if (cRGB[1])
                        for (int l = 0; l < bitOnC; l++)
                        {
                            bit = GetBitfromPixel(pixel.G, l);// вызов метода для получение последнего бита
                            messBits.Add(Convert.ToBoolean(bit));//запоминаем бит
                            bitCount++;
                        }
                    if (cRGB[2])
                        for (int l = 0; l < bitOnC; l++)
                        {
                            bit = GetBitfromPixel(pixel.B, l);// вызов метода для получение последнего бита
                            messBits.Add(Convert.ToBoolean(bit));//запоминаем бит
                            bitCount++;
                        }
                }
            }
            BitArray n = new BitArray(messBits.ToArray()); // булевый список в битовый массив
            byte[] n2 = new byte[n.Length]; //массив байтов
            n.CopyTo(n2, 0); //перевод в байты
            result = Encoding.Default.GetString(n2);// байты в строку
            return result;

        }
        private int GetBitfromPixel(int currentPix, int i)
        {
            BitArray n = new BitArray(new int[] { currentPix });// битовое представление пикселя
            // так как битовый массив хранит биты в дргугом порядке в отличии от привычного порядка
            // Пример  число 2 - 00010 (привычный порядок), в битовом массиве - реверс т.е. 01000
            // последний бит - первый бит в массиве
            return Convert.ToInt32(n[i]);
        }

        private int SetPixel(bool currentBit, int pixel, int i)
        {
            BitArray n = new BitArray(new int[] { pixel }); // битовое представление пикселя
            // так как битовый массив хранит биты в дргугом порядке в отличии от привычного порядка
            // Пример  число 2 - 00010 (привычный порядок), в битовом массиве - реверс т.е. 01000
            // последний бит - первый бит в массиве
            if (currentBit)// если значение бита = 1
                n[i] = true;
            else
                n[i] = false;
            int[] y = new int[1]; //получаем значение  пикселя в привычном виде (натурального числа)
            n.CopyTo(y, 0);
            return y[0];
        }
    }
}
