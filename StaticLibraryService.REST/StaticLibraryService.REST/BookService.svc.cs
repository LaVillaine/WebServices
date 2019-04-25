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
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BookService : IBookService
    {
        private static List<Book> mCatalogue = new List<Book>() { new Book { Title = "Jane Eyre" }, new Book { Title = "Robinson Crusoe" }, new Book { Title = "Macbeth" } };

        public Book[] GetCatalogue()
        {
            return mCatalogue.ToArray();
        }

        public Book GetBook(string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            if (mCatalogue.Count > 0 && index < mCatalogue.Count)
                return mCatalogue[(int)index];
            return new Book { };
        }

        public void DeleteBook(string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            if (mCatalogue.Count > 0 && index < mCatalogue.Count)
                mCatalogue.RemoveAt((int)index);
        }

        public void AddBook(Book addedBook)
        {
            if (addedBook.Title != null && addedBook.Title != "")
                mCatalogue.Add(addedBook);
        }

        public void UpdateBook(Book updatedBook, string catalogueIndex)
        {
            uint index;
            uint.TryParse(catalogueIndex, out index);
            bool validUpdate = (mCatalogue.Count > 0 && index < mCatalogue.Count && updatedBook.Title != null && updatedBook.Title != "");
            if (validUpdate)
                mCatalogue[(int)index] = updatedBook;
        }
    }

    [ServiceContract(Namespace = "StaticLibraryService.REST")]
    public interface IBookService
    {
        [WebInvoke(Method = "POST", UriTemplate = "/Catalogue", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        void AddBook(Book addedBook);

        [WebInvoke(Method = "GET", UriTemplate = "/Catalogue", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Book[] GetCatalogue();

        [WebInvoke(Method = "GET", UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Book GetBook(string catalogueIndex);

        [WebInvoke(Method = "DELETE", UriTemplate = "/Catalogue/{catalogueIndex}")]
        [OperationContract]
        void DeleteBook(string catalogueIndex);

        [WebInvoke(Method = "PUT", UriTemplate = "/Catalogue/{catalogueIndex}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        void UpdateBook(Book updatedBook, string catalogueIndex);
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
