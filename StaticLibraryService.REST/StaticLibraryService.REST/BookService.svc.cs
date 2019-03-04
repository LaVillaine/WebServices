using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

namespace StaticLibraryService.REST
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BookService
    {
        private static List<Book> mCatalogue = new List<Book>() { new Book("Jane Eyre"), new Book("Robinson Crusoe"), new Book("Macbeth")};

        [WebGet(UriTemplate="/Catalogue")]
        public string GetCatalogue()
        {
            string catalogue = "";
            if (mCatalogue.Count > 0)
                catalogue = JsonConvert.SerializeObject(mCatalogue, Formatting.Indented);
            return catalogue;
        }
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]
    }

    class Book
    {
        public Book(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
