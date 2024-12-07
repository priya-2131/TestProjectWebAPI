namespace DapperCRUDAngular.Abstraction.Models.DTO
{

    public class FileValidation
    {
        public long FileValidationID { get; set; }
        public string countryZip { get; set; }
        public long ZipMapFileID { get; set; }

        public string priceZone { get; set; }
        public string rdcId { get; set; }
        public string storeId { get; set; }


        public string fulfillerName { get; set; }
        public string thresholdEnabled { get; set; }
        public string cpwEnabled { get; set; }

        public string supportsOnlineHomeDeliveryScheduling { get; set; }
        public string protectionPlanEnabled { get; set; }
        public long? ImportTableCount { get; set; }


        public long FileToBeValidated { get; set; }
        public string ValidationType { get; set; }
        public string RecordBelogTo { get; set; }

        public long ValidationTypeID { get; set; }
        public long RecordBelongToID { get; set; }
        public long? JOBHistoryID { get; set; }
    }
}
