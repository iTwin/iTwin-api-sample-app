
using iTwinSampleApp.Models;
using System.Net;

namespace iTwinSampleApp
    {
    internal class iTwinManager
        {
        private EndpointManager _endpointMgr;
        private List<iTwin> _iTwins; // iTwins that will be deleted in DisposeAsync

        #region Constructors
        internal iTwinManager(string token)
            {
            _endpointMgr = new EndpointManager(token);
            _iTwins = new List<iTwin>();
            }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete iTwin (DELETE) - If you are writing tests for your code, always delete iTwins when they are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task DeleteiTwin(string id)
            {
            var responseMsg = await _endpointMgr.MakeDeleteCall<iTwin>($"/iTwins/{id}");
            if (responseMsg.Status != HttpStatusCode.NoContent)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");
            }
        #endregion

        #region GET
        /// <summary> 
        /// Get my iTwins - This will return iTwins that the user can access.  It is not using paging so it will only return
        /// the top 100 projects by default.
        /// </summary>
        /// <param name="projectNumber"></param>
        /// 
        /// Filter by number. It should return 1 iTwin with the specified number. 
        /// This is the SQL equivalent of number = '{sampleiTwin.number}'
        /// 
        /// <param name="search"></param>
        /// 
        /// Get my iTwins - Wildcard Search. It should return any iTwin with "iTwin Sample" in the number or displayName.
        /// This is the SQL equivalent of (number like '%iTwin Sample%' OR displayName like '%iTwin Sample%')
        /// Only use the wildcard search if the number or displayName filters are not sufficient. The wildcard search is slower. 
        /// 
        /// <returns></returns>
        internal async Task<List<iTwin>> GetMyiTwins(string subClass="Project", string number = null, string search = null)
            {
            var showAllPropertiesHeader = new Dictionary<string, string>
                {
                    { "Prefer", "return=representation" }
                };

            string queryString = $"?subClass={subClass}";
            if (!string.IsNullOrWhiteSpace(number))
                {
                Console.Write($"\n\n- Getting List of My iTwins with number={number}");
                queryString += $"&number={number}";
                }
            else if (!string.IsNullOrWhiteSpace(search))
                {
                Console.Write($"\n\n- Getting List of My iTwins with $search={search}");
                queryString += $"&$search={search}";
                }
            else
                Console.Write("\n\n- Getting List of My iTwins");

            var responseMsg = await _endpointMgr.MakeGetCall<iTwin>($"/iTwins{queryString}", showAllPropertiesHeader);
            if (responseMsg.Status != HttpStatusCode.OK)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write($" [Retrieved {responseMsg.Instances?.Count} iTwins] (SUCCESS)");

            return responseMsg.Instances;
            }

        /// <summary>
        /// Get single iTwin using the specified iTwin id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<iTwin> GetiTwin(string id)
            {
            Console.Write($"\n\n- Getting iTwin with id {id}");

            var showAllPropertiesHeader = new Dictionary<string, string>
                {
                    { "Prefer", "return=representation" }
                };
            var responseMsg = await _endpointMgr.MakeGetSingleCall<iTwin>($"/iTwins/{id}", showAllPropertiesHeader);
            if (responseMsg.Status != HttpStatusCode.OK)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write(" (SUCCESS)");

            return responseMsg.Instance;
            }

        /// <summary>
        ///  Get my favorite iTwins (GET)
        /// </summary>
        /// <returns></returns>
        internal async Task<List<iTwin>> GetMyFavoriteiTwins(string subClass = "Project")
            {
            Console.Write("\n\n- Getting List of My iTwin Favorites");

            var showAllPropertiesHeader = new Dictionary<string, string>
                {
                    { "Prefer", "return=representation" }
                };
            var responseMsg = await _endpointMgr.MakeGetCall<iTwin>($"/iTwins/favorites?subClass={subClass}", showAllPropertiesHeader);
            if (responseMsg.Status != HttpStatusCode.OK)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write($" [Retrieved {responseMsg.Instances.Count}] (SUCCESS)");

            return responseMsg.Instances;
            }

        /// <summary>
        /// Get my recently used iTwins (GET). 
        /// </summary>
        /// <returns></returns>
        internal async Task<List<iTwin>> GetMyRecentlyUsediTwins(string subClass = "Project")
            {
            Console.Write("\n\n- Getting List of My Recent iTwins");

            var showAllPropertiesHeader = new Dictionary<string, string>
                {
                { "Prefer", "return=representation" }
                };
            var responseMsg = await _endpointMgr.MakeGetCall<iTwin>($"/iTwins/recents?subClass={subClass}", showAllPropertiesHeader);
            if (responseMsg.Status != HttpStatusCode.OK)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write($" [Retrieved {responseMsg.Instances.Count}] (SUCCESS)");

            return responseMsg.Instances;
            }
        #endregion

        #region POST
        /// <summary>
        /// Create iTwin (POST)
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        internal async Task<iTwin> CreateiTwin(iTwin iTwin = null)
            {
            if (iTwin == null)
                iTwin = new iTwin();

            Console.Write("\n\n- Creating an iTwin.");
            var responseMsg = await _endpointMgr.MakePostCall("/iTwins", iTwin);
            if (responseMsg.Status != HttpStatusCode.Created)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write(" (SUCCESS)");

            _iTwins.Add(responseMsg.NewInstance);

            return responseMsg.NewInstance;
            }

        /// <summary>
        /// Add iTwin to my recents. There is a current max of 25 recents per user but this could change in the future.
        /// If you add a 26th project then the oldest recent will be removed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task AddiTwinToMyRecents(string id)
            {
            Console.Write($"\n\n- Adding iTwin {id} to My Recents");

            var responseMsg = await _endpointMgr.MakePostCall<iTwin>($"/iTwins/recents/{id}");
            if (responseMsg.Status != HttpStatusCode.NoContent)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write(" (SUCCESS)");
            }

        /// <summary>
        /// Add iTwin to my favorites. Currently, there is no max number of favorites but this could change in the future.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task AddiTwinToMyFavorites(string id)
            {
            Console.Write($"\n\n- Adding iTwin {id} to My Favorites");

            var responseMsg = await _endpointMgr.MakePostCall<iTwin>($"/iTwins/favorites/{id}");
            if (responseMsg.Status != HttpStatusCode.NoContent)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write(" (SUCCESS)");
            }
        #endregion

        #region PATCH
        /// <summary>
        /// Update iTwin name (PATCH). This method could be modified to update any iTwin property or multiple properties.
        /// You only need to specify the properties that you want to update. In this case, it is only updating the DisplayName property.
        /// </summary>
        /// <param name="iTwin"></param>
        /// <returns></returns>
        internal async Task UpdateiTwin(iTwin iTwin, string newName)
            {
            Console.Write($"\n\n- Updating iTwin Name for ({iTwin.Id})");

            var responseMsg = await _endpointMgr.MakePatchCall<iTwin>($"/iTwins/{iTwin.Id}", new
                {
                DisplayName = newName
                });
            if (responseMsg.Status != HttpStatusCode.OK)
                throw new Exception($"{responseMsg.Status}: {responseMsg.ErrorDetails?.Code} - {responseMsg.ErrorDetails?.Message}");

            Console.Write($" (SUCCESS)");
            }
        #endregion

        /// <summary>
        /// Demonstrates iTwin administration functionality.
        /// </summary>
        /// <returns></returns>
        internal async Task iTwinManagementWorkflow()
            {
            #region Manage iTwins 
            var creatediTwin = await CreateiTwin();

            // Get the iTwin created above
            var retrievedSingleiTwin = await GetiTwin(creatediTwin.Id);

            // Get All iTwins including iTwin created above
            var retrievedProjectiTwins = await GetMyiTwins("Project");

            var retrievedAssetiTwins = await GetMyiTwins("Asset");

            // Get iTwin using Number. 
            var iTwinFilterByNumber = await GetMyiTwins(number: creatediTwin.Number);

            // Get iTwins using a wildcard search.
            var iTwinsSearch = await GetMyiTwins(search: "iTwin Sample");

            // Update the project name
            await UpdateiTwin(creatediTwin, creatediTwin.DisplayName + " Updated");
            #endregion

            #region Manage Recent/Favorite Projects
            // Add project to recent and favorites
            await AddiTwinToMyRecents(creatediTwin.Id);
            await AddiTwinToMyFavorites(creatediTwin.Id);

            // Get recent projects and favorite projects
            var listOfFaves = await GetMyFavoriteiTwins();
            var listOfRecents = await GetMyRecentlyUsediTwins();
            #endregion

            // Any projects that were created as part of this sample will be deleted in DisposeAsync
            }


        internal async ValueTask DisposeAsync()
            {
            Console.Write($"\n\n- Deleting any iTwins that were created.");

            foreach(var i in _iTwins)
                {
                await DeleteiTwin(i.Id);
                }

            Console.Write(" (SUCCESS)\n\n");
            }
        }
    }
