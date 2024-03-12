# LlamaStore C# SDK 0.0.3

A C# SDK for LlamaStore.

- API version: 0.1.7
- SDK version: 0.0.3

The llama store API! Get details on all your favorite llamas. ## To use this API - You will need to register a user, once done you can request an API token. - You can then use your API token to get details about the llamas. ## User registration To register a user, send a POST request to `/user` with the following body: `json {      email :  <your email> ,      password :  <your password>  } ` This API has a maximum of 1000 current users. Once this is exceeded, older users will be deleted. If your user is deleted, you will need to register again. ## Get an API token To get an API token, send a POST request to `/token` with the following body: `json {      email :  <your email> ,      password :  <your password>  } ` This will return a token that you can use to authenticate with the API: `json {    access_token :  <your new token> ,    token_type :  bearer  } ` ## Use the API token To use the API token, add it to the `Authorization` header of your request: `Authorization: Bearer <your token>`

## Table of Contents

- [Authentication](#authentication)
- [Services](#services)

## Authentication

### Access Token

The llama-store API uses a access token as a form of authentication.

The access token can be set when initializing the SDK like this:

```cs
// Constructor initialization
```

Or at a later stage:

```cs
// Setter initialization
```

## Services

### LlamaPictureService

#### **GetLlamaPictureByLlamaIdAsync**

Get a llama's picture by the llama ID. Pictures are in PNG format.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.LlamaPicture.GetLlamaPictureByLlamaIdAsync(1);

Console.WriteLine(response);
```

#### **CreateLlamaPictureAsync**

Create a picture for a llama. The picture is sent as a PNG as binary data in the body of the request.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.LlamaPicture.CreateLlamaPictureAsync(new object {}, 1);

Console.WriteLine(response);
```

#### **UpdateLlamaPictureAsync**

Update a picture for a llama. The picture is sent as a PNG as binary data in the body of the request.

If the llama does not have a picture, one will be created. If the llama already has a picture,
it will be overwritten.
If the llama does not exist, a 404 will be returned.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.LlamaPicture.UpdateLlamaPictureAsync(new object {}, 1);

Console.WriteLine(response);
```

#### **DeleteLlamaPictureAsync**

Delete a llama's picture by ID.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

await client.LlamaPicture.DeleteLlamaPictureAsync(1);
```

### LlamaService

#### **GetLlamasAsync**

Get all the llamas.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.Llama.GetLlamasAsync();

Console.WriteLine(response);
```

#### **CreateLlamaAsync**

Create a new llama. Llama names must be unique.

```csharp
using LlamaStore;
using LlamaStore.Models;

var client = new LlamaStoreClient();

var input = new LlamaCreate("libby the llama", 5, LlamaColor.Brown, 1);

var response = await client.Llama.CreateLlamaAsync(input);

Console.WriteLine(response);
```

#### **GetLlamaByIdAsync**

Get a llama by ID.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.Llama.GetLlamaByIdAsync(1);

Console.WriteLine(response);
```

#### **UpdateLlamaAsync**

Update a llama. If the llama does not exist, create it.

When updating a llama, the llama name must be unique. If the llama name is not unique, a 409 will be returned.

```csharp
using LlamaStore;
using LlamaStore.Models;

var client = new LlamaStoreClient();

var input = new LlamaCreate("libby the llama", 5, LlamaColor.Brown, 1);

var response = await client.Llama.UpdateLlamaAsync(input, 1);

Console.WriteLine(response);
```

#### **DeleteLlamaAsync**

Delete a llama. If the llama does not exist, this will return a 404.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

await client.Llama.DeleteLlamaAsync(1);
```

### TokenService

#### **CreateApiTokenAsync**

Create an API token for a user. These tokens expire after 30 minutes.

Once you have this token, you need to pass it to other endpoints in the Authorization header as a Bearer token.

```csharp
using LlamaStore;
using LlamaStore.Models;

var client = new LlamaStoreClient();

var input = new ApiTokenRequest("7pe6?Z`06@+@mIcksLu.^Yk|\@rd'#", "Password123!");

var response = await client.Token.CreateApiTokenAsync(input);

Console.WriteLine(response);
```

### UserService

#### **GetUserByEmailAsync**

Get a user by email.

This endpoint will return a 404 if the user does not exist. Otherwise, it will return a 200.

```csharp
using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.User.GetUserByEmailAsync("R6*VwL:,QI@5Y8.G6");

Console.WriteLine(response);
```

#### **RegisterUserAsync**

Register a new user.

This endpoint will return a 400 if the user already exists. Otherwise, it will return a 201.

```csharp
using LlamaStore;
using LlamaStore.Models;

var client = new LlamaStoreClient();

var input = new UserRegistration("DU"w@+H,S+E@|g4%6>?Ax?.ETKmcVGon", "Password123!");

var response = await client.User.RegisterUserAsync(input);

Console.WriteLine(response);
```
