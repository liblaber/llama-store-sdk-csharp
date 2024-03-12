using LlamaStore;

var client = new LlamaStoreClient();

var response = await client.Llama.GetLlamasAsync();

Console.WriteLine(response);
