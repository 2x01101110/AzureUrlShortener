# AzureUrlShortener
Simple URL shortener using Azure Functions and CosmosDB

Use `"routePrefix": ""` in host.json to remove API prefix from URL.
```javascript
{
  "extensions": {
    "http": {
      "routePrefix": ""
    }
  }
}
```
