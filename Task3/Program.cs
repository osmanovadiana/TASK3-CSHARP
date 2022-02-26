using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Task3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static List<Tour> getToursByFilters(List<Tour> tours, List<TourType> type_filter, List<Vehicle> vehicle_filter,
            int[] nums)
        {
            List<Tour> result = new List<Tour>();
            for (int i = 0; i < tours.Count; i++)
            {
                bool flag = type_filter.Contains(tours[i].Type);
                List<Vehicle> ts = tours[i].Vehicls;
                bool flag2 = false;
                foreach (var t in ts)
                {
                    if (vehicle_filter.Contains(t))
                    {
                        flag2 = true;
                        break;
                    }
                }


                if (nums[0] > -1 && tours[i].Meals < nums[0] || nums[1] > -1 && tours[i].Days < nums[1] ||
                    nums[2] > -1 && tours[i].Days > nums[2])
                {
                    flag = false;
                }

                if (flag && flag2)
                {
                    result.Add(tours[i]);
                }
            }

            return result;
        }

        public static string getToursToText(List<Tour> l)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < l.Count; i++)
            {
                sb.Append(getTourToText(l[i]));
            }

            return sb.ToString();
        }

        public static string getTourToText(Tour t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(t.Type).Append(" for ").Append(t.Days).Append("d with ").Append(t.Meals)
                .Append(" meals/d on ");
            for (int i = 0; i < t.Transports.Count; i++)
            {
                sb.Append(t.Transports[i]);
                if (i < t.Transports.Count - 1)
                {
                    sb.Append(" or ");
                }
            }

            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        public static List<Tour> GetToursFromFile(string path)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd().Replace(" ", "");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            List<Tour> list = new List<Tour>();
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                string[] words = line.Split(';');
                TourType type = (TourType) Enum.Parse(typeof(TourType), words[0]);
                List<Vehicle> vehicls = new List<Vehicle>();
                Int16 supply = Int16.Parse(words[2]);
                Int16 days = Int16.Parse(words[3]);
                string[] strVehicls = words[1].Split(',');
                for (int i = 0; i < strVehicls.Length; i++)
                {
                    Vehicle vehicle = (Vehicle) Enum.Parse(typeof(Vehicle), strVehicls[i]);
                    vehicls.Add(vehicle);
                }

                list.Add(new Tour(type, vehicls, supply, days));
            }

            return list;
        }
    }
}