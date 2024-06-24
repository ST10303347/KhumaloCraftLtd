using Azure;
using Azure.Search.Documents;
using KhumaloCraftLtd.Models;




namespace KhumaloCraftLtd.Data.Services
{
   
    public class SearchService
    {
        public string SearchServiceName = "zayyysearchservice";
        public string SearchServiceKey = "oTxcRFzUE2mbdZSh7eKv46q7ekdzyOp4BBi1onGD77AzSeB7fzaG";
        public string indexName = "azuresql-index";

        private readonly SearchClient _searchClient;

        public SearchService(string SearchServiceName, string indexName, string SearchServiceKey) { 
            var endpoint = new Uri($"https://zayyysearchservice.search.windows.net");
            _searchClient = new SearchClient(endpoint, indexName, new AzureKeyCredential(SearchServiceKey));

        }

        public async Task<IEnumerable<ProductModel>> SearchProducts(string query) { 
        
        
        var options = new SearchOptions {IncludeTotalCount = true };
            var response = await _searchClient.SearchAsync<ProductModel>(query, options);
            return (IEnumerable<ProductModel>)response.Value.GetResults();
        
        
        
        }
    }
}
