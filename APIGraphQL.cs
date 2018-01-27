using System;

namespace GraphQL {
    public static class APIGraphQL {
        private const string ApiURL = "http://192.168.0.104:3000/graphql";
        public static string Token = null;
        public static bool LoggedIn => !Token.Equals(""); //todo: improve loggedin verification

        private static readonly GraphQLClient API = new GraphQLClient(ApiURL);

        public static void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
            API.Query(query, variables, callback, Token);
        }
    }
}