﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker;
using LootLocker.Requests;
using Newtonsoft.Json;
using System;

namespace LootLocker.Requests
{

    #region GetMessages

    public class LootLockerGetMessagesResponse : LootLockerResponse
    {
        
        public LootLockerGMMessage[] messages { get; set; }
    }

    public class LootLockerGMMessage
    {
        public string title { get; set; }
        public string published_at { get; set; }
        public string body { get; set; }
        public string summary { get; set; }
        public string category { get; set; }
        public bool alert { get; set; }
        public bool _new { get; set; }
        public string action { get; set; }
        public string image { get; set; }
    }

    #endregion

}

namespace LootLocker
{

    public partial class LootLockerAPIManager
    {

        public static void GetMessages(Action<LootLockerGetMessagesResponse> onComplete)
        {
            EndPointClass endPoint = LootLockerEndPoints.getMessages;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, null, (serverResponse) =>
            {
                LootLockerGetMessagesResponse response = new LootLockerGetMessagesResponse();
                if (string.IsNullOrEmpty(serverResponse.Error))
                    response = JsonConvert.DeserializeObject<LootLockerGetMessagesResponse>(serverResponse.text);

                //LootLockerSDKManager.DebugMessage(serverResponse.text, !string.IsNullOrEmpty(serverResponse.Error));
                response.text = serverResponse.text;
                     response.success = serverResponse.success;
            response.Error = serverResponse.Error; response.statusCode = serverResponse.statusCode;
                onComplete?.Invoke(response);
            }, true);
        }

    }

}