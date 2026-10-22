// See https://aka.ms/new-console-template for more information
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using JokeFetcher.Dtos;

//Set the base URL for the Web Service (API)
const string baseUrl = "https://official-joke-api.appspot.com/";
// create custom options for jsonSerialiser to ignore case when deserialising
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var client = new HttpClient{BaseAddress = new Uri(baseUrl)};
try
{
    //send a GET request to the "random_joke" endpoint
    HttpResponseMessage response = client.GetAsync("jokes/random").Result;

    //Ensure the request was succesful(throws an exception if unsuccesful)
    response.EnsureSuccessStatusCode();

    //read the response content as a string
    string responseBody = response.Content.ReadAsStringAsync().Result;

    //Deserialise the JSON response into a joke object (Ignore case on key names)
    var joke = JsonSerializer.Deserialize<Joke>(responseBody, options);

    //check if the joke is null
    if (joke != null)
    {
        //Display the joke in the console
        Console.WriteLine("Here's a joke for you:");
        Console.WriteLine(joke.Setup);
        Console.WriteLine(joke.PunchLine);
    }
    else
    {
        Console.WriteLine("Failed to fetch a Joke :(");
    }
}
catch (HttpRequestException e)
{
    //handle any HTTP request errors
    Console.WriteLine($"Request error:{e.Message}");
}

//start with an empty list
var jokes = new List<Joke>();
try
{
    //send a GET request to the "jokes/{type}/ten" endpoint
    HttpResponseMessage response = client.GetAsync($"jokes/ten").Result;

    //ensure the request was successful
    response.EnsureSuccessStatusCode();

    //read the response content as a string
    string responseBody = response.Content.ReadAsStringAsync().Result;

    //deserialize the json response into a list of joke objects
    var jokeList = JsonSerializer.Deserialize<List<Joke>>(
        responseBody, options
        );
    //add the jokes to the list if they are not null
    if (jokeList != null)
    {
        jokes.AddRange(jokeList);
    }
}
catch(HttpRequestException e){
    //handle any http request errors 
    Console.WriteLine($"Request error: {e.Message}");
}

//display list of jokes
Console.WriteLine("_______________");
Console.WriteLine("\n10 More Jokes");
Console.WriteLine("_______________\n");
foreach (var joke in jokes) {
    Console.WriteLine(joke.Setup);
    Console.WriteLine(joke.PunchLine);
    Console.WriteLine();
}

//getting only programming jokes
var programmingJokes = new List<Joke>();
try
{
    //send a get request to the "jokes/{type}/ten/Programming" endpoint
    HttpResponseMessage response1 = client.GetAsync($"jokes/programming/ten").Result;

    //ensure the request was successful
    response1.EnsureSuccessStatusCode();

    //read the response context as a string
    string responseBody2 = response1.Content.ReadAsStringAsync().Result;

    //deserialize the json response
    var programmingJokeList = JsonSerializer.Deserialize<List<Joke>>(
        responseBody2, options
        );
    //add the jokes to the list if they are not null
    if (programmingJokeList != null)
    {
        programmingJokes.AddRange(programmingJokeList);
    }
}
catch (HttpRequestException e)
{
    //handle any http request errors
    Console.WriteLine($"Request error: {e.Message}");
}
//display list of programming jokes
Console.WriteLine("____________________");
Console.WriteLine("10 Programming Jokes");
Console.WriteLine("____________________");
foreach (var pJoke in programmingJokes) {
    Console.WriteLine(pJoke.Setup);
    Console.WriteLine(pJoke.PunchLine);
    Console.WriteLine();
}