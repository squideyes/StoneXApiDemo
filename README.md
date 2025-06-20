The problem I'm experiencing is that **I can't retrieve Market Information and Tags** even though I can get account info and thereby presume that the problem is a configuration issue on the Forex.com / StoneX side.
  
 
```json
{
  "session": "a4018cf2-b628-4677-b132-1e7bbc4cf0e9",
  "passwordChangeRequired": false,
  "allowedAccountOperator": false,
  "statusCode": 0,
  "is2FAEnabled": false,
  "twoFAToken": null,
  "additional2FAMethods": []
}
```

I'm not quite sure if the fact that **allowedAccountOperator is false** or if **statusCode is 0**, but the values  do seem a little suspicious

The C# project documents the error, but if you're imply interested in the URL that returns a valid but empty FullMarketInformationSearchWithTagsResponseDTO v2 response, here it is:

https://ciapi.cityindex.com/TradingAPI/v2/market/fullSearchWithTags?UserName=24950876&Session=27a0cee3-eff8-44ff-9c89-fcfa084a8adf&ClientAccountId=405166211

Thanks for your help...
