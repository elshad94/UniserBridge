using Microsoft.Extensions.Configuration;
using Project.Core.Entities.SPModels.System;
using Project.Core.Enums;
using Project.Core.Settings;
using Project.Core.Utilities.Security;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

namespace Project.Core.Utilities.Tools
{
    public static class CommonTools
    {
        /// <summary>
        /// <para> Used to get JSON format data </para> 
        /// Example:
        /// <para> Before:
        /// [{"ObjectName":"username", "Value":"İstifadəçi adı"},
        /// {"ObjectName":"password", "Value":"Şifrə"},
        /// {"ObjectName":"email", "Value":"Email ünvanı"}] </para>  
        /// 
        /// After:
        /// {"username" : "İstifadəçi adı",
        /// "password" : "Şifrə",
        /// "email" : "Email ünvanı"}
        /// </summary>
        public static Dictionary<string, object> GetKeyValuePairs(IList<SP_GetPageContents> keyValues)
        {
            var jsonDoc = new Dictionary<string, object>();

            foreach (var item in keyValues)
            {
                jsonDoc.Add(item.Key, item.Value);
            }

            return jsonDoc;
        }


        public static string GetLocalIPAddress()
        {
            string localIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }

            return localIp;
        }


        /// <summary>
        /// TEXT YOU  SENT => Text you  sent
        /// </summary>
        public static string ToTitleCase(this string text)
        {
            return char.ToUpper(text.First()) + text.Substring(1).ToLower();
        }


        /// <summary>
        /// Generates random string (password)
        /// </summary>
        public static string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string GenerateTokenByCurrentDate([StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string dateFormat = "yyyyMMdd", string inputToken = null)
        {
            string date = DateTime.Now.ToString(dateFormat);

            string token = SecurityHelper.CreateMD5($"{date}{inputToken ?? AppSettings.Settings.GlobalKey}");

            return token;
        }

        public static int GenerateOTP()
        {
            Random random = new();
            return random.Next(100000, 999999);
        }


        public static ResultInfo SendEmail(string to, string subject, string message)
        {
            var mailCredentials = AppSettings.Settings.EmailOptions; //Get mail settings from settings.json

            using (MailMessage mm = new(new MailAddress(mailCredentials.Username, mailCredentials.DisplayName), new MailAddress(to)))
            {
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = true;

                try
                {
                    using (SmtpClient sc = new SmtpClient(mailCredentials.Host, (int)mailCredentials.Port))
                    {
                        sc.EnableSsl = (bool)mailCredentials.EnableSsl;
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.UseDefaultCredentials = (bool)mailCredentials.UseDefaultCredentials;
                        sc.Credentials = new NetworkCredential(mailCredentials.Username, mailCredentials.Password);
                        sc.Send(mm);
                    }

                    return ResultInfo.Success;
                }
                catch (Exception ex)
                {
                    return ResultInfo.EmailSendingError;
                }
            }
        }



        /// <summary>
        /// Used for large values that needs to be divided to post API's
        /// </summary>
        /// <param name="itemCount">Partition count</param>
        /// <param name="method">Accepts data up to the number of partition count</param>
        /// <returns>Total API response</returns>
        public static List<ResponseType> PostDataInParts<SourceType, ResponseType>(int itemCount, List<SourceType> sourceData, Func<List<SourceType>, List<ResponseType>> method)
        {
            int next = 0;
            List<ResponseType> response = new();

            for (int i = 0; i < sourceData.Count / itemCount; i++)
            {
                int offset = next * itemCount;

                var subData = sourceData.Skip(offset).Take(itemCount).ToList();

                var apiResponse = method(subData); //method invoking
                response.AddRange(apiResponse);

                next++;
            }

            return response;
        }

        public static DataTable ExcelFileToDataTable(string filePath)
        {
            string sheetName = "";
            DataTable dataTable = new DataTable();
            string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'";
            string connectionString = string.Format(Excel07ConString, filePath);

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        command.CommandText = "SELECT * From [" + sheetName + "]";
                        command.Connection = con;
                        con.Open();
                        oda.SelectCommand = command;
                        oda.Fill(dataTable);
                        con.Close();
                    }
                }
            }

            //remove empty lines from excel data
            dataTable = dataTable.Rows
            .Cast<DataRow>()
            .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string)))
            .CopyToDataTable();

            return dataTable;
        }

        public static string GetAppSetting(string key) => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>(key);

        #region Number to words
        public static string NumberToWords(string number, string lang = "AZ", int currencyId = 1)
        {
            string result;
            string[] numbers = number.Split('.');

            int firstPart = Convert.ToInt32(numbers[0].ToString());
            string secondPart = "0";

            try
            {
                secondPart = numbers[1].ToString();
            }
            catch (Exception)
            { }

            if (currencyId == 3) // USD
            {
                if (lang == "AZ")
                {
                    result = NumberToWords_AZ(firstPart) + " ABŞ dolları";
                    if (secondPart != "0")
                        result += ", " + secondPart + " sent";
                }
                else
                {
                    result = NumberToWords_ENG(firstPart) + " US Dollars";
                    if (secondPart != "0")
                        result += ", " + secondPart + " US Cents";
                }
            }
            else
            {
                if (lang == "AZ")
                {
                    result = NumberToWords_AZ(firstPart) + " AZN";
                    if (secondPart != "0")
                        result += ", " + secondPart + " qəpik";
                }
                else
                {
                    result = NumberToWords_ENG(firstPart) + " AZN";
                    if (secondPart != "0")
                        result += ", " + secondPart + " qepik";
                }
            }

            return result.ToTitleCase();
        }

        public static string NumberToWords_AZ(int number)
        {
            if (number == 0)
                return "sıfır";

            if (number < 0)
                return "mənfi " + NumberToWords_AZ(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords_AZ(number / 1000000) + " milyon ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords_AZ(number / 1000) + " min ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords_AZ(number / 100) + " yüz ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "";

                var unitsMap = new[] { "sıfır", "bir", "iki", "üç", "dörd", "beş", "altı", "yeddi", "səkkiz", "doqquz", "on", "on bir", "on iki", "on üç", "on dörd", "on beş", "on altı", "on yeddi", "on səkkiz", "on doqquz" };
                var tensMap = new[] { "sıfır", "on", "iyirmi", "otuz", "qırx", "əlli", "altmış", "yetmiş", "səksən", "doxsan" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) != 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string NumberToWords_ENG(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords_ENG(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords_ENG(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords_ENG(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords_ENG(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        #endregion
    }
}
