using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;

namespace DapperCRUDAngular.Common
{
    public static class ProcessCsvFile
    {

        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

        public static void InsertSvgData(Exception ex, string filepath)
        {
          
            try
            {

                if (!Directory.Exists(filepath))
                {
                    throw new Exception("File Directory not founnd");
                }
                
                if (!File.Exists(filepath))
                {
                    throw new Exception("File not found");
                }



            }
            catch (Exception e)
            {
                e.ToString();

            }


        }
        private static DataTable NewMethod()
        {
            DataTable csvData = new DataTable();
            using (TextFieldParser csvReader = new TextFieldParser(@"D:\downlode\Development_zipmap_2021-08-26_14-56-52.csv"))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }

            }

            return csvData;
        }

    }
}
