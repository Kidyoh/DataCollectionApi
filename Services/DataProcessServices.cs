using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AutomatedDataCollectionApi.Services
{
   public class DataProcessServices : IDataProcessService
    {
        private readonly IConfiguration _configuration;
        private readonly string _endpointsFilePath;

        public DataProcessServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
          private const string EndpointsFile = "Services/apis_parsed.txt"; 

         public List<string> GetConfigFileEndPoints() 
         {
           // Read endpoints file into list
           var endpoints = File.ReadAllLines(EndpointsFile)
                              .ToList();
       
           return endpoints;
         }
       
        public async Task<string> AddConfigFileEndPoints(List<string> newEndpoints)
         {
             // Get existing endpoints
             var currentEndpoints = GetConfigFileEndPoints();
         
             // Check if any of the new endpoints already exist
             var duplicateEndpoints = newEndpoints.Intersect(currentEndpoints).ToList();
             if (duplicateEndpoints.Count > 0)
             {
                 string duplicateEndpointsMessage = string.Join(", ", duplicateEndpoints);
                 return $"Endpoints already exist: {duplicateEndpointsMessage}";
             }
         
             // Append new endpoints
             currentEndpoints.AddRange(newEndpoints);
         
             // Write combined list back to file
             File.WriteAllLines(EndpointsFile, currentEndpoints);
         
             await Task.Delay(100);
             return "Success";
         }


       
         public async Task<string> EditConfigFileEndPoints(List<string> newEndpoints)
        {
            // Read existing endpoints
            var existingEndpoints = GetConfigFileEndPoints();
        
            // Modify existing endpoints based on your requirement
            // For example, you can use a loop or LINQ to replace/edit specific endpoints
        
            // Replace the specific endpoints with new values
            foreach (var endpoint in newEndpoints)
            {
                var index = existingEndpoints.IndexOf(endpoint);
                if (index != -1)
                {
                    existingEndpoints[index] = "new value"; // Update with the new value
                }
            }
        
            // Write back the modified endpoints
            File.WriteAllLines(_endpointsFilePath, existingEndpoints);
        
            await Task.Delay(100); // Simulate async operation
        
            return "Success"; 
        }

        public async Task<string> DeleteConfigFileEndPoints(List<string> endpointsToDelete)
        {
            // Read existing endpoints
            var existingEndpoints = GetConfigFileEndPoints();
            
            // Remove the specified endpoints
            existingEndpoints.RemoveAll(endpoint => endpointsToDelete.Contains(endpoint));
        
            // Write back the modified endpoints
            File.WriteAllLines(EndpointsFile, existingEndpoints);
        
            await Task.Delay(100); // Simulate async operation
        
            return "Success";
        }

        
       

    }
}

