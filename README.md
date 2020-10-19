# Introduction
Redis is an open-source data store that is used as a database, cache / messaging broker. It supports quite a lot of data structures like string, hashes, lists, queries, and much more. It’s a blazing fast key-value based database that is written in C. It’s a NoSQL Database as well, which is awesome. For these purposes, it is being used at tech-giants like Stackoverflow, Flickr, Github, and so on.

# Setting up Redis on Windows 10
Download the ZIP Folder from this [Github Repo](https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504). You could also use the MSI Executable.

# Getting to know Redis Better
By default, Redis runs on the local 6379 port. To change this, open up Powershell and run the following command.
./redis-server --port {your_port}

# Integrating Redis Caching in ASP.NET Core
Install a package that helps you communicate with the redis server.

> Install-Package **Microsoft.Extensions.Caching.StackExchangeRedis**

