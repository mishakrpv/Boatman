<div align="center">

# Boatman

Boatman is a platform for publishing housing for rent from owners.

<br />

[![license](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

</div>

## Running the instance

### Configuring secrets template

```
{
  "JwtSettings": {
    "Issuer": "Issuer URL",
    "Audience": "Audience URL",
    "Key": "JwtSecretKey",
    "ExpiresInDays": 0,
    "ExpiresInMinutes": 5
  },
  "SendGridKey": "YourSendGridKey"
}
```
