﻿using System;
using System.Net.Http;

namespace Shrooms.Presentation.Api.Helpers
{
    public static class HttpRequestMessageHelpers
    {
        public static string GetRequestedTenant(this HttpRequestMessage request)
        {
            throw new NotImplementedException();
            //return request.GetOwinContext().Get<string>("tenantName");
        }

        public static bool IsOrganizationValid(this HttpRequestMessage request)
        {
            //return request.GetOwinContext().Get<bool>("isOrganizationValid");
            throw new NotImplementedException();
        }
    }
}