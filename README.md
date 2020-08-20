# Pingbot

A simple sharded pingbot example in Discord.NET - making use of background service for websocket interaction.

Example logs:

```
[2020-08-20T20:55:13.1557132+00:00 INF] - [Application] - Booting in Development...
[2020-08-20T20:55:13.2771697+00:00 INF] - [Application] - Discord worker started...
[2020-08-20T20:55:13.2821016+00:00 INF] - [Discord] - Discord.Net v2.2.0 (API v6)
[2020-08-20T20:55:13.3640593+00:00 INF] - [Application] - Now listening on: http://[::]:8080
[2020-08-20T20:55:13.3642157+00:00 INF] - [Application] - Application started. Press Ctrl+C to shut down.
[2020-08-20T20:55:13.3642413+00:00 INF] - [Application] - Hosting environment: Development
[2020-08-20T20:55:13.3642597+00:00 INF] - [Application] - Content root path: /app
[2020-08-20T20:55:14.0785058+00:00 INF] - [Gateway] - Connecting
[2020-08-20T20:55:15.3342049+00:00 INF] - [Application] - Shard 0 is connected and ready...
[2020-08-20T20:55:15.3350192+00:00 INF] - [Gateway] - Ready
[2020-08-20T20:55:19.8440420+00:00 INF] - [Gateway] - Connected
[2020-08-20T20:55:35.2997825+00:00 INF] - [Bot] - Received ping message!
[2020-08-20T20:55:38.9105015+00:00 INF] - [Bot] - Received ping message!
[2020-08-20T20:55:46.5171900+00:00 INF] - [Application] - HTTP GET /service/health responded 200 in 10.5529 ms
[2020-08-20T20:55:52.7116705+00:00 INF] - [Application] - HTTP GET /service/health responded 200 in 1.6675 ms
<... INTERRUPT>
[2020-08-20T20:56:21.4298436+00:00 INF] - [Application] - Application is shutting down...
[2020-08-20T20:56:21.4419294+00:00 INF] - [Application] - Discord worker TERMINATED
[2020-08-20T20:56:21.4445161+00:00 INF] - [Gateway] - Disconnecting

```
