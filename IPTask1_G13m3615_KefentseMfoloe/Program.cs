using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IPTask1_G13m3615_KefentseMfoloe
{
    class Program
    {
        struct Rgb_pts {
            public int r;
            public int g;
            public int b;
        };
        public static List<int> rgbValExtractor(string[] arr, string col)
        {
            List<int> result = new List<int>();
            int index = 0;

            switch (col)
            {
                case "red":
                    index = 4;
                    while(index < arr.Length)
                    {
                        result.Add(Convert.ToInt32(arr[index]));
                        index = index + 3;
                    }
                    break;
                case "green":
                    index = 5;
                    while (index < arr.Length)
                    {
                        result.Add(Convert.ToInt32(arr[index]));
                        index = index + 3;
                    }
                    break;
                case "blue":
                    index = 6;
                    while (index < arr.Length)
                    {
                        result.Add(Convert.ToInt32(arr[index]));
                        index = index + 3;
                    }
                    break;
            }

            return result;
        }

        // To create an int arrary without the useless headers;
        public static int[] makeImgArr(string filename)
        {
            string[] rgblines1 = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\frames\" + filename);
            string[] widthXheight = rgblines1[2].Split(' ');
            int height = Convert.ToInt32(widthXheight[0]);
            int width = Convert.ToInt32(widthXheight[1]);

            int[] imgArr = new int[width * height * 3];
            int index = 0;
            for(int i = 4; i < rgblines1.Length; i++)
            {
                imgArr[index] = Convert.ToInt32(rgblines1[i]);
                index++;
            }
            return imgArr;
        }

        // To find the difference between two images
        public static int[] imgArrDiff(int[] backgrnd, int[] foregrnd)
        {
            int[] diff = new int[backgrnd.Length];
            int i = 0;
            while (i < backgrnd.Length)
            {
                diff[i] = foregrnd[i] - backgrnd[i];
                i++;
            }
            return diff;
        }

        public static void imgArrToPPM(int[] imgArr, string outputfilename, string comment, string widthXheight)
        {
            string path = @"C:\Users\g13m3615\Documents\Image processing\frames\background subtraction results\" + outputfilename;
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("P3");
                    sw.WriteLine(comment);
                    sw.WriteLine(widthXheight);
                    sw.WriteLine(255);
                    foreach(int x in imgArr)
                    {
                        sw.WriteLine(x);
                    }
                }
            }
        }

        public static int[] GaussianAvg(int[] newImg, int[] avgImg)
        {
            int[] result = new int[avgImg.Length];
            for(int index = 0; index < avgImg.Length; index++)
            {
                // current_mu = 0.05*CurrPixel + (1 - 0.05)*previous_mu
                result[index] = Convert.ToInt32(0.05 * newImg[index] + (1 - 0.05) * avgImg[index]);
            }
            return result;
        }

        public static void GrayScale()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);

            // Lets create a ppm/txt file for the gray scale version of the original file
            string path = @"C:\Users\g13m3615\Documents\Image processing\grayscale.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // We can now write content into the new file
                    sw.WriteLine("P2");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine(rgblines[2]);
                    sw.WriteLine(rgblines[3]);

                    //The big step here is for us to read every 3 ascii values in the array and use 
                    //them in the gray scaling formula to calculate the new gray scale pixel value.
                    //We then take that new value and write it into the gray scale file.
                    int index = 4;
                    while (index < rgblines.Length - 1)
                    {
                        if (index % 3 == 0)
                        {
                            //Gray = (Red * 0.3 + Green * 0.59 + Blue * 0.11)
                            double newval = Convert.ToInt32(rgblines[index - 2]) * 0.3 + Convert.ToInt32(rgblines[index - 1]) * 0.59 + Convert.ToInt32(rgblines[index]) * 0.11;
                            sw.WriteLine(Convert.ToInt32(newval));
                        }
                        index++;
                    }
                }
                Console.WriteLine("dsdada");
            }    
        }

        public static void Correct()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);

            // Lets create a ppm/txt file for the gray scale version of the original file
            string path = @"C:\Users\g13m3615\Documents\Image processing\correct.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // We can now write content into the new file
                    sw.WriteLine("P2");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine(rgblines[2]);
                    sw.WriteLine(rgblines[3]);

                    //The big step here is for us to read every 3 ascii values in the array and use 
                    //them in the gray scaling formula to calculate the new gray scale pixel value.
                    //We then take that new value and write it into the gray scale file.
                    int index = 4;
                    while (index < rgblines.Length - 1)
                    {
                        if (index % 3 == 0)
                        {
                            //Gray = (Red * 0.3 + Green * 0.59 + Blue * 0.11)
                            double newval = Convert.ToInt32(rgblines[index - 2]) * 0.3 + Convert.ToInt32(rgblines[index - 1]) * 0.59 + Convert.ToInt32(rgblines[index]) * 0.11;
                            sw.WriteLine(Convert.ToInt32(newval));
                        }
                        index++;
                    }
                }
                Console.WriteLine("dsdada");
            }
        }

        public static void Average()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);

            // Lets create a ppm/txt file for the gray scale version of the original file
            string path = @"C:\Users\g13m3615\Documents\Image processing\average.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // We can now write content into the new file
                    sw.WriteLine("P2");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine(rgblines[2]);
                    sw.WriteLine(rgblines[3]);

                    //The big step here is for us to read every 3 ascii values in the array and use 
                    //them in the gray scaling formula to calculate the new gray scale pixel value.
                    //We then take that new value and write it into the gray scale file.
                    int index = 4;
                    while (index < rgblines.Length - 1)
                    {
                        if (index % 3 == 0)
                        {
                            //Gray = (Red + Green + Blue) / 3
                            double newval = (Convert.ToInt32(rgblines[index - 2]) + Convert.ToInt32(rgblines[index - 1]) + Convert.ToInt32(rgblines[index]) ) / 3;
                            sw.WriteLine(Convert.ToInt32(newval));
                        }
                        index++;
                    }
                }
                Console.WriteLine("dsdada");
            }
        }
      
        public static void Singlechannel()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("Choose the channel to grey scale by: \n* red \n* green \n* blue");
            string col = Console.ReadLine();
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);

            // Lets create a ppm/txt file for the gray scale version of the original file
            string path = @"C:\Users\g13m3615\Documents\Image processing\singlechannel.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // We can now write content into the new file
                    sw.WriteLine("P2");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine(rgblines[2]);
                    sw.WriteLine(rgblines[3]);

                    //The big step here is for us to read every 3 ascii values in the array and use 
                    //them in the gray scaling formula to calculate the new gray scale pixel value.
                    //We then take that new value and write it into the gray scale file.
                    int index = 0;

                    switch (col)
                    {
                        case "red":
                            index = 4;
                            break;
                        case "green":
                            index = 5;
                            break;
                        case "blue":
                            index = 6;
                            break;
                    }

                    while (index < rgblines.Length - 1)
                    {
                        //grey = red or blue or green
                        sw.WriteLine(rgblines[index]);
                        index = index + 3;
                    }
                }
                Console.WriteLine("dsdada");
            }
        }

        public static void Interpolate()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);
            Console.WriteLine("Increase by factor: ");
            double fact = Convert.ToDouble(Console.ReadLine()); 

            string[] widthXheight = rgblines[2].Split(' ');
            int height1 = Convert.ToInt32(widthXheight[0]);
            int width1 = Convert.ToInt32(widthXheight[1]);
            int heigth2 = Convert.ToInt32(Math.Ceiling(height1 * fact));
            int width2 = Convert.ToInt32(Math.Ceiling(width1 * fact));
            //int heigth2 = height1 * 3;
            //int width2 = width1 * 3;

            Rgb_pts[] rgblines2 = new Rgb_pts[height1 * width1];
            int ofs = 0;
            for(int i = 4; i < rgblines.Length; i = i + 3)
            {
                Rgb_pts pts;
                pts.r = Convert.ToInt32(rgblines[i]);
                pts.g = Convert.ToInt32(rgblines[i + 1]);
                pts.b = Convert.ToInt32(rgblines[i + 2]);
                rgblines2[ofs] = pts;
                ofs++;
            } 

            //int[,] newrgblines = new int[heigth2 * width2, 3];
            Rgb_pts[] newrgblines = new Rgb_pts[heigth2 * width2];
            int x, y, index;
            Rgb_pts a, b, c, d;

            double x_ratio = ((double)(width1 - 1)) / width2;
            double y_ratio = ((double)(height1 - 1)) / heigth2;
            double x_diff, y_diff, blue, red, green;
            int offset = 0;

            for(int indx1 = 0; indx1 < heigth2; indx1++)
            {
                for(int indx2 = 0; indx2 < width2; indx2++)
                {
                    x = (int)(x_ratio * indx2);
                    y = (int)(y_ratio * indx1);
                    x_diff = (x_ratio * indx2) - x;
                    y_diff = (y_ratio * indx1) - y;
                    index = y * width1 + x;
                    a = rgblines2[index];
                    b = rgblines2[index + 1];
                    c = rgblines2[index + width1];
                    d = rgblines2[index + width1 + 1];

                    // Value for blue element
                    blue = a.b * (1 - x_diff) * (1 - y_diff) + b.b * (x_diff) * (1 - y_diff) + c.b * y_diff * (1 - x_diff) + d.b * (x_diff * y_diff);

                    // Value for green element
                    green = a.g * (1 - x_diff) * (1 - y_diff) + b.g * x_diff * (1 - y_diff) + c.g * y_diff * (1 - x_diff) + d.g * (x_diff * y_diff);

                    // Value for red element
                    red = a.r * (1 - x_diff) * (1 - y_diff) + b.r * x_diff * (1 - y_diff) + c.r * y_diff * (1 - x_diff) + d.r * (x_diff * y_diff);

                    Rgb_pts newPix;
                    newPix.r = Convert.ToInt32(red);
                    newPix.g = Convert.ToInt32(green);
                    newPix.b = Convert.ToInt32(blue);

                    newrgblines[offset++] = newPix; 
                }
            }

            string path = @"C:\Users\g13m3615\Documents\Image processing\interpolate.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("P3");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine("{0} {1}", heigth2, width2);
                    sw.WriteLine(rgblines[3]);
                    foreach(Rgb_pts point in newrgblines)
                    {
                        sw.WriteLine(point.r);
                        sw.WriteLine(point.g);
                        sw.WriteLine(point.b);
                    }
                }
            }

            //    Console.WriteLine("dsdada");
        }

        public static void NN()
        {
            // The lines of data in the ppm file get read line by line and are stored in an array called rgbline
            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);
            Console.WriteLine("Increase by factor: ");
            double fact = Convert.ToDouble(Console.ReadLine());


            // Now to get the width and height of the original image
            string[] widthXheight = rgblines[2].Split(' ');
            int height1 = Convert.ToInt32(widthXheight[0]);
            int width1 = Convert.ToInt32(widthXheight[1]);
            //int heigth2 = height1 * 3;
            //int width2 = width1 * 3;
            int heigth2 = Convert.ToInt32(Math.Ceiling(height1 * fact));
            int width2 = Convert.ToInt32(Math.Ceiling(width1 * fact));

            // It's time to group all of the respective rgb values for each pixel in an RGB_pts array
            Rgb_pts[] rgblines2 = new Rgb_pts[height1 * width1];
            int ofs = 0;
            for (int i = 4; i < rgblines.Length; i = i + 3)
            {
                Rgb_pts pts;
                pts.r = Convert.ToInt32(rgblines[i]);
                pts.g = Convert.ToInt32(rgblines[i + 1]);
                pts.b = Convert.ToInt32(rgblines[i + 2]);
                rgblines2[ofs] = pts;
                ofs++;
            }

            // Here is the new array for the enlarged image and the variable to help calculated the pixels
            Rgb_pts[] newrgblines = new Rgb_pts[heigth2 * width2];
            double x_ratio = width1 / (double)width2;
            double y_ratio = height1 / (double)heigth2;
            double px, py;

            // Here is the NN scaling algorithm
            for (int y_indx = 0; y_indx < heigth2; y_indx++)
            {
                for(int x_indx = 0; x_indx < width2; x_indx++)
                {
                    px = Math.Floor(x_indx * x_ratio);
                    py = Math.Floor(y_indx * y_ratio);
                    newrgblines[(y_indx * width2) + x_indx] = rgblines2[Convert.ToInt32((py * width1) + px)];
                }
            }



            string path = @"C:\Users\g13m3615\Documents\Image processing\nn.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("P3");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine("{0} {1}", heigth2, width2);
                    sw.WriteLine(rgblines[3]);
                    foreach (Rgb_pts point in newrgblines)
                    {
                        sw.WriteLine(point.r);
                        sw.WriteLine(point.g);
                        sw.WriteLine(point.b);
                    }
                }
            }
            Console.WriteLine("ewewe");
            Console.ReadLine();

        }

        public static void Rotate()
        {
            // Convert angle from degrees to radians
            Console.WriteLine("Give an angle of rotation: ");
            double angle = Convert.ToDouble(Console.ReadLine());
            double angle_rad = Math.PI * angle / 180.0;

            Console.WriteLine("File name: ");
            string filename = Console.ReadLine();
            string[] rgblines = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\" + filename);
            string[] widthXheight = rgblines[2].Split(' ');
            int height = Convert.ToInt32(widthXheight[0]);
            int width = Convert.ToInt32(widthXheight[1]);

            // Let's take the list of rgb values and put them in a 2D RGB_pt array
            Rgb_pts[] rgblines2 = new Rgb_pts[width * height];
            int count = 0;
            for(int indx = 4; indx < rgblines.Length; indx = indx + 3)
            {
                Rgb_pts pt = new Rgb_pts();
                pt.r = Convert.ToInt32(rgblines[indx]);
                pt.g = Convert.ToInt32(rgblines[indx + 1]);
                pt.b = Convert.ToInt32(rgblines[indx + 2]);
                rgblines2[count] = pt;
                count++;
            }
            count = 0;

            Rgb_pts[,] rgblines3 = new Rgb_pts[width,height];            
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    rgblines3[x, y] = rgblines2[count];
                    count++;
                }
            }

            // Now we need to find the dimensions for the resulting image
            // Let's find tthe coordinates for the other 3 corners

            int x1 = Convert.ToInt32(-height * Math.Sin(angle_rad));
            int y1 = Convert.ToInt32(height * Math.Cos(angle_rad));
            int x2 = Convert.ToInt32(width * Math.Cos(angle_rad) - height * Math.Sin(angle_rad));
            int y2 = Convert.ToInt32(height * Math.Cos(angle_rad) + width * Math.Sin(angle_rad));
            int x3 = Convert.ToInt32(width * Math.Cos(angle_rad));
            int y3 = Convert.ToInt32(width * Math.Sin(angle_rad));

            int minx = Math.Min(0, Math.Min(x1, Math.Min(x2, x3)));
            int miny = Math.Min(0, Math.Min(y1, Math.Min(y2, y3)));
            int maxx = Math.Max(x1, Math.Max(x2, x3));
            int maxy = Math.Max(y1, Math.Max(y2, y3));

            int new_height = maxy - miny;
            int new_width = maxx - minx;
            Rgb_pts[,] new_rgblines = new Rgb_pts[new_width, new_height];
            
            // It's time to do the actual image rotation one pixal at a time.
            // To avoid leaving a few pixels uncovered, we shall compute the source point for each destination point
            for(int y_index = miny; y_index < maxy; y_index++)
            {
                for(int x_index = minx; x_index < maxx; x_index++)
                {
                    int sourcex = Convert.ToInt32(x_index * Math.Cos(angle_rad) + y_index * Math.Sin(angle_rad));
                    int sourcey = Convert.ToInt32(y_index * Math.Cos(angle_rad) - x_index * Math.Sin(angle_rad));
                    if (sourcex >= 0 && sourcex < width && sourcey >= 0 && sourcey < height)
                    {
                        // Original: new_rgblines[x_index, y_index] = rgblines3[sourcex, sourcey];
                        // The reason why the original always failed was because x always started 
                        // at a negative number and y always started too high. We can't have negative 
                        // entries in an array. Even if we did handle the x issue the y was too high 
                        // and it would cut off half of the image.
                        new_rgblines[x_index - minx, y_index - miny] = rgblines3[sourcex, sourcey];
                    }
                }
            }


            string path = @"C:\Users\g13m3615\Documents\Image processing\rotate.ppm";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("P3");
                    sw.WriteLine(rgblines[1]);
                    sw.WriteLine("{0} {1}", new_height, new_width);
                    sw.WriteLine(rgblines[3]);
                    for(int x = 0; x < new_width; x++)
                    {
                        for(int y = 0; y < new_height; y++)
                        {
                            sw.WriteLine(new_rgblines[x, y].r);
                            sw.WriteLine(new_rgblines[x, y].g);
                            sw.WriteLine(new_rgblines[x, y].b);
                        }
                    }
                }
            }
        }

        public static void Bsubtr()
        {
            string[] rgblines1 = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\frames\image1.ppm");

            int[] image1 = makeImgArr("image1.ppm");
            int[] image2 = makeImgArr("image6.ppm");
            int[] image3 = makeImgArr("image3.ppm");
            int[] image4 = makeImgArr("image4.ppm");
            int[] image5 = makeImgArr("image5.ppm");
            int[] image6 = makeImgArr("image6.ppm");

            int[] result1 = imgArrDiff(image1, image2);
            int[] result2 = imgArrDiff(image1, image3);
            int[] result3 = imgArrDiff(image1, image4);
            int[] result4 = imgArrDiff(image1, image5);
            int[] result5 = imgArrDiff(image1, image6);

            Directory.CreateDirectory(@"C:\Users\g13m3615\Documents\Image processing\frames\background subtraction results");

            imgArrToPPM(result1, "outputSub1.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result2, "outputSub2.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result3, "outputSub3.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result4, "outputSub4.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result5, "outputSub5.ppm", rgblines1[1], rgblines1[2]);
        }

        public static void FrSubtr()
        {
            string[] rgblines1 = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\frames\image1.ppm");

            int[] image1 = makeImgArr("image1.ppm");
            int[] image2 = makeImgArr("image6.ppm");
            int[] image3 = makeImgArr("image3.ppm");
            int[] image4 = makeImgArr("image4.ppm");
            int[] image5 = makeImgArr("image5.ppm");
            int[] image6 = makeImgArr("image6.ppm");

            int[] result1 = imgArrDiff(image1, image2);
            int[] result2 = imgArrDiff(image2, image3);
            int[] result3 = imgArrDiff(image3, image4);
            int[] result4 = imgArrDiff(image4, image5);
            int[] result5 = imgArrDiff(image5, image6);

            Directory.CreateDirectory(@"C:\Users\g13m3615\Documents\Image processing\frames\background subtraction results");

            imgArrToPPM(result1, "outputSub1.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result2, "outputSub2.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result3, "outputSub3.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result4, "outputSub4.ppm", rgblines1[1], rgblines1[2]);
            imgArrToPPM(result5, "outputSub5.ppm", rgblines1[1], rgblines1[2]);
        }

        public static void RunnningAvg()
        {
            string[] rgblines1 = System.IO.File.ReadAllLines(@"C:\Users\g13m3615\Documents\Image processing\frames\image1.ppm");

            int[] image1 = makeImgArr("image1.ppm");
            int[] image2 = makeImgArr("image6.ppm");
            int[] image3 = makeImgArr("image3.ppm");
            int[] image4 = makeImgArr("image4.ppm");
            int[] image5 = makeImgArr("image5.ppm");
            int[] image6 = makeImgArr("image6.ppm");

            int[] currAvg = new int[image1.Length];
            for (int i = 0; i < image1.Length; i++)
            {
                // current_mu = 0.5*CurrPixel + (1 - 0.05)*previous_mu // previous_mu = 0
                currAvg[i] = Convert.ToInt32(0.05 * image1[i]);
            }

            currAvg = GaussianAvg(image2, currAvg);
            currAvg = GaussianAvg(image3, currAvg);
            currAvg = GaussianAvg(image4, currAvg);
            currAvg = GaussianAvg(image5, currAvg);
            currAvg = GaussianAvg(image6, currAvg);

            Directory.CreateDirectory(@"C:\Users\g13m3615\Documents\Image processing\frames\background subtraction results");
            imgArrToPPM(currAvg, "runningAvg.ppm", rgblines1[1], rgblines1[2]);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Select task: \n1. Gray Scale \n2. Single Channel \n3. Average \n4. Correct \n5. Interpolate \n6. NN(New Neighbor) scaling \n7. Rotate \n8. Background subtraction \n9. Frame difference \n10. Running Average");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    GrayScale();
                    break;
                case "2":
                    Singlechannel();
                    break;
                case "3":
                    Average();
                    break;
                case "4":
                    Correct();
                    break;
                case "5":
                    Interpolate();
                    break;
                case "6":
                    NN();
                    break;
                case "7":
                    Rotate();
                    break;
                case "8":
                    Bsubtr();
                    break;
                case "9":
                    FrSubtr();
                    break;
                case "10":
                    RunnningAvg();
                    break;
                default:
                    Console.WriteLine("invalid option");
                    break;
            }
            Console.ReadLine();
        }
    }
}
