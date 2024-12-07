using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;


namespace DapperCRUDAngular.Abstraction.Enums
{
    public static class EnumLookup
    {

        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
       

        public enum SystemType
        {
            API = 1,
            Console_APP
        }


        public enum ZipMapType
        {
            ZipMapTypeID = 1            
        }

        public enum FileValidationType
        {
            [Description("Required Field Validation")]
            EmptyField = 1,
            [Description("Price Zone Validation")]
            InValidPriceZone = 2,
            [Description("Postal Code Format Validation")]
            InValidPostalCode =3,
            [Description("DeleteThreshold")]
            Delete = 4,
            [Description("InsertThreshold")]
            INSERT = 5,
            [Description("UpdateThreshold")]
            UPDATE = 6,            
        }

        public enum ProcessStatus
        {
            Pending = 1,
            DataGathered,
            VenderIdentified,
            SentToSmartSheet
        }
        public enum ExceptionType
        {
            Hash_Not_Matched = 1,

            // Code_Exception,

            //Unidentified_licensee = 2,
            //Not_Sent_To_SmartSheet = 3,
            // Code_Exception,
            Application_Log = 4,
            File_Process_Hold = 5,
            Code_Exception = 6,
            EmailExcpetion = 7,
            WEB_JOB_Summary_Email = 8,


            Downloding = 9,
            Under_Process = 10,
            InvalidFile = 11

        }

        public enum JobStatus
        {
            [Description("In Process")]
            InProcess = 1,
            [Description("Success")]
            Success = 2,
            [Description("Failed")]
            Failed = 3
        }


        public enum FileStatus
        {
            [Description("Downloading")]
            Downloading = 1,
            [Description("Downloaded")]
            Downloaded = 2,
            [Description("Under Process")]
            Under_Process = 3,
            [Description("Imported Record")]
            Imported_Record = 4,
            [Description("Downloaded Fail")]
            Downloaded_Fail = 5,
            [Description("Imported To Live Table")]
            ImportedToLiveTable = 6,
            [Description("Processing Discaded")]
            Processing_Discaded = 7,
            [Description("Import To tblImportedPostalCode Fail")]
            Import_To_tblImportedPostalCode_Fail = 8,
            [Description("Import To tblPostalCode Fail")]
            Import_To_tblPostalCode_Fail = 9,
            [Description("Deny by user")]
            Denybyuser = 10,
            [Description("File Invalid")]
            File_Invaid = 11
        }

        public enum JobRunby
        {
            System = 1,
            Manual
        }

    }
}
