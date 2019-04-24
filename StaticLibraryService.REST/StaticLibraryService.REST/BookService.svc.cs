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

        [WebGet(UriTemplate="/Catalogue", ResponseFormat =WebMessageFormat.Xml)]
        [OperationContract]
        public string GetCatalogue()
        {
            string catalogue = "";
            if (mCatalogue.Count > 0)
                catalogue = JsonConvert.SerializeObject(mCatalogue, Formatting.Indented);
            return catalogue;
        }

        [WebGet(UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public string GetBook(string catalogueIndex)
        {
            string catalogue = "";
            uint index;
            uint.TryParse(catalogueIndex, out index);
            if (mCatalogue.Count > 0 && index < mCatalogue.Count)
                catalogue = JsonConvert.SerializeObject(mCatalogue[(int)index], Formatting.Indented);
            return catalogue;
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Catalogue/{catalogueIndex}")]
        [OperationContract]
        public void DeleteBook(string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            if (mCatalogue.Count > 0 && index < mCatalogue.Count)
                mCatalogue.RemoveAt((int)index);
        }

        [WebInvoke(Method = "POST", UriTemplate = "/Catalogue", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        public void AddBook(string bookAsString)
        {
            Book addedBook = JsonConvert.DeserializeObject<Book>(bookAsString);
            if (addedBook.Title != null && addedBook.Title != "")
                mCatalogue.Add(addedBook);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        public void UpdateBook(string bookAsString, string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            Book updatedBook = JsonConvert.DeserializeObject<Book>(bookAsString);
            bool validUpdate = (mCatalogue.Count > 0 && index < mCatalogue.Count && updatedBook.Title != null && updatedBook.Title != "");
            if (validUpdate)
                mCatalogue[(int)index] = updatedBook;
        }
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
