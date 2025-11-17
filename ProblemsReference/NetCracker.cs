using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

/* PROBLEM STATEMENT

    There is a website that shows offers of multiple mobile phone plans to customers.
    
    Imagine you are building an in memory cache to store customer ids and the offer ids they have received.

    You need to implement the updateCache and cacheContains methods. You can use any data structure to store the data in memory.

    updateCache adds information to the cache

    cacheContains verifies if the customer id and offer id are in the cache

*/

// Dict<string, Hash<>>

class Solution
{

    private static Dictionary<string, HashSet<string>> cache = new(); // my solution

    static void Main(string[] args)
    {
        updateCache("cli1", "111");
        updateCache("cli1", "222");
        updateCache("cli1", "333");
        updateCache("cli1", "444");
        updateCache("cli2", "111");
        updateCache("cli2", "555");
  
        Debug.Assert(cacheContains("cli1", "111"));
        Debug.Assert(cacheContains("cli1", "222"));
        Debug.Assert(cacheContains("cli1", "333"));
        Debug.Assert(cacheContains("cli1", "444"));
        Debug.Assert(cacheContains("cli2", "111"));
        Debug.Assert(cacheContains("cli2", "555"));
        Debug.Assert(!cacheContains("cli2", "666"));

        Debug.Assert(!cacheContains("cli1", "555"));
        Debug.Assert(!cacheContains("cli1", "999"));
        Debug.Assert(!cacheContains("cli2", "222"));
        Debug.Assert(!cacheContains("cli3", "333"));
        Debug.Assert(!cacheContains("cli2", "999"));
    }

    public static void updateCache(String clientId, String offerId) {

        #region MySolution
        //TODO update this method to include logic to update cache 
        if(!cache.ContainsKey(clientId))
        {
            cache[clientId] = new HashSet<string>();
        }

        cache[clientId].Add(offerId);
        #region EndRegion
    }
  
    public static Boolean cacheContains(String clientId, String offerId) {
        //TODO update this method to include logic to verify if a certain record is in the cache

        #region MySolution
        if (cache.TryGetValue(clientId, out HashSet<string> offers))
        {
            var response = offers.Contains(offerId);
            // Console.WriteLine($"{clientId} e {offerId}" + response);
            return response;
        }

        return false;
        #region EndRegion
    }
}