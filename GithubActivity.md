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
