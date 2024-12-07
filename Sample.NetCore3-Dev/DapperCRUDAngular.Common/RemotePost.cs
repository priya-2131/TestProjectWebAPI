namespace DapperCRUDAngular.Common
{
    public class RemotePost
    {
        public System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();

        public string Url = "";
        public string Method = "post";
        public string FormName = "form1";

        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }
    }
}
