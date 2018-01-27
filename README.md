# GraphQL Client for Unity using UnityWebRequest

Here is a sample client I made for a Unity app that utilizes a GraphQL api.
I based my code on these sources:

- [bkniffler/graphql-net-client](https://github.com/bkniffler/graphql-net-client)
- [SaladLab/Json.Net.Unity3D (release 9.0.1)](https://github.com/SaladLab/Json.Net.Unity3D)
- [carlflor/unity_graphql_client](https://github.com/carlflor/unity_graphql_client)
- [Coroutiner from facebookarchive/friendsmash-unity](https://github.com/facebookarchive/friendsmash-unity/blob/master/friendsmash/Assets/Scripts/GameScripts/Coroutiner.cs)

### Configuration

Set the GraphQL server URL at APIGraphQL.cs:5

### Sample Query & Mutation

``` c#

public class SomeGameObject : MonoBehaviour {
  private class Credentials {
    public string email;
    public string password;
  }

  public InputField Email;
  public InputField Password;
  
  private Credentials GetCredentials => new Credentials {
      email = Email.text,
      password = Password.text
  };
  
  string query =
    @"query ($input: Credentials!) {
        token: Login(credentials: $input)
    }";

  string mutation =
    @"mutation ($input: Credentials!) {
        token: Register(credentials: $input)
    }";

  void Login () {
    APIGraphQL.Query(query, new {input = GetCredentials}, callback);
  }

  void Register () {
    APIGraphQL.Query(mutation, new {input = GetCredentials}, callback);
  }

  private void callback (GraphQLResponse response) {
    string token = response.Get<string>("token");
    ...
  }

```

### Example getting a list

``` c#
public class Scores : MonoBehaviour {
  public class Score {
      public string name;
      public string score;
  }

  List<Score> scores;

  private string query = 
      @"query ($top: Int) {
          scores: GetScores (top: $top) {
              name
              score
          }
      }";

  private void Start () {
      APIGraphQL.Query(query, new {top = 10}, response => scores = response.GetList<Score>("scores"));
  }
```

*Issues are welcome! :)*
