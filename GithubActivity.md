# User Activity


## GraphQL To Fetch user Activity
```
{
  viewer {
    name
    contributionsCollection {
      contributionCalendar {
        weeks {
          contributionDays {
            date
            color
            contributionCount
            weekday
          }
        }
      }
    }
  }
}
```


## for rest Service you have to dig depper

var username = "prashantunity"; 
var path = $"https://api.github.com/users/{username}/events";

HttpClient client = new HttpClient(); 

client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0")); 
client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); 

var myJsonResponse = await client.GetAsync(path); 
