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
        private static List<Book> mCatalogue = new List<Book>() { new Book { Title = "Jane Eyre" }, new Book { Title = "Robinson Crusoe" }, new Book { Title = "Macbeth" } };

        [WebGet(UriTemplate="Catalogue", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public Book[] GetCatalogue()
        {
            return mCatalogue.ToArray();
        }

        [WebGet(UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public Book GetBook(string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            if (mCatalogue.Count > 0 && index < mCatalogue.Count)
                return mCatalogue[(int)index];
            return new Book { };
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
        public void AddBook(Book addedBook)
        {
            if (addedBook.Title != null && addedBook.Title != "")
                mCatalogue.Add(addedBook);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        public void UpdateBook(Book updatedBook, string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            bool validUpdate = (mCatalogue.Count > 0 && index < mCatalogue.Count && updatedBook.Title != null && updatedBook.Title != "");
            if (validUpdate)
                mCatalogue[(int)index] = updatedBook;
        }
    }

    [DataContract]
    public class Book
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Publisher { get; set; }
    }
}
